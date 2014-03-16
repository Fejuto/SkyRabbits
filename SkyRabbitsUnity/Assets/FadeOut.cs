using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public float lifeTime;
	private float time;

	// Use this for initialization
	void Start () {
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - time > lifeTime) {
			Destroy(gameObject);
		}
	}
}
