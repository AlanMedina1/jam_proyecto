using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f; // Distancia máxima de golpe
    public int attackDamage = 20; // Daño que hace el ataque
    public Transform attackPoint; // Punto desde donde se lanza el ataque
    public LayerMask enemyLayer; // Capa para detectar enemigos

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Por ejemplo, tecla J para atacar
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detecta si hay enemigos en un radio desde attackPoint
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossHealth>()?.TakeDamage(attackDamage);
            //hit.collider.GetComponent<BossHealth>()?.TakeDamage(20);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
