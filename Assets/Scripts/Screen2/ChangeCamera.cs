using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject secondaryCamera;
    public float delayTime = 15f;
    [SerializeField] private Animation blackScreen;
    public float blackScreenDelay = 5f;

    void Start()
    {
        if (blackScreen == null)
        {
            blackScreen = GetComponent<Animation>();
            if (blackScreen== null)
            {
                Debug.LogError("Door animation is not assigned and could not be found on the GameObject.");
            }
        }
        StartCoroutine(SwitchCameraAndShowBlackScreen());
    }

    IEnumerator SwitchCameraAndShowBlackScreen()
    {
        
        yield return new WaitForSeconds(delayTime);
        secondaryCamera.SetActive(true);
        gameObject.SetActive(false);

        
        yield return new WaitForSeconds(blackScreenDelay);
        blackScreen.Play();
      
    }

} 


