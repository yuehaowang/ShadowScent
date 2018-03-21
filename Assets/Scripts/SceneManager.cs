using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    GameController.localPlayer localPlayerController;
    GameController.otherPlayer otherPlayerController;

    // Use this for initialization
    void Start () {
        // Debugging, the local player is down one
        // there should be a GUI to set up the room and choose the identity
        string localPlayerIdentity = GameData.Static.PlayerIdentity.PLAYER_DOWN;
        GameData.Init.gameInitScene init_data = new GameData.Init.gameInitScene(localPlayerIdentity);
        localPlayerController = new GameController.localPlayer(init_data.localPlayerInfo);
        otherPlayerController = new GameController.otherPlayer(init_data.otherPlayerInfo);
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

namespace GameController
{

    public class localPlayer
    {
        public string playerIdentity;
        public Dictionary<string, Vector3> playerInfo;

        public localPlayer(GameData.Init.localPlayer init_data)
        {
            this.playerIdentity = init_data.playerIdentity;
            this.playerInfo = init_data.playerInfo;
        }

        public void updateLocalPlayerInfo(Dictionary<string, Vector3> playerInfo)
        {
            // update the infomation by the object in scene, the detailed information
            // can refer to InitData Part, be supposed to be done by YueHao
            this.playerInfo = playerInfo;
        }

        public Dictionary<string, Vector3> getLocalPlayerInfo()
        {
            // this function returns the information of localPlayer, should be upload to server
            // be supposed to be done by YinCen
            return this.playerInfo;
        }
        
    }

    public class otherPlayer
    {
        public string playerIdentity;
        public Dictionary<string, Vector3> playerInfo;

        public otherPlayer(GameData.Init.otherPlayer init_data)
        {
            this.playerIdentity = init_data.playerIdentity;
            this.playerInfo = init_data.playerInfo;
        }

        public void syncOtherPlayerInfo(Dictionary<string, Vector3> playerInfo)
        {
            // TODO: this function should receive the information from the server, should be done
            // by YinCen
            this.playerInfo = playerInfo;
        }

        public void updateSceneCharacter()
        //TODO: input the character manager maybe? this function aims at updating the states of
        // objects in scene, should be done by YueHao
        {
            if (this.playerIdentity == GameData.Static.PlayerIdentity.PLAYER_UP)
            {
                // TODO: if the player is the upper one, sync the scene character with pos and rotation
                Vector3 player_localEularAngle = this.playerInfo["player_localEularAngles"];
                Vector3 player_position = this.playerInfo["player_position"];
            } else {
                Vector3 arrow_localEularAngle = this.playerInfo["arrow_localEularAngles"];
                // TODO: Similarily
            }
        }
    }
}