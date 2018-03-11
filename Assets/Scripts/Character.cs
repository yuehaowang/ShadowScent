using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
	
	public GameObject mainCameraPrefab;

	void Start () {
		if (gameObject.tag == "Player") {
			GameObject mainCamera = Instantiate<GameObject>(mainCameraPrefab, gameObject.transform);
//			mainCamera.transform.Translate(0, 3.0f, 3.0f);
			mainCamera.transform.Translate(0, 4.0f, -11.0f);
			mainCamera.transform.Rotate(new Vector3(5.0f, 0, 0));
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

}
