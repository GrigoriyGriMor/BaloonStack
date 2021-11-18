/* =======================================MoveController===================================
 * Класс контролирует движение персонажа
 * ========================================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [Header("Rotate")]
    [SerializeField]
    private GameObject visualPlayer;

    //[SerializeField] private float rotateSpeedMultiple = 5;


    [Header("")]
    [SerializeField]
    private float speedBegin = 3.0f;

    [Header("Коэфициент уеньшения скорости")]
    [SerializeField]
    private float multiplySpeed = 3;

    [Header("Кол-во шариков режим RunHard")]
    [SerializeField]
    private int maxRunHard = 2;

    private float speed;

    private Animator animator;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    
    private int countAirPlayer;  // сколько у игрока воздуха
        

    private void Awake()
    {
    }

    private void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
       

        if (countAirPlayer >= maxRunHard)
        {
            ActiveHardRun(multiplySpeed);
        }
        else
        {
            ActiveEaseRun();
        }


    }

    void FixedUpdate()
    {
        if (!GameController.Instance) return;

        Move();
    }

    private bool nextLevelAnimPlay = false;

    public void NextLevelAnim()
    {
        nextLevelAnimPlay = true;
    }

    private IEnumerator WaitEndGameAnim()
    {
        yield return new WaitForSeconds(3);

        //GameController.Instance.ReadyToGONextLevel();
    }

    private void Move()
    {
        SetValueBlendShapes();

        float horizMove = JoystickStick.Instance.HorizontalAxis();
        float verticalMove = JoystickStick.Instance.VerticalAxis();

        if (horizMove == 0.0f && verticalMove == 0.0f)
        {
            //if (anim.Length != 0)
            //{
            //    for (int i = 0; i < anim.Length; i++)
            //    {
            if (animator)
            {
                animator.SetBool("Run", false);
                animator.SetBool("RunHard", false);

            }
            //        anim[i].SetBool("HardRun", false);
            //    }
            //}
            return;
        }

        float angle = Mathf.Atan2(JoystickStick.Instance.HorizontalAxis(), JoystickStick.Instance.VerticalAxis()) * Mathf.Rad2Deg;
        visualPlayer.transform.rotation = Quaternion.Euler(0, angle, 0);

        //if (anim.Length != 0)
        //{
        //    for (int i = 0; i < anim.Length; i++)
        if (animator)
        {
            if (speed == speedBegin)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("RunHard", true);
            }
            //            anim[i].SetBool("HardRun", true);
        }

        Vector3 movement = new Vector3(horizMove, 0, verticalMove) * speed;
        transform.Translate(movement * Time.fixedDeltaTime);
    }

    public void ActiveHardRun(float multiply)
    {
        //for (int i = 0; i < anim.Length; i++)
        //    anim[i].SetBool("HardRun", true);
        // animator.SetBool("RunHard", true);
        animator.SetBool("Run", false);
        speed = speedBegin / multiply;
        // skinnedMeshRenderer.SetBlendShapeWeight(0, 50);
        //  Debug.Log("Mode Hard");
    }

    public void ActiveEaseRun()
    {
        //for (int i = 0; i < anim.Length; i++)
        //    anim[i].SetBool("HardRun", false);
        animator.SetBool("RunHard", false);
        //  Debug.Log("Easy Hard");
        speed = speedBegin;
    }


    public void WinGame()
    {
        //if (anim.Length != 0)
        //{
        //    for (int i = 0; i < anim.Length; i++)
        //    {
        //        anim[i].SetBool("Run", false);
        //        anim[i].SetBool("HardRun", false);
        //        anim[i].SetBool("WinGame", true);
        //    }
        // }
    }

    public void LoseGame()
    {
        //if (anim.Length != 0)
        //{
        //    for (int i = 0; i < anim.Length; i++)
        //    {
        //        anim[i].SetBool("Run", false);
        //        anim[i].SetBool("HardRun", false);
        //        anim[i].SetBool("LoseGame", true);
        //    }
        //}
    }

    public void SetValueBlendShapes()
    {
        countAirPlayer = GameController.Instance.checkerAirPlayer.countAir; // сколько у игрока воздуха
        int maxCountAirPlayer = GameController.Instance.checkerAirPlayer.maxCountAir; // сколько мах у игрока воздуха

        float valueBlendShape = (float)countAirPlayer / maxCountAirPlayer * 100; // значение BlendShape в процентах

    skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // значение  BlendShape
    }
}
