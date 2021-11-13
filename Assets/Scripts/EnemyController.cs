using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private Animator animator;

    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>() as NavMeshAgent; //FindObjectOfType(typeof(NavMeshAgent)) as NavMeshAgent;
        currentSpeed = navMeshAgent.speed;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            UpdateTarget(targetObject);
        }
    }

    void UpdateTarget(Transform targetObject)
    {
        if (targetObject)
        {
            navMeshAgent.speed = currentSpeed;
            navMeshAgent.destination = targetObject.position;
            animator.SetBool("Run", true);
        }
        else
        {
            navMeshAgent.speed = 0;
            animator.SetBool("Run", false);

        }
    }

    void GetNewTarget(Transform targetObject)
    {

    }

}
