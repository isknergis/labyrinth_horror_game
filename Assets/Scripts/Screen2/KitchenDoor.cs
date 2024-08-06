using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoor : MonoBehaviour
{
    [SerializeField] private Animation doorAnimation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform door;

    public float activationDistance = 3.0f;

    private float doorDistance;
    private bool isDoorOpen = false; // Kapýnýn açýk olup olmadýðýný kontrol eder

    private void Start()
    {
        if (doorAnimation == null)
        {
            doorAnimation = GetComponent<Animation>();
        }

        if (doorAnimation != null)
        {
            doorAnimation.Stop(); // Baþlangýçta animasyonu durdurur
        }
        else
        {
            Debug.LogError("Animation component is missing!");
        }
    }

    private void Update()
    {
        UpdatePosition();
        DoorInteraction();
    }

    private void UpdatePosition()
    {
        doorDistance = Vector3.Distance(player.position, door.position);
    }

    private void DoorInteraction()
    {
        if (doorDistance <= activationDistance && !isDoorOpen)
        {
            if (doorAnimation != null && !doorAnimation.isPlaying)
            {
                doorAnimation.Play();
                isDoorOpen = true; // Kapýnýn açýk olduðunu belirtir
                Debug.Log("Animation played");
            }
        }
    }
}
    

