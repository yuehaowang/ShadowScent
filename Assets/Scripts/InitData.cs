using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    namespace Init
    {
        public class localPlayer
        {
            public string playerIdentity;
            public Dictionary<string, Vector3> playerInfo;

            public localPlayer(string playerIdentity)
            {
                if (playerIdentity == GameData.Static.PlayerIdentity.PLAYER_UP)
                {
                    this.playerInfo.Add("player_localEularAngles", new Vector3(0.0F, 0.0F, 0.0F));
                    this.playerInfo.Add("player_position", new Vector3(0.0F, 0.0F, 0.0F));
                } else if (playerIdentity == GameData.Static.PlayerIdentity.PLAYER_DOWN) {
                    this.playerInfo.Add("arrow_localEularAngles", new Vector3(0.0F, 0.0F, 0.0F));
                } else {
                    throw new System.Exception("Expected player Identity, but[" + playerIdentity + "] found");
                }
                this.playerIdentity = playerIdentity;
            }
        }

        public class otherPlayer
        {
            public string playerIdentity;
            public Dictionary<string, Vector3> playerInfo;

            public otherPlayer(string playerIdentity)
            {
                if (playerIdentity == GameData.Static.PlayerIdentity.PLAYER_UP)
                {
                    this.playerInfo.Add("arrow_localEularAngles", new Vector3(0.0F, 0.0F, 0.0F));
                }
                else if (playerIdentity == GameData.Static.PlayerIdentity.PLAYER_DOWN)
                {
                    this.playerInfo.Add("player_localEularAngles", new Vector3(0.0F, 0.0F, 0.0F));
                    this.playerInfo.Add("player_position", new Vector3(0.0F, 0.0F, 0.0F));
                }
                else
                {
                    throw new System.Exception("Expected player Identity, but[" + playerIdentity + "] found");
                }
                this.playerIdentity = playerIdentity;
            }
        }

        public class gameInitScene
        {
            public string localPlayerIdentity;
            public localPlayer localPlayerInfo;
            public otherPlayer otherPlayerInfo;
            
            public gameInitScene(string localPlayerIdentity)
            {
                if (localPlayerIdentity == GameData.Static.PlayerIdentity.PLAYER_UP)
                {
                    localPlayerInfo = new localPlayer(localPlayerIdentity);
                    otherPlayerInfo = new otherPlayer(GameData.Static.PlayerIdentity.PLAYER_DOWN);
                } else if (localPlayerIdentity == GameData.Static.PlayerIdentity.PLAYER_DOWN)
                {
                    localPlayerInfo = new localPlayer(localPlayerIdentity);
                    otherPlayerInfo = new otherPlayer(GameData.Static.PlayerIdentity.PLAYER_UP);
                } else
                {
                    throw new System.Exception("Expected local player Identity, but[" + localPlayerIdentity + "] found");
                }
                this.localPlayerIdentity = localPlayerIdentity;
            }
        }
    }
}
