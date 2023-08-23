using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public LightBrazier brazier1;
    public LightBrazier brazier2;

    private bool _isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            brazier1.isLit = true;
            brazier2.isLit = true;
            if(_isPlaying == false)
            {
                _isPlaying = true;
                VideoPlayer.Play();
                AudioSource.PlayOneShot(AudioClip);
            }
        }
    }
}
