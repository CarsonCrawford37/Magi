using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public FadeToBlack blackOut;

    public void LoadScene()
    {
        SceneManager.LoadScene("Magi");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackOut.FadeOut();
            StartCoroutine(WaitForNextScene());
        }
    }
    public IEnumerator WaitForNextScene()
    {
        Debug.Log("Scene");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("EndDemo");
    }

}
