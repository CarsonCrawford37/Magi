using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FreezableScript : MonoBehaviour
{
    private Material material;
    //public AudioSource source;
    //private Collider col;
    private CubeFollow[] handles;

    public bool isFrozen = false;
    private bool isThawing = false;
    private float freezeTimer = 0f;
    public float freezeDuration = 6f;

    bool trigger = false;
    void Start()
    {
        handles = GetComponentsInChildren<CubeFollow>();
        material = GetComponent<MeshRenderer>().sharedMaterial;
        //source.GetComponent<AudioSource>();
        //col = GetComponent<Collider>();
    }

    void Update()
    {
        if (isFrozen)
        {
            foreach (CubeFollow handle in handles)
            {
                handle.enabled = true;
            }
            //col.enabled = false;
            //source.Play();
            material.SetFloat("_Outline", 0);
            if (isThawing)
            {
                if (freezeTimer < freezeDuration)
                {
                    material.SetFloat("_Freeze_Amount", Mathf.Lerp(1f, 0f, freezeTimer / freezeDuration));
                    freezeTimer += Time.deltaTime;
                }
                else
                {
                    foreach (CubeFollow handle in handles)
                    {
                        handle.enabled = false;
                    }

                    material.SetFloat("_Freeze_Amount", 0);
                    material.SetFloat("_Outline", 1);
                    isFrozen = false;
                    isThawing = false;
                    freezeTimer = 0f;
                }
            }
            else
            {
                if (freezeTimer < freezeDuration)
                {
                    material.SetFloat("_Freeze_Amount", Mathf.Lerp(0f, 1f, freezeTimer / freezeDuration));
                    freezeTimer += Time.deltaTime;
                    trigger = true;
                }
                else if (trigger)
                {
                    StartCoroutine(Thaw());
                }

            }
        }
    }

    IEnumerator Thaw()
    {
        yield return new WaitForSeconds(5);
        freezeTimer = 0f;
        isThawing = true;
        trigger = false;
    }
}
