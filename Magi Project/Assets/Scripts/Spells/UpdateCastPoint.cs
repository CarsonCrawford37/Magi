using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class UpdateCastPoint : MonoBehaviour
{
    public GameObject spellManager;

    public Transform newSpellCastPoint;
    public Transform defaultSpellCastPoint;

    public bool isHolding = false;

    private void Update()
    {
        if (isHolding == true)
        {
            spellManager.GetComponent<MoveCastPoint>().ChangeCastPoint(newSpellCastPoint);
        }
        else if(isHolding == false)
        {
            spellManager.GetComponent<MoveCastPoint>().ChangeCastPoint(defaultSpellCastPoint);
        }

    }
    public void IsHeld()
    {
        isHolding = true;
    }

    public void IsDropped()
    {
        isHolding = false;
    }

}
