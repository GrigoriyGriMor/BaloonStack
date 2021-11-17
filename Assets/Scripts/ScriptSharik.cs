using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSharik : MonoBehaviour
{

    [SerializeField]
    private Transform pointStartCord;

    [SerializeField]
    private Transform PointEndCord;

    private LineRenderer lineRenderer;
    private Rigidbody rigidbody;

    [SerializeField]
    private float speedMoveBallon = 1;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.AddForce(transform.forward * speedMoveBallon) ;


        lineRenderer.SetPosition(0, pointStartCord.position);  //0-начальная точка линии
        lineRenderer.SetPosition(1, PointEndCord.position);   //1-конечная точка линии
    }

}