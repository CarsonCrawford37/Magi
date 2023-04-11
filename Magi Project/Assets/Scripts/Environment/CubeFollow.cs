using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeFollow : MonoBehaviour
{
    public GameObject Cube;

    private bool isFollowing = false;
    private bool letGo = false;
    private bool isLocked = false;

    private Vector3 defaultLocation;

    [SerializeField] float xPos;
    [SerializeField] float yPos;
    [SerializeField] float zPos;
    [SerializeField] string slotTag;

    private void Awake()
    {
        defaultLocation = transform.localPosition;
    }

    void Start()
    {
        transform.localPosition = defaultLocation;
    }

    void Update()
    {
        if (isFollowing)
        {
            Cube.transform.position = new Vector3(transform.localPosition.x + xPos, transform.localPosition.y + yPos, transform.localPosition.z + zPos);
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
        letGo = false;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        letGo = true;
        transform.localPosition = defaultLocation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(slotTag) && letGo)
        {
            if (!isLocked)
            {
                Cube.transform.position = other.transform.position;
                other.GetComponent<MeshRenderer>().enabled = false;
                DisableGrabbable();
            }
            else
            {
                isLocked = true;
            }
        }
    }

    private void DisableGrabbable()
    {
        XRGrabInteractable[] cubeChildren = Cube.GetComponentsInChildren<XRGrabInteractable>();
        foreach(XRGrabInteractable cubeChild in cubeChildren)
        {
            cubeChild.gameObject.SetActive(false);
        }
    }
}
