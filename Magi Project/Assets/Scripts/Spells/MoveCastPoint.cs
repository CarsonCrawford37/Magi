using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCastPoint : MonoBehaviour
{
    //[SerializeField] private Transform defaultSpellPoint;
    [SerializeField] private Transform spellCastPoint;

    void Update()
    {
       /* spellCastPoint.position = defaultSpellPoint.position;
        spellCastPoint.rotation = defaultSpellPoint.rotation;*/
    }

    public void ChangeCastPoint(Transform newSpellCastPoint)
    {
/*        Debug.Log("ChangeCastPoint being called");
*/        spellCastPoint.position = newSpellCastPoint.position;
        spellCastPoint.rotation = newSpellCastPoint.rotation;

    }
}
