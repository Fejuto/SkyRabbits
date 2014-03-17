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
		
	}
	
	// Update is called once per frame
	void Update () {
		List<Player> players = scoreManager.GetSortedPlayers ();
		if (players.Count > 0) {
			line1.setPlayer (players [0]);
		} else {
			line1.enabled = false;
		}
		if (players.Count > 1) {
			line2.setPlayer (players [1]);
		} else {
			line2.enabled = false;
		}
		if (players.Count > 2) {
			line3.setPlayer (players [2]);
		} else {
			line3.enabled = false;
		}
		if (players.Count > 3) {
			line4.setPlayer (players [3]);
		} else {
			line4.enabled = false;
		}
	}
}
