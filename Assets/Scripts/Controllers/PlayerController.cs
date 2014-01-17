using UnityEngine;
using System.Collections;
using Surge.Core;
using Surge.Actors;

namespace Surge.Controllers
{

    public class PlayerController : MonoBehaviour, IController {

        //private members
        private bool bCanControl;
        private GameObject m_PlayerGameObject;

		//public members
        public GameObject PlayerPrefab;
        public PlayerPawn PlayerPawn;

        public GameObject PlayerGameObject
        {
            get
            {
                if(m_PlayerGameObject == null)
                    m_PlayerGameObject = GameObject.FindGameObjectWithTag("Player");

                return m_PlayerGameObject;
            }
        }

        #region Events declarations

        public delegate void PlayerDyingEventHandler();
        public delegate void PlayerDeathEventHandler();
        public delegate void PlayerSpawnedEventHander();

        public event PlayerDyingEventHandler PlayerDyingEvent;
        public event PlayerDeathEventHandler PlayerDeathEvent;
        public event PlayerSpawnedEventHander PlayerSpawnedEvent;

        void onPlayerDying() { if (PlayerDyingEvent != null) PlayerDyingEvent(); }
        void onPlayerDeath() { if (PlayerDeathEvent != null) PlayerDeathEvent(); }
        void onPlayerSpawned() { if (PlayerSpawnedEvent != null) PlayerSpawnedEvent(); }

        #endregion

		void Start () 
        {
            GameInfo.GameStateCtrl.GameStateChanged += onGameStateChanged;
            bCanControl = false;
		}
		
		void Update () {

            if(!bCanControl)
                return;
            
            GetPlayerInput();
		}
		
        void GetPlayerInput()
        {
            if( PlayerPawn == null)
                return;

            Vector3 Direction = GameInfo.InputCtrl.GetAimDirection();
            Direction.y = PlayerPawn.transform.position.y;

            PlayerPawn.RotatePawn(Direction);

            if (GameInfo.InputCtrl.GetPress())
                PlayerPawn.ActivateThrusters();
        }
		
        public bool SpawnPlayerPawn()
        {
            Vector3 SpawnLocation;
                        
            if( PlayerGameObject == null)
            {
                Debug.Log("[PlayerController] No PlayerGameObject detected");

                SpawnLocation = new Vector3(0,0,0);
                Instantiate(PlayerPrefab, SpawnLocation, Quaternion.identity);

                if( PlayerGameObject == null)
                {
                    Debug.LogError("[PlayerController] Failed to spawn PlayerGameObject from prefab");
                    return false;
                }

                Debug.Log("[PlayerController] Successfully spawned PlayerGameObject");
            }
            else
            {
                Debug.Log("[PlayerController] PlayerGameObject already exists");
            }

            PlayerPawn = PlayerGameObject.GetComponent<PlayerPawn>() as PlayerPawn;

            if( PlayerPawn == null)
            {
                Debug.LogError("[PlayerController] Failed to retrieve PlayerPawn script from PlayerGameObject");
                return false;
            }

            Debug.Log("[PlayerController] Successfully retrieved PlayerPawn script");
            
            PlayerPawn.PlayerCtrl = this;
            onPlayerSpawned();
            return true;
        }

        public void PawnDying()
        {
            onPlayerDying();
            bCanControl = false;
        }

        public void PawnDeath()
        {
            Debug.Log("[PlayerController] Destroying PlayerGameObject");
            Destroy(PlayerGameObject);
            PlayerPawn = null;
            m_PlayerGameObject = null;

            onPlayerDeath();
        }

        //return true when 
        public bool ReadyToStartGame()
        {
            if(PlayerPawn == null||
               PlayerGameObject == null)
                return false;

            Debug.Log("[PlayerController] PlayerController is Ready to StartGame");

            return true;
        }

        #region Notifications and Event listeners

        void onGameStateChanged(GameState newState, GameState oldState)
        {
            Debug.Log("[PlayerController] GameStateChanged from " + oldState + " to " + newState);
            
            if (newState == GameState.PLAYING)
            {
                SpawnPlayerPawn();
                bCanControl = true;
            }
        }

        #endregion

	}
}