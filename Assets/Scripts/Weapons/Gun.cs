﻿using UnityEngine;
using System.Collections;
using Surge.Core;
using Surge.Projectiles;

namespace Surge.Weapons
{

    public class Gun : MonoBehaviour {

        //private float m_FireCooldown;
        private float m_LaserFireCooldown;
        private float m_MGBFireCooldown;

    	protected bool m_bActive;
        protected Transform GunSocket;

        public Transform LaserPrefab;
        public Transform MGBPrefab;

        //public float FireCooldownTime = 0.15f;
        public float MGBFireCooldownTime = 0.15f;
        public float LaserFireCooldownTime = 0.15f;
    	
    	// Use this for initialization
    	void Awake () {
    		GunSocket = transform.FindChild("GunSocket");

            GameInfo.PlayerCtrl.PlayerDyingEvent += onPlayerDying;
            GameInfo.MusicCtrl.BeatDetectedEvent += onBeatDetected;
            GameInfo.GameStateCtrl.GameStartEvent += onGameStart;

    	}
    	
        void Update () {
            if( m_LaserFireCooldown > 0.0f)
                m_LaserFireCooldown -= Time.deltaTime;

            if( m_MGBFireCooldown > 0.0f)
                m_LaserFireCooldown -= Time.deltaTime;
        }

        public virtual void ShootLaser()
        {
            if( m_LaserFireCooldown > 0.0f)
                return;

            if (LaserPrefab == null)
            {
                Debug.LogWarning("[Gun] LaserPrefab not set");
                return;
            }

            SpawnProjectile(LaserPrefab);
            m_LaserFireCooldown = LaserFireCooldownTime;
        }

        
        public virtual void ShootMGB()
        {
            if( m_MGBFireCooldown > 0.0f)
                return;
            
            if (MGBPrefab == null)
            {
                Debug.LogWarning("[Gun] MGBPrefab not set");
                return;
            }
            
            SpawnProjectile(MGBPrefab);
            m_MGBFireCooldown = MGBFireCooldownTime;
        }
       
        protected virtual void SpawnProjectile(Transform ProjectilePrefab)
        {
            if( LaserPrefab == null)
            {
                Debug.LogWarning("[LaserGun] LaserPrefab is not set");
                return;
            }
            
            Transform t = Instantiate(ProjectilePrefab, GunSocket.position, GunSocket.rotation) as Transform;
            SurgeProjectile projectile = t.GetComponent<SurgeProjectile>() as SurgeProjectile;
            projectile.Source = this.gameObject;
            projectile.StartProjectile(transform.forward);
            
        }

        #region Notifications and Event listeners

        protected virtual void onGameStart()
        {
            m_bActive = true;
        }

        protected virtual void onBeatDetected(int subband)
        {
            if(m_bActive)
                ShootMGB();
        }
        protected virtual void onPlayerDying()
        {
            m_bActive = false;
        }
        #endregion
    }
}