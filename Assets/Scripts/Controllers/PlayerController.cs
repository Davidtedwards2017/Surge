using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public members
	public float TurningRate;
	public float ThrustAmt;
	public Vector3 direction;

	//private members
	private bool bCanControl = true;
	private InputController m_InputCtrl;
	//private SurgeActor m_Actor;		
	private Rigidbody m_Rigidbody;
	
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnGameStateChanged");
		
		//m_Actor = GetComponent<SurgeActor>();
		m_InputCtrl = new InputController();

		m_Rigidbody = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!bCanControl)
			return;

		GetPlayerInput();
		gameObject.transform.rotation = Quaternion.Lerp(
			gameObject.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * TurningRate);
	}
	
	void GetPlayerInput()
	{
		direction = m_InputCtrl.GetAimDirection();
		direction.y = transform.position.y;

		//gameObject.transform.rotation = Quaternion.LookRotation(direction);
				
		if(m_InputCtrl.GetPress())
			m_Rigidbody.AddForce(transform.forward * ThrustAmt);

	}
	
			
	private IEnumerator RotatePlayer(Vector3 degrees, float rate)
	{
		var startRotation = transform.rotation;
		var endRotation = startRotation * Quaternion.Euler(degrees);
		float t = 0.0f;
		while (t < 1.0f)
		{
			t += Time.deltaTime * rate;
			gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	public void OnGameStateChanged( object obj )
	{
		Notification notification = obj as Notification;

		GameStateController.GameState NewState = (GameStateController.GameState) notification.data["NewGameState"];
		GameStateController.GameState OldState = (GameStateController.GameState) notification.data["PrevGameState"];
		
		Debug.Log(string.Format("[(0)] GameStateChanged from (1) to (2).",
			this.GetType(), OldState.ToString(), NewState.ToString()));
		
		bCanControl = ( NewState == GameStateController.GameState.PLAYING) ? true : false;
	}


	protected void OnCollisionEnter(Collision collision)
	{
		//hit by enemy
		if(collision.gameObject.tag.Equals("Enemy"))
		{
			Death();
		}
	}

	private void Death()
	{
		NotificationCenter.DefaultCenter.PostNotification(this, "OnPlayerDeath");
		bCanControl = false;
		m_Rigidbody.detectCollisions = false;
	}
}
