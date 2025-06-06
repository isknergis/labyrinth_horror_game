﻿//Script written by Matthew Rukas - Volumetric Games || volumetricgames@gmail.com || www.volumetric-games.com

using UnityEngine;
using System.Collections;


public class KeyPadRay : MonoBehaviour
{
    [SerializeField] private GameObject crosshairN; 
    [SerializeField] private GameObject crosshairH; 

 
    [SerializeField] private LayerMask layerMaskInteract;

    private int rayLength = 10;
    private GameObject rayCastedObj; 

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        {
            if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
            {
                if (hit.collider.CompareTag("KeyPad")) 
                {
                    rayCastedObj = hit.collider.gameObject;
           
                    crosshairN.SetActive(false);
                    crosshairH.SetActive(true);

                    if (Input.GetKeyDown("e"))
                    {
                        rayCastedObj.GetComponent<CanvasInteract>().CanvasOn();
                        DisablePlayer();
                    }
                }
            }
            else
            {
                crosshairN.SetActive(true);
                crosshairH.SetActive(false);
            }
        }
    }

    public void DisableUI()
    {
        rayCastedObj.GetComponent<CanvasInteract>().CanvasOff();
    }

    void DisablePlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   
}
