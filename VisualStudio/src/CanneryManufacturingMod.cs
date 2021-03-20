using System.IO;
using MelonLoader;
using UnityEngine;

namespace CanneryManufacturing
{

	internal class CanneryManufacturingMod : MelonMod
	{

		public static AssetBundle assetBundle = null;

		public override void OnApplicationStart()
		{
			MemoryStream memoryStream;
			using (Stream stream = Assembly.GetManifestResourceStream("CanneryManufacturing.res.cannerymanufacturing"))
			{
				memoryStream = new MemoryStream((int) stream.Length);
				stream.CopyTo(memoryStream);
			}
			if (memoryStream.Length == 0)
			{
				throw new System.Exception("No data loaded!");
			}
			assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());

			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Settings.OnLoad();
		}
	}
}
