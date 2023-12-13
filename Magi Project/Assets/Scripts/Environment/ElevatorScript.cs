using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ElevatorScript : MonoBehaviour
{
    public List<SymbolActivated> symbols;
    public Light elevatorLight;
    public AudioSource source;

    public GameObject anchor;
    public GameObject player;

    bool _playerIsInElevator = false;
    bool _isPlaying = false;
    bool _startFade = false;
    int _symbolsActivated = 0;
    Animator _animator;


    //private Vector3 elevatorPos;// = new Vector3 (1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (SymbolActivated symbol in symbols)
        {
            if (symbol.isActivated)
            {
                _symbolsActivated++;
                symbols.Remove(symbol);
            }
        }

        if (_symbolsActivated == 3)
        {

            if (_playerIsInElevator)
            {
                player.transform.position = anchor.transform.position;
                _animator.enabled = true;
                if (!_isPlaying)
                {
                    _isPlaying = true;
                    source.Play();
                    StartCoroutine(AudioFade());
                    if (_startFade)
                    {
                        source.volume = Mathf.Lerp(1.0f, 0.0f, Time.deltaTime * 10f);

                    }
                }
            }
            else
            {
                _animator.enabled = false;
                if (_isPlaying)
                {
                    _isPlaying = false;
                    source.Pause();
                }
            }
            elevatorLight.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player = other.gameObject;
            _playerIsInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInElevator = false;
        }
    }

    IEnumerator AudioFade()
    {
        yield return new WaitForSeconds(3);
        _startFade = true;
    }
}
