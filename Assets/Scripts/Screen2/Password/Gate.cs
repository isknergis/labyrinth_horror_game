using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gate : MonoBehaviour
{
    public string gatePassword;
    public Text enteredPassword;
    public GameObject keypadUI;

    public PlayerMovement playerScript;

    public GameObject dropText;

    
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.G))
        {
           
            playerScript.enabled = true;
         
            keypadUI.SetActive(false);
        
           
        }
    }


    public void OnTriggerEnter(Collider other)
    {
            if(other.tag== "Player")
        {
            keypadUI.SetActive(true);
            playerScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dropText.SetActive(true);
        }
    }

    public void Key(string key)
    {
        enteredPassword.text = enteredPassword.text + key;
    }
    public void ResetPassword()

    {
        enteredPassword.text = "";
    }

    public void CheckPassword()
    {
        if(enteredPassword.text== gatePassword )
        {
            Debug.Log("kapý açýldý");
            keypadUI.SetActive(false);
        }
        else
        {
            ResetPassword();    
        }
    }
}
