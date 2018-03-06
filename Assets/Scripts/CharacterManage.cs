using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManage : MonoBehaviour
{

    public GameObject characterPrefab;
    private GameObject player;
    private float mh, lmh, llmh, mhft, lmhft;
    const float v = 0.5f, w = 1.0f;
    Quaternion qe;

    void Start()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;
        player = Instantiate<GameObject>(characterPrefab, gameObject.transform);
        player.tag = "Player";
        lmh = Input.compass.magneticHeading;
        llmh = Input.compass.magneticHeading;
        lmhft = Input.compass.magneticHeading;
    }

    void OnGUI()
    {
        GUILayout.Label(Input.compass.rawVector.ToString());//-42-42??
        GUILayout.Label(Input.gyro.attitude.ToString());
        GUILayout.Label(Input.compass.magneticHeading.ToString());
    }

    void Update()
    {
        //player.transform.Rotate(Input.compass.rawVector-lvct);
        //lvct=Input.compass.rawVector;

        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Rotate(new Vector3(0, -w));
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Rotate(new Vector3(0, w));
        }
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector3.forward * v, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector3.forward * -v, Space.Self);
        }

        mh = Input.compass.magneticHeading;
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

        if (lmhft - mhft <= -2 || lmhft - mhft >= 2)
        {
            qe = Quaternion.Euler(0, mhft, 0);
            player.transform.rotation = qe;
            llmh = lmh % 360;
            lmh = Input.compass.magneticHeading;
            lmhft = mhft % 360;
        }

        if (Input.touchCount > 0)
            player.transform.Translate(Vector3.forward * v, Space.Self);


    }

}