using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public static float distancefromTarget;
    public float toTarget;


  //  private void Awake()
   // {
     //   DontDestroyOnLoad(gameObject);
   // }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distancefromTarget = toTarget;
        }
    }
}
