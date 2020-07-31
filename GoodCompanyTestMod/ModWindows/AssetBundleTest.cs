using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GoodCompany.Assets;

namespace GoodCompanyTestMod
{
	public class AssetBundleTest : BaseModWindow
	{
		private AssetBundle _bundle;
		private GameObject _testObject;

		public AssetBundleTest(bool active = true)
		{
			Activated = active;
		}
		public override void OnStart()
		{
			base.OnStart();

			// _bundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "../CustomAssets/mytestbundle"));
			// var prefab = _bundle.LoadAsset<GameObject>("measure_cube");
			// _testObject = GameObject.Instantiate(prefab);
			_testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			// GoodCompany.Assets.
		}

		Rect _windowRect = new Rect(100, 100, 400, 200);
		public override void OnRender()
		{
			base.OnRender();

			_windowRect = GUI.Window(5, _windowRect, RenderObjectWindow, "Measure Cube location");
		}

		private string x = "0.0";
		private string y = "0.0";
		private string z = "0.0";
		private string _assetToSpawnId = "";
		void RenderObjectWindow(int id)
		{
			using (var horizontalScope = new GUILayout.HorizontalScope("box"))
			{
				x = GUILayout.TextField(x);
				y = GUILayout.TextField(y);
				z = GUILayout.TextField(z);
			}

			Vector3 pos = new Vector3();
			bool success = true;

			if (float.TryParse(x, out pos.x) &&
			    float.TryParse(y, out pos.y) &&
			    float.TryParse(z, out pos.z))
			{
				_testObject.transform.position = pos;
			}
			else
			{
				success = false;
			}

			GUILayout.Label(success ? "everything ok!" : "Parsing failed! check input!");


			GUILayout.Space(10);


			GUILayout.Label(SessionManagerStatic.Instance
				? "SessionManagerStatic instantiated."
				: "SessionManagerStatic not instantiated!");

			if (SessionManagerStatic.Instance)
			{
				GUILayout.Label(
					$"{SessionManagerStatic.Instance.AssetCollection.Models.Assets.Count} assets loaded in AssetCollection");

				_assetToSpawnId = GUILayout.TextField(_assetToSpawnId);
				if (GUILayout.Button("Spawn Asset"))
				{
					if (_testObject)
					{
						GameObject.Destroy(_testObject);
					}

					_testObject = GameObject.Instantiate(_assetToSpawnId == ""
						? SessionManagerStatic.Instance.AssetCollection.Models.DefaultValue
						: SessionManagerStatic.Instance.AssetCollection.Models.Find(_assetToSpawnId));
				}

				// IEnumerable<ModelAsset> model =
				// 	SessionManagerStatic.Instance.AssetCollection.Models.Assets.Where(asset =>
				// 		asset.AssetID == "workplaces/tables/bld_tinker_tier01");
				//
				// SessionManagerStatic.Instance.AssetCollection.Models.Find("workplaces/tables/bld_tinker_tier01");
			}

			GUI.DragWindow();
		}
	}
}