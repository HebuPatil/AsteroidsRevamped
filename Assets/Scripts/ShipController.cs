using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipController : MonoBehaviour
{

    float rotationSpeed = 200.0f;
    float thrustForce = 3f;

    public AudioClip crash;
    public AudioClip shoot;
    public GameObject bullet;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();

        
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));


    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            ShootBullet();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        
        if (c.gameObject.tag != "Bullet")
        {
            if (c.gameObject.tag == "Alien Bullet")
            {
                Destroy(c.gameObject);
            }
            AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);
            transform.position = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();
        }
    }

    void ShootBullet()
    {
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position, 0.5f);
        
    }

    
}