using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private float healthRechargeRate = 15f;
    [SerializeField] private float timeToWaitForRecharge = 1f;
    private float currentHealthRechargeTimer;

    [SerializeField] public Material hurtOverlay;
    public FadeToBlack blackScreen;
    private Color color;

    private void Awake()
    {
        currentHealth = maxHealth;
        color.r = 1;
        color.a = 0f;
    }

    private void Update()
    {
        if(currentHealth >= 56)
        {
            color.a = 0f;
            hurtOverlay.color = color;
        }
        else if(55 >= currentHealth && currentHealth >= 41)
        {
            color.a = .25f;
            hurtOverlay.color = color;
        }
        else if (40 >= currentHealth && currentHealth >= 26)
        {
            color.a = .5f;
            hurtOverlay.color = color;
        }
        else if (25 >= currentHealth && currentHealth >= 11)
        {
            color.a = 1f;
            hurtOverlay.color = color;
        }
        else if (10 >= currentHealth && currentHealth >= 1)
        {
            color.a = 1.5f;
            hurtOverlay.color = color;
        }
        else if(currentHealth <= 0)
        {
            color.a = 0f;
            hurtOverlay.color = color;
        }
    }

    // Update is called once per frame
    public void TakeSpellDamage()
    {
        color = hurtOverlay.color;
        //Debug.Log(hurtOverlay.color);
        currentHealth -= 15;
        if (currentHealth <= 0)
        {
            blackScreen.FadeOut();
            StartCoroutine(WaitForNextScene());
        }

        
        Debug.Log("Player Health: " + currentHealth);
    }

    public void HealthRecharge()
    {
        if (currentHealth < maxHealth)
        {
            currentHealthRechargeTimer += Time.deltaTime;
            if (currentHealthRechargeTimer > timeToWaitForRecharge)
            {
                currentHealth += healthRechargeRate * Time.deltaTime;
                if (currentHealth > maxHealth) currentHealth = maxHealth;
            }
        }
    }

    public IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("PassedOut");
    }
}
