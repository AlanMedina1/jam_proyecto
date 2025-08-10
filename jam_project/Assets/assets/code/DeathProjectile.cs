using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
      public float speed = 5f;
    public int damage = 20;
    public float lifeTime = 5f;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("DeathProjectile necesita un Rigidbody2D.");
            return;
        }

        rb.linearVelocity = direction * speed;

        // Rotar para que el sprite apunte hacia la dirección
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(gameObject, lifeTime);
    }

    // Método para establecer la dirección antes de que empiece el movimiento
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Proyectil colisionó con: {collision.gameObject.name}");
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
