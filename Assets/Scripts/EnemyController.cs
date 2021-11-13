using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private CheckerAir checkerAir;

    //[SerializeField]
    private Transform currentTarget;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    [Header("Кол-во шаров для скачивания")]
    private int countBaloonsInflating = 3;

    [SerializeField]
    [Header("Место скачивания")]
    private Transform pointInflating;

    private float currentSpeed; // Текущая цель

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent;
        currentSpeed = navMeshAgent.speed;
        //currentTarget = GetNewTarget(gameObject.transform);
    }


    void Update()
    {
        if (GameController.Instance.isPlayGame)
        {
            //int remainderDivision = checkerAir.countAir % countBaloonsInflating;
            //  if (remainderDivision == 0)
            //  {
            //  currentTarget = pointInflating;
            //  }
            //  else
            //  {
            currentTarget = GetNewTarget(gameObject.transform);
            //  }
            UpdateTarget();
        }

    }

    void UpdateTarget()
    {
        if (currentTarget)
        {
            if (currentTarget.gameObject.activeInHierarchy)
            {
                RunEnemy();
            }
            else
            {
                IdleEnemy();
            }
        }
        else
        {
            IdleEnemy();
        }
    }


    /// <summary>
    /// побежал перс 
    /// </summary>
    void RunEnemy()
    {
        navMeshAgent.speed = currentSpeed;
        navMeshAgent.destination = currentTarget.position;
        animator.SetBool("Run", true);
    }


    /// <summary>
    /// стоп перс
    /// </summary>
    void IdleEnemy()
    {
        navMeshAgent.speed = 0;
        animator.SetBool("Run", false);
    }

    /// <summary>
    /// поиск цели
    /// </summary>
    public Transform GetNewTarget(Transform positionObject)
    {
        Transform currentPosition = GameController.Instance.GetNewTarget(positionObject);
        return currentPosition;
    }


}

