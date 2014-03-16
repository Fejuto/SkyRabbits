using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	public enum Side
	{
		Left, Right, Top, Bottom
	};
	
	public Side entranceSide;
	public Side exitSide;
	
	public Component entrancePoint;
	public Component exitPoint;

	private int currentCamera = -1;
	public Component[] cameraPoints;

	public enum Turn
	{
		Straight,
		Left,
		Right
	}

	public Component GetNextCameraPoint()
	{


		if(++currentCamera == cameraPoints.Length)
		{
			// First time return the exitPoint as last camera point
			return exitPoint;
		}else if(currentCamera > cameraPoints.Length){
			// Next time return null so next level can be inited
			return null;
		}

		return cameraPoints[currentCamera];
	}

	//Inverses the input (for mapping exit to next entrance side)
	public static Side InverseSide(Side exitside)
	{
		switch (exitside)
		{
		case Side.Left:
			return Side.Right;
		case Side.Right:
			return Side.Left;
		case Side.Top:
			return Side.Bottom;
		case Side.Bottom:
			return Side.Top;
		}

		throw new UnityException("Cannot find inverse side! :D");
	}

	public Turn GetTurn()
	{
		if(exitSide == InverseSide(entranceSide)){
			return Turn.Straight;
		}else if (TurnLeft (entranceSide) == exitSide){
			return Turn.Left;
		}else if (TurnRight (entranceSide) == exitSide){
			return Turn.Right;
		}

		// This is actually a U-turn
		return Turn.Straight;
	}

	public static Side TurnLeft(Side inputSide)
	{
		switch (inputSide){
		case Side.Bottom:
			return Side.Left;
		case Side.Left:
			return Side.Top;
		case Side.Top:
			return Side.Right;
		case Side.Right:
			return Side.Bottom;
		}

		return Side.Bottom;
	}

	public static Side TurnRight(Side inputSide)
	{
		switch (inputSide){
		case Side.Bottom:
			return Side.Right;
		case Side.Left:
			return Side.Bottom;
		case Side.Top:
			return Side.Left;
		case Side.Right:
			return Side.Top;
		}
		
		return Side.Bottom;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
