using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurnableScript : MonoBehaviour
{
    private Material material;
    public bool isBurning = false;
    private float burnTimer = 0f;
    public float burnDuration = 6f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning)
        {

            if (burnTimer < burnDuration)
            {
                material.SetFloat("_Burn_Amount", Mathf.Lerp(0f, 1f, burnTimer / burnDuration));
                burnTimer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                material.SetFloat("_Burn_Amount", 0);
                Destroy(gameObject);
            }
        }
    }
}
