using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����� �� ��������� � ������
/// </summary>
public class PlatformForBallons : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;


    private GameObject currentPlayer;

    [Header("����� ����� �������� �������")]
    [SerializeField]
    private float deltaTime = 2.0f;

    [Header("����� ������ �����")]
    [SerializeField]
    private Transform pointSpawnBallon;

    [Header("������ ���")]
    [SerializeField]
    private GameObject gameObjectBaloon;

    void InflatingBallons()
    {
        //������� ������� ��������
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
