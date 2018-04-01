//using Microsoft.WindowsAzure.MobileServices;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;
//using System;
//using System.Threading;
//using UnityEngine.UI;
//
//public class NetWorkManage : MonoBehaviour
//{
//    // Use this for initialization
//	public CharacterData0 p0Data = DataUtils.emptyCharacterData0();
//	public CharacterData1 p1Data = DataUtils.emptyCharacterData1();
//
//    void Start()
//    {
////        networkLoop();
//    }
//
//    private async void networkLoop()
//    {
//		if (LevelManage.currentPlayerId == 0) {
//			var upTask0 = DataSync.uploadPlayerData0 (p0Data);
//			var downTask1 = DataSync.getPlayerData1 ();
//			while (true) {
//				if (upTask0.IsCompleted) {
//					this.p0Data.Id = Guid.NewGuid ().ToString ();
//					await upTask0;
//					upTask0 = DataSync.uploadPlayerData0 (p0Data);
//				}
//				if (downTask1.IsCompleted) {
//					this.p1Data = await downTask1;
//					downTask1 = DataSync.getPlayerData1 ();
//				}
//				 await Task.Delay (10);
//			}
//		} else if (LevelManage.currentPlayerId == 1) {
//			var upTask1 = DataSync.uploadPlayerData1 (p1Data);
//			var downTask0 = DataSync.getPlayerData0 ();
//			while (true) {
//				if (upTask1.IsCompleted) {
//					this.p1Data.Id = Guid.NewGuid ().ToString ();
//					await upTask1;
//					upTask1 = DataSync.uploadPlayerData1 (p1Data);
//				}
//				if (downTask0.IsCompleted) {
//					this.p0Data = await downTask0;
//					downTask0 = DataSync.getPlayerData0 ();
//				}
//				 await Task.Delay (10);
//			}
//		}
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//    }
//		
//}
