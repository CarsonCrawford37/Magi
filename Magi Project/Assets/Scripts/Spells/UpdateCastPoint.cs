using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class UpdateCastPoint : MonoBehaviour
{
    public GameObject spellManager;
    public Material wand;

    public Transform newSpellCastPoint;
    public Transform defaultSpellCastPoint;

    public bool isHolding = false;

    private void Start()
    {
        //wand = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        if (isHolding == true)
        {
            spellManager.GetComponent<MoveCastPoint>().ChangeCastPoint(newSpellCastPoint);
        }
        else if (isHolding == false)
        {
            spellManager.GetComponent<MoveCastPoint>().ChangeCastPoint(defaultSpellCastPoint);
        }

    }
    public void IsHeld()
    {
        isHolding = true;
        wand.SetFloat("_Outline", 0.0f);
    }

    public void IsDropped()
    {
        isHolding = false;
        wand.SetFloat("_Outline", 1.0f);
    }

}
