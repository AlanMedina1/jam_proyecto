using UnityEngine;

public class BossTriggerZone : MonoBehaviour

{
    public BossHealth bossHealth;
    public BossAttack bossAttack;
    private bool fightStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fightStarted && other.CompareTag("Player"))
        {
            fightStarted = true;
            bossAttack.canAttack = true; // Permite que el jefe ataque
            bossHealth.ShowHealthBar();
            Destroy(gameObject); // Destruye el trigger para que no se active de nuevo
            // Acá podés empezar música, animaciones, etc.
        }
    }
}
