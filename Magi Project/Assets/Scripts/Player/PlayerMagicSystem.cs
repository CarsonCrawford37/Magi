using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PlayerMagicSystem : MonoBehaviour
{
    // PLAYER STUFF //
    [SerializeField] GameObject player;
    //[SerializeField] private Animator hand;
    //[SerializeField] GameObject wand;

    public float health;

    // PLAYER UI //
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Mana;

    // SPELLS //
    [SerializeField] private Spell fireball;
    [SerializeField] private Spell iceSpike;
    //[SerializeField] private Spell lux;
    private Spell spellToCast;

    bool activeSpell = false;

    // MANA SYSTEM //
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 15f;
    [SerializeField] private float timeToWaitForRecharge = 1f;
    private float currentManaRechargeTimer;
    [SerializeField] private float timeBetweenCasts = 0.25f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;
    bool hasEnoughMana;

    [SerializeField] private HapticsController hapticsController;

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
        actions.Add("ignis", Fireball);
        actions.Add("gelu", Icespike);
        //actions.Add("lux", Light);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void Update()
    {
        health = player.GetComponent<PlayerHealthComponent>().currentHealth;

        //Cast cooldown
        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts) castingMagic = false;
        }

        //Mana recharge
        if (currentMana < maxMana & !castingMagic)
        {
            currentManaRechargeTimer += Time.deltaTime;
            if (currentManaRechargeTimer > timeToWaitForRecharge)
            {
                currentMana += manaRechargeRate * Time.deltaTime;
                if (currentMana > maxMana) currentMana = maxMana;

            }
        }

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

    //Method for when a spell is cast
    void CastSpell()
    {
        hasEnoughMana = currentMana - spellToCast.SpellToCast.ManaCost >= 0f; //if not work put in update
        if (activeSpell == true)
        {

        }
        else
        {
            if (!castingMagic && hasEnoughMana)
            {
                //HarmPlayer();
                castingMagic = true;
                currentMana -= spellToCast.SpellToCast.ManaCost;
                currentCastTimer = 0;
                currentManaRechargeTimer = 0;
                //if (hand.isActiveAndEnabled == true)
                //{
                //    Debug.Log("HANDS");
                //    hand.Play("cast_spell"); //Play hand animation when casting spell -- might not work
                //}

                Instantiate(spellToCast, castPoint.position, castPoint.rotation);
            }

            if (!hasEnoughMana)
            {
                hapticsController.SendHaptics(.3f, .7f);
            }

        }
    }

   /* void HarmPlayer()
    {
        PlayerHealthComponent playerHealth = player.GetComponent<PlayerHealthComponent>();
        bool isHoldingWand = wand.GetComponent<UpdateCastPoint>().isHolding;
        Debug.Log(isHoldingWand);
        if (!isHoldingWand)
        {
            playerHealth.TakeSpellDamage();
            hapticsController.SendHaptics(.2f, .5f);
        }
    }*/

}