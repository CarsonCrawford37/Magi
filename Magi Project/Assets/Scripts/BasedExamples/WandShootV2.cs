using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
public class WandShootV2 : MonoBehaviour
{
    public GameObject wandLight;

    private AudioSource wandAudio;
    bool isHolding = false;
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("lux", Lux);

        wandAudio = wandLight.GetComponent<AudioSource>();

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
    }

    void Update()
    {
        if(isHolding == false){
            wandLight.SetActive(false);
        }
    }

    public void IsHeld()
    {
        keywordRecognizer.Start();
        isHolding = true;
    }

    public void IsDropped()
    {
        keywordRecognizer.Stop();
        isHolding = false;
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Lux()
    {
        if (!wandLight.activeInHierarchy && isHolding)
        {
            wandLight.SetActive(true);
            wandAudio.pitch = 2f;
            wandAudio.Play();
        }
        else if (wandLight.activeInHierarchy && isHolding)
        {
            wandAudio.pitch = -1.0f;
            wandAudio.Play();
            wandLight.SetActive(false);
        }
    }
}
