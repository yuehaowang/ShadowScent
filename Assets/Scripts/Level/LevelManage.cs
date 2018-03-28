using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManage : MonoBehaviour {

	public static int currentPlayerId = 1;
	public static string[] levelList = new string[] {
		"Scene1"
	};
	public GameObject curtainPrefab;

    void Start ()
	{

		if (currentPlayerId == 0) {
			Instantiate<GameObject>(curtainPrefab);
		}
	}

	void Update ()
	{

	}
}
