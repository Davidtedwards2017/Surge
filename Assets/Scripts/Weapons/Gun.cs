using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Weapons
{

    public class Gun : MonoBehaviour {

        private float m_FireCooldown;

    	protected bool m_bActive;
        protected Transform GunSocket;


        public float FireCooldownTime = 0.15f;
    	
    	// Use this for initialization
    	void Start () {
    		GunSocket = transform.FindChild("GunSocket");

            GameInfo.PlayerCtrl.PlayerDyingEvent += onPlayerDying;
            GameInfo.MusicCtrl.BeatDetectedEvent += onBeatDetected;

    		onGameStart ();
    	}
    	
        void Update () {
            if( m_FireCooldown > 0.0f)
                m_FireCooldown -= Time.deltaTime;
        }

        public virtual void Shoot()
        {
            if( m_FireCooldown > 0.0f)
                return;
            
            SpawnProjectile();
            m_FireCooldown = FireCooldownTime;
        }

        protected virtual void SpawnProjectile()
        {
        }

        #region Notifications and Event listeners

        protected virtual void onGameStart()
        {
            m_bActive = true;
        }

        protected virtual void onBeatDetected(int subband)
        {
            if(m_bActive)
                Shoot();
        }
        protected virtual void onPlayerDying()
        {
            m_bActive = false;
        }
        #endregion
    }
}