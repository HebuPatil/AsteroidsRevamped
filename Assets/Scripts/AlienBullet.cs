using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
	// Start is called before the first frame update
	float moveSpeed = 6f;

	Rigidbody2D rb;

	ShipController target;
	Vector2 moveDirection;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<ShipController>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		Destroy(gameObject, 1.5f);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Ship"))
		{
			Destroy(gameObject);
		}
	}
}
