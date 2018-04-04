using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject mainCameraPrefab;
	public ParticleSystem raycastPrefab;
	public int playerId;
	public SocketIONetworking networkingManage;
	private CompassController compassControl;
	private TouchController touchControl;

	void Start ()
	{
		playerId = LevelManage.currentPlayerId;

		GameObject mainCamera = Instantiate<GameObject>(mainCameraPrefab, gameObject.transform);
		mainCamera.tag = "MainCamera";

		compassControl = new CompassController();
		touchControl = new TouchController();

		networkingManage = GameObject.Find("NetworkManage").GetComponent<SocketIONetworking>();
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Wall") {
			AudioSource audio = c.gameObject.GetComponent<AudioSource>();
			audio.Play();
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Key" && c.gameObject.GetComponent<Key>().isVisible) {
			Destroy(c.gameObject);
		}
	}

	void OnGUI()
	{
		#if UNITY_EDITOR

		compassControl.Debug();

		#endif
	}

	void Update()
	{
		Character c = GetComponent<Character>();

		ControlPlayer(c);

		UpdateData(c);
	}

	private void ControlPlayer(Character c)
	{
		int moveDir = 0, emitRay = 0;

		if (playerId == 1) {
			if (Input.GetKey(KeyCode.W)) {
				moveDir = 1;
			}
			if (Input.GetKey(KeyCode.S)) {
				moveDir = -1;
			}
		}

		touchControl.Update();

		if (touchControl.touchCount >= 3) {
			emitRay = 1;
		} else if (playerId == 1) {
			if (touchControl.directionY == TouchController.Direction.UP) {
				moveDir = 1;
			} else if (touchControl.directionY == TouchController.Direction.DOWN) {
				moveDir = -1;
			}
		}

		c.Propel(moveDir);

		if (Input.GetKey(KeyCode.A)) {
			if (playerId == 1) {
				c.Yaw(-1);
			} else if (playerId == 0) {
				RotateSoundProber(-1);
			}
		}
		if (Input.GetKey(KeyCode.D)) {
			if (playerId == 1) {
				c.Yaw(1);
			} else if (playerId == 0) {
				RotateSoundProber(1);
			}
		}

		if (playerId == 0) {
			if (Input.GetKey(KeyCode.Space)) {
				emitRay = 1;
			}

			if (emitRay == 1) {
				EmitRaycast();
			}
		}

		compassControl.Update();

		if (compassControl.changed) {
			if (playerId == 1) {
				c.YawTo(compassControl.value);
			} else if (playerId == 0) {
				RotateSoundProberTo(compassControl.value);
			}
		}
	}

	private void EmitRaycast()
	{
		Transform spTrans = transform.Find("SoundProber");

		Instantiate<ParticleSystem>(raycastPrefab, spTrans);

		RaycastHit hit;
		Vector3 fwd = spTrans.TransformDirection(Vector3.right);

		if (Physics.Raycast(transform.position, fwd, out hit, 95.0f)) {
			GameObject gameObj = hit.collider.gameObject;

			Debug.Log("hit: " + gameObj.name);

			if (gameObj.tag == "Key") {
				Key k = gameObj.GetComponent<Key>();

				if (!k.isVisible) {
					k.SetVisible(true);
				}
			}
		}
	}

	private void RotateSoundProber(int dir)
	{
		float soundProberW = 2.0f;

		transform.Find("SoundProber").Rotate(new Vector3(0, soundProberW * dir));
	}

	private void RotateSoundProberTo(Quaternion eq)
	{
		transform.Find("SoundProber").localRotation = Quaternion.Euler(new Vector3(0, -90.0f)) * eq;
	}

	private void UpdateData(Character c)
	{
		if (playerId == 0) {
			networkingManage.p0Transform.rotationAngle = transform.Find("SoundProber").localRotation.eulerAngles.y + 90;

			Player1Transform p1Trans = networkingManage.p1Transform;

			c.MoveTo(new Vector3(
				p1Trans.positionX,
				transform.position.y,
				p1Trans.positionZ
			));

			c.YawTo(Quaternion.Euler(0, p1Trans.rotationAngle, 0));
		} else if (playerId == 1) {
			Vector3 pos = transform.position;

			networkingManage.p1Transform.positionX = pos.x;
			networkingManage.p1Transform.positionZ = pos.z;
			networkingManage.p1Transform.rotationAngle = transform.rotation.eulerAngles.y;

			RotateSoundProberTo(Quaternion.Euler(0, networkingManage.p0Transform.rotationAngle, 0));
		}
	}

}
