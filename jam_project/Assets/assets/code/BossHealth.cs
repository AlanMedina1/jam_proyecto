using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour

{
   public int maxHealth = 100;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float flashDuration = 0.1f;

    public GameObject healthBarUI; // Objeto que contiene la barra de vida (puede ser un Canvas o Slider)

    private Slider healthBar;
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        if (healthBarUI != null)
        {
            healthBarUI.SetActive(false); // Oculta la barra al inicio
            healthBar = healthBarUI.GetComponentInChildren<Slider>();
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;
                healthBar.value = maxHealth;
            }
        }
    }

    public void ShowHealthBar()
    {
        if (healthBarUI != null)
            healthBarUI.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
            currentHealth = 0; // Asegura que la salud no sea negativa
            
        if (healthBar != null)
            healthBar.value = currentHealth;

        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        if (currentHealth <= 0)
            Die();
    }

    private System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        Destroy(gameObject);
    }
}
