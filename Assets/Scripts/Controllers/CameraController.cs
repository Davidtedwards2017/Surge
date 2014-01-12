using UnityEngine;
using System.Collections;
using Surge.Actors;
using Surge.Core;

namespace Surge.Controllers
{

	public class CameraController : MonoBehaviour {
		
		//public members
		public float VelocityOffsetScale;
		public float MaxOffset;
		public float EdgeDistance; 
		public float MaxCameraDistance;
        public float CurrentCameraYOffset;
        public GameObject Focus;
        public Rigidbody FocusRigidbody
        {
            get
            {
                if(m_FocusRigidbody == null)
                    m_FocusRigidbody = Focus.GetComponent<Rigidbody>();
                
                return m_FocusRigidbody;
            }
        }

		
		//private members
		private float m_BaseCameraDistance;
		private float m_CurrentCameraDistance;
		private bool m_bFollowPlayer;
        private Rigidbody m_FocusRigidbody;
        private Vector3 m_lastKnownFocusLocation;
 		
        // Use this for initialization
		void Start () {        
            GameInfo.PlayerCtrl.PlayerSpawnedEvent += onPlayerSpawned;
            GameInfo.PlayerCtrl.PlayerDeathEvent += onPlayerDeath;
		}
       
		// Update is called once per frame
		void LateUpdate () {

			if (m_bFollowPlayer)
				camera.transform.position = GetCameraFocus();
		}
		
		bool findPlayerToAttachTo()
		{
            Focus = GameInfo.PlayerCtrl.PlayerGameObject;

            if (Focus == null)
				return false;

            //CurrentCameraYOffset = Vector3.Distance(Focus.transform.position, transform.position);
			return true;
		}
		
		void ResetPosition()
		{
		}

		Vector3 GetCameraFocus()
		{
			Vector3 center;

            center = GetFocusLocation() + GetVelocityOffset();
            center.y = CurrentCameraYOffset;

			if((center.z + EdgeDistance) >= Stage.North.transform.position.z)
			{
				center.z = Stage.North.transform.position.z - EdgeDistance;
			}
			else if((center.z - EdgeDistance) <= Stage.South.transform.position.z)
			{
				center.z = Stage.South.transform.position.z + EdgeDistance;
			}
			
			if((center.x + EdgeDistance) >= Stage.East.transform.position.x)
			{
				center.x = Stage.East.transform.position.x - EdgeDistance;
			}
			else if((center.x - EdgeDistance) <= Stage.West.transform.position.x)
			{
				center.x = Stage.West.transform.position.x + EdgeDistance;
			}
			
			return center;	
		}
		
		Vector3 GetVelocityOffset()
		{
            if( FocusRigidbody == null)
                return new Vector3(0,0,0);

			Vector3 VelocityOffset = VelocityOffsetScale * FocusRigidbody.velocity;
			
			if(VelocityOffset.magnitude > MaxOffset)
				VelocityOffset = VelocityOffset.normalized * MaxOffset;
			
			return VelocityOffset;
		}

        Vector3 GetFocusLocation()
        {
            if( Focus != null)
                 m_lastKnownFocusLocation = Focus.transform.position;

            return m_lastKnownFocusLocation;
        }

        #region Events and Notifications

        void onPlayerSpawned()
        {
            Debug.Log("[CameraController] player spawned");
            if(findPlayerToAttachTo())
            {
                m_bFollowPlayer = true;
                Debug.Log("camera attached found player");
            }
        }

        void onPlayerDeath()
        {
            m_bFollowPlayer = false;
        }
		
        #endregion		
	}
}