﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce;
	public float walkSpeed;

	public Transform groundedPoint;
	public Transform frontPoint;

	PlayerControls playerControls;
	
	private bool movingLeft;
	private bool movingRight;

	Vector2 pushForce = new Vector2();
	float pushTime;

	// Use this for initialization
	void Start () {
		playerControls = GetComponent<PlayerControls> ();
	}

	// Update is called once per frame
	void Update () {
		Collider2D playerFront = GetPlayerFront ();
		print (playerFront);
		if (playerFront != null && playerFront.GetComponent<Player>() != null && Time.time - pushTime > 0.1f && Time.time - playerFront.GetComponent<Player>().pushTime > 0.1f) {
			playerFront.GetComponent<Player>().pushForce.x = 3 * transform.localScale.x;
			playerFront.GetComponent<Player>().pushTime = Time.time;
			pushForce.x = -3 * transform.localScale.x;
			pushTime = Time.time;
		}
		
		if (Time.time - pushTime < 0.3f) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (pushForce.x, GetComponent<Rigidbody2D> ().velocity.y + pushForce.y);
		} else {
			if (Input.GetKey (playerControls.buttonLeft) && !Input.GetKey (playerControls.buttonRight)) {
				movingLeft = true;
				movingRight = false;
				transform.localScale = new Vector3 (-1, 1, 1);
				//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-100, GetComponent<Rigidbody2D> ().velocity.y);
			} else if (!Input.GetKey (playerControls.buttonLeft) && Input.GetKey (playerControls.buttonRight)) {
				movingLeft = false;
				movingRight = true;
				transform.localScale = new Vector3 (1, 1, 1);
				//GetComponent<Rigidbody2D> ().velocity = new Vector2 (100, GetComponent<Rigidbody2D> ().velocity.y);
			} else if (!Input.GetKey (playerControls.buttonLeft) && !Input.GetKey (playerControls.buttonRight)) {
				movingLeft = false;
				movingRight = false;
			}

			if (Input.GetKey (playerControls.buttonLeft) && Input.GetKey (playerControls.buttonRight) && IsGrounded ()) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpForce);
			}

			if (movingLeft) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else if (movingRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}
	}

	void FixedUpdate(){
	}


	bool IsGrounded(){
		return Physics2D.OverlapPoint (new Vector2 (groundedPoint.position.x, groundedPoint.position.y)) != null && GetComponent<Rigidbody2D>().velocity.y <= 0.01;
	}
	//Time.time
	Collider2D GetPlayerFront(){
		return Physics2D.OverlapPoint (new Vector2 (frontPoint.position.x, frontPoint.position.y), LayerMask.NameToLayer("players"));
	}

	/*public void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
			KeyPressedEventHandler(Event.current.keyCode);
		}
	}
	
	private void KeyPressedEventHandler(KeyCode keyCode) {
		print (keyCode.ToString());
	}*/
}