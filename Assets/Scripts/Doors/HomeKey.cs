using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeKey : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private Animation doorAnimation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform door;

    public float activationDistance = 3.0f;

    [SerializeField] private bool canOpen;

    private float keyDistance;
    private float doorDistance;

    private void Start()
    {
        canOpen = false;
    }

    private void UpdatePosition()
    {
        keyDistance = Vector3.Distance(player.position, key.transform.position);
        doorDistance = Vector3.Distance(player.position, door.transform.position);
    }

    private void Update()
    {
        UpdatePosition();

        if (keyDistance <= activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (key != null && !canOpen)
                {
                    key.SetActive(false);
                    canOpen = true;
                    Debug.Log("Key deactivated and canOpen set to true");
                }
            }
        }

        DoorInteraction();
    }

    private void DoorInteraction()
    {
        UpdatePosition();
        Debug.Log("update position");
        if (canOpen==true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Z pressed");
             
                if (doorDistance <= activationDistance)
                {
                  

                    if (doorAnimation != null)
                    {
                        //UpdatePosition();
                       // Debug.Log("update position");
                        doorAnimation.Play();
                        Debug.Log("Animation played");
                        canOpen = false;
                    }
                    
                }
                
            }
        }
    }
}

