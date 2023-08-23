using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAudio : MonoBehaviour
{
    public AudioClip AudioClip;
    AudioSource AudioSource;

    private bool _isPlaying = false;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_isPlaying == false)
            {
                _isPlaying = true;
                AudioSource.PlayOneShot(AudioClip);
            }
        }
    }
}
