using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class DataUtils
{
	public static string CharacterData02String (CharacterData0 cData)
	{
		string header = "CData0:";
		string time = "time: " + cData.Time;
		string rotationY = String.Format ("rotationY: ({0})", cData.rotationY);

		return String.Format ("{0} [{1}, {2}]", header, time, rotationY);
	}

	public static CharacterData0 emptyCharacterData0() {
		return new CharacterData0 {
			rotationY=0,
		};
	}

	public static string CharacterData12String (CharacterData1 cData)
	{
		string header = "CData1:";
		string time = "time: " + cData.Time;
		string xyz = String.Format ("xyz: ({0}, {1}, {2})", cData.x, cData.y, cData.z);
		string rotationY = String.Format ("rotationY: ({0})", cData.rotationY);

		return String.Format ("{0} [{1}, {2}, {3}]", header, time, xyz, rotationY);
	}

	public static CharacterData1 emptyCharacterData1() {
		return new CharacterData1 {
			x = 0,
			y = 0,
			z = 0,
			rotationY=0,
		};
	}
}