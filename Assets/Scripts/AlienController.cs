using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{

	public GameObject bullet;

	float fireRate;
	float nextFire;
	public float speed;
	private Transform target;
	public AudioClip crash;
	public AudioClip shoot;
	public GameObject floatingpoints;

	private GameController gameController;

	void Start()
	{

		GameObject gameControllerObject =
			GameObject.FindWithTag("GameController");

		gameController =
			gameControllerObject.GetComponent<GameController>();
		fireRate = 1.5f;
		nextFire = Time.time;
		target = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();

	}

	// Update is called once per frame
	void Update()
	{
		CheckIfTimeToFire();
		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

	void CheckIfTimeToFire()
	{
		if (Time.time > nextFire)
		{
			Instantiate(bullet, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position, 0.7f);
			nextFire = Time.time + fireRate;
		}

	}

	void OnTriggerEnter2D(Collider2D c)
	{

		if (c.gameObject.tag == "Bullet")
		{
			Debug.Log("Destroyed by Alien");
			GameObject points = Instantiate(floatingpoints, gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
			points.transform.GetChild(0).GetComponent<TextMesh>().text = "+500";
			AudioSource.PlayClipAtPoint
				(crash, Camera.main.transform.position);
			transform.position = new Vector3(0, 0, 0);
			GetComponent<Rigidbody2D>().
				velocity = new Vector3(0, 0, 0);
			gameController.IncrementScore(4);
			Destroy(gameObject);
		}
	}
}
