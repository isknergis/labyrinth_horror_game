using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


using UnityEngine.UI;






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


    public Text flashLightText;
    public Text batteryCountText;


    public static FlashlightController instance;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesneyi yok etme
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ASource = GetComponent<AudioSource>();
        currentIntensity=spotLight.intensity;
        UpdateFlashLightUI();
        UpdateBatteryCountUI();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (canOpen)
            {
                ToggleLight();
            }
            UpdateFlashLightUI();


        }
        if(isOpen)
        {
            batteryHealth-=Time.deltaTime;
            UpdateFlashLightUI();

        }

        if(batteryHealth <= 0)
        {
            if(batteryCount > 0)
            {
                batteryHealth = 5;
                batteryCount--;
                UpdateBatteryCountUI();
                return;
            }
           

            if (isOpen)
            {
                ToggleLight();

            }
            batteryHealth=0;
            UpdateBatteryCountUI();
            canOpen = false;
           
        }
        UpdateFlashLightUI();
    }

    public void UpdateFlashLightUI()
    {
        flashLightText.text = batteryHealth.ToString();
    }


    public void UpdateBatteryCountUI()
    {
    batteryCountText.text = batteryCount.ToString();    
    }

    private void ToggleLight()
    {
        isOpen = !isOpen;
        ASource.PlayOneShot(AClip);
        currentIntensity = isOpen ? maxIntensity : 0;
        spotLight.DOIntensity(currentIntensity, 2);
        UpdateFlashLightUI();
    }
    public void IncreaseBatteryCount()
    {
        batteryCount++;
        UpdateBatteryCountUI();
        canOpen = true;
    }
}
