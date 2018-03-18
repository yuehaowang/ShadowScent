﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CharacterManage : MonoBehaviour
{

    public GameObject characterPrefab;
	public const float v = 0.5f, w = 1.0f;
	private GameObject player;
	private CompassController compass;

    void Start()
    {

        player = Instantiate<GameObject>(characterPrefab, gameObject.transform);
        player.tag = "Player";

		compass = new CompassController();
    }

    void OnGUI()
    {
		compass.Debug();
    }

    void Update()
    {
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
			
		compass.Update();

		if (compass.changed) {
			player.transform.rotation = compass.value;
		}

		if (Input.touchCount > 0) {
			player.transform.Translate(Vector3.forward * v, Space.Self);
		}
    }

}

class CompassController
{
	private bool flag = true;
	private Quaternion qe;
	private float basis, mh, lmh, llmh, mhft, lmhft;
	public Quaternion value;
	public bool changed;

	public CompassController()
	{
		Input.compass.enabled = true;
		Input.gyro.enabled = true;

		basis = 0f;
		lmh = Input.compass.magneticHeading;
		llmh = Input.compass.magneticHeading;
		lmhft = Input.compass.magneticHeading;
	}

	public void Debug()
	{
        GUILayout.Label(Input.gyro.attitude.ToString());
        GUILayout.Label(Input.compass.magneticHeading.ToString());
        GUILayout.Label(basis.ToString());
        GUILayout.Label(mh.ToString());
	}

	public void Update()
	{
		if (flag && Input.compass.magneticHeading != 0)
		{
			basis = Input.compass.magneticHeading;
			flag = false;
		}

		mh = Input.compass.magneticHeading - basis > 0 ? Input.compass.magneticHeading - basis : Input.compass.magneticHeading - basis + 360;
		if (llmh > 270)
		{
			if (lmh < 90)
				lmh += 360;
			if (mh < 90)
				mh += 360;
		}
		else if (lmh > 270)
		{
			if (llmh < 90)
				llmh += 360;
			if (mh < 90)
				mh += 360;
		}
		else if (mh > 270)
		{
			if (llmh < 90)
				llmh += 360;
			if (lmh < 90)
				lmh += 360;
		}

		mhft = 0.3f * llmh + 0.4f * lmh + 0.3f * mh;

		if (lmhft > 270)
		{
			if (mhft < 90)
				mhft += 360;
		}
		else if (mhft > 270)
		{
			if (lmhft < 90)
				lmhft += 360;
		}

		if (lmhft - mhft <= -2 || lmhft - mhft >= 2) {
			value = Quaternion.Euler(0, mhft, 0);

			changed = true;

			llmh = lmh % 360;
			lmh = mh % 360;
			lmhft = mhft % 360;
		} else {
			changed = false;
		}
	}

}