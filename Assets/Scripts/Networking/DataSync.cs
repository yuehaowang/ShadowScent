using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public static class DataSync
{

    private static IMobileServiceTable<CharacterData> characterDataTable_useProperty;

    // get current time stamp, the function is derieved from internet
    private static long GetCurrentTime()
    {
        TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
        long t = (long)cha.TotalSeconds;
        return t;
    }

    // upload the characterData
    public static async Task uploadPlayerData(CharacterData cData)
    {
        cData.Time = GetCurrentTime();
        // await uploadAsync(cData);

        var allEntriesBefore = await updateAsync();
        // if successfully update the table, then insert latest data, otherwise, the upload is fail
        if (allEntriesBefore != null)
        {
            try
            {
                Debug.Log("Uploading player Data to Azure...\n" + DataUtils.CharacterData2String(cData));
                await characterDataTable.InsertAsync(cData);

                Debug.Log("Finished uploading player data.\n" + DataUtils.CharacterData2String(cData));
            }
            catch (System.Exception e)
            {
                Debug.Log("Error uploading player data: " + e.Message);
            }
            // clear up the historical player data for that player to avoid redundancy
            foreach (var item in allEntriesBefore)
            {
                try
                {
                    if (item.playerId == cData.playerId) await characterDataTable.DeleteAsync(item);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Debug.Log("Delete OK");
        }


    }

    // get the objective characterData
    public static async Task<CharacterData> getPlayerData(int playerID)
    {
        var allEntries = await updateAsync();
        allEntries.Sort((a, b) => a.Time.CompareTo(b.Time));
        foreach (var item in allEntries)
        {
            if (item.playerId == playerID)
                return item;
        }
        return null;
    }

    public static async Task outputCharacterData()
    {
        var allEntries = await updateAsync();
        String output;
        output = "CharacterData Table:\n";
        foreach (var item in allEntries)
        {
            output += DataUtils.CharacterData2String(item) + "\n";
        }
        Debug.Log(output);
    }

    public static async Task clearCharacterData()
    {
        var allEntries = await updateAsync();
        foreach (var item in allEntries)
        {
            try
            {
                await characterDataTable.DeleteAsync(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public static IMobileServiceTable<CharacterData> characterDataTable
    {
        get
        {
            if (characterDataTable_useProperty == null)
            {
                characterDataTable_useProperty = AzureServiceClient.Client.GetTable<CharacterData>();
            }

            return characterDataTable_useProperty;
        }
    }

    private static async Task<List<CharacterData>> updateAsync()
    {
        try
        {
            var allEntries = await characterDataTable.ToListAsync();
            Debug.Assert(allEntries != null, "ToListAsync failed!");
            return allEntries;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task uploadAsync(CharacterData cData)
    {

    }
}
