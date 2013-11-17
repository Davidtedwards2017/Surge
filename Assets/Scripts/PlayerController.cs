using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public members
	
	
	//private members
	private bool bCanControl;
	
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnGameStateChanged");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!bCanControl)
			return;
		
		GetPlayerInput();
	}
	
	void GetPlayerInput()
	{
	}
	
	
	public void OnGameStateChanged( object obj )
	{
		Notification notification = obj as Notification;

		GameStateController.GameState NewState = (GameStateController.GameState) notification.data["NewGameState"];
		GameStateController.GameState OldState = (GameStateController.GameState) notification.data["PrevGameState"];
		
		Debug.Log(string.Format("[(0)] GameStateChanged from (1) to (2).",
			this.GetType(), OldState.ToString(), NewState.ToString()));
		
		bCanControl = ( NewState == GameStateController.GameState.PLAYING) ? true : false;
	}
}
