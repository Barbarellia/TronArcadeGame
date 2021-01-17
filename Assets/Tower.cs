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

	protected List<Direction> legalDir1;
	protected List<Direction> legalDir2;

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
	void FixedUpdate () {

		if (gameObject != null)
		{
			// Do something  
			Destroy(gameObject);
		}

		currentDirection1 = move1.GetDirection();
		currentDirection2 = move2.GetDirection();

		currDist1 = help1.GetDistances();
		currDist2 = help2.GetDistances();

		legalDir1 = help1.GetLegalDirections();
		legalDir2 = help2.GetLegalDirections();

		Direction best = help1.GetBestDirection();

        if (currDist1[0] < 20 || currDist1[1] < 20)
        {
            //if (best != currentDirection1)
            //    move1.SetSuggestion(best);
            Direction newDir = PickRandomDirection();
            move1.SetSuggestion(best);
        }



		//Direction newDir = PickRandomDirection();
		//move1.SetSuggestion(best);
		//float maxDist = PickBestDirection();
	}


	protected Direction PickRandomDirection()
    {
		//Direction d1;
		//int randomint = rnd.Next(0, 2);
		////int r = rnd.Next(legalDir1.Count - 1);
		////sprawdz ktory kierunek lepszy
		//if (randomint == 0)
		//{
		//	d1 = legalDir1[0];
		//}
		//else d1 = legalDir1[1];

        return legalDir1[1];
    }

    //  protected float PickBestDirection()
    //  {
    //currDist1.Sort();
    //int n = currDist1.Count;

    //return currDist1[n-1];
    //  }

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
