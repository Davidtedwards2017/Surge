using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Controllers
{

	public class InputController {

		//private members
		private Vector3 m_MouseScreenLocation;
        private Plane plane = new Plane(Vector3.up, Vector3.zero);
		
		//public members
        public Vector3 MouseHitWorldLocation;
		
		// Use this for initialization
		public InputController()
		{

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
            UpdateMouseHitWorldLocation();

            if(GameInfo.PlayerCtrl.PlayerPawn == null)
                return Vector3.zero;

            return (MouseHitWorldLocation - GameInfo.PlayerCtrl.PlayerPawn.transform.position).normalized;
        }

		private void UpdateMouseHitWorldLocation()
		{
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = GameInfo.CameraCtrl.CurrentCameraYOffset;
               			
            MouseHitWorldLocation = Camera.main.ScreenToWorldPoint(mousePos);

            /*
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float ent = 100f;
            if( plane.Raycast(ray, out ent))
            {
                var hitpoint = ray.GetPoint(ent);
                MouseHitWorldLocation = hitpoint;
            }
            */
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