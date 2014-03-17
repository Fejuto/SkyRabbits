using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

	public List<Player> players;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		players = new List<Player>(GameObject.FindObjectsOfType<Player> ());
	}

	public List<Player> GetSortedPlayers(){
		List<Player> playersClone = new List<Player> (players);
		//List<Player> returnList = new List<Players> (ok
		playersClone.Sort (
			delegate (Player p1, Player p2) {
				return p1.GetComponent<PlayerDeath>().deadCount.CompareTo(p2.GetComponent<PlayerDeath>().deadCount);
			}
		);

		return playersClone;
	}
}
