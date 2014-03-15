using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		
		Transform player0 = transform.Find ("player0");
		bool P0Enabled = player0.GetComponent<PlayerEnable> ().playerEnabled;
		KeyCode P0_L = player0.transform.Find ("controls_left").GetComponent<SetControls> ().key;
		KeyCode P0_R = player0.transform.Find ("controls_right").GetComponent<SetControls> ().key;

		Transform player1 = transform.Find ("player1");
		bool P1Enabled = player1.GetComponent<PlayerEnable> ().playerEnabled;
		KeyCode P1_L = player1.transform.Find ("controls_left").GetComponent<SetControls> ().key;
		KeyCode P1_R = player1.transform.Find ("controls_right").GetComponent<SetControls> ().key;

		Transform player2 = transform.Find ("player2");
		bool P2Enabled = player2.GetComponent<PlayerEnable> ().playerEnabled;
		KeyCode P2_L = player2.transform.Find ("controls_left").GetComponent<SetControls> ().key;
		KeyCode P2_R = player2.transform.Find ("controls_right").GetComponent<SetControls> ().key;

		Debug.Log ("P0 " + P0Enabled + " " + P0_L + " " + P0_R);

	}
}
