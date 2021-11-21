using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBasket : MonoBehaviour
{



    private bool isOnFlyBasket;

    [Header("�������� ������ �������")]
    [SerializeField]
    private float speedFlyBasket = 1.0f;

    [Header("����� ����� ������ �������")]
    [SerializeField]
    private Transform PointFinishFlyBasket;

    private GameObject collisionObject;    // ������ ������� � �������

    private Transform thisTransform;      // ����� �� ���������

    private Camera mainCamera;            // ����� �� ������

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        mainCamera = Camera.main;
    }


    private void OnTriggerEnter(Collider other)
    {
        collisionObject = other.gameObject;
        if (collisionObject.GetComponent<CheckerAir>())
        {
            Invoke("OnFlyBasket", 1.0f);
            collisionObject.transform.parent = thisTransform.transform;
            mainCamera.transform.parent = null;

        }
    }

    private void OnFlyBasket()
    {
        isOnFlyBasket = true;
    }

    private void FixedUpdate()
    {
        if (isOnFlyBasket)
        {
            thisTransform.position = Vector3.Lerp(thisTransform.position, PointFinishFlyBasket.position, speedFlyBasket * Time.deltaTime);
        }
    }


}
