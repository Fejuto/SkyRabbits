using UnityEngine;
using System.Collections;

public class PlayerEnable : MonoBehaviour {

	public int playerId = 0;
	public bool playerEnabled = false;
	
	// Use this for initialization
	void Start () {
		TextMesh text = transform.Find ("text").GetComponent<TextMesh>();
		if (playerEnabled) {
			text.text = "P" + playerId;
		} else {
			text.text = "Disabled";
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseUp(){
	

		playerEnabled =! playerEnabled;
		TextMesh text = transform.Find ("text").GetComponent<TextMesh>();
		if (playerEnabled) {
			text.text = "P" + playerId;
		} else {
			text.text = "Disabled";

		}


		Debug.Log("player" + playerId + " " + playerEnabled);
		
	}
}
