using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManage : MonoBehaviour {
	
	public GameObject characterPrefab;

	private GameObject player;
	private float lmh;

	void Start () {
		Input.compass.enabled = true; 
		Input.gyro.enabled = true; 
		player = Instantiate<GameObject>(characterPrefab, gameObject.transform);
		player.tag = "Player";
		lmh = Input.compass.magneticHeading;
	}

	void OnGUI(){
		GUILayout.Label(Input.compass.rawVector.ToString());
		GUILayout.Label (Input.gyro.attitude.ToString ());
	}

	void Update () {
		const float v = 0.5f, w = 1.0f;

		//player.transform.Rotate(Input.compass.rawVector-lvct);
		//lvct=Input.compass.rawVector;

		if (Input.GetKey(KeyCode.A)) {
			player.transform.Rotate(new Vector3(0, -w));
		}
		if (Input.GetKey(KeyCode.D)) {
			player.transform.Rotate(new Vector3(0, w));
		}
		if (Input.GetKey(KeyCode.W)) {
			player.transform.Translate(Vector3.forward * v, Space.Self);
		}
		if (Input.GetKey(KeyCode.S)) {
			player.transform.Translate(Vector3.forward * -v, Space.Self);
		}
		if (lmh - Input.compass.magneticHeading <= -7 || lmh - Input.compass.magneticHeading >= 7) {
			player.transform.rotation = Quaternion.Euler (0, Input.compass.magneticHeading, 0);
			lmh = Input.compass.magneticHeading;
		}
		if (Input.touchCount > 0)
			player.transform.Translate(Vector3.forward * v, Space.Self);
		
	}

}
