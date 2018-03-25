using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public static class DataSync
{

    private static IMobileServiceTable<CharacterData0> characterDataTable0_useProperty;
	private static IMobileServiceTable<CharacterData1> characterDataTable1_useProperty;

    // get current time stamp, the function is derieved from internet
    private static long GetCurrentTime()
    {
        TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
        long t = (long)cha.TotalSeconds;
        return t;
    }

    // upload the characterData0
    public static async Task uploadPlayerData0(CharacterData0 cData)
    {
        cData.Time = GetCurrentTime();

        var allEntriesBefore = await updateAsync0();
        // if successfully update the table, then insert latest data, otherwise, the upload is fail
        if (allEntriesBefore != null)
        {
            try
            {
                Debug.Log("Uploading player Data to Azure...\n" + DataUtils.CharacterData02String(cData));
                await characterDataTable0.InsertAsync(cData);

                Debug.Log("Finished uploading player data.\n" + DataUtils.CharacterData02String(cData));
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
                    await characterDataTable0.DeleteAsync(item);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Debug.Log("Delete OK");
        }
    }

	// upload the characterData1
	public static async Task uploadPlayerData1(CharacterData1 cData)
	{
		cData.Time = GetCurrentTime();

		var allEntriesBefore = await updateAsync1();
		// if successfully update the table, then insert latest data, otherwise, the upload is fail
		if (allEntriesBefore != null)
		{
			try
			{
				Debug.Log("Uploading player Data to Azure...\n" + DataUtils.CharacterData12String(cData));
				await characterDataTable1.InsertAsync(cData);

				Debug.Log("Finished uploading player data.\n" + DataUtils.CharacterData12String(cData));
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
					await characterDataTable1.DeleteAsync(item);
				}
				catch (Exception)
				{
					throw;
				}
			}
			Debug.Log("Delete OK");
		}
	}

    // get the objective characterData0
    public static async Task<CharacterData0> getPlayerData0()
    {
        var allEntries = await updateAsync0();
        allEntries.Sort((a, b) => a.Time.CompareTo(b.Time));
        foreach (var item in allEntries)
        {
                return item;
        }
        return null;
    }

	// get the objective characterData1
	public static async Task<CharacterData1> getPlayerData1()
	{
		var allEntries = await updateAsync1();
		allEntries.Sort((a, b) => a.Time.CompareTo(b.Time));
		foreach (var item in allEntries)
		{
				return item;
		}
		return null;
	}

    public static async Task outputCharacterData0()
    {
        var allEntries = await updateAsync0();
        String output;
        output = "CharacterData Table:\n";
        foreach (var item in allEntries)
        {
            output += DataUtils.CharacterData02String(item) + "\n";
        }
        Debug.Log(output);
    }

	public static async Task outputCharacterData1()
	{
		var allEntries = await updateAsync1();
		String output;
		output = "CharacterData Table:\n";
		foreach (var item in allEntries)
		{
			output += DataUtils.CharacterData12String(item) + "\n";
		}
		Debug.Log(output);
	}
		

    public static IMobileServiceTable<CharacterData0> characterDataTable0
    {
        get
        {
            if (characterDataTable0_useProperty == null)
            {
                characterDataTable0_useProperty = AzureServiceClient.Client.GetTable<CharacterData0>();
            }

            return characterDataTable0_useProperty;
        }
    }

	public static IMobileServiceTable<CharacterData1> characterDataTable1
	{
		get
		{
			if (characterDataTable1_useProperty == null)
			{
				characterDataTable1_useProperty = AzureServiceClient.Client.GetTable<CharacterData1>();
			}

			return characterDataTable1_useProperty;
		}
	}

    private static async Task<List<CharacterData0>> updateAsync0()
    {
        try
        {
            var allEntries = await characterDataTable0.ToListAsync();
            Debug.Assert(allEntries != null, "ToListAsync failed!");
            return allEntries;
        }
        catch (Exception)
        {
            throw;
        }
    }

	private static async Task<List<CharacterData1>> updateAsync1()
	{
		try
		{
			var allEntries = await characterDataTable1.ToListAsync();
			Debug.Assert(allEntries != null, "ToListAsync failed!");
			return allEntries;
		}
		catch (Exception)
		{
			throw;
		}
	}
		
}
