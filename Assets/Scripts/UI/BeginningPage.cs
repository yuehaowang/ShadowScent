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
        touchControl.Update();

        if (touchControl.directionX == TouchController.Direction.NONE && !Input.anyKey)
        {
            return;
        }
        if (touchControl.directionX == TouchController.Direction.LEFT || Input.GetKey(KeyCode.A))
        {
            LevelManage.currentPlayerId = 0;
        }
        else if (touchControl.directionX == TouchController.Direction.RIGHT || Input.GetKey(KeyCode.D))
        {
            LevelManage.currentPlayerId = 1;
        }

        SceneManager.LoadScene(LevelManage.levelList[0]);
    }

}
