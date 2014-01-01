using UnityEngine;
using System.Collections;
using Surge.Core;

public class Gun : MonoBehaviour {

	//public members
	public Transform BulletPrefab;
	public float FireCooldownTime = 0.15f;

	//private members
	private bool m_bActive;
	private Transform GunSocket;
	private float m_FireCooldown;
	
	// Use this for initialization
	void Start () {
		GunSocket = transform.FindChild("GunSocket");

        GameInfo.PlayerCtrl.PlayerDyingEvent += onPlayerDying;
        GameInfo.MusicCtrl.BeatDetectedEvent += onBeatDetected;

		onGameStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if( m_FireCooldown > 0.0f)
			m_FireCooldown -= Time.deltaTime;
	}
	
	private void onGameStart()
	{
		m_bActive = true;
	}
	private void onBeatDetected(int subband)
	{
		if(m_bActive)
			Shoot();
	}
	private void onPlayerDying()
	{
		m_bActive = false;
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
