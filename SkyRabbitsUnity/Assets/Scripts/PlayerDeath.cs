using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {
	
	public int screenWidth = 480; //pixels
	public int screenHeight = 270; //pixels
	
	public int margin = 50; //pixels
	public int pixelsPerUnit = 100; 
	
	public GameObject deadEffect = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		checkAlive();

	}

	private void checkAlive(){
		
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = transform.position;
		
		Vector3 delta = playerPosition - cameraPosition;

		float horizontalLimit = (screenHeight * 0.5f + margin ) / pixelsPerUnit;
		float verticalLimit = (screenWidth * 0.5f + margin)  / pixelsPerUnit;


		if (Mathf.Abs(delta.x) >  verticalLimit  ||
		    Mathf.Abs(delta.y) > horizontalLimit) {
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
		GameObject effect = (GameObject) Instantiate (deadEffect, transform.position,deadEffect.transform.rotation);
		effect.transform.parent = Camera.main.transform;

		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 target = new Vector3 (cameraPosition.x, cameraPosition.y, transform.position.z);

		effect.transform.LookAt(target);


	}
}
