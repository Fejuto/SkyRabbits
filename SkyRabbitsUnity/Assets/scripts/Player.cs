using UnityEngine;
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
		Collider2D playerFront = GetPlayerFront ();

		if (!threeButton () || (Time.time - attackTime > 0.4f && IsAttack ())) {
			attackTime = Time.time;
			print (attackTime);
			if (playerFront != null && playerFront.GetComponent<Player> () != null && Time.time - pushTime > 0.3f) {
				attackTime = Time.time;
				playerFront.GetComponent<Player> ().pushForce.x = 3 * transform.localScale.x;
				playerFront.GetComponent<Player> ().pushTime = Time.time;

				if (!threeButton ()) {
					pushForce.x = -3 * transform.localScale.x;
					pushTime = Time.time;
				}
			}
		}
		
		if (Time.time - pushTime < 0.3f) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (pushForce.x, GetComponent<Rigidbody2D> ().velocity.y + pushForce.y);
		} else {

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

	float attackTime = -999f;
	bool IsAttack(){
		return Input.GetKey(playerControls.actionButton);
	}

	bool threeButton(){
		return true;
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
