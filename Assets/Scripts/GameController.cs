using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Game Controller
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


}
