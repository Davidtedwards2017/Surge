using UnityEngine;
using System.Collections;

namespace Surge.Weapons
{
    
    public class MachineGun : Gun {
        
        //public members
        public Transform BulletPrefab;

        // Update is called once per frame
        void LateUpdate () {

        }


        protected override void SpawnProjectile()
        {
            Transform t = Instantiate(BulletPrefab, GunSocket.position, GunSocket.rotation) as Transform;
            SurgeProjectile projectile = t.GetComponent<SurgeProjectile>() as SurgeProjectile;
            projectile.Source = this.gameObject;
            projectile.StartProjectile(transform.forward);
            
        }
        
    }
}
