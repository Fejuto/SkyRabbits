using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce;
	public float walkSpeed;

	public Transform groundedPoint;
	public Transform groundedPoint2;
	public Transform groundedPoint3;
	public Transform topPoint;
	public Transform topPoint2;
	public Transform topPoint3;
	public Transform frontPoint;

	PlayerControls playerControls;
	
	private bool movingLeft;
	private bool movingRight;

	Vector2 pushForce = new Vector2();
	float pushTime;

	public AudioClip jumpSound;
	public AudioClip bumpSound;

	private bool jumped = false;
	
	// Use this for initialization
	void Start () {
		playerControls = GetComponent<PlayerControls> ();
		print ("!!");
	}

	// Update is called once per frame
	void Update () {

		print (Input.GetKey (playerControls.buttonLeft));
		print ("!!");
		attack();
		
		if (Time.time - pushTime < 0.3f) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (pushForce.x, GetComponent<Rigidbody2D> ().velocity.y + pushForce.y);
		} else {
			move ();
		}

		GetComponent<Animator> ().SetBool ("isGrounded", IsGrounded ());
		GetComponent<Animator> ().SetBool ("isStunned", isStunned());
		GetComponent<Animator> ().SetBool ("isWalking", Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.x) > 0.01f);
		GetComponent<Animator> ().SetFloat ("horizontalSpeed", GetComponent<Rigidbody2D> ().velocity.y);
		GetComponent<Animator> ().SetBool ("isPressed", IsPressed());
	}

	public void attack(){
		Collider2D playerFront = GetPlayerFront ();
		if (!threeButton () || (Time.time - attackTime > 0.4f && IsAttack ())) {
				attackTime = Time.time;
				if (playerFront != null && playerFront.GetComponent<Player> () != null && Time.time - pushTime > 0.3f) {
						attackTime = Time.time;
						playerFront.GetComponent<Player> ().pushForce.x = 3 * transform.localScale.x;
						playerFront.GetComponent<Player> ().pushTime = Time.time;
	
						if (!threeButton ()) {
								pushForce.x = -3 * transform.localScale.x;
								pushTime = Time.time;
						}
	
						audio.PlayOneShot (bumpSound);
				}
		}
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
			jumped = true;
		} else {
			if(jumped) audio.PlayOneShot(jumpSound);
			jumped = false;
		}



		if (movingLeft) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else if (movingRight) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}

	}

	void FixedUpdate(){
	}


	bool IsGrounded(){
		return Physics2D.OverlapPoint (new Vector2 (groundedPoint.position.x, groundedPoint.position.y)) != null ||
						Physics2D.OverlapPoint (new Vector2 (groundedPoint2.position.x, groundedPoint2.position.y)) != null ||
						Physics2D.OverlapPoint (new Vector2 (groundedPoint3.position.x, groundedPoint3.position.y)) != null;
	}

	bool IsPressed(){
		return Physics2D.OverlapPoint (new Vector2 (topPoint.position.x, topPoint.position.y)) != null ||
			Physics2D.OverlapPoint (new Vector2 (topPoint2.position.x, topPoint2.position.y)) != null ||
				Physics2D.OverlapPoint (new Vector2 (topPoint3.position.x, topPoint3.position.y)) != null;
	}
	
	bool isStunned(){
		return Time.time - pushTime < 0.3f;
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
		return false;
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
