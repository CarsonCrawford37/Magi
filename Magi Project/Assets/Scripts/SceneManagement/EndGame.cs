using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public FadeToBlack blackScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackScreen.FadeOut();
            StartCoroutine(WaitForNextScene());
        }
    }
    public IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("EndDemo");
    }

}
