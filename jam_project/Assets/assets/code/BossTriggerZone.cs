using UnityEngine;

public class BossTriggerZone : MonoBehaviour

{
    public BossHealth bossHealth;

    private bool fightStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fightStarted && other.CompareTag("Player"))
        {
            fightStarted = true;
            bossHealth.ShowHealthBar();
            // Acá podés empezar música, animaciones, etc.
        }
    }
}
