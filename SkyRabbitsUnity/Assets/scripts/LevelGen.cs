using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGen : MonoBehaviour {
	// The levels being added in Unity to the LevelGenerator
	public List<Level> levels;

	// Hold all the levels that are currently active
	// 0 = 2 previous (can be visible)
	// 1 = previous levelblock (can be visible)
	// 2 = current active
	// 3 = next(can be visible)
	// 4 = already generated (should not be visible)
	public Level[] instantiatedLevels = new Level[5];

	// Use this for initialization
	void Start ()
	{
		// Generate the level
		//GenerateMap ();
		GenerateFirstLevelBlocks();

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public Level GetCurrentLevel()
	{
		return instantiatedLevels[2];
	}

	void GenerateFirstLevelBlocks()
	{
		// Create level 4
		Level levelPrototype = FindLevelWithEntrance(Level.Side.Left, Level.Side.Left);
		Level newLevel = (Level) Instantiate(levelPrototype,
		                                     new Vector3(1.2f - levelPrototype.entrancePoint.transform.position.x,
		            									 0f - levelPrototype.entrancePoint.transform.position.y),
		                                     Quaternion.identity);
		instantiatedLevels[4] = newLevel;

		// Call next level twice ;-)
		NextLevel ();
		NextLevel ();
	}

//	void GenerateMap()
//	{
//		// Starting position
//		Vector3 lastPosition = new Vector3(1.2f, 0);
//		Level.Side nextEntranceSide = Level.Side.Left;
//
//		// Generate 10 levels from left to right
//		for (int i = 0; i < 10; i++)
//		{
//			Level level = FindNextLevelBlock(nextEntranceSide, null);
//			Level newLevel = (Level) Instantiate(level,
//			                                     new Vector3(lastPosition.x - level.entrancePoint.transform.position.x,
//			                                                 lastPosition.y - level.entrancePoint.transform.position.y),
//			                                     Quaternion.identity);
//			// Update for next levelblock
//			lastPosition = newLevel.exitPoint.transform.position;
//			nextEntranceSide = Level.InverseSide(level.exitSide);
//		}
//	}

	public void NextLevel()
	{
		// Destroy last level
		if(instantiatedLevels[0] != null){
			Destroy(instantiatedLevels[0]);
		}
		
		// Move previous one position
		instantiatedLevels[0] = instantiatedLevels[1];

		// Move current to previous
		instantiatedLevels[1] = instantiatedLevels[2];
		
		// Set the active level
		instantiatedLevels[2] = instantiatedLevels[3];

		// Set the next level
		instantiatedLevels[3] = instantiatedLevels[4];

		// Generate a new level
		Level levelPrototype = FindNextLevelBlock(instantiatedLevels[3]);
		Level newLevel = (Level) Instantiate(levelPrototype,
		                                     new Vector3(instantiatedLevels[3].exitPoint.transform.position.x - levelPrototype.entrancePoint.transform.position.x,
		            instantiatedLevels[3].exitPoint.transform.position.y - levelPrototype.entrancePoint.transform.position.y),
		                                     Quaternion.identity);
		instantiatedLevels[4] = newLevel;
	}

	private Level FindNextLevelBlock(Level previousLevel)
	{
		// EntrancePoint
		Level.Side entranceSide = Level.InverseSide(previousLevel.exitSide);

		// Ok this is a hack but it works :P
		Level.Side forbiddenExitSide = entranceSide;

		// We cannot make a turn if we already made a same turn in the previous levelblock
		if(previousLevel.GetTurn() == Level.Turn.Left)
		{
			forbiddenExitSide = Level.TurnLeft(entranceSide);
		}else if (previousLevel.GetTurn() == Level.Turn.Right){
			forbiddenExitSide = Level.TurnRight(entranceSide);
		}

		return FindLevelWithEntrance(Level.InverseSide(previousLevel.exitSide), forbiddenExitSide);
	}

	private Level FindLevelWithEntrance(Level.Side entranceSide, Level.Side forbiddenExitSide)
	{
		Level matchingLevel = null;

		// Loop as long as we cannot find a matching level :D
		while(matchingLevel == null)
		{
			Random.seed += 1;
			Level level = levels[Random.Range(0, levels.Count)];
			
			if(level.entranceSide == entranceSide && !(level.exitSide == forbiddenExitSide)){
				matchingLevel = level;
			}
		}

		return matchingLevel;
	}
}
