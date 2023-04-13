using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBrazier : MonoBehaviour
{
    [SerializeField] public Light brazierLight;
    [SerializeField] public ParticleSystem fire;

    public bool isLit = false;
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
            brazierLight.intensity = Mathf.Lerp(0f, 3f, Time.deltaTime * lightSpeed);

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
