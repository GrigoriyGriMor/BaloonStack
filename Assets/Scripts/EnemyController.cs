using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private Transform currentObject;

    [SerializeField]
    private Animator animator;

    private float currentSpeed;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent; 
        currentSpeed = navMeshAgent.speed;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            UpdateTarget(currentObject);
        }
    }

    void UpdateTarget(Transform targetObject)
    {
        if (targetObject && targetObject.gameObject.activeInHierarchy)
        {
            navMeshAgent.speed = currentSpeed;
            navMeshAgent.destination = targetObject.position;
            animator.SetBool("Run", true);
        }
        else
        {
            navMeshAgent.speed = 0;
            animator.SetBool("Run", false);
            GetNewTarget();
        }
    }

    void GetNewTarget()
    {
        currentObject = GameController.Instance.GetNewTarget(gameObject.transform);
        Debug.Log("Поиск целей  " + currentObject.gameObject);
    }

}
