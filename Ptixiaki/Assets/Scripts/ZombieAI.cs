
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public float speed = 3.5f;
    public float damage = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    public WaveManager waveManager;
    public Slider healthSlider;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isDead = false;
    private bool isPaused = false;

    private float lastAttackTime;

    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }
    public void PauseZombie(bool pause)
    {
        isPaused = pause;

        if (agent) agent.isStopped = pause;
        if (animator) animator.speed = pause ? 0f : 1f;   // freeze / resume anim
    }


void Update()
    {
        
        if (isPaused || Time.timeScale == 0f) return;

        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }

        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
    }




    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;

        if (healthSlider != null)
            healthSlider.value = health;

        if (health <= 0)
            Die();
    }

    private void AttackPlayer()
    {
        
        if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetTrigger("Die");

        if (waveManager != null)
            waveManager.RegisterKill();

        Destroy(gameObject, 2f);
    }
}
