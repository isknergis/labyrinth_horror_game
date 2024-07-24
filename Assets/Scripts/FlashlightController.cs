using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private AudioClip AClip;
    private AudioSource ASource;

    private float maxIntensity=4;
    private float currentIntensity;
    [SerializeField] private Light spotLight;

    [SerializeField] private float batteryHealth=5;
    [SerializeField] private int batteryCount=2;

    private bool isOpen=true;

    private bool canOpen=true;



    void Start()
    {
        ASource = GetComponent<AudioSource>();
        currentIntensity=spotLight.intensity;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (canOpen)
            {
                ToggleLight();
            }

           
        }
        if(isOpen)
        {
            batteryHealth-=Time.deltaTime;

        }

        if(batteryHealth <= 0)
        {
            if(batteryCount > 0)
            {
                batteryHealth = 20;
                batteryCount--;
                return;
            }


            if(isOpen)
            {
                ToggleLight();
            }
            batteryHealth=0;
            canOpen = false;
        }
        
    }

    private void ToggleLight()
    {
        isOpen = !isOpen;
        ASource.PlayOneShot(AClip);
        currentIntensity = isOpen ? maxIntensity : 0;
        spotLight.DOIntensity(currentIntensity, 2);
    }
    public void IncreaseBatteryCount()
    {
        batteryCount++;
        canOpen = true;
    }
}
