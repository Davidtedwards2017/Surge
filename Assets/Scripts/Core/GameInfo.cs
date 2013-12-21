using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {
	
	public enum Platform {COMPUTER, MOBILE};
	
	
	//private members
	
	
	//public members
	public static Platform CurrentPlatform;

	// Use this for initialization
	void Start () {
		InitiateForCurrentDevice();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void InitiateForCurrentDevice()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
		case RuntimePlatform.IPhonePlayer:
			CurrentPlatform = Platform.MOBILE;
			break;
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.WindowsWebPlayer:
				CurrentPlatform = Platform.COMPUTER;
			break;
		default:
			break;
		}
	}
}
