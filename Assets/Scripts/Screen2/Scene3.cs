using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3 : MonoBehaviour
{
    public string nextSceneName = "NewScene"; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Inventory.AllPiecesCollected) 
            {
                if (IsSceneInBuildSettings(nextSceneName))
                {
                    Debug.Log("Loading scene: " + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogError("Scene '" + nextSceneName + "' is not in Build Settings.");
                }
            }
            else
            {
                Debug.Log("Cannot load scene: Not all pieces are collected.");
            }
        }
    }

    bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
