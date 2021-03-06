﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
	
	private Rigidbody body;
	private float v = 0.5f, w = 1.0f;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
	}

	public void Propel(int dir)
	{
		if (dir == 0) {
			return;
		}
		
		body.MovePosition(transform.position + transform.forward * v * dir);
	}

	public void MoveTo(Vector3 pos)
	{
		body.MovePosition(pos);
	}

	public void YawTo(Quaternion eq)
	{
		if (body != null) {
			body.MoveRotation(eq);
		}
	}

	public void Yaw(int dir)
	{
		if (body == null) {
			return;
		}

		if (dir == 0) {
			return;
		}

		body.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, dir * w)));
	}

}
