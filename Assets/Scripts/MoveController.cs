// движение персонажа
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [Header("Rotate")]
    [SerializeField]
    private GameObject visualPlayer;

    [Header("")]
    [SerializeField]
    private float speedBegin = 3.0f;

    [Header("Коэфициент уеньшения скорости")]
    [SerializeField]
    private float multiplySpeed = 3;

    [Header("Кол-во шариков режим RunHard")]
    [SerializeField]
    private int maxRunHard = 2;

    [SerializeField]
    [Header("Таймер увеличения")]
    private float timerIncrease = 0.3f;

    [SerializeField]
    [Header("Таймер уменьшения")]
    private float timerDecrease = 0.3f;

    [SerializeField]
    [Header("Мах процент увеличения")]
    private int maxPercentIncrease = 20;

    private float percentIncrease;

    private float speed;

    private Animator animator;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private CheckerAir checkerAirPlayer;

    private int countAirPlayer;  // сколько у игрока воздуха


    private void Awake()
    {

    }

    private void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponentInChildren<Animator>();
        checkerAirPlayer = GetComponent<CheckerAir>();
    }

    private void Update()
    {
        SetValueBlendShapes();

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

    private void Move()
    {

        float horizMove = JoystickStick.Instance.HorizontalAxis();
        float verticalMove = JoystickStick.Instance.VerticalAxis();

        if (horizMove == 0.0f && verticalMove == 0.0f)
        {
            if (animator)
            {
                animator.SetBool("Run", false);
                animator.SetBool("RunHard", false);
            }
            return;
        }

        float angle = Mathf.Atan2(JoystickStick.Instance.HorizontalAxis(), JoystickStick.Instance.VerticalAxis()) * Mathf.Rad2Deg;
        visualPlayer.transform.rotation = Quaternion.Euler(0, angle, 0);

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
        }

        Vector3 movement = new Vector3(horizMove, 0, verticalMove) * speed;
        transform.Translate(movement * Time.fixedDeltaTime);
    }

    public void ActiveHardRun(float multiply)
    {
        animator.SetBool("Run", false);
        speed = speedBegin / multiply;
        //  Debug.Log("Mode Hard");
    }

    public void ActiveEaseRun()
    {
        animator.SetBool("RunHard", false);
        //  Debug.Log("Easy Hard");
        speed = speedBegin;
    }

    public void SetValueBlendShapes()
    {

        if (countAirPlayer < checkerAirPlayer.countAir)
            StartCoroutine(SetPercentIncrease(timerIncrease, timerDecrease));

        countAirPlayer = checkerAirPlayer.countAir; // сколько у игрока воздуха
        int maxCountAirPlayer = checkerAirPlayer.maxCountAir; // сколько мах у игрока воздуха

        float valueBlendShape = ((float)countAirPlayer / maxCountAirPlayer * 100) + percentIncrease; // значение BlendShape в процентах
        skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // значение  BlendShape


    }

    IEnumerator SetPercentIncrease(float timerIncrease, float timerDecrease)
    {
        while (percentIncrease < maxPercentIncrease)
        {
            percentIncrease += 1 * timerIncrease;
            yield return new WaitForFixedUpdate();
        }

        while (percentIncrease > 0)
        {
            percentIncrease -= 1 * timerIncrease;
            yield return new WaitForFixedUpdate();
        }
    }
}


