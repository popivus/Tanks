using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int hp = 100;
    public float bulletForce = 15f;
    public GameObject firstAidKitPrefab, bulletPrefab, player, firePoint;
    private NavMeshAgent agent;
    private bool seenPlayer = false, shooted = false;
    private Rigidbody2D rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    
    void Update()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity) && !seenPlayer)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Заметил");
                seenPlayer = true;
                agent.destination = player.transform.position;
            }
        }
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity) && seenPlayer)
        {
            if (hit.transform.tag == "Player")
            {
                agent.isStopped = true;
                float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
                if (!shooted) Shoot();
            }
            else 
            {
                agent.isStopped = false;
                agent.destination = player.transform.position;
            }
        }

    }

    private void Shoot()
    {
        AudioManager.instanse.PlaySound(0);
        shooted = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.5f);
        shooted = false;
    }

    public void Damage(int points)
    {
        hp -= points;
        if (hp <= 0)
        {
            GameManager.instanse.killsCount++;
            PlayerPrefs.SetInt("Kills Count", PlayerPrefs.GetInt("Kills Count", 0) + 1);
            AudioManager.instanse.PlaySound(2);
            Instantiate(firstAidKitPrefab, transform.position, new Quaternion(0f, 0f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}
