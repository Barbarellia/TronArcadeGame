using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct DistanceDirectionPair
{
	public Direction Dir { get; set; }
	public float Distance { get; set; }
}

public class Help : MonoBehaviour
{
	private Move playerMove;

	private Direction? suggestedDirection = null;

	public static Vector2 DirToVector(Direction dir)
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
		playerMove = GetComponent<Move>();
		InvokeRepeating("Decide", 1, 0.1f);
	}

	void Decide()
	{
		var playerPosition = transform.position;

		var legalDir = GetLegalDirections();
		//Debug.Log(string.Join(", ", legalDir.Select(d => d.ToString())));
		var pairs = CheckDistances(legalDir);

		ApplySuggestionIfNeeded(pairs);

		float max = pairs.Max(pair => pair.Distance);
		Direction dir = pairs.Find(d => d.Distance == max).Dir;
		playerMove.SetDir(dir);
		//Debug.Log(max + " " +dir);
	}

	private void ApplySuggestionIfNeeded(List<DistanceDirectionPair> pairs)
	{
		if (!suggestedDirection.HasValue)
		{
			return;
		}

		var suggestedPair = pairs.Find(d => d.Dir == suggestedDirection.Value);
		if (suggestedDirection != null)
		{
			suggestedPair.Distance *= 1.5f;
		}
		else
		{
			// to nie powinno mieć miejsca, wieża nie może zaproponować niedozwolonego kierunku
		}

		suggestedDirection = null;
	}

	public void Suggest(Direction suggestedDirection)
	{
		this.suggestedDirection = suggestedDirection;
	}

	public List<Direction> GetLegalDirections()
	{
		var currDir = playerMove.GetDirection();
		List<Direction> legalDirections = new List<Direction>();
		foreach (Direction dir in (Direction[])System.Enum.GetValues(typeof(Direction)))
		{
			if (DirToVector(dir) != -DirToVector(currDir))
			{
				legalDirections.Add(dir);
			}
		}

		return legalDirections;
	}

	protected List<DistanceDirectionPair> CheckDistances(List<Direction> legalDirs)
	{
		List<DistanceDirectionPair> pairs = new List<DistanceDirectionPair>();

		foreach (Direction d in legalDirs)
		{
			pairs.Add(new DistanceDirectionPair
			{
				Dir = d,
				Distance = GetDistance(transform.position, d) * UnityEngine.Random.Range(0.6f, 1.6f),
			});
		}
		return pairs;
	}

	public static float GetDistance(Vector3 origin, Direction dir)
	{
		var dirVec = DirToVector(dir);
		var dirVecPlus90 = new Vector2(-dirVec.y, dirVec.x);
		RaycastHit2D hit1 = Physics2D.Raycast(origin + (Vector3)dirVec + (Vector3)dirVecPlus90, dirVec);
		RaycastHit2D hit2 = Physics2D.Raycast(origin + (Vector3)dirVec - (Vector3)dirVecPlus90, dirVec);

		if (hit1.collider != null && hit2.collider == null)
		{
			Debug.DrawLine(origin, hit1.point);
			return hit1.distance;
		}
		else if (hit1.collider == null && hit2.collider != null)
		{
			Debug.DrawLine(origin, hit2.point);
			return hit2.distance;
		}
		else if (hit1.collider != null && hit2.collider != null)
		{
			RaycastHit2D hit;
			if (hit1.distance < hit2.distance)
			{
				hit = hit1;
			}
			else
			{
				hit = hit2;
			}

			Debug.DrawLine(origin, hit.point);
			return hit.distance;
		}
		return Mathf.NegativeInfinity;
	}
}
