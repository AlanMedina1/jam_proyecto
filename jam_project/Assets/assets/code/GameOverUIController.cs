using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameOverUIController : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Asigna el CanvasGroup del Game Over UI
    public float fadeDuration = 1f;

    private void Start()
    {
        // Al inicio la UI está invisible
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }
    
    /*public void ShowGameOverInstant()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }*/
    public void ShowGameOver()
    {
        Debug.Log("Activando Game Over UI");
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float timer = 0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
