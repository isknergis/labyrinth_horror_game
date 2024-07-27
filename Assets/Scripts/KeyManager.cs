using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{


    [SerializeField] int keysRequiredToAdvance; 
    private int currentKeyCount = 0;

    public void IncrementKeyCount()
    {
        
        currentKeyCount++;
        Debug.Log("Current Key Count: " + currentKeyCount);
    }

    public bool HasRequiredKeys()
    {
        return currentKeyCount >= keysRequiredToAdvance;
    }

}
