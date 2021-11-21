using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSharik : MonoBehaviour
{
    [Header("����� ������ �����")]
    [SerializeField]
    private Transform spawnBallons;

    [Header("����� ������ �������")]
    [SerializeField]
    private Transform anchorCord;

    [Header("����� ������ �����")]
    [SerializeField]
    private Transform finishFlyBallons;

    [SerializeField]
    private Transform PointEndCord;

    [Header("����� ����������� ���� ����� ���������")]
    [SerializeField]
    private float timeMoveAnchorCord = 0.5f;

    private Rigidbody rigidbody;
    private Animator animator;
    private LineRenderer lineRenderer;
    private SpringJoint springJoint;
    private Transform transformBallon;

    private bool isFlyBallons;

    [Header("�������� ��������� ����")]
    [SerializeField]
    private float speedMoveBallon = 1;


    private void Start()
    {
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        springJoint = GetComponent<SpringJoint>();

        //animator.enabled = false;
        lineRenderer.enabled = false;

        transformBallon = transform;
        transformBallon.position = spawnBallons.position;

        Invoke("AchorCord", timeMoveAnchorCord);
    }

    void Update()
    {
        if (lineRenderer.enabled)
        {
            MoveBallons();
        }
    }

    void MoveBallons()
    {
        rigidbody.AddForce(transform.forward * speedMoveBallon);
        lineRenderer.SetPosition(0, anchorCord.position);  //0-��������� ����� �����
        lineRenderer.SetPosition(1, PointEndCord.position);   //1-�������� ����� �����
    }

    void AchorCord()
    {
        //transformBallon.position = anchorCord.position;
        StartFlyBallons();
    }

    void StartFlyBallons()
    {
        lineRenderer.enabled = true;
    }




}