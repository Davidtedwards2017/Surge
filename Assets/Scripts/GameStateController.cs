using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour {

	//public members
	public enum GameState { UNINITLAIZED, LOADING, MENU, PLAYING };
	
	//private members
	private GameState m_CurrentGameState;
	
	
	public GameState CurrentGameState
	{
		get
		{
			return m_CurrentGameState;
		}
		set
		{
			Hashtable HT = new Hashtable(2);
			HT.Add("NewGameState", value);
			HT.Add("PrevGameState", m_CurrentGameState);
			NotificationCenter.DefaultCenter.PostNotification(this, "OnGameStateChanged",HT);
			
			m_CurrentGameState = value;
		}
	}
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnGameStateChanged");
		CurrentGameState = GameState.UNINITLAIZED;
		
		CurrentGameState = GameState.PLAYING;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}

