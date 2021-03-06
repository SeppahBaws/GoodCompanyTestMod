﻿namespace GoodCompanyTestMod
{
	using StreamWriter = System.IO.StreamWriter;
	using UnityEngine;

	public static class SceneDumper
	{
		public static void Dump(GameObject obj, string path)
		{
			using (StreamWriter writer = new StreamWriter(path, false))
			{
				DumpGameObject(obj, writer, "");
			}
		}
		
		// [MenuItem("Debug/Dump Scene")]
		// public static void DumpScene()
		// {
		// 	if ((Selection.gameObjects == null) || (Selection.gameObjects.Length == 0))
		// 	{
		// 		Debug.LogError("Please select the object(s) you wish to dump.");
		// 		return;
		// 	}
		//
		// 	string filename = @"C:\unity-scene.txt";
		//
		// 	Debug.Log("Dumping scene to " + filename + " ...");
		// 	using (StreamWriter writer = new StreamWriter(filename, false))
		// 	{
		// 		foreach (GameObject gameObject in Selection.gameObjects)
		// 		{
		// 			DumpGameObject(gameObject, writer, "");
		// 		}
		// 	}
		// 	Debug.Log("Scene dumped to " + filename);
		// }

		private static void DumpGameObject(GameObject gameObject, StreamWriter writer, string indent)
		{
			writer.WriteLine("{0}+{1}", indent, gameObject.name);

			foreach (Component component in gameObject.GetComponents<Component>())
			{
				DumpComponent(component, writer, indent + "  ");
			}

			foreach (Transform child in gameObject.transform)
			{
				DumpGameObject(child.gameObject, writer, indent + "  ");
			}
		}

		private static void DumpComponent(Component component, StreamWriter writer, string indent)
		{
			writer.WriteLine("{0}{1}", indent, (component == null ? "(null)" : component.GetType().Name));
		}
	}

}