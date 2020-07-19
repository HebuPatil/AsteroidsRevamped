using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{

    public AudioClip destroy;
    public GameObject medAsteroid;
    public GameObject smallAsteroid;

    public GameObject largeexplosion;
    public GameObject medexplosion;
    public GameObject smallexplosion;
    public GameObject floatingpoints;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);

        InvokeRepeating("SpawnTrailPart", 0, 0.05f);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        
        if ((c.gameObject.tag.Equals("Bullet")))
        {
            Destroy(c.gameObject);

            if (tag.Equals("Large Asteroid"))
            {

                GameObject points = Instantiate(floatingpoints, gameObject.transform.position , Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+50";

                gameController.IncrementScore(1);
                Instantiate(medAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));
                Instantiate(medAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));

                GameObject splosion = Instantiate(medexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();
                gameController.SplitAsteroid();

            } else if (tag.Equals("Medium Asteroid")){

                GameObject points = Instantiate(floatingpoints, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+75";

                gameController.IncrementScore(2);
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));
                gameController.SplitAsteroid();
                GameObject splosion = Instantiate(medexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();

            }
            else
            {
                GameObject points = Instantiate(floatingpoints, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+100";
                gameController.DecrementAsteroids();
                gameController.IncrementScore(3);
                GameObject splosion = Instantiate(smallexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();


            }

            AudioSource.PlayClipAtPoint(
                destroy, Camera.main.transform.position);
            Destroy(gameObject);

        }
    }

    
    
    void OnTriggerEnter2D(Collider2D c)
    {

        if ((c.gameObject.tag.Equals("Ship")))
        {
            if (tag.Equals("Large Asteroid"))
            {
                GameObject points = Instantiate(floatingpoints, gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+50";
                gameController.IncrementScore(1);
                Instantiate(medAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));
                Instantiate(medAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));

                GameObject splosion = Instantiate(largeexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();
                gameController.SplitAsteroid(); // +2

            }
            else if (tag.Equals("Medium Asteroid"))
            {
                GameObject points = Instantiate(floatingpoints, gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+75";
                gameController.IncrementScore(2);
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));
                GameObject splosion = Instantiate(medexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();


            }
            else
            {
                GameObject points = Instantiate(floatingpoints, gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                points.transform.GetChild(0).GetComponent<TextMesh>().text = "+100";
                gameController.DecrementAsteroids();
                gameController.IncrementScore(3);
                GameObject splosion = Instantiate(smallexplosion, gameObject.transform.position, gameObject.transform.rotation);
                splosion.GetComponent<ParticleSystem>().Play();

            }
            AudioSource.PlayClipAtPoint(
                destroy, Camera.main.transform.position);
            Destroy(gameObject);

        }
    
    }

    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale;
        trailPart.transform.rotation = transform.rotation;


        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, 0.2f);
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.1f;
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }
}