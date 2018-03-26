using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class sanityTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TestMain();
	}

	private void TestMain()
	{
		TestConnection ();
		TestConnection2 ();
	}

	private void TestConnection()
	{
		var testData = new CharacterData0 {
			rotationY=0,
		};
		var up1 = DataSync.uploadPlayerData0 (testData);
		var testData2 = new CharacterData1 {
			x=0, y=0, z=0,
			rotationY=0,
		};
		var up2 = DataSync.uploadPlayerData1 (testData2);

		up1.Wait();
		up2.Wait();
		// CharacterData pData = await DataSync.getPlayerData(0);
	}

	private void TestConnection2()
	{
		DataSync.outputCharacterData0().Wait();
		DataSync.outputCharacterData1().Wait();
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
