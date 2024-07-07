using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CrawlerAttack : MonoBehaviour
{
    public GameObject healthPrefab;
    public GameObject coinPrefab;
      public GameObject ammoPrefab;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private int crawlerHealth;
    public float attackRange = 1f;
    bool soundPlayed=false;
    
    private AudioSource audioSource;
    public ParticleSystem blood;
    public bool isCrawling = false; 
    public Transform armatureTransform;
    public float soundRange = 20f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        crawlerHealth = 100;
        navMeshAgent.stoppingDistance = attackRange;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        

        if (distanceToPlayer >attackRange)
        {
            Move();
        }
        else if (distanceToPlayer <= attackRange)
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
    animator.SetBool("Crawling", true);
        animator.SetBool("Attacking", false);
  
    navMeshAgent.SetDestination(player.position);


}

    void Attack()
    {
        animator.SetBool("Crawling", false);
        animator.SetBool("Attacking", true);
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void CrawlerDie()
    {
        
        animator.SetBool("Attacking", false);
        animator.SetBool("Crawling", isCrawling);

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
            crawlerHealth -= 10;
            if (crawlerHealth <= 30)
            {
                CrawlerDie();
                Destroy(gameObject,0.1f);
            }
        }
    }

    IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
