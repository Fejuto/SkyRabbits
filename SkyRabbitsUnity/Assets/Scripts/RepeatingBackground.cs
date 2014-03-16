using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour {

	// Provide sprites of 480x270. These will be used to fill in the background. 
	public GameObject[] scenes;
	public float width = 4.8f;

	private ArrayList stage = new ArrayList();

	// Use this for initialization
	void Start () {
			//Pick random scene
			GameObject scene =  selectRandomScene();
			//Put it on the stage
			scene = (GameObject)Instantiate(scene);
			scene.transform.parent = this.gameObject.transform;
			stage.Add(scene);
	}



	private GameObject selectRandomScene(){
		return  scenes[Mathf.FloorToInt(Random.value * scenes.Length)];
	}
	
	// Update is called once per frame
	void Update () {

		if (stage.Count == 1 && !testVisible(0)) {
			return;
		}

		if (testVisible (0)) {
			// First element is visible.
			// Insert scene to  the left.
			Debug.Log("insertBefore");
			insertRandomSceneBefore();
		} else if (!testVisible (1)) {
			// First and second element are not visible
			// Remove left most scene.
			removeScene(0);
		}

		if (testVisible (stage.Count -1)) {
			// Last element is visible.
			// Insert scene to  the right.
			//Debug.Log("insertAfter");

			insertRandomSceneAfter();
		} else if (!testVisible (stage.Count -2)) {
			// Last and second last element are not visible
			// Remove right most scene.
			removeScene(stage.Count-1);
		}

		
	}

	private void removeScene(int i){
		if (i < 0 || i >= stage.Count) {
			return;
		}


		GameObject scene = (GameObject)stage [i];
		stage.RemoveAt (i);
		Destroy (scene);
	}


	private bool testVisible(int i){
		//Debug.Log("testVisible " + i + " stageCount " + stage.Count + " visible: " + ((GameObject)stage[i]).renderer.isVisible);

		return i >= 0 && i < stage.Count && ((GameObject)stage[i]).renderer.isVisible;
	}

	private GameObject insertRandomSceneAfter(){

		GameObject scene = (GameObject)stage [stage.Count -1];
		//Pick random scene
		GameObject r =  selectRandomScene();
		
		//Put it on the stage
		r = (GameObject)Instantiate(scene);
		r.transform.parent = this.gameObject.transform;
		r.transform.position = scene.transform.position + Vector3.right * width;
		stage.Add (r);
		return r;
	}

	private GameObject insertRandomSceneBefore(){

		GameObject scene = (GameObject)stage [0];

		//Pick random scene
		GameObject r =  selectRandomScene();
		
		//Put it on the stage
		r = (GameObject)Instantiate(scene);
		r.transform.parent = this.gameObject.transform;
		r.transform.position = scene.transform.position + Vector3.left * width;
		stage.Insert (0,r);

		return r;
	}
}
