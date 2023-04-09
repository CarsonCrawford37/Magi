using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class bodySocket
{
    public GameObject gameObject;
    [Range(0.01f, 1f)]
    public float heightRatio;
}

public class HolsterMove : MonoBehaviour
{
    public GameObject playerCamera;
    public bodySocket[] bodySockets;

    private Vector3 camPos;
    private Quaternion camRot;
    void Update()
    {
        camPos = playerCamera.transform.position;
        camRot = playerCamera.transform.rotation;
        foreach(var bodySocket in bodySockets)
        {
            UpdateBodySocketHeight(bodySocket);
        }
        UpdateSocketInventory();
    }

    private void UpdateBodySocketHeight(bodySocket bodySocket)
    {
        bodySocket.gameObject.transform.position = new Vector3(bodySocket.gameObject.transform.position.x, camPos.y - bodySocket.heightRatio, bodySocket.gameObject.transform.position.z);
    }

    private void UpdateSocketInventory()
    {
        transform.position = new Vector3(camPos.x, 0, camPos.z);
        transform.rotation = new Quaternion(transform.rotation.x, camRot.y, transform.rotation.z, camRot.w);
    }
}
