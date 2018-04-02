using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CharacterManage : MonoBehaviour
{

    public GameObject playerPrefab;
	private GameObject player;

    void Start()
    {
		player = Instantiate<GameObject>(playerPrefab, gameObject.transform);	
    }

}
