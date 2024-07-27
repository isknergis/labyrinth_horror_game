using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    [SerializeField] private KeyManager keyManager;

   private void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    private void Update()
    {
        if (IsPlayerLookingAtKey() && Input.GetKeyDown(KeyCode.E))
        {
            if (keyManager != null)
            {
                keyManager.IncrementKeyCount();
                Destroy(gameObject); // Anahtarý yok et
            }
        }
    }

    private bool IsPlayerLookingAtKey()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
}