using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    public List<SymbolActivated> symbols;
    public Light elevatorLight;

    private bool playerIsInElevator = false;
    private int symbolsActivated = 0;
    private Animator animator;

    //private Vector3 elevatorPos;// = new Vector3 (1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (SymbolActivated symbol in symbols)
        {
            if (symbol.isActivated)
            {
                symbolsActivated++;
                symbols.Remove(symbol);
            }
        }

        if(symbolsActivated == 3)
        {
            elevatorLight.enabled = true;
        }

        if (/*symbolsActivated == 3 &&*/ playerIsInElevator)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInElevator = false;
        }
    }
}
