using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce;
	public float walkSpeed;

	PlayerControls playerControls;
	public Transform groundedPoint;

	private bool movingLeft;
	private bool movingRight;

	public int screenWidth = 480; //pixels
	public int screenHeight = 270; //pixels

	public int margin = 50; //pixels
	public int pixelsPerUnit = 100; 

	public GameObject deadEffect = null;

	// Use this for initialization
	void Start () {
		playerControls = GetComponent<PlayerControls> ();
	}

	// Update is called once per frame
	void Update () {

		checkAlive();

		move();
		
	}

	private void checkAlive(){

		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = transform.position;

		Vector3 delta = playerPosition - cameraPosition;

		if ((Mathf.Abs(delta.x) > (screenWidth * 0.5 + margin)  / pixelsPerUnit ) ||
		    (Mathf.Abs (delta.y) > (screenHeight * 0.5 + margin ) / pixelsPerUnit)) {
			spawnBoneFountain();
			respawn();
		} 



	}

	private void respawn(){
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = transform.position;

		transform.position = new Vector3 (cameraPosition.x,
		                                 cameraPosition.y + (screenHeight * 0.25f + margin * 0.9f) / pixelsPerUnit, 
		                                 playerPosition.z); 

		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	private void spawnBoneFountain(){
		Instantiate (deadEffect, transform.position,deadEffect.transform.rotation);
	}

	private void move(){

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

	bool IsGrounded(){
		return Physics2D.OverlapPoint (new Vector2 (groundedPoint.position.x, groundedPoint.position.y)) != null && GetComponent<Rigidbody2D>().velocity.y <= 0.01;
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
