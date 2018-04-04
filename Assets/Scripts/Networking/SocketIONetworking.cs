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

	public Player1Transform p1Transform;
	public Player0Transform p0Transform;
	public PlayerEmitRaycast playerRaycast;
	public PlayerGetKey playerGetKey;

	private bool anotherPlayerConnected = false;
	private bool playerConnected = false;

	// used to limit the frequency of sending message.
	private int frameRate = 10, frameRateIndex = 0;

	private const string serverURL = "http://ss.chinacloudsites.cn/";
	private Socket socket = null;
		
	void Start()
	{
		p0Transform = new Player0Transform();
		p1Transform = new Player1Transform();
		playerGetKey = null;
		playerRaycast = null;

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
			socket.Emit("player0", JsonConvert.SerializeObject(new SocketMessage {
				operation = "player0_transform",
				data = JsonConvert.SerializeObject(p0Transform)
			}));
		} else if (LevelManage.currentPlayerId == 1) {
			socket.Emit("player1", JsonConvert.SerializeObject(new SocketMessage {
				operation = "player1_transform",
				data = JsonConvert.SerializeObject(p1Transform)
			}));
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
				connectionHintText = "Server disconnected.";
			});

			socket.On(GetAnotherPlayerInterface(), (data) => {
				if (!anotherPlayerConnected) {
					anotherPlayerConnected = true;

					connectionHintText = "";
				}

				string str = data.ToString();

				SocketMessage msg = JsonConvert.DeserializeObject<SocketMessage>(str);
				DoHandleMessage(msg);
			});
		}
	}

	void DoEmitDisconnect()
	{
		playerConnected = false;

		SocketMessage msg = new SocketMessage {
			operation = "player_disconnect"
		};

		socket.Emit(GetPlayerInterface(), JsonConvert.SerializeObject(msg));
	}

	string GetPlayerInterface()
	{
		return "player" + LevelManage.currentPlayerId;
	}

	string GetAnotherPlayerInterface()
	{
		return "player" + ((LevelManage.currentPlayerId == 0) ? "1" : "0");
	}

	void DoHandleMessage(SocketMessage msg)
	{
		if (msg.operation == "player_disconnect") {
			anotherPlayerConnected = false;

			connectionHintText = "Lose the connection of the other player.";
		} else if (msg.operation == "player0_transform") {
			p0Transform = JsonConvert.DeserializeObject<Player0Transform>(msg.data);
		} else if (msg.operation == "player1_transform") {
			p1Transform = JsonConvert.DeserializeObject<Player1Transform>(msg.data);
		}
	}

	void DoClose()
	{
		if (socket != null) {
			DoEmitDisconnect();

			socket.Disconnect();
			socket = null;
		}
	}
}
