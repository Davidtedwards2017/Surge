using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Controllers
{
	public enum GameState { UNINITLAIZED, LOADING, STARTSCREEN, PLAYING, ENDSCREEN };

	public class GameStateController : MonoBehaviour {

		//private members
		private GameState m_CurrentGameState;
		
        //public members
        public delegate void GameStateChangedEventHandler(GameState NewState, GameState OldState);
        public event GameStateChangedEventHandler GameStateChanged;

		public GameState CurrentGameState
		{
			get
			{
				return m_CurrentGameState;
			}
			set
			{
                //dont switch to state we are already in
                if(m_CurrentGameState == value)
                    return;
              
                onGameStateChanged(value, CurrentGameState);
				m_CurrentGameState = value;

				Debug.Log("[GameStateController] GameState changed to " + m_CurrentGameState);
			}
		}
        void onGameStateChanged(GameState NewState, GameState OldState)
        {
            if (GameStateChanged != null)
                GameStateChanged(NewState, OldState);
        }

		// Use this for initialization
		void Start () {
            GameInfo.PlayerCtrl.PlayerDeathEvent += onPlayerDeath;
            CurrentGameState = GameState.UNINITLAIZED;
		}

        public void StartGame()
        {
            CurrentGameState = GameState.PLAYING;
        }
        
        public void OpenStartMenu()
        {
            CurrentGameState = GameState.STARTSCREEN;
        }
        
        public void StartLoading()
        {
            CurrentGameState = GameState.LOADING;
            
            //TODO loading sequence goes here
            Invoke ("OpenStartMenu", 1);
        }

        #region Notifications and event listeners

        void onPlayerDeath()
        {
            Debug.Log ("[GameInfo] Player death detected");
            CurrentGameState = GameState.ENDSCREEN;
        }
        
        #endregion
	}
}

