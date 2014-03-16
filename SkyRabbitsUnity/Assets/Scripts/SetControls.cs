using UnityEngine;
using System.Collections;

public class SetControls : MonoBehaviour {

	public KeyCode key;

	// Use this for initialization
	void Start () {
		TextMesh text = transform.Find ("text").GetComponent<TextMesh>();
		text.text = key.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
