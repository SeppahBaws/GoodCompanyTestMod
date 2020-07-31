using GoodCompany.GUI;
using HarmonyLib;

namespace GoodCompanyTestMod
{
	// [HarmonyPatch(typeof(MainMenuController))]
	// [HarmonyPatch("StartGame")]
	// class MainMenuController_StartGame_Patch
	// {
	// 	// Augments the default behaviour to also support launching modded games
	// 	static bool Prefix(MainMenuController __instance,
	// 		MainMenu.StartMode mode,
	// 		bool useLoginWindow,
	// 		ref GameRoot ____root)
	// 	{
	// 		BackendThreadManager.StartParams parameters = new BackendThreadManager.StartParams();
	// 		switch (mode)
	// 		{
	// 			case MainMenu.StartMode.Singleplayer:
	// 				parameters.Mod = "base/freeplay";
	// 				break;
	//
	// 			case MainMenu.StartMode.SingleplayerWithMod:
	// 				parameters.Mod = "custom/freeplay";
	// 				break;
	// 		}
	//
	// 		____root.InitializeSession(mode, parameters, useLoginWindow);
	//
	// 		return false; // skip the original method
	// 	}
	// }

	[HarmonyPatch(typeof(MainMenuController))]
	[HarmonyPatch("StartHeadstartGame")]
	class MainMenuController_StartHeadstartGame_Patch
	{
		static bool Prefix(MainMenuController __instance, ref GameRoot ____root)
		{
			ModLogger.Log("{MainMenuController} -- altered headstart called!");
			BackendThreadManager.StartParams parameters = new BackendThreadManager.StartParams();
			parameters.Savegame = (string)null;
			parameters.Mod = "custom/freeplay";
			bool useLoginWindow = false;
			____root.InitializeSession(MainMenu.StartMode.SingleplayerWithMod, parameters, useLoginWindow);
			
			return false; // skip the original method
		}
	}
}
