using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHolsters : MonoBehaviour
{
    private bool isInHolser = false;
    private bool isSelected = false;

    private Collider objCol;

    private void Start()
    {
        objCol = GetComponent<BoxCollider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Holster"))
        {
            //Debug.Log("Enter Holster");
            isInHolser=true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Holster"))
        {
            if (isInHolser && isSelected == false)
            {
                //Debug.Log("Trigger Disabled");
                //objCol.isTrigger = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Holster"))
        {
            //Debug.Log("Exit Holster");
            isInHolser = false;

        }
    }

    public void ObjectSelected()
    {
        Debug.Log("Object Selected");
        isSelected = true;
    }

    public void ObjectDeselected()
    {
        Debug.Log("Object Deselected");
        isSelected = false;
    }
}
