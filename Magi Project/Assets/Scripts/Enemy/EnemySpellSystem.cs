using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpellSystem : MonoBehaviour
{
    public GameObject hand;
    private bool _playerInSight;

    [Header("SPELLS")]
    [SerializeField] private List<Spell> spells;
    /*[SerializeField] private Spell fireball;
    [SerializeField] private Spell iceSpike;*/
    private Spell spellToCast;
    private int randomSpell;

    [Header("HEALTH")]
    public float health;
    public HealthComponent enemyHealth;

    [Header("MANA")]
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 15f;
    [SerializeField] private float timeToWaitForRecharge = 1f;
    private float currentManaRechargeTimer;
    [SerializeField] private float timeBetweenCasts = 0.25f;
    private float currentCastTimer;
    private Coroutine _manaRechargeCoroutine;

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;
    bool hasEnoughMana;

    private void Awake()
    {
        currentMana = maxMana;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthComponent>().currentHealth;

    }

    // Update is called once per frame
    void Update()
    {
        

        //Cast cooldown
        /*if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts) castingMagic = false;
        }*/

        //Mana recharge
        if (currentMana < maxMana /*& !castingMagic*/)
        {

            currentManaRechargeTimer += Time.deltaTime;
            if (currentManaRechargeTimer > timeToWaitForRecharge)
            {
                currentMana += manaRechargeRate * Time.deltaTime;
                if (currentMana > maxMana) currentMana = maxMana;
            }
            //StartManaRecharge();
        }

        if (_playerInSight && !castingMagic)
        {
            StartCoroutine(CastSpell());
            //CastSpell();
        }
    }

    // Method to stop the mana recharge coroutine
    private void StopManaRecharge()
    {
        if (_manaRechargeCoroutine != null)
        {
            StopCoroutine(_manaRechargeCoroutine);
            _manaRechargeCoroutine = null;
        }
    }

    private IEnumerator CastSpell()
    {
        RandomSpell();

        //Checks if the enemy has enough mana to cast the spell 
        hasEnoughMana = currentMana - spellToCast.SpellToCast.ManaCost >= 0f;

        if (!castingMagic)
        {
            if (hasEnoughMana)
            {
                //Deduct mana cost and reset timers
                currentMana -= spellToCast.SpellToCast.ManaCost;
                currentCastTimer = 0;
                currentManaRechargeTimer = 0;

                //Stop mana recharge while casting
                StopManaRecharge();

                //Instantiate the spell
                Instantiate(spellToCast, castPoint.position, castPoint.rotation);
            }
            castingMagic = true;
        }

        yield return new WaitForSeconds(Random.Range(5, 10));

        castingMagic = false;


    }

    private void RandomSpell()
    {
        randomSpell = Random.Range(0, spells.Count);
        spellToCast = spells[randomSpell];
        return;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInSight = true;
            //transform.LookAt(other.transform);
            transform.LookAt(other.transform.position);
            hand.transform.LookAt(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInSight = false;
        }
    }
}
