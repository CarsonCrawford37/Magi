using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBrazier : MonoBehaviour
{
    [SerializeField] public Light brazierLight;
    [SerializeField] public ParticleSystem fire;

    public bool isLit = false;
    private bool _isPlaying = false;
    public float lightSpeed = 50f;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isLit)
        {
            if (_isPlaying == false)
            {
                brazierLight.gameObject.SetActive(true);
                fire.gameObject.SetActive(true);
                fire.Play();
                source.Play();
                _isPlaying = true;
            }
            brazierLight.intensity = Mathf.Lerp(1f, 6f, Time.deltaTime * lightSpeed);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fireball"))
        {
            brazierLight.gameObject.SetActive(true);
            fire.gameObject.SetActive(true);
            fire.Play();
            isLit = true;
            source.Play();
        }
    }
}
