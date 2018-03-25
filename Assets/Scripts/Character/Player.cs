using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject mainCameraPrefab;
	public int playerId;
	private CompassController compassControl;
	private TouchController touchControl;

	void Start ()
	{
		playerId = LevelManage.currentPlayerId;

		GameObject mainCamera = Instantiate<GameObject>(mainCameraPrefab, gameObject.transform);
		mainCamera.tag = "MainCamera";

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

	public CharacterData GetData ()
	{
		return new CharacterData {
			playerId = playerId,
			x = transform.position.x,
			y = transform.position.y,
			z = transform.position.z,
			rotationX = transform.rotation.x,
			rotationY = transform.rotation.y,
			rotationZ = transform.rotation.z
		};
	}

	private void RotateSoundProber(int dir)
	{
		float soundProberW = 2.0f;

		GameObject soundProber = transform.Find("SoundProber").gameObject;
		soundProber.transform.Rotate(new Vector3(0, soundProberW * dir));
	}

	private void RotateSoundProberTo(Quaternion eq)
	{
		GameObject soundProber = transform.Find("SoundProber").gameObject;
		soundProber.transform.rotation = Quaternion.Euler(new Vector3(0, -90.0f)) * eq;
	}

}
