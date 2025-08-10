using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject gameOverUI; // Esto es el canva, lo tengo que poner después en el editor. SAS
    public Slider healthBar; // Referencia a la barra de salud en la UI
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {

        if (isDead) return; // Si ya está muerto, no hace nada más.
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        Debug.Log($"Jugador recibió daño: {damage}");
        Debug.Log($"Jugador recibe {damage} de daño. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        isDead = true;

        // Desactivar controles
        GetComponent<PlayerController>().enabled = false;
        // Mostrar UI de Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        //Esto congela al Jugador para que no se mueva.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Detiene el movimiento
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // Desactiva la física
        }
        Debug.Log("Jugador ha muerto.");
        //Acá va la diferente logica que después implemento :D 
        //muerte, animación, reinicio, etc.
        // Desactivar controles
        //GetComponent<PlayerMovement>().enabled = false; 

        // Destruir al jugador o reiniciar nivel
        // Destroy(gameObject);
    }

    public void Retry()
    {
        // Reiniciar el juego o nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        // Cargar el menú principal
        SceneManager.LoadScene("MainMenu"); // Asegúrate de que "MainMenu" sea el nombre correcto de tu escena de menú principal
    }


}
