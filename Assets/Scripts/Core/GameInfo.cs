using UnityEngine;
using System.Collections;
using Surge.Controllers;
using Surge.Core.Android;

namespace Surge.Core
{
	public enum Platform {COMPUTER, MOBILE};

	public class GameInfo : MonoBehaviour {
	
		//private members
		private static GameStateController m_GameStateCtrl;
        private static InputController m_InputCtrl;
        private static PlayerController m_PlayerCtrl;
        private static CameraController m_CameraCtrl;
        private static MusicController m_MusicCtrl;
        private static GameObject m_PlayerGameObject;
        private static ScoreController m_ScoreCtrl;

		//public members
		public static Platform CurrentPlatform;
        public static InputController InputCtrl
        {
            get
            {
                if(m_InputCtrl == null)
                    m_InputCtrl = new InputController();

                return m_InputCtrl;
            }
        }
		public static GameStateController GameStateCtrl 
		{
			get 
			{
				if(m_GameStateCtrl == null)
					m_GameStateCtrl = GameObject.FindGameObjectWithTag("Controllers").GetComponent<GameStateController>() as GameStateController;

				return m_GameStateCtrl;
			}
		}
        public static PlayerController PlayerCtrl
        { 
            get
            {
                if (m_PlayerCtrl == null)
                    m_PlayerCtrl = GameObject.FindGameObjectWithTag("Controllers").GetComponent<PlayerController>() as PlayerController;

                return m_PlayerCtrl;
            }
        }
        public static CameraController CameraCtrl
        {
            get
            {
                if(m_CameraCtrl == null)
                    m_CameraCtrl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

                return m_CameraCtrl;  
            }
        }
        public static MusicController MusicCtrl
        {
            get
            {
                if(m_MusicCtrl == null)
                    m_MusicCtrl = GameObject.FindGameObjectWithTag("Controllers").GetComponent<MusicController>();
                
                return m_MusicCtrl;  
            }
        }
        public static ScoreController ScoreCtrl
        {
            get
            {
                if( m_ScoreCtrl == null)
                    m_ScoreCtrl = GameObject.FindGameObjectWithTag("Controllers").GetComponent<ScoreController>();

                return m_ScoreCtrl;
            }
        }
        public static GameObject Player
        { get { return PlayerCtrl.PlayerGameObject; }}

        // Use this for initialization
		void Start () {
            GameStateCtrl.GameStateChanged += onGameStateChanged;
			InitiateForCurrentDevice();
		}
		
		// Update is called once per frame
		void Update () {
		
			if (GameStateCtrl.CurrentGameState == GameState.UNINITLAIZED)
			{
				GameStateCtrl.StartLoading();
			}

		}

        void InitiateForCurrentDevice()
        {
            switch (Application.platform)
            {
            case RuntimePlatform.Android:
                    AndroidInitalization();
                return;
            case RuntimePlatform.IPhonePlayer:
                CurrentPlatform = Platform.MOBILE;
                return;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsWebPlayer:
                CurrentPlatform = Platform.COMPUTER;
                return;
            default:
                return;
            }
        }

        private void AndroidInitalization()
        {
            CurrentPlatform = Platform.MOBILE;
            gameObject.AddComponent<AndroidInfo>();
        }

        private void onGameStateChanged(GameState newState, GameState oldState)
        {
            switch (newState) 
            {
                case GameState.PLAYING:
                    break;
                case GameState.STARTSCREEN:
                    break;
                case GameState.ENDSCREEN:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.UNINITLAIZED:
                    break;
            }

	    }

    }
}