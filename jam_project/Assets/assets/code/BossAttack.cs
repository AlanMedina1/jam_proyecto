using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public BossHealth bossHealth;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public GameObject scytheHitbox;
    public Transform spawnPoint;

    [Header("Ataques")]
    public float attackRange = 2f;       // Distancia melee
    public float projectileRange = 8f;   // Distancia ranged
    public int meleeDamage = 10;
    public int rangedDamage = 20;
    public float attackCooldown = 2f;

    [Header("Movimiento")]
    public float moveSpeed = 0.8f;
    public float stopDistance = 2f;      // Detención frente al jugador
    public float maxMoveDistance = 10f;  // Distancia máxima desde spawn

    [Header("Estado")]
    public bool canAttack = false;
    private float attackTimer = 0f;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (scytheHitbox != null)
            scytheHitbox.SetActive(false);

        if (spawnPoint == null)
            spawnPoint = transform; // Si no asignaste spawnPoint, usa la posición inicial
    }

    void FixedUpdate()
    {
        if (!canAttack || bossHealth == null || player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceFromSpawn = Vector2.Distance(transform.position, spawnPoint.position);
        float distancePlayerFromSpawn = Vector2.Distance(player.position, spawnPoint.position);

        Vector2 direction = Vector2.zero;

        // 🔹 Movimiento hacia el jugador si está dentro de zona de acción y fuera del stopDistance
        if (distancePlayerFromSpawn <= maxMoveDistance && distanceToPlayer > stopDistance)
        {
            direction = (player.position - transform.position).normalized;
        }
        // 🔹 Retroceso al spawn si se pasó de la zona
        else if (distanceFromSpawn > 0.1f)
        {
            direction = (spawnPoint.position - transform.position).normalized;
        }
        // 🔹 Si está cerca del spawn y jugador dentro de stopDistance → no moverse
        else
        {
            direction = Vector2.zero;
        }

        // 🔹 Aplicar movimiento
        if (direction != Vector2.zero)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // 🔹 Ataques con cooldown
        if (attackTimer <= 0f)
        {
            int currentHealth = bossHealth.GetCurrentHealth();

            // Ataque cuerpo a cuerpo si vida alta y jugador en rango
            if (currentHealth > 50 && distanceToPlayer <= attackRange)
            {
                animator.SetTrigger("AttackMelee");
                attackTimer = attackCooldown;
            }
            // Ataque a distancia si vida baja y jugador en rango
            else if (currentHealth <= 50 && distanceToPlayer <= projectileRange)
            {
                animator.SetTrigger("AttackRanged");
                RangedAttack();
                attackTimer = attackCooldown;
            }
        }
        else
        {
            attackTimer -= Time.fixedDeltaTime;
        }
    }

    // 🔹 Animation Events
    public void EnableHitbox()
    {
        if (scytheHitbox != null)
            scytheHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        if (scytheHitbox != null)
            scytheHitbox.SetActive(false);
    }

    // 🔹 Ataque a distancia
    void RangedAttack()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        DeathProjectile dp = projectile.GetComponent<DeathProjectile>();
        if (dp != null)
        {
            Vector2 dir = (player.position - shootPoint.position).normalized;
            dp.SetDirection(dir);
        }
    }
}
