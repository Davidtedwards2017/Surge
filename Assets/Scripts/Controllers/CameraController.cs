using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	//public members
	public GameObject Player;

	public float VelocityOffsetScale;
	public float MaxOffset;
	public float EdgeDistance; 
	public float MaxCameraDistance;
	
	//private members
	private float m_BaseCameraDistance;
	private PlayerController m_Player;
	private float m_CurrentCameraDistance;

	// Use this for initialization
	void Start () {
		//TODO: subscribe to player death and spawn messages messages
		NotificationCenter.DefaultCenter.AddObserver(this, "onPlayerSpawn");
		
		findPlayerToAttachTo();
		
		m_BaseCameraDistance = Vector3.Distance(Player.transform.position, transform.position);
	}
	

	
	// Update is called once per frame
	void Update () {
		camera.transform.position = GetCameraFocus();
	}
	
	bool findPlayerToAttachTo()
	{
		if(Player == null)
			Player = GameObject.Find("Player");
		
		m_Player = Player.GetComponent<PlayerController>();
		return (m_Player != null) ? true : false;
	}
	
	void ResetPosition()
	{
	}
		
	void onPlayerSpawn()
	{
		if(findPlayerToAttachTo())
			Debug.Log("camera attached found player");
	}
	
	Vector3 GetCameraFocus()
	{
		Vector3 center;
		
		center = Player.transform.position + GetVelocityOffset();
		center.y = m_BaseCameraDistance;

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
		Vector3 VelocityOffset = VelocityOffsetScale * m_Player.rigidbody.velocity;
		
		if(VelocityOffset.magnitude > MaxOffset)
			VelocityOffset = VelocityOffset.normalized * MaxOffset;
		
		return VelocityOffset;
	}
			
}
