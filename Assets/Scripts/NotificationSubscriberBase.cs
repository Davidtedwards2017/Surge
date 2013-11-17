using UnityEngine;
using System.Collections;

public class NotificationSubscriberBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected Hashtable GetHashTableData(object obj)
	{
		return obj as Hashtable;
	}
}
