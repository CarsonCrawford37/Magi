using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuObj;
    public GameObject OptionsObj;
    public GameObject rightHandRay;

    public AudioSource open;
    public AudioSource close;

/*    private void Start()
    {
        rightHandRay.GetComponent<ToggleRay>();
    }
*/    public void ToogleMenu()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            MenuObj.SetActive(true);
            OptionsObj.SetActive(false);
            rightHandRay.SetActive(false);
            close.Play();
        }
        else
        {
            gameObject.SetActive(true);
            rightHandRay.SetActive(true); 
            open.Play();
        }
    }
}
