using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    public float currentHealth;

    public FadeToBlack blackScreen;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeSpellDamage()
    {
        currentHealth -= 15;
        if (currentHealth <= 0)
        {
            blackScreen.FadeOut();
            StartCoroutine(WaitForNextScene());
        }

        Debug.Log("Player Health: " + currentHealth);
    }

    public IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("PassedOut");
    }
}
