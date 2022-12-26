using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject effect;
    
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(35);
        }
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
