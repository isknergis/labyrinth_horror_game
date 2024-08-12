using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class Finish : MonoBehaviour
{
    public GameObject finish;

   
    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger.");
            StartCoroutine (FinishGmae());  
        }
    }
    IEnumerator FinishGmae()
    {
        finish.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(4);
    }
}
