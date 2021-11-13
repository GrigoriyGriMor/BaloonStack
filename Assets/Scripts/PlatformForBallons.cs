using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForBallons : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;

    [SerializeField]
    private GameObject currentPlayer;

    [Header("Время между надувами воздуха")]
    [SerializeField]
    private float deltaTime = 2.0f;

    [Header("Точка спавна шаров")]
    [SerializeField]
    private Transform pointSpawnBallon;

    [Header("Обьект Шар")]
    [SerializeField]
    private GameObject gameObjectBaloon;

    private void Update()
    {

    }

    void InflatingBallons()
    {
        //Сколько воздуха забираем
        int countAirDel = 1;
        if (currentPlayer.GetComponent<CheckerAir>().DelAir(countAirDel))
        {

            gameObjectBaloon.transform.position = pointSpawnBallon.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CheckerAir>())
        {
            currentPlayer = collision.gameObject;
            InvokeRepeating("InflatingBallons", 2, deltaTime);
            isPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CheckerAir>())
        {
            CancelInvoke();
            currentPlayer = null;
            isPlayer = false;
        }
    }
}
