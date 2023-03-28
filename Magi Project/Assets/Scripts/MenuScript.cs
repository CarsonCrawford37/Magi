using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject MenuObj;
    [SerializeField] GameObject OptionsObj;
    [SerializeField] ToggleRay rightHandRay;

    private void Start()
    {
        rightHandRay.GetComponent<ToggleRay>();
    }
    public void ToogleMenu()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            MenuObj.SetActive(true);
            OptionsObj.SetActive(false);
            rightHandRay.DeactivateRay();
        }
        else
        {
            gameObject.SetActive(true);
            rightHandRay.ActivateRay();
        }
    }
}
