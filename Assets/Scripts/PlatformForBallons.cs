using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TypeOfPlayer
{
    Player,
    Enemy
}


/// <summary>
/// скрипт весит на платформе с шарами
/// </summary>
public class PlatformForBallons : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;

    //[Header("Массив шаров на платформе")]
   // [SerializeField]
    private ScriptSharik[] arrayBaloons;

    //[SerializeField]
    private GameObject currentPlayer;

    [Header("Время между надувами воздуха")]
    [SerializeField]
    private float deltaTime = 1.0f;

    [Header("Мах кол-во шаров на платформе")]
    public int maxCountBallons;

    //[HideInInspector]
    public int currentCountBallonsPlanform;  // текущие кол во надутых шаров

    [Header("Точка начала перещения")]
    [SerializeField]
    private Transform pointStartMove;

    [Header("Точка начала ")]
    [SerializeField]
    private Transform pointStartFly;

    [Header("Скорость перемещения Игрока на платформе")]
    [SerializeField]
    private float speedPlayerPlatform = 0.5f;

    [Header("ССыль на аниматор двери корзины")]
    [SerializeField]
    private Animator animatorDoorBasket;

    [Header("Пауза перед закр двери на платформе")]
    [SerializeField]
    private float delayCloseDoor = 1.0f;

    [Header("Тип игрока для платформы")]
    [SerializeField]
    private TypeOfPlayer typeOfPlayer;

    private Animator animatorCurrentObject; // аниматор обьекта который чекнула платформа

    private GameObject parentObject;        // родительский обьект 

    private bool isAction;                  // флаг действия

    // [SerializeField]
    private int action = 1;                 // Номер действия

    private int currentIndex;               // индекс шара

    [SerializeField]
    private FlyBasket flyBasket;

    

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
        arrayBaloons = parentObject.GetComponentsInChildren<ScriptSharik>();
        flyBasket = parentObject.GetComponentInChildren<FlyBasket>();

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

        CheckCountBallons();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<CheckerAir>())
        {
            isPlayer = true;

            currentPlayer = collision.gameObject;

            if (currentPlayer.GetComponent<EnemyController>() && typeOfPlayer == TypeOfPlayer.Enemy)
            {
                currentPlayer.GetComponent<EnemyController>().isInflating = true;
                InvokeRepeating("InflatingBallons", 2, deltaTime);
            }

            if (currentPlayer.GetComponent<MoveController>() && typeOfPlayer == TypeOfPlayer.Player)
            {
                InvokeRepeating("InflatingBallons", 2, deltaTime);
            }
            
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
    private void Update()
    {
        if (isAction)

            switch (action)
            {
                case 1:
                    if (MoveObject(currentPlayer, pointStartMove))  // подбежал к корзине
                    {
                        action = 2;
                    }
                    break;

                case 2:
                    if (MoveObject(currentPlayer, pointStartFly))  // забежал в кооорзину
                    {
                        action = 3;
                    }
                    break;

                case 3:
                    animatorCurrentObject.SetBool("Run", false);    // остановился
                    OnAnimationDoor();
                    action = 4;
                    break;

                case 4:
                    
                    Invoke("OnFlyObject", delayCloseDoor);       // закрыли дверь с задержкой
                    action = 5; 
                    break;

                case 5:
                    break;
            }
    }

    private bool MoveObject(GameObject currentObject, Transform endPosition)
    {
        bool result = false;

        if (currentObject.GetComponent<EnemyController>())
        {
            EnemyController enemyController = currentObject.GetComponent<EnemyController>();

            enemyController.enabled = false;
            enemyController.navMeshAgent.enabled = false;
        }

        if (currentObject.GetComponent<MoveController>())
        {
           currentObject.GetComponent<MoveController>().enabled = false;
        }

        GameObject VisualAll = currentObject.transform.Find("VisualAll").gameObject;
        animatorCurrentObject = currentObject.GetComponentInChildren<Animator>();

        animatorCurrentObject.SetBool("Run", true);        // бежим

        VisualAll.transform.LookAt(endPosition);           // разворот

       
        currentObject.transform.position = Vector3.Lerp(currentObject.transform.position,   // перемещаем
                             endPosition.position, speedPlayerPlatform * Time.deltaTime);   // обьект

        if (Vector3.Distance(currentObject.transform.position, endPosition.position) < 0.3)
        {
            result = true;
        }

        return result;
    }

    private void OnAnimationDoor()
    {
        animatorDoorBasket.enabled = true;
       
    }

    private void OnFlyObject()
    {
        Rigidbody rigidbodyCurrentObject = currentPlayer.GetComponent<Rigidbody>();
        rigidbodyCurrentObject.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rigidbodyCurrentObject.useGravity = false;
        flyBasket.isCanFly = true;
        Debug.Log("OnFly");


    }

    private void CheckCountBallons()
    {
        if (currentCountBallonsPlanform == maxCountBallons)
        {
            isAction = true;

            if (typeOfPlayer == TypeOfPlayer.Player)
            {
                GameController.Instance.stateGame = StateGame.WinGame;
            }
            else if (typeOfPlayer == TypeOfPlayer.Enemy)
            {
                GameController.Instance.stateGame = StateGame.LoseGame;
            }

            Debug.Log(" Max count Ballons");
        }
    }

}
