using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyManage : MonoBehaviour {
	public struct KeyPosPair {
		public float x;
		public float z;

		public KeyPosPair (float _x = 0, float _z = 0){
			x = _x;
			z = _z;
		}
	}

	public GameObject keyPrefab;

	public KeyPosPair[] keyPosList = new KeyPosPair[5]{
		new KeyPosPair(125.2f, 37.2f),
		new KeyPosPair(-365f, 437.6f),
		new KeyPosPair(262f, 320.6f),
		new KeyPosPair(355.5f, 27.7f),
		new KeyPosPair(342.5f, -263.8f)
	};

	private int currentKeyIndex = 0;
	private GameObject currentKey;

	void Start () {
		
	}

	void Update () {
		if (currentKeyIndex < 5 && !currentKey) {
			KeyPosPair pos = keyPosList[currentKeyIndex];

			currentKey = Instantiate(keyPrefab, gameObject.transform);
			currentKey.transform.position = new Vector3(pos.x, 3.5f, pos.z);

			currentKeyIndex++;
		}
	}
}
