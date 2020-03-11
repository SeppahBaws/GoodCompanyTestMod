using UnityEngine;

namespace GoodCompanyTestMod
{
	public static class ModLogger
	{
		public static void Log(string msg)
		{
			Debug.Log($"[Test Mod] {msg}");
		}

		public static void LogError(string msg)
		{
			Debug.LogError($"[Test Mod] {msg}");
		}
	}
}