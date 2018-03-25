using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject mainCameraPrefab;
	public int playerId;
	public NetWorkManage networkManage;
	private CompassController compassControl;
	private TouchController touchControl;

	void Start ()
	{
		playerId = LevelManage.currentPlayerId;

		GameObject mainCamera = Instantiate<GameObject>(mainCameraPrefab, gameObject.transform);
		mainCamera.tag = "MainCamera";

		networkManage = GameObject.Find("NetWorkManage").GetComponent<NetWorkManage>();

		Debug.Log(networkManage);

		compassControl = new CompassController();
		touchControl = new TouchController();
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Wall") {
			AudioSource audio = c.gameObject.GetComponent<AudioSource>();
			audio.Play();
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Key") {
			Destroy(c.gameObject);
		}
	}

	void OnGUI()
	{
		compassControl.Debug();
	}

	void Update()
	{
		ControlPlayer();

		UpdateData();
	}

	private void ControlPlayer()
	{
		int moveDir = 0;
		Character c = GetComponent<Character>();

		if (playerId == 1) {
			if (Input.GetKey(KeyCode.W)) {
				moveDir = 1;
			}
			if (Input.GetKey(KeyCode.S)) {
				moveDir = -1;
			}

			touchControl.Update();

			if (touchControl.directionY == TouchController.Direction.UP) {
				moveDir = 1;
			} else if (touchControl.directionY == TouchController.Direction.DOWN) {
				moveDir = -1;
			}

			c.Propel(moveDir);
		}

		if (Input.GetKey(KeyCode.A)) {
			if (playerId == 1) {
				c.Yaw(-1);
			} else {
				RotateSoundProber(-1);
			}
				
		}
		if (Input.GetKey(KeyCode.D)) {
			if (playerId == 1) {
				c.Yaw(1);
			} else {
				RotateSoundProber(1);
			}
		}

		compassControl.Update();

		if (compassControl.changed) {
			if (playerId == 1) {
				c.YawTo(compassControl.value);
			} else {
				RotateSoundProberTo(compassControl.value);
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
		transform.Find("SoundProber").rotation = Quaternion.Euler(new Vector3(0, -90.0f)) * eq;
	}

	private void UpdateData()
	{
		if (playerId == 0) {
			networkManage.p0Data = new CharacterData0 {
				rotationY = 0
			};

			if (networkManage.p1Data != null) {
				transform.position = new Vector3(
					networkManage.p1Data.x,
					networkManage.p1Data.y,
					networkManage.p1Data.z
				);

				GetComponent<Character>().YawTo(Quaternion.Euler(new Vector3(0, networkManage.p1Data.rotationY)));
			}
		} else if (playerId == 1) {
			networkManage.p1Data = new CharacterData1 {
				x = transform.position.x,
				y = transform.position.y,
				z = transform.position.z,
				rotationY = transform.Find("SoundProber").transform.rotation.eulerAngles.y
			};

			if (networkManage.p0Data != null) {
				RotateSoundProberTo(Quaternion.Euler(new Vector3(0, networkManage.p0Data.rotationY)));
			}
		}
	}

}
