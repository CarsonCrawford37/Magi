using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    public float currentHealth;
    public bool hasDied = false;
    private Animation anim;
    public ParticleSystem ps;

    //private float timeToWait = 5f;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageToApply)
    {
        currentHealth -= damageToApply;
        if (anim != null)
        {
            anim.Play("Impact");
        }

        if (currentHealth <= 0)
        {
            if (ps != null)
            {
                ParticleSystem psClone = Instantiate(ps, transform.position, Quaternion.Euler(0, 90, 0));
                Destroy(psClone.gameObject, 5);
            }
            //Destroy(gameObject);
            gameObject.SetActive(false);
            hasDied = true;
            currentHealth = maxHealth;
        }

        Debug.Log("Enemy Health: " + currentHealth);
    }


}
