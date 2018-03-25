using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class DataUtils
{
	public static string CharacterData2String (CharacterData cData)
	{
		string header = "CData:";
		string time = "time: " + cData.Time;
		string playerID = "playerId:" + cData.playerId;
		string xyz = String.Format ("xyz: ({0}, {1}, {2})", cData.x, cData.y, cData.z);
		string rotationXyz = String.Format ("rotation: ({0}, {1}, {2})", cData.rotationX, cData.rotationY, cData.rotationZ);

		return String.Format ("{0} [{1}, {2}, {3}, {4}]", header, playerID, time, xyz, rotationXyz);
	}

	public static CharacterData emptyCharacterData(int playerId) {
		return new CharacterData {
			playerId=playerId,
			x=0, y=0, z=0,
			rotationX=0,
			rotationY=0,
			rotationZ=0,
		};
	}
}