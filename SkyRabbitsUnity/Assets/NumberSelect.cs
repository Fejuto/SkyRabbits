using UnityEngine;
using System.Collections;

public class NumberSelect : MonoBehaviour {

	public int numberOfPlayers = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){

		Debug.Log("selected " + numberOfPlayers);

	}
}
