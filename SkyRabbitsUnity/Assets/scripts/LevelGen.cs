using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGen : MonoBehaviour {
	// The levels being added in Unity to the LevelGenerator
	public List<Level> levels;

	// Use this for initialization
	void Start () {
		// Generate the level
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateMap(){
		// Starting position
		Vector3 lastPosition = new Vector3(1.2f, 0);
		Level.Side nextEntranceSide = Level.Side.Left;

		// Generate 10 levels from left to right
		for (int i = 0; i < 10; i++)
		{
			Level level = FindLevelWithEntrance(nextEntranceSide);
			Level newLevel = (Level) Instantiate(level,
			                                     new Vector3(lastPosition.x - level.entrancePoint.transform.position.x,
			                                                 lastPosition.y - level.entrancePoint.transform.position.y),
			                                     Quaternion.identity);
			// Update for next levelblock
			lastPosition = newLevel.exitPoint.transform.position;
			nextEntranceSide = Level.InverseSide(level.exitSide);
		}

	}

	private Level FindLevelWithEntrance(Level.Side entranceSide)
	{
		Level matchingLevel = null;

		while(matchingLevel == null){
			Random.seed += 1;
			Level level = levels[Random.Range(0, levels.Count)];

			if(level.entranceSide == entranceSide){
				matchingLevel = level;
			}
		}

		return matchingLevel;
	}
}
