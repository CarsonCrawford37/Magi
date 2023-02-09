using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer; //creates a KeywordRecognizer to recognize phrases
    private Dictionary<string, Action> actions = new Dictionary<string, Action>(); //creates a custom dictionary that stores phrases and methods

    void Start()
    {
        //Populates dictionary with phrases and actions
        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("left", Left);
        //Populates dictionary with phrases and actions

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); //defines the keyworRecognizer with the phrases from dictionary
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech; //Looks for phrases from microphone and sends whats been said to the RecognizedSpeech function
        keywordRecognizer.Start(); //Enables the microphone and will start looking for phrases

    }

    // void Update()
    // {
    //     if (Input.GetKeyDown("space"))
    //     {
    //         keywordRecognizer.Start(); //Enables the microphone and will start looking for phrases
    //     }
    //     else
    //     {
    //         keywordRecognizer.Stop(); //Disables the microphone and stops looking for phrases.Might be useful when casting spells
    //     }
    // }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        transform.Translate(0, 0, -1);
    }
    private void Up()
    {
        transform.Translate(0, 1, 0);
    }
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }
    private void Back()
    {
        transform.Translate(0, 0, 1);
    }
    private void Right()
    {
        transform.Translate(1, 0, 0);
    }
    private void Left()
    {
        transform.Translate(-1, 0, 0);
    }

}

