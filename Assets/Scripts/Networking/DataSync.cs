using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public static class DataSync {

	private static IMobileServiceTable<CharacterData> characterDataTable_useProperty;

	private static long GetCurrentTimeUnix()  
	{  
		TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));  
		long t = (long)cha.TotalSeconds;  
		return t;  
	}

	// upload the characterData
	public static async Task<int> uploadPlayerData(CharacterData cData)
	{
		try
		{
			cData.Time = GetCurrentTimeUnix();
			await uploadAsync(cData);
		}
		catch (Exception) 
		{
			return -1;
		}

		return 0;
	}

	// get the objective characterData
	public static async Task<CharacterData> getPlayerData(int playerID)
	{
		var allEntries = await updateAsync ();
		allEntries.Sort((a,b) => a.Time.CompareTo(b.Time));
		foreach (var item in allEntries)
		{
			if (item.playerId == playerID)
				return item;
		}
		return null;
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

	private static async Task<int> uploadAsync(CharacterData cData)
	{
		var allEntriesBefore = updateAsync ().Result;
		// if successfully update the table, then insert latest data, otherwise, the upload is fail
		if (allEntriesBefore != null) 
		{
			try
			{
				Debug.Log("Uploading player Data to Azure...");

				await characterDataTable.InsertAsync(cData);

				Debug.Log("Finished uploading player data.");
			}
			catch (System.Exception e)
			{
				Debug.Log("Error uploading player data: " + e.Message);
			}
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

		return 0;
	}
}
