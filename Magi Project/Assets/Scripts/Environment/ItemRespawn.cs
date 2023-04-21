using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> objs;

    private void Awake()
    {
        foreach (GameObject obj in objs)
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
