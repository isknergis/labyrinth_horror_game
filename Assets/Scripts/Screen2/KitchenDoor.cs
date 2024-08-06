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
    private bool isDoorOpen = false; // Kap�n�n a��k olup olmad���n� kontrol eder

    private void Start()
    {
        if (doorAnimation == null)
        {
            doorAnimation = GetComponent<Animation>();
        }

        if (doorAnimation != null)
        {
            doorAnimation.Stop(); // Ba�lang��ta animasyonu durdurur
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
                isDoorOpen = true; // Kap�n�n a��k oldu�unu belirtir
                Debug.Log("Animation played");
            }
        }
    }
}
    

