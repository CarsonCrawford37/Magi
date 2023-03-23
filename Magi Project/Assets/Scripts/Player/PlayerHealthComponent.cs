using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] public Material hurtOverlay;
    public FadeToBlack blackScreen;
    private Color color;

    private void Awake()
    {
        currentHealth = maxHealth;
        color.a = 0f;
    }

    // Update is called once per frame
    public void TakeSpellDamage()
    {
        color = hurtOverlay.color;

        currentHealth -= 15;
        if (currentHealth <= 0)
        {
            blackScreen.FadeOut();
            StartCoroutine(WaitForNextScene());
        }

        switch (currentHealth)
        {
            case 55:
                color.a = .25f;
                hurtOverlay.color = color;
                break;

            case 40:
                color.a = .5f;
                hurtOverlay.color = color;
                break;

            case 25:
                color.a = 1f;
                hurtOverlay.color = color;
                break;

            case 10:
                color.a = 1.5f;
                hurtOverlay.color = color;
                break;
            case -5:
                color.a = 0f;
                hurtOverlay.color = color;
                break;

        }
        Debug.Log("Player Health: " + currentHealth);
    }

    public IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("PassedOut");
    }
}
