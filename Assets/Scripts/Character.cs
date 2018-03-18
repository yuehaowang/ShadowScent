using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
	
	public GameObject mainCameraPrefab;
	private Rigidbody body;
	private float v = 0.7f, w = 0.7f;

	void Start () {
		if (gameObject.tag == "Player") {
			GameObject mainCamera = Instantiate<GameObject>(mainCameraPrefab, gameObject.transform);
			mainCamera.tag = "MainCamera";

			body = GetComponent<Rigidbody>();
		}
	}

	void Update () {
		
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

	public void Propel(int dir)
	{
		if (dir == 0) {
			return;
		}
		
		body.MovePosition(transform.position + transform.forward * v * dir);
	}

	public void YawTo(Quaternion eq)
	{
		body.MoveRotation(eq);
	}

	public void Yaw(int dir)
	{
		if (dir == 0) {
			return;
		}

		body.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, dir * w)));
	}

}
