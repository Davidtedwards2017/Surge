using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	//public members
	public Transform BulletPrefab;
	public float FireCooldownTime = 0.15f;

	//private members
	private Transform GunSocket;
	private float m_FireCooldown;
	
	// Use this for initialization
	void Start () {
		GunSocket = transform.FindChild("GunSocket");

		NotificationCenter.DefaultCenter.AddObserver(this, "onBeatDetected");
	}
	
	// Update is called once per frame
	void Update () {
		if( m_FireCooldown > 0.0f)
			m_FireCooldown -= Time.deltaTime;
	}
	

	private void onBeatDetected(Notification notification)
	{
		Shoot();
	}

	public void Shoot()
	{
		if( m_FireCooldown > 0.0f)
			return;
		
		SpawnProjectile(transform.forward);
		m_FireCooldown = FireCooldownTime;
	}

	private void SpawnProjectile(Vector3 direction)
	{
		Transform t = Instantiate(BulletPrefab, GunSocket.position, Quaternion.identity) as Transform;
		SurgeProjectile projectile = t.GetComponent<SurgeProjectile>() as SurgeProjectile;
		projectile.Source = this.gameObject;
		projectile.StartProjectile(direction);

	}

}
