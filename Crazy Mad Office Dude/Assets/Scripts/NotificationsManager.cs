using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour {

	private Dictionary<string, List<Component>> listeners = new Dictionary<string, List<Component>>();

	public void AddListener(Component sender, string notificationName)
	{
		//add the listener to the dictionary if it doesnt already exist
		if (!listeners.ContainsKey(notificationName))
			listeners.Add(notificationName, new List<Component>());

		//add object to the listener list
		listeners[notificationName].Add(sender);
	}
	public void PostNotification(Component sender, string notificationName)
	{
		//if the listener doesnt exist in the dictionary then exit
		if (!listeners.ContainsKey(notificationName))
			return;

		//post notification message to all matching listeners
		foreach (Component listener in listeners[notificationName])
			listener.SendMessage(notificationName, sender, SendMessageOptions.DontRequireReceiver);

	}
	public void RemoveListener(Component sender, string notificationName)
	{
		//if the listener doesnt exist then exit
		if (!listeners.ContainsKey(notificationName))
			return;

		for (int i = listeners[notificationName].Count -1; i >= 0; i--)
		{
			//if the instance ID at the name provided matches the component sender passed
			if (listeners[notificationName][i].GetInstanceID() == sender.GetInstanceID())
				listeners[notificationName].RemoveAt(i); //remove at the index found
		}
	}
	//will rm=emove any null references found and regeneerates a new dictionary with remaining valid entries
	public void RemoveRedundancies()
	{
		//creat a new tmp dictionary
		Dictionary<string, List<Component>> tmpListeners = new Dictionary<string, List<Component>>();

		//cycle through all dictionary entries
		foreach (KeyValuePair<string, List<Component>> item in listeners)
		{
			//cycle through all the listeners in the list 
			for (int i = item.Value.Count - 1; i >= 0; i--)
			{
				//remove any ones that are null
				if (item.Value[i] == null)
					item.Value.RemoveAt(i);
			}
			//add any remaining items in this list to the tmp dictionary
			if (item.Value.Count > 0)
				tmpListeners.Add(item.Key, item.Value);

			//replace the old dictionary with the new updated and optimozed one
			listeners = tmpListeners;
		}
	}
	void OnLevelWasLoaded()
	{
		//clean up whenever a new level is loaded
		RemoveRedundancies();
	}

}
