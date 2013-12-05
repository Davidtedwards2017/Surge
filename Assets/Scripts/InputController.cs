using UnityEngine;
using System.Collections;

public class InputController {

	//private members
	private PlayerController PlayerController;
	private Vector3 m_MouseScreenLocation;
	
	//public members
	public float DistanceFromCamera;
	
	// Use this for initialization
	public InputController()
	{
		DistanceFromCamera = Vector3.Distance(new Vector3(0,1,0), Camera.main.transform.position);
		PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
	}
			
	public bool GetPress()
	{
		switch(GameInfo.CurrentPlatform)
		{
		case GameInfo.Platform.COMPUTER:
			return Input.GetMouseButton(0);
		case GameInfo.Platform.MOBILE:
			return (Input.touchCount > 0) ? true : false;
		default:
			Debug.Log(string.Format("[(0)] Invalid platform type: (1)", 
				GetType(), GameInfo.CurrentPlatform));
			return false;
		}
	}
	
	
	public Vector3 GetAimDirection()
	{
		Vector3 playerLocation;
		playerLocation = PlayerController.transform.position;
		
		switch(GameInfo.CurrentPlatform)
		{
		case GameInfo.Platform.COMPUTER:
			//return (GetMouseWorldLocation() - playerLocation).normalized;
		case GameInfo.Platform.MOBILE:
			return GetMobileTiltAimDirection();
		default:
			Debug.Log(string.Format("[(0)] Invalid platform type: (1)", 
				GetType(), GameInfo.CurrentPlatform));
			return new Vector3(0,0,0);
		}
	}
	
	
	private Vector3 GetMouseWorldLocation()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Vector3.Distance(PlayerController.transform.position, Camera.main.transform.position);;
		
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		return mousePos;	
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
