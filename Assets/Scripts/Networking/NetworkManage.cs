using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;

public class NetWorkManage : MonoBehaviour
{
    // Use this for initialization
    public int localPlayerIdentity = 0;
    private CharacterData p0Data = DataUtils.emptyCharacterData(0);
    private CharacterData p1Data = DataUtils.emptyCharacterData(1);
    public Task upTask;

    void Start()
    {
        networkLoop();
    }

    private async void networkLoop()
    {
        this.upTask = DataSync.uploadPlayerData(p0Data);
        while (true)
        {
            if (upTask.IsCompleted)
            {
                this.p0Data = DataUtils.emptyCharacterData(0);
                await this.upTask;
                upTask = DataSync.uploadPlayerData(p0Data);
            }
            await Task.Delay(100);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public CharacterData getCharacterData(int playerID)
    {
        if (playerID == 0)
        {
            return this.p0Data;
        }
        else if (playerID == 1)
        {
            return this.p1Data;
        }
        else
        {
            return null;
        }
    }
}
