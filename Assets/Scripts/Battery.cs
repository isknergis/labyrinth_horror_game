using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery :Collectable
{
    [SerializeField]  private FlashlightController flashLight;

    private void Start()
    {
        flashLight=GameObject.FindObjectOfType<FlashlightController>(); 
    }

    public override void Collect()
    {
        base.Collect();

    }

}
