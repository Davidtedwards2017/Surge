using UnityEngine;
using System.Collections;
using Surge.Core;
using Surge.Core.Debug;

namespace Surge.Controllers
{
	public class UIController : MonoBehaviour {

        //private members
        private InputController m_InputCtrl;
        private DebugGui m_DebugGui;

		//public members
		public bool bDisplayScore;
		public bool bDisplayEndScreen;
		public bool bDisplayStartScreen;
        public bool bDebugOverlay;
/*
        public DebugGui DebugGui
        { 
            get 
            { 
                if(m_DebugGui== null)
                    m_DebugGui = new DebugGui();

                return m_DebugGui;
            }
        }
*/
        public InputController InputCtrl
        {  
            get
            {
                if(m_InputCtrl == null)
                    m_InputCtrl = GameInfo.InputCtrl;

                return m_InputCtrl;
            }
        }

		// Use this for initialization
		void Start () {
            GameInfo.GameStateCtrl.GameStateChanged += onGameStateChanged;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnGUI () 
        {
            //update Gui objects
			if (bDisplayStartScreen) 
			{
				UpdateStartScreen();
                if(InputCtrl.GetPress())
                    GameInfo.GameStateCtrl.StartGame();
			}

			if (bDisplayScore)
				UpdateScore();

			if (bDisplayEndScreen)
            {
                UpdateEndScreen();
                if(InputCtrl.GetPress())
                    GameInfo.GameStateCtrl.StartGame();
            }

            //update debug overlay
            //if(bDebugOverlay)
                //DebugGui.UpdateSliders();

		}

		private void onGameStateChanged(GameState newState, GameState oldState)
		{
			switch (oldState) 
			{
				case GameState.PLAYING:
					hidePlayingUI();
					break;
				case GameState.STARTSCREEN:
					hideStartScreenUI();
					break;
				case GameState.ENDSCREEN:
					hideEndScreenUI();
					break;
				case GameState.LOADING:
					hideLoadingUI();
					break;
				case GameState.UNINITLAIZED:
					break;
			}

			switch (newState) 
			{
				case GameState.PLAYING:
					showPlayingUI();
					break;
				case GameState.STARTSCREEN:
					showStartScreenUI();
					break;
				case GameState.ENDSCREEN:
					showEndScreenUI();
					break;
				case GameState.LOADING:
					showLoadingUI();
					break;
				case GameState.UNINITLAIZED:
					break;
			}


		}

		private void showPlayingUI()
		{
			bDisplayScore = true;
		}
		private void hidePlayingUI()
		{
			bDisplayScore = false;
		}
		private void showStartScreenUI()
		{
			bDisplayStartScreen = true;
		}
		private void hideStartScreenUI()
		{
			bDisplayStartScreen = false;
		}
		private void showEndScreenUI()
		{
			bDisplayEndScreen = true;
		}
		private void hideEndScreenUI()
		{
			bDisplayEndScreen = false;
		}
		private void showLoadingUI()
		{
		}
		private void hideLoadingUI()
		{
		}
		private void UpdateScore()
		{
			GUI.Label (new Rect (10, 10, 150, 50), "Score");
		}
		private void UpdateStartScreen()
		{
			GUI.Label (new Rect (50, 50, 150, 150), "Start Screen");
		}

		private void UpdateEndScreen()
		{
			GUI.Label(new Rect (160,10,150,50), "End Screen");
		}
	}
}