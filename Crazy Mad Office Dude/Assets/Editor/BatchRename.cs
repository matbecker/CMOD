using UnityEngine;
using UnityEditor;
using System.Collections;

public class BatchRename : ScriptableWizard {

	public string baseName = "MyObject_";
	public int startNumber = 0;
	public int increment = 1;

	[MenuItem("Edit/Batch Rename...")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard("Batch Rename", typeof(BatchRename), "Rename");
	}
	//called when the window first appears 
	void OnEnable()
	{
		UpdateSelectionHelper();
	}
	//called when the selection changes in the scene
	void OnSelectionChange()
	{
		UpdateSelectionHelper();
	}
	//update selection counter
	void UpdateSelectionHelper()
	{
		//sets the help text of the wizard
		helpString = "";

		//tells you how many objects are selected
		if (Selection.objects != null)
			helpString = "Number of objects selected: " + Selection.objects.Length;

	}
	//rename
	void OnWizardCreate()
	{
		//if no objects are selected than return
		if (Selection.objects == null)
			return;

		int postFix = startNumber;

		//iterate through selected objects
		foreach (Object o in Selection.objects)
		{
			o.name = baseName + postFix;

			postFix += increment;
		}
	}

}
