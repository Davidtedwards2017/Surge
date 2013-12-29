using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Controllers
{

	public class InputController {

		//private members
		private PlayerController m_PlayerController;
		private Vector3 m_MouseScreenLocation;
		
		//public members
		public float DistanceFromCamera;
		
		// Use this for initialization
		public InputController()
		{
			DistanceFromCamera = Vector3.Distance(new Vector3(0,1,0), Camera.main.transform.position);

        }
				
		public bool GetPress()
		{
			switch(GameInfo.CurrentPlatform)
			{
			case Platform.COMPUTER:
				return Input.GetMouseButton(0);
			case Platform.MOBILE:
				return (Input.touchCount > 0) ? true : false;
			default:
                Debug.Log("[InputController] Invalid platform type: " + GameInfo.CurrentPlatform);
				return false;
			}
		}
		
		
		public Vector3 GetAimDirection()
		{
			switch(GameInfo.CurrentPlatform)
			{
			case Platform.COMPUTER:
                return GetMouseAimDirection();
			case Platform.MOBILE:
				return GetMobileTiltAimDirection();
			default:
                Debug.Log("[InputController] Invalid platform type: " + GameInfo.CurrentPlatform);
				return new Vector3(0,0,0);
			}
		}
		
		
		private Vector3 GetMouseAimDirection()
		{
			if (GameInfo.PlayerCtrl == null)
                return new Vector3(0, 0, 0);

            Vector3 playerLocation = GameInfo.PlayerCtrl.PlayerPawn.transform.position;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = GameInfo.CameraCtrl.CurrentCameraYOffset;
                //Vector3.Distance(playerLocation, Camera.main.transform.position);
			
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            return (mousePos - playerLocation).normalized;
		}
		
		private Vector3 GetMobileTiltAimDirection()
		{
			Vector3 direction = Vector3.zero;

			direction.z = Input.acceleration.y;
			direction.x = Input.acceleration.x;

			if(direction.sqrMagnitude > 1)
				direction.Normalize();

			return direction;
		}
	}
}