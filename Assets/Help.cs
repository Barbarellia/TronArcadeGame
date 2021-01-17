using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour 
{
	Move playerMove = new Move();
	protected List<float> distances;
	protected List<Direction> legalDir;
	protected Direction currDir;
	protected Vector2 playerPosition;

	protected Direction bestDir;

	protected Vector2 DirToVector(Direction dir)
	{
		Vector2 v;
		switch (dir)
		{
			case Direction.UP:
				return v = Vector2.up;
			case Direction.DOWN:
				return v = -Vector2.up;
			case Direction.LEFT:
				return v = -Vector2.right;
			case Direction.RIGHT:
				return v = Vector2.right;
			default:
				return v = Vector2.up;
		}

	}

	// Use this for initialization
	void Start()
    {
		playerPosition = transform.position;
		currDir = playerMove.GetDirection();

		legalDir = CheckLegalDirections(currDir);
	}

	void FixedUpdate()
    {
		playerPosition = transform.position;
		currDir = playerMove.GetDirection();

		legalDir = CheckLegalDirections(currDir);
		distances = CheckDistances();
	}

	protected List<Direction> CheckLegalDirections(Direction currDir)
    {
		List<Direction> legalDirections = new List<Direction>();
		legalDirections.Clear();
		foreach(Direction dir in (Direction[]) Enum.GetValues(typeof(Direction)))
        {
            if (dir != currDir && DirToVector(dir) != -DirToVector(currDir))
            {
				legalDirections.Add(dir);
            }
        }

		return legalDirections;
    }

	public List<float> GetDistances()
    {
		return this.distances;
    }

	public List<Direction> GetLegalDirections()
    {
		return this.legalDir;
    }

	public Direction GetBestDirection()
    {
		return this.bestDir;
    }

	protected List<float> CheckDistances()
    {
		List<float> distancesInAllDirections = new List<float>();
		

		if (currDir.Equals(Direction.UP) || currDir.Equals(Direction.DOWN))
        {
			float distRight = CheckDistanceRight();
			float distLeft = CheckDistanceLeft();
			if (distRight > distLeft)
				bestDir = Direction.RIGHT;
			else bestDir = Direction.LEFT;

			distancesInAllDirections.Add(distLeft);
			distancesInAllDirections.Add(distRight);
			
		}
		else if(currDir.Equals(Direction.RIGHT) || currDir.Equals(Direction.LEFT))
        {
			float distUp = CheckDistanceUp();
			float distDown = CheckDistanceDown();
			if (distUp > distDown)
				bestDir = Direction.UP;
			else bestDir = Direction.DOWN;

			distancesInAllDirections.Add(distUp);
			distancesInAllDirections.Add(distDown);
		}
				
		return distancesInAllDirections;
	}


	protected float CheckDistanceUp()
	{
		Vector2 playerPosition = transform.position;
		playerPosition.y += 1;
		//cast a ray right
		RaycastHit2D hit = Physics2D.Raycast(playerPosition, Vector2.up);

		//if the cast hits sth
		if (hit.collider != null)
		{
			float distance = Vector2.Distance(hit.point, playerPosition);
			return distance;
		}
		return 0f;
	}

	protected float CheckDistanceDown()
	{
		Vector2 playerPosition = transform.position;
		playerPosition.y -= 1;
		//cast a ray right
		RaycastHit2D hit = Physics2D.Raycast(playerPosition, -Vector2.up);

		//if the cast hits sth
		if (hit.collider != null)
		{
			float distance = Vector2.Distance(hit.point, playerPosition);
			return distance;
		}
		return 0f;
	}

	protected float CheckDistanceRight()
	{
		Vector2 playerPosition = transform.position;
		playerPosition.x += 1;
		//cast a ray right
		RaycastHit2D hit = Physics2D.Raycast(playerPosition, Vector2.right);

		//if the cast hits sth
		if (hit.collider != null)
		{
			float distance = Vector2.Distance(hit.point, playerPosition);
			return distance;
		}
		return 0f;
	}

	protected float CheckDistanceLeft()
	{
		Vector2 playerPosition = transform.position;
		playerPosition.y -= 1;
		//cast a ray right
		RaycastHit2D hit = Physics2D.Raycast(playerPosition, -Vector2.right);

		//if the cast hits sth
		if (hit.collider != null)
		{
			float distance = Vector2.Distance(hit.point, playerPosition);
			return distance;
		}
		return 0f;
	}
}

//protected List<float> CalculateDistances(List<Direction> legalDirections)
//{
//	List<float> distances = new List<float>();

//	foreach (Direction d in legalDirections)
//	{
//		Vector2 dir = DirToVector(d);
//		float dist = CheckDistance(playerPosition, dir);
//		distances.Add(dist);
//	}
//	return distances;
//}

//protected float CheckDistance(Vector2 playerPosition, Vector2 directionVector)
//   {
//	RaycastHit2D hit = Physics2D.Raycast(playerPosition, directionVector);
//	float distance=0f;
//	//if the cast hits sth
//	if (hit.collider != null)
//	{
//           if (Vector2.Equals(directionVector, Vector2.up) == true)
//           {
//			Vector2 v1 = playerPosition;
//			v1.y += 1;
//			distance = Vector2.Distance(hit.point, v1);
//			//return distance;
//		}
//           else if (Vector2.Equals(directionVector, Vector2.down)==true)
//		{
//			Vector2 v2 = playerPosition;
//			v2.y -= 1;
//			distance = Vector2.Distance(hit.point, v2);
//			//return distance;
//		}
//		else if (Vector2.Equals(directionVector, Vector2.left)==true)
//		{
//			Vector2 v3 = playerPosition;
//			v3.x -= 1;
//			distance = Vector2.Distance(hit.point, v3);
//			//return distance;
//		}
//		else if (Vector2.Equals(directionVector, Vector2.right)==true)
//		{
//			Vector2 v4 = playerPosition;
//			v4.x += 1;
//			distance = Vector2.Distance(hit.point, v4);
//			//return distance;
//		}						
//	}
//	return distance;		
//}	
