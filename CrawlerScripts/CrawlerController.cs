using UnityEngine;
using UnityEngine.AI;

public class CrawlerController : MonoBehaviour
{
    public Transform player; 
    private NavMeshAgent navMeshAgent;
    private Vector3 originalDestination;
    public float stopDistance = .5f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
       
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

           
            if (distanceToPlayer > stopDistance)
            {
                navMeshAgent.SetDestination(player.position);
                originalDestination = player.position;
            }
            else
            {
                
                navMeshAgent.SetDestination(transform.position);

            
                if (player.position != originalDestination)
                {
                  
                    navMeshAgent.SetDestination(player.position);
                    originalDestination = player.position;
                }
            }
        }
    }
}
