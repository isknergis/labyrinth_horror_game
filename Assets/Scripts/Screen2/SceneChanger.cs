using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


    public string sceneToLoad; // Unity Editor'dan ayarlamak i�in

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.tag);

        if (other.CompareTag("Player")) // Tag "Player" olmal�, "Scene2" de�il
        {
            Debug.Log("Loading Scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

