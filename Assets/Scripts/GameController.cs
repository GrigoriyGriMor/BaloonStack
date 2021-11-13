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
    private CheckerAir checkerAirPlayer;

    [Header("Массив противников ")]
    [SerializeField]
    private List<GameObject> arrayEnemys;

    [Header("Массив шариков")]
    [SerializeField]
    private List<Transform> arrayBaloons;

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
    }


    public Transform GetNewTarget(Transform targetObject)
    {
        Transform currentPosition = null;

        if (arrayBaloons.Count > 0)
        {
            float lastPosition = Vector3.Distance(arrayBaloons[0].transform.position, targetObject.transform.position); ;

            foreach (Transform currentBaloon in arrayBaloons)
            {
                float currentDistance = Vector3.Distance(currentBaloon.transform.position, targetObject.transform.position);

                if (currentBaloon.gameObject.activeInHierarchy)
                {
                    if (lastPosition > currentDistance)
                    {
                        currentPosition = currentBaloon;
                    }
                }
            }

        }
        return currentPosition;
    }

}
