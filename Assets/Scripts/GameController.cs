using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Game Controller
/// </summary>

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("ссылка на игрока") ]
    public GameObject player;

    public CheckerAir checkerAirPlayer;

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

    void Update()
    {
       // Debug.Log(" Start ");
    }
}
