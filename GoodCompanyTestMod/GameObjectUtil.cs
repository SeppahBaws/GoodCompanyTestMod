using System.Collections.Generic;
using UnityEngine;

namespace GoodCompanyTestMod
{
	// Thanks to BloodyPenguin and dynamoid from the Cities: Skylines modding community,
	// this class is based on ModTools' GameObjectUti class
	// https://github.com/bloodypenguin/Skylines-ModTools/blob/master/Debugger/Explorer/GameObjectUtil.cs
	internal static class GameObjectUtil
	{
		public static List<GameObject> FindSceneRoots()
		{
			var roots = new List<GameObject>();

			foreach (var obj in Object.FindObjectsOfType<GameObject>())
			{
				if (!roots.Contains(obj.transform.root.gameObject))
				{
					roots.Add(obj.transform.root.gameObject);
				}
			}

			return roots;
		}
	}
}