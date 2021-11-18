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

    [SerializeField]
    private ScriptSharik[] arrayBaloons;

    private GameObject currentPlayer;

    [Header("Время между надувами воздуха")]
    [SerializeField]
    private float deltaTime = 2.0f;

    [Header("Мах кол-во шаров на платформе")]
    public int maxCountBallons;

    [HideInInspector]
    public int currentCountBallonsPlanform;

    //[Header("Точка спавна шаров")]
    //[SerializeField]
    //private Transform pointSpawnBallon;


    private int currentIndex;

    //[Header("Обьект Шар")]
    //[SerializeField]
    //private GameObject gameObjectBaloon;

    private void Awake()
    {
        GameObject parentObject = transform.parent.gameObject;
        arrayBaloons = parentObject.GetComponentsInChildren<ScriptSharik>();

        foreach (ScriptSharik compObject in arrayBaloons)
        {
            compObject.gameObject.SetActive(false);
        }

    }


    void InflatingBallons()
    {
       
        int countAirDel = 1;   //Сколько воздуха забираем

        if (currentPlayer.GetComponent<CheckerAir>().DelAir(countAirDel))
        {
            SetBalloon();
            currentCountBallonsPlanform++;

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

    void SetBalloon()
    {
        GameObject currnetObject;

        currnetObject = arrayBaloons[currentIndex].gameObject;

        if (!currnetObject.activeInHierarchy)
        {
            currnetObject.SetActive(true);
            currentIndex++;
            currentIndex = Mathf.Clamp(currentIndex, 0, arrayBaloons.Length - 1);
            return;
        }
    }

}
