using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    public GameObject effect;

    void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<MoveController>().Damage(20);
        }
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
