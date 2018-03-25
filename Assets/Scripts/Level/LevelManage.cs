using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManage : MonoBehaviour {

	public static int currentPlayerId = 0;
	public static string[] levelList = new string[] {
		"Scene1"
	};

    // Use this for initialization
    void Start () {
        // Debugging, the local player is down one
        // there should be a GUI to set up the room and choose the identity
//        string localPlayerIdentity = GameData.Static.PlayerIdentity.PLAYER_DOWN;
//        GameData.Init.gameInitScene init_data = new GameData.Init.gameInitScene(localPlayerIdentity);
//        localPlayerController = new GameController.localPlayer(init_data.localPlayerInfo);
//        otherPlayerController = new GameController.otherPlayer(init_data.otherPlayerInfo);
	}
	
	// Update is called once per frame
	void Update () {
        // Here are four steps we need per step
        // You may feel puzzled with Dictionary<string, Vector3> playerInfo,
        // see the detailed informaiton in the InitData.cs script

        // TODO: update local player info from the scene (by Yuehao)
        // helpful function: this.localPlayerController.updateLocalPlayerInfo
        // ********

        // TODO: upload local player info asychronously TO the server(by YinCen)
        // helpful function: this.localPlayerController.getLocalPlayerInfo
        // ********

        // TODO: update other player info asychromously FROM the server(by YinCen)
        // helpful function: this.otherPlayerController.syncOtherPlayerInfo
        // ********

        // TODO: update the model and apply action for player in the scene (by Yuehao)
        // helpful function: this.otherPlayerController.updateSceneCharacter
        // ********
    }
}
