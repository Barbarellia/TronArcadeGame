﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	UP = 0,
	DOWN = 1,
	LEFT = 2,
	RIGHT = 3
}

public class Move : MonoBehaviour
{

	//Movement keys (customizable in Inspector)
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode rightKey;
	public KeyCode leftKey;

	public float speed = 16;

	public GameObject wallPrefab;
	public SpriteRenderer sprite;

	protected Collider2D wall;
	protected Vector2 lastWallEnd;
	protected Vector2 boundSize;
	protected Direction currDirection;
	protected Direction lastSuggestion;

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

	void spawnWall()
	{
		lastWallEnd = transform.position;
		GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
		wall = g.GetComponent<Collider2D>();
	}

	void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
	{
		co.transform.position = a + (b - a) * 0.5f;

		float dist = Vector2.Distance(a, b);
		if (a.x != b.x)
			co.transform.localScale = new Vector2(dist + 1, 1);
		else
			co.transform.localScale = new Vector2(1, dist + 1);
	}

	public void SetDir(Direction dir)
	{
		GetComponent<Rigidbody2D>().velocity = DirToVector(dir) * speed;
		spawnWall();
		currDirection = dir;
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		if (co != wall)
		{
			Debug.Log(co);
			print("Player lost: " + name);
			Destroy(gameObject);
		}
	}

	void Start()
	{
		MoveUp();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(upKey) && GetComponent<Rigidbody2D>().velocity != -Vector2.up * speed)
		{
			MoveUp();
		}
		else if (Input.GetKeyDown(downKey) && GetComponent<Rigidbody2D>().velocity != Vector2.up * speed)
		{
			MoveDown();
		}
		else if (Input.GetKeyDown(rightKey) && GetComponent<Rigidbody2D>().velocity != -Vector2.right * speed)
		{
			MoveRight();
		}
		else if (Input.GetKeyDown(leftKey) && GetComponent<Rigidbody2D>().velocity != Vector2.right * speed)
		{
			MoveLeft();
		}

		fitColliderBetween(wall, lastWallEnd, transform.position);
	}

	protected void MoveUp()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
		spawnWall();
		currDirection = Direction.UP;
	}

	protected void MoveDown()
	{
		GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
		spawnWall();
		currDirection = Direction.DOWN;
	}

	protected void MoveRight()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
		spawnWall();
		currDirection = Direction.RIGHT;
	}

	protected void MoveLeft()
	{
		GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
		spawnWall();
		currDirection = Direction.LEFT;
	}

	public Direction GetDirection()
	{
		return this.currDirection;
	}

	//public Transform GetPosition()
	//{
	//	return transform;
	//}
}