using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Datos de Ataque")]
    public float attackRange = 0.5f; // Distancia máxima del ataque
    public int attackDamage = 10; // Daño del ataque
    public Transform attackPoint; // Punto desde donde se lanza el ataque
    public LayerMask enemyLayer; // Capa para detectar enemigos

    [Header("Control de Ataque")]
    public float attackCooldown = 0.5f; // Tiempo entre ataques
    private bool canAttack = true; // Controla si el jugador puede atacar

    [HideInInspector]
    public bool isDead = false; // Estado de muerte del jugador

    private bool isAttacking = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            //animator.SetTrigger("isAttacking", false);
            return; // Bloquear todo si está muerto
        }

        if (Input.GetKeyDown(KeyCode.J) && canAttack)
            {
                Attack();
            }
    }

    void Attack()
    {
        canAttack = false;
        animator.SetTrigger("Attack"); // Dispara la animación de ataque
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    // Estos métodos deben ser llamados desde eventos en la animación de ataque
    public void StartAttack()
    {
        isAttacking = true;
        DetectHit();
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    void DetectHit()
    {
        if (!isAttacking) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            BossHealth boss = enemy.GetComponent<BossHealth>() ?? enemy.GetComponentInParent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);
                Debug.Log($"Daño aplicado a {enemy.name}");
            }
            else
            {
                Debug.LogWarning($"Enemigo detectado sin BossHealth: {enemy.name}");
            }
        }
    }

    public void Die()
    {
        isDead = true;
        canAttack = false;
        isAttacking = false;
        //playerAttack.Die();
        animator.ResetTrigger("Attack");
        animator.SetBool("isAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
