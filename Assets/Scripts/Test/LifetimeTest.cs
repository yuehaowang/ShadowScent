using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeTest : MonoBehaviour {
	
	public bool debugStart = false;
	public bool debugUpdate = false;
	public bool debugOnDestroy = false;

	void Start ()
	{
		if (debugStart) {
			Debug.Log(gameObject.name + ": start");
		}
	}

	void Update ()
	{
		if (debugUpdate) {
			Debug.Log(gameObject.name + ": update");
		}
	}

	void OnDestroy ()
	{
		if (debugOnDestroy) {
			Debug.Log(gameObject.name + ": ondestroy");
		}
	}

}
