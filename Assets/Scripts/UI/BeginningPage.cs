using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginningPage : MonoBehaviour
{

    private TouchController touchControl;

    void Start()
    {
        touchControl = new TouchController();
        touchControl.threshold = 250;
    }

    void Update()
    {
		if (touchControl == null) {
			return;
		}

        touchControl.Update();

		bool isLoadScene = false;

		if (touchControl.directionX == TouchController.Direction.LEFT || Input.GetKey(KeyCode.A))
		{
			LevelManage.currentPlayerId = 0;

			isLoadScene = true;
		}
		else if (touchControl.directionX == TouchController.Direction.RIGHT || Input.GetKey(KeyCode.D))
		{
			LevelManage.currentPlayerId = 1;

			isLoadScene = true;
		}

		if (isLoadScene) {
			SceneManager.LoadScene(LevelManage.levelList[0]);
		}
    }

}
