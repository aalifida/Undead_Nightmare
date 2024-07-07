using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantAttack : MonoBehaviour
{
    public GameObject healthPrefab;
    public GameObject coinPrefab;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private int GiantHealth;
    public float attackRange = 1f;
    private ScoreManager sm;
    private AudioSource audioSource;
    public ParticleSystem blood;
    public float soundRange = 20f;
    public GameObject ammoPrefab;
    private bool soundPlayed = false;

    void Start()
    {
        sm = GetComponent<ScoreManager>();
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        GiantHealth = 100;
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
        animator.SetBool("Walking", true);
        animator.SetBool("Attack", false);

        navMeshAgent.SetDestination(player.position);
    }

    void Attack()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Attack", true);

        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void GiantDie()
    {
        animator.SetBool("Dying", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Walking", false);
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
            GiantHealth -= 5;

            if (GiantHealth <= 20)
            {
                GiantDie();
                navMeshAgent.isStopped = true;
                StartCoroutine(DestroyAfterAnimation(0.5f));
            }
        }
    }

    IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
