using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// скрипт весит на платформе с шарами
/// </summary>
public class PlatformForBallons : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;


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
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<CheckerAir>())
        {
            currentPlayer = collision.gameObject;
            if (currentPlayer.GetComponent<EnemyController>())
            {
                currentPlayer.GetComponent<EnemyController>().isInflating = true;
            }

            isPlayer = true;
            InvokeRepeating("InflatingBallons", 2, deltaTime);
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
