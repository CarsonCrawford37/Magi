using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject MenuObj;
    [SerializeField] GameObject OptionsObj;
   public void ToogleMenu()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            MenuObj.SetActive(true);
            OptionsObj.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
