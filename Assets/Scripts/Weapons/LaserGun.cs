using UnityEngine;
using System.Collections;

namespace Surge.Weapons
{
    public class LaserGun : Gun 
    {
        //private bool b

        public Transform LaserPrefab;
        public float EffectDuration;
        public float Range;

        void Awake()
        {

        }

        void LateUpdate()
        {

            //m_LaserEffect.SetPosition(0, GunSocket.transform.position);
            //m_LaserEffect.SetPosition(1, (GunSocket.transform.forward * Range) + GunSocket.transform.position);
        }

        protected override void SpawnProjectile()
        {
            if( LaserPrefab == null)
            {
                Debug.LogWarning("[LaserGun] LaserPrefab is not set");
                return;
            }

            Transform t = Instantiate(LaserPrefab, GunSocket.position, GunSocket.rotation) as Transform;
            SurgeProjectile projectile = t.GetComponent<SurgeProjectile>() as SurgeProjectile;
            projectile.Source = this.gameObject;
            projectile.StartProjectile(transform.forward);

        }

    }
}