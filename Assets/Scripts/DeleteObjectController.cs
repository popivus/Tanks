using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjectController : MonoBehaviour
{
    public float seconds = 0.5f;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
