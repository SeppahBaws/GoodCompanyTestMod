// using HarmonyLib;
// using BepInEx;
// using BepInEx.Harmony;
//
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using GoodCompany;
// using GoodCompany.GUI;
// using UnityEngine.Events;
//
// namespace GoodCompanyTestMod
// {
// 	public class ControllerInputData
// 	{
// 		public PlayerMovementInput input;
// 		public PlayerMovementSender sender;
// 	}
//
// 	[HarmonyPatch(typeof(PlayerMovement))]
// 	[HarmonyPatch("PlayerMovement")]
// 	public class PlayerMovement_Constructor_Patch
// 	{
// 		static void Postfix(SessionManager ___session)
// 		{
// 			ModManager.Instance.ControllerInput.input = new PlayerMovementInput();
// 			ModManager.Instance.ControllerInput.sender = new PlayerMovementSender(___session);
// 		}
// 	}
//
//
// 	[HarmonyPatch(typeof(PlayerMovement))]
// 	[HarmonyPatch("Initialize")]
// 	public class PlayerMovement_Activate_Patch
// 	{
// 		static void Postfix(PlayerMovement __instance)
// 		{
// 			ModManager.Instance.ControllerInput.input.
// 		}
// 	}
// }