using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// скрипт весит на противнике
/// </summary>

public class EnemyController : MonoBehaviour
{
    enum ModeBot
    {
        MinDist,
        Random,
        FollowPlayer
    }

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    private CheckerAir checkerAir;    // кешируем CheckerAir

    [SerializeField]
    private Transform currentTarget;   // текущая цель

    private SkinnedMeshRenderer skinnedMeshRenderer;

    public bool isInflating; // началась скачка воздуха

    //[SerializeField]
    private Animator animator;

    //[Header("Рандом кол-ва шаров для скачивания")]
    //[SerializeField]
    //private bool isRandomBallonInflating;

    [SerializeField]
    [Header("Кол-во шаров для скачивания")]
    private int countBaloonsInflating = 3;

    //public int baloonsInflating = 0;

    [SerializeField]
    [Header("Место скачивания")]
    private Transform pointInflating;

    [Header("Кол-во шариков режим RunHard")]
    [SerializeField]
    private int maxRunHard = 2;

    [Header("Начальная скорость перемещения")]
    [SerializeField]
    private float speedBegin = 3.0f;

    [Header("Коэфициент уменьшения скорости")]
    [SerializeField]
    private float multiplySpeed = 3;

    [Header("Режим бота нахождения цели")]
    [SerializeField]
    private ModeBot modeBot;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent;
        checkerAir = GetComponent<CheckerAir>();
        animator = GetComponentInChildren<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }


    void Update()
    {
        if (GameController.Instance.isPlayGame)
        {
            if (checkerAir.countAir >= maxRunHard)
            {
                ActiveHardRun(multiplySpeed);
            }
            else
            {
                ActiveEaseRun();
            }

            UpdateMove();
            UpdateInflating();
        }

    }

    void UpdateMove()
    {
        SetValueBlendShapes();

        if (currentTarget)// && !checkerAir.isAddAir)
        {
            if (currentTarget.gameObject.activeInHierarchy)
            {
                if (currentTarget.gameObject.GetComponentInChildren<AirScript>())
                {
                    if (currentTarget.gameObject.GetComponentInChildren<AirScript>().isActive)
                    {
                        RunEnemy();
                    }
                    else
                    {
                        UpdateTarget();
                    }
                }
                else
                {
                    RunEnemy();
                }
            }
            else
            {
                UpdateTarget();
            }
        }
        else
        {
            checkerAir.isAddAir = false;
            UpdateTarget();
            IdleEnemy();
        }
    }

    /// <summary>
    /// побежал перс 
    /// </summary>
    void RunEnemy()
    {
        navMeshAgent.destination = currentTarget.position;

        if (animator)
        {
            if (navMeshAgent.speed == speedBegin)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("RunHard", true);
            }
        }
    }

    /// <summary>
    /// стоп перс
    /// </summary>
    void IdleEnemy()
    {
        navMeshAgent.speed = 0;
        animator.SetBool("Run", false);
        animator.SetBool("RunHard", false);
    }


    /// <summary>
    /// поиск цели
    /// </summary>
    public Transform GetNewTarget(Transform positionObject)
    {
        Transform currentPosition = null;

        switch (modeBot)
        {
            case ModeBot.MinDist:
                currentPosition = GameController.Instance.GetNewTarget(positionObject);
                break;

            case ModeBot.Random:
                currentPosition = GameController.Instance.GetNewTargetRamdom(positionObject);
                break;

            case ModeBot.FollowPlayer:
                currentPosition = GameController.Instance.GetNewTarget(GameController.Instance.player.transform);
                break;
        }
        return currentPosition;
    }

    public void ActiveHardRun(float multiply)
    {
        animator.SetBool("Run", false);
        navMeshAgent.speed = speedBegin / multiply;
        //Debug.Log("Mode Hard");
    }

    public void ActiveEaseRun()
    {
        animator.SetBool("RunHard", false);
        navMeshAgent.speed = speedBegin;
        // Debug.Log("Easy Hard");
    }

    void UpdateTarget()
    {
        currentTarget = GetNewTarget(gameObject.transform);

    }


    void UpdateInflating()
    {
        if (checkerAir.countAir > 0 && !isInflating)
        {
            if (checkerAir.countAir > countBaloonsInflating || currentTarget == null)
            {
                currentTarget = pointInflating;
            }
        }

        if (isInflating)
        {
            IdleEnemy();

            if (checkerAir.countAir == 0)
            {
                isInflating = false;
                UpdateTarget();
            }
        }
    }

    public void SetValueBlendShapes()
    {

        int countAirPlayer = checkerAir.countAir; // сколько у игрока воздуха
        int maxCountAirPlayer = GameController.Instance.checkerAirPlayer.maxCountAir; // сколько мах у игрока воздуха

        float valueBlendShape = (float)countAirPlayer / maxCountAirPlayer * 100; // значение BlendShape в процентах

        skinnedMeshRenderer.SetBlendShapeWeight(0, valueBlendShape); // значение  BlendShape
    }
}

