using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	
	public enum Side{
		Left, Right, Top, Bottom
	};

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

	public Side entranceSide;
	public Side exitSide;

	public Component entrancePoint;
	public Component exitPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
