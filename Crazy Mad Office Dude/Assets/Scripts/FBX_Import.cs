using UnityEngine;
using UnityEditor;
using System.Collections;

public class FBX_Import : AssetPostprocessor {

	public const float scale = 1.0f;

	//called every time the a model is imported into unity
	void OnPreprocessModel()
	{
		ModelImporter importer = assetImporter as ModelImporter;
		importer.globalScale = scale;
	}
}
