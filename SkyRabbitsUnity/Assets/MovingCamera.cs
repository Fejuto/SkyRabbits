using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {

	public Component targetPoint;
	public float moveSpeed;

	public LevelGen levelGenerator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		return;
		// First time (might move to Start() )
		if(targetPoint == null)
		{
			targetPoint = levelGenerator.GetCurrentLevel().exitPoint;
		}

		Vector3 moveDirection = targetPoint.transform.position - transform.position;
		moveDirection.z = 0;
		moveDirection.Normalize();

		Vector3 target = moveDirection * moveSpeed + transform.position;

		transform.position = Vector3.Lerp( transform.position, target, Time.deltaTime );

		// Change target if we are too close
		if(Vector3.Distance(targetPoint.transform.position, transform.position) < 10.01f)
		{
			NextLevel ();
		}
	}

	void NextLevel()
	{
		// Change the level
		levelGenerator.NextLevel();

		// Change the camera target
		targetPoint = levelGenerator.GetCurrentLevel().exitPoint;
	}
}
