using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	void Start ()
	{
		if (LevelManage.currentPlayerId == 1) {
			GetComponent<AudioSource>().enabled = false;
		}
	}
}
