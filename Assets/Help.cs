using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour 
{
	Move playerMove = new Move();
	Direction currDir;
	List<Direction> legalDir;
	Vector2 playerPosition;

	// Use this for initialization
	void Start()
    {
		playerPosition = transform.position;
		CheckLegalDirections();
	}

	void FixedUpdate()
    {
		playerPosition = transform.position;
		CheckLegalDirections();
		
		foreach(Direction d in legalDir)
		{
			CheckDistance(playerPosition, Vector2.up);
		}
	}

	protected void CheckLegalDirections()
    {
		currDir = playerMove.GetDirection();
		foreach(Direction dir in (Direction[]) Enum.GetValues(typeof(Direction)))
        {
            if (dir == currDir)
            {
				//dont add
            }
            else
            {
				legalDir.Add(dir);
            }
        }
    }

	protected float CheckDistance(Vector2 playerPosition, Vector2 directionVector)
    {
		RaycastHit2D hit = Physics2D.Raycast(playerPosition, directionVector);

		//if the cast hits sth
		if (hit.collider != null)
		{
			float distance = Vector2.Distance(hit.point, playerPosition);
			return distance;
		}
		return 0f;
		//Vector2 playerPosition = transform.position;
		//playerPosition.y += 1;
		//cast a ray right
		
	}
	protected Direction PickRandomDirection()
	{
		currDir = playerMove.GetDirection();

		float distUp = CheckDistanceUp();
		float distDown = CheckDistanceDown();
		float distRight = CheckDistanceRight();
		float distLeft = CheckDistanceLeft();

		return Direction.DOWN;
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
