using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Gate : MonoBehaviour
{
    public string gatePassword;
    public Text enteredPassword;
    public GameObject keypadUI;

    public PlayerMovement playerScript;

    public GameObject dropText;

    [SerializeField] private Animation doorAnimation;
    [SerializeField] private bool canOpen;


    
    private void Start()
    {
        canOpen = false;
        // Animation bile�enini al ve kontrol et
        if (doorAnimation == null)
        {
            doorAnimation = GetComponent<Animation>();
            if (doorAnimation == null)
            {
                Debug.LogError("Door animation is not assigned and could not be found on the GameObject.");
            }
        }

        // Animasyonun ba�lang��ta oynat�lmamas�n� sa�la
        if (doorAnimation != null)
        {
            doorAnimation.playAutomatically = false;
            doorAnimation.Stop();  // Animasyonu ba�lang��ta durdur
        }
    }
    

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.G))
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
            canOpen = true;
            DoorInteraction();
           
           
           
            keypadUI.SetActive(false);
            Debug.Log("kap� a��ld�");
           
            

        }
      

        else
        {
            
            ResetPassword();
            canOpen = false;
        }
    }
      
    



    private void DoorInteraction()
    {



        if (canOpen && doorAnimation != null)
        {
            if (doorAnimation.GetClip("d") != null)
            {
                doorAnimation.Play("d"); // Do�ru animasyon klip ismini kullan�n
                Debug.Log("Animasyon oynat�ld�");
            }
            else
            {
                Debug.LogError("HorrorGate adl� animasyon klibi bulunamad�.");
            }
        }
        else
        {
            Debug.LogError("Kap� a�ma animasyonu bulunamad� veya canOpen false.");
        }




    }
}
