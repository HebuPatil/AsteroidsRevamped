using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toruseffect : MonoBehaviour
{
    public GameObject bullet;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 9)
        {
            StartCoroutine(bullet_traildelay());
            transform.position = new Vector3(-9, transform.position.y, 0);
            
        }
        else if (transform.position.x < -9)
        {
            StartCoroutine(bullet_traildelay());
            transform.position = new Vector3(9, transform.position.y, 0);
            
        }

        else if (transform.position.y > 5)
        {
            StartCoroutine(bullet_traildelay());
            transform.position = new Vector3(transform.position.x, -5, 0);
            
        }

        else if (transform.position.y < -5)
        {
            StartCoroutine(bullet_traildelay());
            transform.position = new Vector3(transform.position.x, 5, 0);
            
        }
    }

    IEnumerator bullet_traildelay()
    {
        bullet.GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(0.11f);
        bullet.GetComponent<TrailRenderer>().enabled = true;
    }
}
