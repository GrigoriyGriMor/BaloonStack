using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSharik : MonoBehaviour
{
    [Header("Точка спавна шаров")]
    [SerializeField]
    private Transform spawnBallons;

    [Header("Точка начала веревки")]
    [SerializeField]
    private Transform anchorCord;

    [Header("Финиш полета шаров")]
    [SerializeField]
    private Transform finishFlyBallons;

    [SerializeField]
    private Transform PointEndCord;

    [Header("Время перемещение шара после надувания")]
    [SerializeField]
    private float timeMoveAnchorCord = 0.5f;

    private Rigidbody rigidbody;
    private Animator animator;
    private LineRenderer lineRenderer;
    private SpringJoint springJoint;
    private Transform transformBallon;

    private bool isFlyBallons;

    [Header("Скорость перещения Шара")]
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
        lineRenderer.SetPosition(0, anchorCord.position);  //0-начальная точка линии
        lineRenderer.SetPosition(1, PointEndCord.position);   //1-конечная точка линии
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