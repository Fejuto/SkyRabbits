using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	public ScoreManager scoreManager;

	public MenuLine line1;
	public MenuLine line2;
	public MenuLine line3;
	public MenuLine line4;

	// Use this for initialization
	void Start () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		List<Player> players = scoreManager.GetSortedPlayers ();
		int winner = players [0].GetComponent<PlayerDeath> ().deadCount;
		if (players.Count > 0) {
			line1.setPlayer (players [0], winner);
		} else {
			line1.gameObject.SetActive(false);
		}
		if (players.Count > 1) {
			line2.setPlayer (players [1], winner);
		} else {
			line2.gameObject.SetActive(false);
		}
		if (players.Count > 2) {
			line3.setPlayer (players [2], winner);
		} else {
			line3.gameObject.SetActive(false);
		}
		if (players.Count > 3) {
			line4.setPlayer (players [3], winner);
		} else {

			line4.gameObject.SetActive(false);
		}

		if (GameObject.Find ("Background")) {
			GameObject.Find ("Background").SetActive(false);
		}

		if (GameObject.Find ("Main Camera")) {
			GameObject.Find ("Main Camera").GetComponent<MovingCamera>().enabled = false;

			Vector3 v = GameObject.Find ("Main Camera").transform.position;
			v.y = 100;
			GameObject.Find ("Main Camera").transform.position = v;

			transform.position = new Vector3(GameObject.Find ("Main Camera").transform.position.x, GameObject.Find ("Main Camera").transform.position.y - 0.2f, 0);
		}

	}
}
