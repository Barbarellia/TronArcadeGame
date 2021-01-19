using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
	private Help help1;
	private Help help2;

	void Start()
	{
		help1 = GameObject.Find("player_cyan").GetComponent<Help>();
		help2 = GameObject.Find("player_pink").GetComponent<Help>();

		InvokeRepeating("Decide", 0.95f, 0.5f);
	}

	public void Decide()
	{
		if (help1 == null || help2 == null)
		{
			CancelInvoke("Decide");
			return;
		}

		var distancePairs1 = GetDistancePairs(help1);
		var distancePairs2 = GetDistancePairs(help2);

		// sumujemy wszystkie odległości, a nie tylko te w danym kierunku
		var sum1 = distancePairs1.Sum(pair => pair.Distance);
		var sum2 = distancePairs2.Sum(pair => pair.Distance);
		if (sum1 > sum2)
		{
			Debug.Log("help player " + help2.name);
			Direction maxDirection = GetMaxDirection(distancePairs2);
			help2.Suggest(maxDirection);
		}
		else
		{
			Debug.Log("help player " + help1.name);
			Direction maxDirection = GetMaxDirection(distancePairs1);
			help1.Suggest(maxDirection);
		}
	}

	private Direction GetMaxDirection(List<DistanceDirectionPair> pairs)
	{
		float max = pairs.Max(d => d.Distance);
		return pairs.Find(d => d.Distance == max).Dir;
	}

	private List<DistanceDirectionPair> GetDistancePairs(Help move)
	{
		var legalDirections = move.GetLegalDirections();

		List<DistanceDirectionPair> pairs = new List<DistanceDirectionPair>();

		foreach (Direction d in legalDirections)
		{
			pairs.Add(new DistanceDirectionPair
			{
				Dir = d,
				Distance = GetFancyDistance(move.transform.position, d),
			});
		}
		return pairs;
	}

	private float GetFancyDistance(Vector3 origin, Direction d)
	{
		var hit = GetHit(origin, d);
		Direction right = RightOf(d);
		Direction left = LeftOf(d);
		Vector2 newOrigin = hit.point - Help.DirToVector(d);
		return hit.distance + GetDistance(newOrigin, right) + GetDistance(newOrigin, left);
	}

	private RaycastHit2D GetHit(Vector3 origin, Direction dir)
	{
		var dirVec = Help.DirToVector(dir);
		RaycastHit2D hit = Physics2D.Raycast(origin + (Vector3)dirVec, dirVec);

		if (hit.collider != null)
		{
			Debug.DrawLine(origin + (Vector3)dirVec, hit.point, Color.yellow, 0.1f);
			return hit;
		}
		// nie powinno się zdarzyć, bo jesteśmy wewnątrz kwadratu i teoretycznie zawsze raycast w coś trafi
		return default(RaycastHit2D);
	}

	private float GetDistance(Vector3 origin, Direction dir)
	{
		return GetHit(origin, dir).distance;
	}

	private Direction LeftOf(Direction d)
	{
		switch (d)
		{
			case Direction.UP:
				return Direction.LEFT;
			case Direction.DOWN:
				return Direction.RIGHT;
			case Direction.LEFT:
				return Direction.DOWN;
			case Direction.RIGHT:
				return Direction.UP;
		}
		throw new ArgumentOutOfRangeException();
	}

	private Direction RightOf(Direction d)
	{
		// hmm
		return LeftOf(LeftOf(LeftOf(d)));
	}
}
