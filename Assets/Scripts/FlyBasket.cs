using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBasket : MonoBehaviour
{
    [SerializeField]
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

    // [HideInInspector]
    public bool isCanFly;

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        mainCamera = Camera.main;
    }


    private void OnTriggerStay(Collider other)
    {
        collisionObject = other.gameObject;

        if (collisionObject.GetComponent<CheckerAir>())
        {
            if (isCanFly)
            {
                //Invoke("OnFlyBasket", 1.0f);
                OnFlyBasket();
                collisionObject.transform.parent = thisTransform.transform;
                mainCamera.transform.parent = null;
            }
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
