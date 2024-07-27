using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private KeyManager keyManager;


    public string sceneToLoad; // Unity Editor'dan ayarlamak için


    private void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyManager.HasRequiredKeys())
            {
                Debug.Log("Loading Scene: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("Not enough keys collected to advance.");
            }
        }
    }
}

