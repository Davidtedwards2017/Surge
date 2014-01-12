using UnityEngine;
using System.Collections;

namespace Surge.Weapons
{
    
    public class MachineGun : Gun {
        
        //public members
        public Transform BulletPrefab;
        public float FireCooldownTime = 0.15f;
        
        //private members
        private bool m_bActive;
        private Transform GunSocket;
        private float m_FireCooldown;

        // Update is called once per frame
        void Update () {
            if( m_FireCooldown > 0.0f)
                m_FireCooldown -= Time.deltaTime;
        }

        public override void Shoot()
        {
            if( m_FireCooldown > 0.0f)
                return;
            
            SpawnProjectile();
            m_FireCooldown = FireCooldownTime;
        }
        
        private void SpawnProjectile()
        {
            Transform t = Instantiate(BulletPrefab, GunSocket.position, GunSocket.rotation) as Transform;
            SurgeProjectile projectile = t.GetComponent<SurgeProjectile>() as SurgeProjectile;
            projectile.Source = this.gameObject;
            projectile.StartProjectile(transform.forward);
            
        }
        
    }
}
