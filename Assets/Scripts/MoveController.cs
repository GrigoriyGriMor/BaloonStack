// �������� ���������
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

    [Header("���������� ��������� ��������")]
    [SerializeField]
    private float multiplySpeed = 3;

    [Header("���-�� ������� ����� RunHard")]
    [SerializeField]
    private int maxRunHard = 2;

    [SerializeField]
    [Header("������ ����������")]
    private float timerIncrease = 0.3f;

    [SerializeField]
    [Header("������ ����������")]
    private float timerDecrease = 0.3f;

    [SerializeField]
    [Header("��� ������� ����������")]
    private int maxPercentIncrease = 20;

    private float percentIncrease;

    private float speed;

    private Animator animator;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private CheckerAir checkerAirPlayer;

    private int countAirPlayer;  // ������� � ������ �������

    // private float valueBlendShape;

    //private float finishValueBlendShape;




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
        //countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������
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
        //SetValueBlendShapes();

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
        //float valBlendShape = Mathf.Lerp();
        //float _z = Mathf.Lerp(_startPos, _endPos, Time.time);

        if (countAirPlayer < checkerAirPlayer.countAir)
        {
            StartCoroutine(SetPercentIncrease(timerIncrease, timerDecrease));
        }

        countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������
        int maxCountAirPlayer = checkerAirPlayer.maxCountAir; // ������� ��� � ������ �������

        float valueBlendShape = ((float)countAirPlayer / maxCountAirPlayer * 100) + percentIncrease; // �������� BlendShape � ���������
        skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // ��������  BlendShape


    }

    IEnumerator SetPercentIncrease(float timerIncrease, float timerDecrease)
    {
        yield return new WaitForSeconds(timerIncrease);
        percentIncrease = maxPercentIncrease;
        yield return new WaitForSeconds(timerDecrease);
        percentIncrease = 0;
    }
}


