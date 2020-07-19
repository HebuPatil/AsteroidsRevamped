using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1.0f);
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * 400);
    }

}
