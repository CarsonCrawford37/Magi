using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WandShoot : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    public Rigidbody sphere;
    public float thrust = 1000.0f;

    private GameObject chosenPos;

    private ParticleSystem pSystem;

    private KeywordRecognizer keywordRecognizer; //creates a KeywordRecognizer to recognize phrases
    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); //creates a custom dictionary that stores phrases and methods

    void Start()
    {
        //Populates dictionary with phrases and actions
        actions.Add("poof", Poof);
        actions.Add("ignis", Ball);

        //Populates dictionary with phrases and actions

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); //defines the keyworRecognizer with the phrases from dictionary
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech; //Looks for phrases from microphone and sends whats been said to the RecognizedSpeech function
    }

    public void IsHeld() //Function that enables the microphone to pickup key words when holding wand
    {
        keywordRecognizer.Start();
    }

    public void IsDropped() //Function that disables the microphone to pickup key words when dropped wand
    {
        keywordRecognizer.Stop();
    }


    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) //Function for when a keyword is recognized
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Poof() //Poof function to play the particle system when keyword is recongized
    {
        Instantiate(pSystem, RandomPos().transform.position, RandomPos().transform.rotation);
    }

    private void Ball() //Poof function to play the particle system when keyword is recongized
    {
        Rigidbody ball = Instantiate(sphere, RandomPos().transform.position, RandomPos().transform.rotation);
        ball.AddRelativeForce(new Vector3(thrust, 0, 0));
    }

    private GameObject RandomPos()
    {
        if (UnityEngine.Random.value > 0.7)//30% chance of obejct spawning at pointB
        {
            return pointB;
        }
        else
        {
            return pointA;
        }
    }

}
