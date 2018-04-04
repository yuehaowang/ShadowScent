using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

public class SocketIONetworking : MonoBehaviour {
	public GameObject connectionHint;
	private Text connectionHintTxtObj;
	private string connectionHintText;

	public Player1Data p1Data;
	public Player0Data p0Data;

	private bool anotherPlayerConnected = false;
	private bool playerConnected = false;

	// used to limit the frequency of sending message.
	private int frameRate = 1, frameRateIndex = 0;

	private const string serverURL = "http://ss.chinacloudsites.cn/";
	private Socket socket = null;
		
	void Start()
	{
		p0Data = new Player0Data();
		p1Data = new Player1Data();

		connectionHintTxtObj = connectionHint.GetComponent<Text>();
		connectionHintText = "Server connecting...";

		DoOpen();
	}

	void OnDestroy()
	{
		DoClose();
	}

	void Update()
	{
		connectionHintTxtObj.text = connectionHintText;

		if (socket == null || !playerConnected) {
			return;
		}

		if (frameRateIndex++ < frameRate) {
			return;
		}

		frameRateIndex = 0;

		if (LevelManage.currentPlayerId == 0) {
			socket.Emit("player0", JsonConvert.SerializeObject(p0Data));
		} else if (LevelManage.currentPlayerId == 1) {
			socket.Emit("player1", JsonConvert.SerializeObject(p1Data));
		}
	}

	void DoOpen()
	{
		if (socket == null) {
			socket = IO.Socket(serverURL);

			socket.On(Socket.EVENT_CONNECT, () => {
				playerConnected = true;

				connectionHintText = "Waiting for another player...";
			});

			socket.On(Socket.EVENT_DISCONNECT, () => {
				playerConnected = false;

				connectionHintText = "Server disconnected.";
			});

			socket.On(GetAnotherPlayerInterface(), (data) => {
				if (!anotherPlayerConnected) {
					anotherPlayerConnected = true;

					connectionHintText = "";
				}

				string str = data.ToString();

				if (LevelManage.currentPlayerId == 0) {
					p1Data = JsonConvert.DeserializeObject<Player1Data>(str);
				} else if (LevelManage.currentPlayerId == 1) {
					p0Data = JsonConvert.DeserializeObject<Player0Data>(str);
				}

				if (!p1Data.connected || !p0Data.connected) {
					anotherPlayerConnected = false;
					connectionHintText = "Lose the connection of the other player.";
				}
			});
		}
	}

	string GetPlayerInterface()
	{
		return "player" + LevelManage.currentPlayerId;
	}

	string GetAnotherPlayerInterface()
	{
		return "player" + ((LevelManage.currentPlayerId == 0) ? "1" : "0");
	}

	void DoClose()
	{
		if (socket != null) {
			playerConnected = false;

			if (LevelManage.currentPlayerId == 0) {
				socket.Emit(GetPlayerInterface(), JsonConvert.SerializeObject(new Player0Data {
					connected = false
				}));
			} else if (LevelManage.currentPlayerId == 1) {
				socket.Emit(GetPlayerInterface(), JsonConvert.SerializeObject(new Player1Data {
					connected = false
				}));
			}

			socket.Disconnect();
			socket = null;
		}
	}
}
