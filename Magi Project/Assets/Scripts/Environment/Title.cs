using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private Animator anim;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartEndScreen());
    }

    IEnumerator StartEndScreen()
    {
        yield return new WaitForSeconds(5);
        //anim.Play("TitleEnterScreen");
        //anim.enabled = true;
        ps.Play();
    }
}
