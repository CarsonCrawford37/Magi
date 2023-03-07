using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class HandSpells : MonoBehaviour
{
    public GameObject spawnPoint;

    public Rigidbody sphere;
    public float thrust = 1000.0f;

    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("ignis", Ball);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) 
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Ball() 
    {
        Rigidbody ball = Instantiate(sphere, spawnPoint.transform.position, spawnPoint.transform.rotation);
        ball.AddRelativeForce(new Vector3(thrust, 0, 0));
    }

}
