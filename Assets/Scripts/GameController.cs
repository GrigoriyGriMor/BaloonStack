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
/// Game Controller ������ ����,�������� ������ �� ������, ������ � �����������
/// </summary>

public class GameController : MonoBehaviour
{
   

    public static GameController Instance;

    [Header("������ �� ������")]
    public GameObject player;

    [HideInInspector]
    public CheckerAir checkerAirPlayer;

    [Header("������ ����������� ")]
    [SerializeField]
    private List<GameObject> arrayEnemys;

    [Header("������ �������")]
    public List<Transform> arrayAirs;

    //[Header("����� �� ������ ����������")]
    //[SerializeField]
    public GameObject refPanelResult;

    [Header("����� �� ������ ������ ������")]
    [SerializeField]
    private GameObject refPanelWinGame;

    [Header("����� �� ������ ��������� ������")]
    [SerializeField]
    private GameObject refPanelLoseGame;

    [Header("������ �� ��������� ������")]
    public PlatformForBallons platformForBallonsPlayer;

    [Header("������ �� ��������� ����")]
    public PlatformForBallons platformForBallonsBot;

    [Header("�������� �� ��������� ������ Win/Lose")]
    [SerializeField]
    private float delayOnPanelWinLose = 1.0f;

    //[HideInInspector]
    public bool isPlayGame;

    [HideInInspector]
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
       // Time.timeScale = 0;
        isPlayGame = false;
        stateGame = StateGame.Game;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public Transform GetNewTarget(Transform positionObject)
    {
        Transform currentPosition = null;

        if (arrayAirs.Count > 0)
        {
            float lastPosition = 100;

            foreach (Transform currentBaloon in arrayAirs)
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
        if (arrayAirs.Count > 0)
        {
            currentPosition = arrayAirs[Random.Range(0, arrayAirs.Count - 1)];
        }
        return currentPosition;
    }

    private void Update()
    {
        if (isPlayGame)
        {
            UpdateGame();
        }
    }


    /// <summary>
    /// ���������� ����
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
    /// ��������� ���������� ������� ����� �� ���������
    /// </summary>
    /// <param name="platformForBallons"></param>
    /// <returns></returns>
    private bool CheckCountBallonsPlayers(PlatformForBallons platformForBallons)
    {
        bool result = false;
        if (platformForBallons.currentCountBallonsPlanform == platformForBallons.maxCountBallons)
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public void StartGame()
    {
        //Time.timeScale = 1;
        isPlayGame = true;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public void EndGame()
    {
        Debug.Log("End Game");
        //Time.timeScale = 0;
        isPlayGame = false;
        //refPanelResult.SetActive(false);
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
