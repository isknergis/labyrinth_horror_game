using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


    public string sceneToLoad; // Unity Editor'dan ayarlamak için

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.tag);

        if (other.CompareTag("Player")) // Tag "Player" olmalý, "Scene2" deðil
        {
            Debug.Log("Loading Scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

