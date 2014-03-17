using UnityEngine;
using System.Collections;

public class MenuLine : MonoBehaviour {

	public GameObject menuText;
	public GameObject playerPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPlayer(Player player, int winner)
	{
		player.transform.position = playerPosition.transform.position;
		menuText.GetComponent<TextMesh> ().text = player.GetComponent<PlayerDeath> ().deadCount + "";
		if (player.GetComponent<PlayerDeath> ().deadCount == winner) {
			menuText.GetComponent<TextMesh> ().text += " Winner!";
		}
		player.GetComponent<PlayerDeath> ().disablePlayer ();
		player.GetComponent<PlayerDeath> ().enabled = false;
	}
}
