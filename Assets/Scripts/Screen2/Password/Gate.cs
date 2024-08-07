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
        // Animation bileþenini al ve kontrol et
        if (doorAnimation == null)
        {
            doorAnimation = GetComponent<Animation>();
            if (doorAnimation == null)
            {
                Debug.LogError("Door animation is not assigned and could not be found on the GameObject.");
            }
        }

        // Animasyonun baþlangýçta oynatýlmamasýný saðla
        if (doorAnimation != null)
        {
            doorAnimation.playAutomatically = false;
            doorAnimation.Stop();  // Animasyonu baþlangýçta durdur
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
            Debug.Log("kapý açýldý");
           
            

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
                doorAnimation.Play("d"); // Doðru animasyon klip ismini kullanýn
                Debug.Log("Animasyon oynatýldý");
            }
            else
            {
                Debug.LogError("HorrorGate adlý animasyon klibi bulunamadý.");
            }
        }
        else
        {
            Debug.LogError("Kapý açma animasyonu bulunamadý veya canOpen false.");
        }




    }
}
