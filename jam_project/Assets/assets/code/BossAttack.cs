using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public BossHealth bossHealth; // Referencia al script de vida del jefe
    public float attackRange = 2f; // Distancia para ataque cuerpo a cuerpo
    public float projectileRange = 8f; // Distancia para ataque a distancia
    public int meleeDamage = 10; // Daño del ataque cuerpo a cuerpo
    public int rangedDamage = 20; // Daño del ataque a distancia
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde donde se lanza el proyectil

    public float attackCooldown = 2f;
    private float attackTimer;
    public bool canAttack = false; // Controla si el jefe puede atacar

    void Update()
    {

        if (!canAttack || bossHealth == null || player == null)
            return;
        attackTimer -= Time.deltaTime;
    
        if (attackTimer <= 0f)
        {
            int currentHealth = bossHealth.GetCurrentHealth();

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (currentHealth > 50)
            {
                if (distanceToPlayer <= attackRange) // Si la vida es alta, ataca cuerpo a cuerpo
                {
                    MeleeAttack();
                    attackTimer = attackCooldown;
                }
            }
            else if (currentHealth <= 50 && distanceToPlayer <= projectileRange)
            {
                RangedAttack(); // Si la vida es baja, ataca a distancia
                attackTimer = attackCooldown;
            }    

                
        }
    }

    void MeleeAttack()
    {
        Debug.Log("La Muerte hace ataque cuerpo a cuerpo.");
        // Aquí podés poner la lógica para hacer daño si el jugador está en rango,
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(meleeDamage); // Ajusta el daño según sea necesario
            }
        // animaciones, efectos, etc.
        }
    }
    void RangedAttack()
    {
   // Instancia el proyectil en el punto de disparo
    GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

    // Obtiene el script del proyectil para setear la dirección
    DeathProjectile dp = projectile.GetComponent<DeathProjectile>();

    if (dp != null)
    {
        // Calcula la dirección hacia el jugador en el momento del disparo
        Vector2 dir = (player.position - shootPoint.position).normalized;

        // Le indica al proyectil la dirección en la que debe moverse
        dp.SetDirection(dir);
    }
    }
}

