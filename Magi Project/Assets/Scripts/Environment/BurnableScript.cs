using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurnableScript : MonoBehaviour
{
    private Material material;
    public AudioSource source;
    public Light burnLight;
    private Collider col;

    public bool isBurning = false;
    private float burnTimer = 0f;
    public float burnDuration = 6f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
        source.GetComponent<AudioSource>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning)
        {
            burnLight.gameObject.SetActive(true);
            col.enabled = false;
            source.Play();
            material.SetFloat("_Outline", 0);
            if (burnTimer < burnDuration)
            {
                burnLight.intensity = Mathf.Lerp(burnLight.intensity, 0f, burnTimer / burnDuration);
                material.SetFloat("_Burn_Amount", Mathf.Lerp(0f, 1f, burnTimer / burnDuration));
                burnTimer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                material.SetFloat("_Burn_Amount", 0);
                material.SetFloat("_Outline", 1);
                Destroy(gameObject);
            }
        }
    }
}
