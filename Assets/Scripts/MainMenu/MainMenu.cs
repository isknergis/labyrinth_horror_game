using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject fadeOut;

    public void StartButton()

    {
        StartCoroutine(NewGmae());
    }


    IEnumerator NewGmae()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
