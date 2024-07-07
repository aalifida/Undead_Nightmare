using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public Transform player; // Drag the player GameObject here in the Inspector
    private NavMeshAgent navMeshAgent;
    private Vector3 originalDestination;
    public float stopDistance = .5f; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        // Move the enemy towards the player
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the distance is greater than the stop distance, move towards the player
            if (distanceToPlayer > stopDistance)
            {
                navMeshAgent.SetDestination(player.position);
                originalDestination = player.position;
            }
            else
            {
                // If the distance is less than or equal to the stop distance, stop moving
                navMeshAgent.SetDestination(transform.position);
                
                // Check if the player's position has changed
                if (player.position != originalDestination)
                {
                    // Player has moved, update the destination
                    navMeshAgent.SetDestination(player.position);
                    originalDestination = player.position;
                }
            }
        }
    }

    }

