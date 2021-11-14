using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ������ ����� �� ����������
/// </summary>

public class EnemyController : MonoBehaviour
{
    enum ModeBot
    {
        MinDist,
        Random,
        FollowPlayer
    }

    private NavMeshAgent navMeshAgent;

    private CheckerAir checkerAir;

    [SerializeField]
    private Transform currentTarget;


    public bool isInflating; // �������� ������ �������

    //[SerializeField]
    private Animator animator;

    [Header("������ ���-�� ����� ��� ����������")]
    [SerializeField]
    private bool isRandomBallonInflating;

    [SerializeField]
    [Header("���-�� ����� ��� ����������")]
    private int countBaloonsInflating = 3;

    public int baloonsInflating = 0;


    [SerializeField]
    [Header("����� ����������")]
    private Transform pointInflating;

    [Header("���-�� ������� ����� RunHard")]
    [SerializeField]
    private int maxRunHard = 2;

    [Header("��������� �������� �����������")]
    [SerializeField]
    private float speedBegin = 3.0f;

    [Header("���������� ���������� ��������")]
    [SerializeField]
    private float multiplySpeed = 3;

    [Header("����� ���� ���������� ����")]
    [SerializeField]
    private ModeBot modeBot;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent;
        checkerAir = GetComponent<CheckerAir>();
        animator = GetComponentInChildren<Animator>();
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
        if (currentTarget)
        {
            if (currentTarget.gameObject.activeInHierarchy)
            {
                RunEnemy();
            }
            else
            {
                UpdateTarget();
                IdleEnemy();
            }
        }
        else
        {
            UpdateTarget();
            IdleEnemy();
        }
    }


    /// <summary>
    /// ������� ���� 
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
    /// ���� ����
    /// </summary>
    void IdleEnemy()
    {
        navMeshAgent.speed = 0;
        animator.SetBool("Run", false);
        animator.SetBool("RunHard", false);
    }

    /// <summary>
    /// ����� ����
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
      //  int baloonsInflating = 0;

        if (checkerAir.countAir > 0 && !isInflating)
        {
            if (isRandomBallonInflating)
            {
                baloonsInflating = Random.Range(1, countBaloonsInflating + 1);
            }
            else
            {
                baloonsInflating =  countBaloonsInflating;
            }

            int remainderDivision = checkerAir.countAir % baloonsInflating;

            if (remainderDivision == 0)
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
}

