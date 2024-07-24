using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    RaycastHit hit;

    bool isReloding;
    public ParticleSystem muzzleFlash;
    public int currentAmmo = 8;
    public int maxAmmo = 12;
    public int carriedAmmo = 60;

    [SerializeField]
    float rateOfFire;
    float nextFire = 0;
    [SerializeField]
    float weaponRange;
    public float damage = 10f;

    public Transform shootPoint;

    EnemyHealth enemy;

    void Start()
    {
        muzzleFlash.Stop();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && currentAmmo <= 0 && !isReloding)
        {
            EmptyFire();
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloding)
        {
            isReloding = true;
            Reload();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateOfFire;
            currentAmmo--;
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

    void Reload()
    {
        if (carriedAmmo <= 0) return;
        StartCoroutine(ReloadCountDown(2f));
    }

    void EmptyFire()
    {
        if (Time.time > nextFire)
        {
            nextFire -= Time.time + rateOfFire;
        }
    }

    IEnumerator pistolEffect()
    {
        yield return new WaitForEndOfFrame();
        muzzleFlash.Stop();
    }

    IEnumerator ReloadCountDown(float timer)
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        if (timer <= 0)
        {
            isReloding = false;
            int needed = maxAmmo - currentAmmo;
            int toDeduct = (carriedAmmo >= needed) ? needed : carriedAmmo;
            carriedAmmo -= toDeduct;
            currentAmmo += toDeduct;
        }
    }
}
