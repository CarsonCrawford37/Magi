using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class RuneScript : MonoBehaviour
{
    [SerializeField] string rune;
    [SerializeField] SpellBookManager spellBook;
    [SerializeField] VisualEffect effect;
    [SerializeField] TextMeshProUGUI runeText;
    public Color newColor;

    private AudioSource runeSound;
    private SphereCollider runeCol;
    private Light glow;
    private bool fadingLight = false;
    public float fadeDuration = 6f; // time in seconds to fade out the light
    private float fadeTimer = 0f;

    private void Awake()
    {
        runeSound = GetComponent<AudioSource>();
        runeCol = GetComponent<SphereCollider>();
        glow = GetComponent<Light>();
/*        runeText = GetComponent<TextMeshProUGUI>();
*/    }

    private void Update()
    {
        if (fadingLight)
        {
            if (fadeTimer < fadeDuration)
            {
                glow.intensity = Mathf.Lerp(glow.intensity, 0f, fadeTimer / fadeDuration);
                runeText.color = Color.Lerp(runeText.color, newColor, fadeTimer / fadeDuration);
                fadeTimer += Time.deltaTime;
            }
            else
            {
                glow.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spellBook.AddSpellToBook(rune);
            runeSound.Play();
            runeCol.enabled = false;
            fadingLight = true;
            effect.Stop();
        }
    }

}
