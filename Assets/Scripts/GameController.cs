using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGame
{
    Game,
    WinGame,
    LoseGame
}


/// <summary>
/// Game Controller логика игры,содержит ссылки на игрока, шарики и противников
/// </summary>

public class GameController : MonoBehaviour
{


    public static GameController Instance;

    [Header("ссылка на игрока")]
    public GameObject player;

    [HideInInspector]
    public CheckerAir checkerAirPlayer;

    [Header("Массив противников ")]
    [SerializeField]
    private List<GameObject> arrayEnemys;

    [Header("Массив воздуха")]
    public List<Transform> arrayAirs;

    public GameObject refPanelResult;

    [Header("Ссыль на панель Победы игрока")]
    [SerializeField]
    private GameObject refPanelWinGame;

    [Header("Ссыль на панель Поражения игрока")]
    [SerializeField]
    private GameObject refPanelLoseGame;

    [Header("Ссылка на платформу игрока")]
    public PlatformForBallons platformForBallonsPlayer;

    [Header("Задержка на появление панель Win/Lose")]
    [SerializeField]
    private float delayOnPanelWinLose = 1.0f;

    [Header("Таймер респавна шара после удаления")]
    public float timeActived = 2.0f;

    //[HideInInspector]
    public bool isPlayGame;

    // [HideInInspector]
    public StateGame stateGame;

    public int countAirPlayer
    {
        get
        {
            return checkerAirPlayer.countAir;
        }

    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        checkerAirPlayer = player.GetComponent<CheckerAir>();
        isPlayGame = false;
        stateGame = StateGame.Game;
    }

    /// <summary>
    /// поиск цели
    /// </summary>
    public Transform GetNewTarget(Transform positionObject)
    {
        Transform currentPosition = null;

        if (arrayAirs.Count > 0)
        {
            float lastPosition = 100;

            foreach (Transform currentBaloon in arrayAirs)
            {
                Transform currentChildBaloon = currentBaloon.GetComponentInChildren<AirScript>().gameObject.transform;
                float currentDistance = Vector3.Distance(currentChildBaloon.position, positionObject.position);

                if (currentChildBaloon.gameObject.activeInHierarchy)
                {
                    if (lastPosition > currentDistance)
                    {
                        currentPosition = currentChildBaloon;
                        lastPosition = currentDistance;
                    }
                }
            }

        }
        return currentPosition;
    }

    public Transform GetNewTargetRamdom(Transform positionObject)
    {
        Transform currentPosition = null;
        foreach (Transform currentBaloon in arrayAirs)
        {
            Transform targetPosition = arrayAirs[Random.Range(0, arrayAirs.Count)];

            if (targetPosition.GetComponentInChildren<AirScript>())
            {
                targetPosition = targetPosition.GetComponentInChildren<AirScript>().gameObject.transform;

                if (targetPosition.gameObject.activeInHierarchy)
                {
                    return targetPosition;
                }
            }
        }
        return currentPosition;
    }

    private void Update()
    {
        UpdateGame();
    }


    /// <summary>
    /// обновления игры
    /// </summary>
    public void UpdateGame()
    {
        if (stateGame == StateGame.LoseGame)
        {
            Invoke("LoseGame", delayOnPanelWinLose);
        }

        if (stateGame == StateGame.WinGame)
        {
            Invoke("WinGame", delayOnPanelWinLose);
        }

    }


    /// <summary>
    /// старт игры
    /// </summary>
    public void StartGame()
    {
        isPlayGame = true;
    }

    /// <summary>
    /// конец игры
    /// </summary>
    public void EndGame()
    {
        Debug.Log("End Game");
    }

    void WinGame()
    {
        EndGame();
        Debug.Log("Win Game");
        refPanelWinGame.SetActive(true);
    }

    void LoseGame()
    {
        EndGame();
        Debug.Log("Lose Game");
        refPanelLoseGame.SetActive(true);
    }


}
