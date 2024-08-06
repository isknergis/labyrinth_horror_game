using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Password : MonoBehaviour
{
    public GameObject passwordUI;
    private string enteredPassword;
    public bool isClosing=false;

    private void Start()
    {
        if (passwordUI != null)
        {
            passwordUI.SetActive(false);
        }
        else
        {
            Debug.LogError("passwordUI is not assigned.");
        }
    }

    private void Update()
    {
        
        PasswordManager();

        if(Input.GetKeyDown(KeyCode.C))
        {
            ClosePassword();
        }
        
        
    }

    public void PasswordManager()
    {
        if (Inventory.AllPiecesCollected)
        {
            if ( passwordUI != null && !passwordUI.activeSelf && isClosing==false)
            {
                passwordUI.SetActive(true);
                isClosing = true;  
                
                
               
                
            }

           
        }
    }


    public void ClosePassword()

    {
        if(passwordUI != null && passwordUI.activeSelf && isClosing==true)
        {
            passwordUI.SetActive(false);

        }
    }

    public string GetEnteredPassword()
    {
        return enteredPassword;
    }
}