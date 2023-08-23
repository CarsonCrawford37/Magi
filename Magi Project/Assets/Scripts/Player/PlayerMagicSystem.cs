using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerMagicSystem : MonoBehaviour
{
    // PLAYER STUFF //
    [SerializeField] GameObject player;
    [SerializeField] private Animator hand;
    [SerializeField] GameObject wand;

    public float health;
    PlayerHealthComponent playerHealth;

    // PLAYER UI //
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Mana;

    // SPELLS //
    [SerializeField] private Spell fireball;
    [SerializeField] private Spell iceSpike;
    //[SerializeField] private Spell lux;
    private Spell spellToCast;

    // MANA SYSTEM //
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 15f;
    [SerializeField] private float timeToWaitForRecharge = 1f;
    private float currentManaRechargeTimer;
    [SerializeField] private float timeBetweenCasts = 0.25f;
    private float currentCastTimer;
    private Coroutine _manaRechargeCoroutine;

    // HEALTH SYSTEM //

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;
    bool hasEnoughMana;

    // HAPTICS //
    [SerializeField] private HandController rHandController;
    private HapticsController hapticsController;

    // VOICE COMMAND SYSTEM //
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Awake()
    {
        currentMana = maxMana;

    }

    //When game starts, add spell phrases to dictionary and start listening for them
    private void Start()
    {
        hapticsController = GetComponent<HapticsController>();
        //hand = hand.GetComponent<Animator>();

        actions.Add("ignis", Fireball);
        actions.Add("duratus", Icespike);
        //actions.Add("lux", Light);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void Update()
    {
        //health = player.GetComponent<PlayerHealthComponent>().currentHealth;

        //Cast cooldown
        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts) castingMagic = false;
        }

        //Mana recharge
        if (currentMana < maxMana & !castingMagic)
        {
            /*currentManaRechargeTimer += Time.deltaTime;
            if (currentManaRechargeTimer > timeToWaitForRecharge)
            {
                currentMana += manaRechargeRate * Time.deltaTime;
                if (currentMana > maxMana) currentMana = maxMana;
            }*/
            StartManaRecharge();
        }

        if (health < 100)
            playerHealth.HealthRecharge();


        Health.text = "Health: " + health.ToString("N0");
        Mana.text = "Mana: " + currentMana.ToString("N0");

    }

    //Function that will recognize the phrases and invoke the method paired with it
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    //Method for when the fireball spell is called
    private void Fireball()
    {
        spellToCast = fireball;

        CastSpell();
    }

    //Method for when the ice spike spell is called
    private void Icespike()
    {
        spellToCast = iceSpike;
        CastSpell();
    }

    /* private void Light() //needs to toogle on and off
     {
         spellToCast = lux;
         if (activeSpell == false)
         {
             Debug.Log("on");
             activeSpell = true;
             int i = 0;
             while (i < 100 && activeSpell == true) //hasEnoughMana && activeSpell == true 
             {
                 Debug.Log("Spell casting");
                 i++;
                 //CastSpell();
             }
         }
         else
         {
             Debug.Log("off");
             activeSpell = false;
         }
     }*/

    //Coroutine to recharge mana
    private IEnumerator ManaRechargeCoroutine()
    {
        while (currentMana < maxMana && !castingMagic)
        {
            // Recharge the mana over time
            currentMana += manaRechargeRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);

            // Wait for a short duration before recharging more
            yield return new WaitForSeconds(timeToWaitForRecharge);
        }

        // Coroutine finished or interrupted, reset the reference
        _manaRechargeCoroutine = null;
    }

    // Method to start the mana recharge coroutine
    private void StartManaRecharge()
    {
        if (_manaRechargeCoroutine == null)
        {
            _manaRechargeCoroutine = StartCoroutine(ManaRechargeCoroutine());
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

    //Method for when a spell is cast
    void CastSpell()
    {
        hasEnoughMana = currentMana - spellToCast.SpellToCast.ManaCost >= 0f; //if not work put in update

        hand.Play("SpellCast");
        if (!hasEnoughMana)
        {
            hapticsController.SendHaptics(.3f, .7f);
            return;
        }

        if (!castingMagic && hasEnoughMana)
        {
            HarmPlayer();
            castingMagic = true;
            currentMana -= spellToCast.SpellToCast.ManaCost;
            currentCastTimer = 0;
            currentManaRechargeTimer = 0;
            /*if (hand.isActiveAndEnabled == true)
            {
                Debug.Log("HANDS");
                hand.Play("cast_spell"); //Play hand animation when casting spell -- might not work
            }*/
            StopManaRecharge();
            Instantiate(spellToCast, castPoint.position, castPoint.rotation);
        }

       /* if (!hasEnoughMana)
        {
            hapticsController.SendHaptics(.3f, .7f);
        }*/

    }

    void HarmPlayer()
    {
        health = player.GetComponent<PlayerHealthComponent>().currentHealth;
        playerHealth = player.GetComponent<PlayerHealthComponent>();
        bool isHoldingWand = wand.GetComponent<UpdateCastPoint>().isHolding;
        //Debug.Log(isHoldingWand);
        if (!isHoldingWand)
        {
            playerHealth.TakeSpellDamage();
            hapticsController.SendHaptics(rHandController, .2f, .5f);
        }


    }

}