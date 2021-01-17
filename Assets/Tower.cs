using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	protected Move move1;
	protected Move move2;
	protected Help help1;
	protected Help help2;


	protected List<float> currDist1;
	protected List<float> currDist2;

	protected Direction currentDirection1;
	protected Direction currentDirection2;

	private GameObject tower;

	protected static System.Random rnd;

	// Use this for initialization
	void Start () {
		move1 = GameObject.Find("player_cyan").GetComponent<Move>();
		move2 = GameObject.Find("player_pink").GetComponent<Move>();
		currentDirection1 = move1.GetDirection();
		currentDirection2 = move2.GetDirection();

		help1 = GameObject.Find("player_cyan").GetComponent<Help>();
		help2 = GameObject.Find("player_pink").GetComponent<Help>();

		
	}

	// Update is called once per frame
	void Update () {
		currentDirection1 = move1.GetDirection();
		currentDirection2 = move2.GetDirection();

		currDist1 = help1.GetDistances();
		currDist2 = help2.GetDistances();
	}

	//protected Direction PickRandomDirection()
	//{
	//	int r = rnd.Next(distances.Count);
	//	return (Direction)distances[r];
	//}

	//protected void RemoveWorstDirection()
	//{
	//	distances.Sort();
	//	distances.RemoveAt(0);
	//}

	//public void SetNewDirection(Direction newDirection)
 //   {
	//	move.SetSuggestion(newDirection);
 //   }

	//protected void CheckIfDistanceTooSmall()
	//{
	//	foreach (float d in currDist1)
	//	{
	//		if (d < 10)
	//		{
	//			//pick best direction
	//		}
	//	}
	//}
}
