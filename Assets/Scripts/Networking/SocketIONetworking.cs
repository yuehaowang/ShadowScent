using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

public class SocketIONetworking : MonoBehaviour {
	private const string serverURL = "http://ss.chinacloudsites.cn/";
	protected Socket socket = null;

	// data for player0 to get
	public Player0Transform p0Transform;

	// data for player1 to get
	public Player1Transform p1Transform;
	public PlayerGetKey playerGetKey;

	// data for both player1 and player0 to get
	public PlayerConnection anotherPConn;
	public PlayerEmitRaycast playerRaycast;

	// used to limit the frequency of sending message.
	private int frameRate = 10, frameRateIndex = 0;
		
	void Start()
	{
		p0Transform = new Player0Transform();
		p1Transform = new Player1Transform();
		anotherPConn = new PlayerConnection();
		playerGetKey = null;
		playerRaycast = null;

		DoOpen();
	}

	void OnDestroy()
	{
		DoClose();
	}

	void Update()
	{
		if (socket == null) {
			return;
		}

		if (frameRateIndex++ < frameRate) {
			return;
		}

		frameRateIndex = 0;

		if (LevelManage.currentPlayerId == 0) {
			socket.Emit("player0_transform", JsonConvert.SerializeObject(p0Transform));
		} else if (LevelManage.currentPlayerId == 1) {
			socket.Emit("player1_transform", JsonConvert.SerializeObject(p1Transform));
		}
	}

	void DoOpen()
	{
		if (socket == null) {
			socket = IO.Socket(serverURL);

			socket.On(Socket.EVENT_CONNECT, () => {
				Debug.Log("connected");

				DoEmitConnection(0);
			});

			socket.On("player_connection", (data) => {
				string str = data.ToString();

				PlayerConnection pConn = JsonConvert.DeserializeObject<PlayerConnection>(str);

				if (pConn.uid != LevelManage.currentPlayerId) {
					anotherPConn = pConn;

					Debug.Log("another player login");
				}
			});

			socket.On("player0_action", (data) => {
				p0Transform = JsonConvert.DeserializeObject<Player0Transform>(data.ToString());
			});

			socket.On("player1_action", (data) => {
				p1Transform = JsonConvert.DeserializeObject<Player1Transform>(data.ToString());
			});
		}
	}

	void DoEmitConnection(int operation)
	{
		PlayerConnection pConn = new PlayerConnection {
			uid = LevelManage.currentPlayerId,
			operation = 0
		};

		socket.Emit("player_connection", JsonConvert.SerializeObject(pConn));
	}

	void DoClose()
	{
		if (socket != null) {
			DoEmitConnection(1);

			socket.Disconnect();
			socket = null;
		}
	}
}
