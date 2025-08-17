using UnityEngine;

public class BossMeleeHitbox : MonoBehaviour
{
    public int damage = 40; // Daño configurable
    public Transform boss;  // Referencia al boss para saber hacia quién atacar (opcional)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Jugador golpeado por la guadaña!");
            }
        }
    }
}
