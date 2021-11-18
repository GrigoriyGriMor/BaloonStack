using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [Header("Массив шариков")]
    public List<Transform> arrayBaloons;

    [Header("Ссыль на панель конца игры")]
    [SerializeField]
    private GameObject refPanelEndGame;

    [Header("Ссылка на платформу игрока")]
    public PlatformForBallons platformForBallonsPlayer;

    [Header("Ссылка на платформу Бота")]
    public PlatformForBallons platformForBallonsBot;

    [HideInInspector]
    public bool isPlayGame;


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
        refPanelEndGame.SetActive(false);
        Time.timeScale = 0;
        isPlayGame = false;
    }

    /// <summary>
    /// поиск цели
    /// </summary>
    public Transform GetNewTarget(Transform positionObject)
    {
        Transform currentPosition = null;

        if (arrayBaloons.Count > 0)
        {
            float lastPosition = 100;

            foreach (Transform currentBaloon in arrayBaloons)
            {
                float currentDistance = Vector3.Distance(currentBaloon.position, positionObject.position);

                if (currentBaloon.gameObject.activeInHierarchy)
                {
                    if (lastPosition > currentDistance)
                    {
                        currentPosition = currentBaloon;
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
        if (arrayBaloons.Count > 0)
        {
            currentPosition = arrayBaloons[Random.Range(0, arrayBaloons.Count - 1)];
        }
        return currentPosition;
    }

    private void Update()
    {
        UpdateGame();
    }

    public void UpdateGame()
    {
        if (CheckCountBallonsPlayers(platformForBallonsBot) || CheckCountBallonsPlayers(platformForBallonsPlayer))
        {
            refPanelEndGame.SetActive(true);
            EndGame();

        }
    }

   private bool CheckCountBallonsPlayers(PlatformForBallons platformForBallons)
    {
        bool result = false;
        if (platformForBallons.currentCountBallonsPlanform == platformForBallons.maxCountBallons)
        {
            result = true;
        }

        return result;
    } 


    public void StartGame()
    {
        Time.timeScale = 1;
        isPlayGame = true;
    }


    public void EndGame()
    {
        Time.timeScale = 0;
        isPlayGame = false;
    }




}
