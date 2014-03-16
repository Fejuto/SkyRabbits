using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {
	
	public int screenWidth = 480; //pixels
	public int screenHeight = 270; //pixels
	
	public int margin = 50; //pixels
	public int pixelsPerUnit = 100; 
	
	public GameObject deadEffect = null;

	public AudioClip deathSound;

	private float timeOfDeath = 0.0f;
	public float deathTime = 3.0f; // seconds.

	private int deadCount = 0;

	// Use this for initialization
	void Start () {
	
	}


	private Rigidbody2D rigidBody;

	public GameObject deathCount;

	// Update is called once per frame
	void Update () {
		
		if(!checkAlive()){

			if(timeOfDeath == 0.0){
				timeOfDeath = Time.time;

				deadCount++;

				spawnBoneFountain();
				audio.PlayOneShot(deathSound);





				gameObject.rigidbody2D.isKinematic = true;
				gameObject.collider2D.enabled = false;
				gameObject.renderer.sortingLayerID = 2;
				GetComponent<Player>().enabled = false;

				Vector3 cameraPosition = Camera.main.transform.position;
				Vector3 playerPosition = transform.position;

				transform.position = new Vector3(cameraPosition.x, cameraPosition.y, playerPosition.z);
				transform.localScale = Vector3.one * 5.0f;

				deathCount.renderer.enabled = true;
				deathCount.GetComponent<TextMesh>().text = "" + deadCount;
			}

		}

		if (timeOfDeath != 0.0) {

			float timeDeath = Time.time - timeOfDeath; 

			if (timeDeath > deathTime) {
					timeOfDeath = 0.0f;

					gameObject.rigidbody2D.isKinematic = false;
					gameObject.collider2D.enabled = true;
					gameObject.renderer.sortingLayerID = 4;
					GetComponent<Player> ().enabled = true;
					gameObject.rigidbody2D.WakeUp ();
					transform.localScale = Vector3.one;

					gameObject.renderer.material.color = new Color(1,1,1,1);
					deathCount.renderer.material.color = new Color(1,1,1,1);

					deathCount.renderer.enabled = false;



					respawn ();
			} else {

				float alpha = Mathf.Lerp(1,0,timeDeath/deathTime);

				gameObject.renderer.material.color = new Color(1,1,1,alpha);
				deathCount.renderer.material.color = new Color(1,1,1,alpha);

			}
		}
	}

	private bool checkAlive(){
		
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = transform.position;
		
		Vector3 delta = playerPosition - cameraPosition;

		float horizontalLimit = (screenHeight * 0.5f + margin ) / pixelsPerUnit;
		float verticalLimit = (screenWidth * 0.5f + margin)  / pixelsPerUnit;


		if (Mathf.Abs(delta.x) >  verticalLimit  ||
		    Mathf.Abs(delta.y) > horizontalLimit) {
			return false;
		} 

		return true;
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
		GameObject effect = (GameObject) Instantiate (deadEffect, transform.position,deadEffect.transform.rotation);
		effect.transform.parent = Camera.main.transform;

		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 target = new Vector3 (cameraPosition.x, cameraPosition.y, transform.position.z);

		effect.transform.LookAt(target);


	}
}
