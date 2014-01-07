﻿using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Controllers
{
	public enum GameState { UNINITLAIZED, LOADING, STARTSCREEN, PLAYING, ENDSCREEN };

	public class GameStateController : MonoBehaviour {

		//private members
		private GameState m_CurrentGameState;
		
        //public members
        #region Events declarations
        public delegate void GameStateChangedEventHandler(GameState NewState, GameState OldState);
        public delegate void GameLoadingEventHandler();
        public delegate void GameLoadedEventHander();
        public delegate void GameStartEventHandler();
        public delegate void GameEndEventHandler();

        public event GameStateChangedEventHandler GameStateChanged;
        public event GameLoadingEventHandler GameLoadingEvent;
        public event GameLoadedEventHander GameLoadedEvent;
        public event GameStartEventHandler GameStartEvent;
        public event GameStartEventHandler GameEndEvent;

        #endregion

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
        void onGameLoading()
        {
            if(GameLoadingEvent != null)
                GameLoadingEvent();
        }
        void onGameLoaded()
        {
            if(GameLoadedEvent != null)
                GameLoadedEvent();
        }
        void onGameStart()
        {
            if(GameStartEvent != null)
                GameStartEvent();
        }
        void onGameEnd()
        {
            if(GameEndEvent != null)
                GameEndEvent();
        }

		// Use this for initialization
		void Start () {
            GameInfo.PlayerCtrl.PlayerDeathEvent += onPlayerDeath;
            CurrentGameState = GameState.UNINITLAIZED;
		}

        public void StartGame()
        {
            CurrentGameState = GameState.PLAYING;
            onGameStart();
        }
        public void EndGame()
        {
            CurrentGameState = GameState.ENDSCREEN;
            onGameEnd();
        }
        
        public void OpenStartMenu()
        {
            CurrentGameState = GameState.STARTSCREEN;
        }
        
        public void StartLoading()
        {
            CurrentGameState = GameState.LOADING;
            onGameLoading();
            //TODO loading sequence goes here
            Invoke ("DoneLoading", 1);
        }
        public void DoneLoading()
        {
            onGameLoaded();
            OpenStartMenu();
        }

        #region Notifications and event listeners

        void onPlayerDeath()
        {
            Debug.Log ("[GameInfo] Player death detected");
            EndGame();
        }
        
        #endregion
	}
}

