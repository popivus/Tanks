using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitController : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<MoveController>().hp < 100) 
            {
                collider.gameObject.GetComponent<MoveController>().hp += 15;
                if (collider.gameObject.GetComponent<MoveController>().hp > 100) collider.gameObject.GetComponent<MoveController>().hp = 100;
                Destroy(gameObject);
            }
        }
    }
}
