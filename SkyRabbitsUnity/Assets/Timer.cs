using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public TextMesh time;

	public GameObject menuPrefab;

	public float totalTime = 10f;
	public float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float timeLeft = totalTime - (Time.time - startTime);
		time.text = "Time:"+Mathf.Ceil(timeLeft) + "";

		if (timeLeft < 0) {
			GameObject menu = Instantiate(menuPrefab) as GameObject;
			menu.transform.parent = GameObject.Find("Main Camera").transform;
			gameObject.SetActive(false);
		}
	}

}
