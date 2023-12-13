using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolActivated : MonoBehaviour
{
    public bool isActivated = false;
    float emissiveIntensity = 20f;
    Color emissiveColor = Color.cyan;

    private void Update()
    {
        if (isActivated)
        {
            GetComponent<AudioSource>().enabled = true;
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity); //Change this to modify the emission intensity instead of enabling it -- baked wont show
        }
    }
    /*    private void Update()
        {
            if (isActivated)
            {
                Debug.Log(name + " activated");
            }
        }
    */
}
