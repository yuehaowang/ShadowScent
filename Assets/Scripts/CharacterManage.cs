using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterManage : MonoBehaviour
{

    public GameObject characterPrefab;
    private GameObject player;
    private float lmh, llmh, mhft, lmhft;
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
        mhft = 0.3f * llmh + 0.4f * lmh + 0.3f * Input.compass.magneticHeading;
        if (lmhft - mhft <= -2 || lmhft - mhft >= 2)
        {
            qe = Quaternion.Euler(0, mhft, 0);
            player.transform.rotation = qe;
            llmh = lmh;
            lmh = Input.compass.magneticHeading;
            lmhft = mhft;
        }


        if (Input.touchCount > 0)
            player.transform.Translate(Vector3.forward * v, Space.Self);

    }

}

public class FilterButterworth
{
    /// <summary>
    /// rez amount, from sqrt(2) to ~ 0.1
    /// </summary>
    private readonly float resonance;

    private readonly float frequency;
    private readonly int sampleRate;
    private readonly PassType passType;

    private readonly float c, a1, a2, a3, b1, b2;

    /// <summary>
    /// Array of input values, latest are in front
    /// </summary>
    private float[] inputHistory = new float[2];

    /// <summary>
    /// Array of output values, latest are in front
    /// </summary>
    private float[] outputHistory = new float[3];

    public FilterButterworth(float frequency, int sampleRate, PassType passType, float resonance)
    {
        this.resonance = resonance;
        this.frequency = frequency;
        this.sampleRate = sampleRate;
        this.passType = passType;

        switch (passType)
        {
            case PassType.Lowpass:
                c = 1.0f / (float)Math.Tan(Math.PI * frequency / sampleRate);
                a1 = 1.0f / (1.0f + resonance * c + c * c);
                a2 = 2f * a1;
                a3 = a1;
                b1 = 2.0f * (1.0f - c * c) * a1;
                b2 = (1.0f - resonance * c + c * c) * a1;
                break;
            case PassType.Highpass:
                c = (float)Math.Tan(Math.PI * frequency / sampleRate);
                a1 = 1.0f / (1.0f + resonance * c + c * c);
                a2 = -2f * a1;
                a3 = a1;
                b1 = 2.0f * (c * c - 1.0f) * a1;
                b2 = (1.0f - resonance * c + c * c) * a1;
                break;
        }
    }

    public enum PassType
    {
        Highpass,
        Lowpass,
    }

    public void Update(float newInput)
    {
        float newOutput = a1 * newInput + a2 * this.inputHistory[0] + a3 * this.inputHistory[1] - b1 * this.outputHistory[0] - b2 * this.outputHistory[1];

        this.inputHistory[1] = this.inputHistory[0];
        this.inputHistory[0] = newInput;

        this.outputHistory[2] = this.outputHistory[1];
        this.outputHistory[1] = this.outputHistory[0];
        this.outputHistory[0] = newOutput;
    }

    public float Value
    {
        get { return this.outputHistory[0]; }
    }
}
