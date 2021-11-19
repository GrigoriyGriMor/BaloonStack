using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public List<Transform> arrayBaloons;

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
    /// ����� ����
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
        if (CheckCountBallonsPlayers(platformForBallonsBot))
        {
            LoseGame();
            EndGame();
        }

        if (CheckCountBallonsPlayers(platformForBallonsPlayer))
        {
            WinGame();
            EndGame();
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
        Time.timeScale = 1;
        isPlayGame = true;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public void EndGame()
    {
        Debug.Log("End Game");
        Time.timeScale = 0;
        isPlayGame = false;
    }

    void WinGame()
    {
        Debug.Log("Win Game");

        refPanelWinGame.SetActive(true);
    }

    void LoseGame()
    {
        Debug.Log("Lose Game");
        refPanelLoseGame.SetActive(true);
    }


}
