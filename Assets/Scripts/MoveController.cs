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

    private float speed;

    private Animator animator;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private CheckerAir checkerAirPlayer;

    private int countAirPlayer;  // ������� � ������ �������

    private float valueBlendShape;

    private float finishValueBlendShape;


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
        int maxCountAirPlayer = checkerAirPlayer.maxCountAir; // ������� ��� � ������ �������

        if (countAirPlayer > checkerAirPlayer.countAir)
        {
            valueBlendShape = (float)countAirPlayer / maxCountAirPlayer * 100; // �������� BlendShape � ���������
           countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������
        }
        else if (countAirPlayer < checkerAirPlayer.countAir)
        {
            finishValueBlendShape = (float)countAirPlayer / maxCountAirPlayer * 100; // �������� BlendShape � ���������
            valueBlendShape = finishValueBlendShape + 20;
            countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������
        }

        //valueBlendShape = Mathf.Lerp(valueBlendShape, finishValueBlendShape, 1.0f);

        skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // ��������  BlendShape



        //float valBlendShape = Mathf.Lerp();

        //float _z = Mathf.Lerp(_startPos, _endPos, Time.time);



        //countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������
        //int maxCountAirPlayer = checkerAirPlayer.maxCountAir; // ������� ��� � ������ �������
        ////float valBlendShape = Mathf.Lerp();

        ////float _z = Mathf.Lerp(_startPos, _endPos, Time.time);
        //float valueBlendShape = (float)countAirPlayer / maxCountAirPlayer * 100; // �������� BlendShape � ���������
        //skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // ��������  BlendShape

        countAirPlayer = checkerAirPlayer.countAir; // ������� � ������ �������

    }
}
