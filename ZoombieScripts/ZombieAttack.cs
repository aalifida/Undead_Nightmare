using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour
{
    public GameObject healthPrefab;
    public GameObject coinPrefab;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private int zombieHealth;
    public float attackRange = 1f;
    private AudioSource audioSource;
    public ParticleSystem blood;
    public float soundRange = 20f;
    public GameObject ammoPrefab;
    private bool soundPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieHealth = 100;
        navMeshAgent.stoppingDistance = attackRange;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else
        {
            Move();
        }

        if (distanceToPlayer <= soundRange && !soundPlayed)
        {
            audioSource.Play();
            soundPlayed = true;
        }
        else if (distanceToPlayer > soundRange && soundPlayed)
        {
            audioSource.Stop();
            soundPlayed = false;
        }
    }

    void Move()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Attacking", false);

        navMeshAgent.SetDestination(player.position);
    }

    void Attack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attacking", true);

        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void ZombieDie()
    {
        animator.SetBool("Dying", true);
        animator.SetBool("Attacking", false);
        animator.SetBool("Walk", false);
        Vector3 spawnPosition = transform.position + transform.forward * 1f;
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        Instantiate(healthPrefab, transform.position, Quaternion.identity);
        Instantiate(ammoPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Instantiate(blood, hitPoint, Quaternion.identity);
            zombieHealth -= 20;

            if (zombieHealth <= 50)
            {
                ZombieDie();
                navMeshAgent.isStopped = true;
                StartCoroutine(DestroyAfterAnimation(2f));
            }
        }
    }

    IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
