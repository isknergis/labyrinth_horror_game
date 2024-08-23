using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectable
{


    [SerializeField] private Fire fire;


    private void Start()
    {
        fire = GameObject.FindObjectOfType<Fire>();
    }

    public override void Collect()
    {
        Debug.Log("Collect metodu çalýþtý");
        base.Collect();

        


    }

}
