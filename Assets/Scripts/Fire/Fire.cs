using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [SerializeField] private Text currentAmmoUI;

    RaycastHit hit;

    public ParticleSystem muzzleFlash;
    public int currentAmmo = 8;
    public float rateOfFire;
    public float weaponRange;
    public float damage = 10f;
    public float pickupRange = 4f;

    public Transform shootPoint;

    float nextFire = 0;

    void Start()
    {
        currentAmmoUI.text = currentAmmo.ToString();
        muzzleFlash.Stop();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryPickupAmmo();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateOfFire;
            currentAmmo--;
            currentAmmoUI.text = currentAmmo.ToString();
            muzzleFlash.Play();
            StartCoroutine(pistolEffect());
            ShootRay();
        }
    }

    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.ReduceHealth(damage);
                }
                else
                {
                    Debug.LogError("EnemyHealth component not found on hit object.");
                }
            }
            else
            {
                Debug.Log("Hit something else.");
            }
        }
    }

    void TryPickupAmmo()
    {
        Collider[] hitColliders = Physics.OverlapSphere(shootPoint.position, pickupRange);
        foreach (var collider in hitColliders)
        {
            Ammo ammo = collider.GetComponent<Ammo>();
            if (ammo != null)
            {
                ammo.Collect();
                IncreaseAmmoCount();
                break; 
            }
        }
    }

    void EmptyFire()
    {
        if (Time.time > nextFire)
        {
            nextFire -= Time.time + rateOfFire;
            Debug.Log("No ammo left!");
        }
    }

    IEnumerator pistolEffect()
    {
        yield return new WaitForEndOfFrame();
        muzzleFlash.Stop();
    }

    public void IncreaseAmmoCount()
    {
        currentAmmo++;
        Debug.Log("Ammo eklendi");
        currentAmmoUI.text = currentAmmo.ToString();
    }
}
