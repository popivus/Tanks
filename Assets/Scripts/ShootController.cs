using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int ammoCount = 8;
    
    private int maxAmmoCount;

    public float bulletForce = 20f;
    private bool shooted = false;

    void Start()
    {
        maxAmmoCount = ammoCount;
    }

    void Update()
    {
        UIController.instanse.SetAmmoText($"{ammoCount.ToString()}/{maxAmmoCount.ToString()}");
        if (Input.GetButtonDown("Fire1") && !shooted && !UIController.instanse.paused)
        {
            Shoot();
        }
        
        if (Input.GetKeyUp(KeyCode.R) && !shooted && ammoCount < maxAmmoCount)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        if (ammoCount > 0)
        {
            shooted = true;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            ammoCount--;
            AudioManager.instanse.PlaySound(0);
            StartCoroutine(ShootDelay());
        }
        else 
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.25f);
        shooted = false;
    }

    private IEnumerator Reload()
    {
        AudioManager.instanse.PlaySound(1);
        shooted = true;
        UIController.instanse.ReloadUI(true);
        yield return new WaitForSeconds(2f);
        ammoCount = maxAmmoCount;
        UIController.instanse.ReloadUI(false);
        shooted = false;
    }
}
