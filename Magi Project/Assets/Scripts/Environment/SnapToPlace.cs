using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPlace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object Entered");
        if(other.tag == tag)
        {
            Debug.Log("Puzzle Piece Entered");
            other.gameObject.transform.position = transform.position;
        }
    }
}
