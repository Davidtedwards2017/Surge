using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour, IInput {

	//private members
	//private Vector2 m_MouseScreenLocation;
	private Vector3 m_MouseWorldLocation;
	
	//public members
	public float DistanceFromCamera;
	
	// Use this for initialization
	void Start () {
		DistanceFromCamera = Vector3.Distance(new Vector3(0,1,0), Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		m_MouseScreenLocation = GetMouseWorldLocation();
	}
		
	public bool GetPress(out Vector2 screenLoc)
	{
		
	}
	
	
	public Vector3 GetAimDirection()
	{
		return if(GameInfo.CurrentPlatform == GameInfo.Platform.COMPUTER)
		{
	}
	
	
	private Vector3 GetMouceAimDirection()
	{
		if(GameInfo.CurrentPlatform == GameInfo.Platform.COMPUTER)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = DistanceFromCamera;
			
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			return mousePos;
		}
	}
	
	private Vector3 GetMobileTiltAimDirection()
	{
	}
}
