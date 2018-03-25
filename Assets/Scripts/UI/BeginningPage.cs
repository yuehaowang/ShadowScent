using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginningPage : MonoBehaviour {

	private TouchController touchControl;

	void Start()
	{
		touchControl = new TouchController();
		touchControl.threshold = 250;
	}

	void Update()
	{
		touchControl.Update();

		if (touchControl.directionX == TouchController.Direction.NONE) {
			return;
		}

		if (touchControl.directionX == TouchController.Direction.LEFT){
			LevelManage.currentPlayerId = 0;
		} else {
			LevelManage.currentPlayerId = 1;
		}

		SceneManager.LoadScene(LevelManage.levelList[0]);
	}

}
