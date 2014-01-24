using UnityEngine;
using System.Collections;
using Surge.Actors;

namespace Surge.Projectiles
{

    public class MachineGunBulletProjectile : SurgeProjectile {


    	// Use this for initialization
    	void Start () {
    	    
    	}
    	
        void LateUpdate()
        {
            DecaySpeed();
        }

        void DecaySpeed()
        {
            CurrentSpeed = StartingSpeed * (m_RemaingingLifeTime / LifeTime);
        }

        protected override void HitEnemy(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
            velocity = Vector3.Reflect(velocity, hitNormal);
            m_RemaingingLifeTime /= 2;

            SurgeActor ActorScript = hitObject.GetComponent<SurgeActor>();
            if( ActorScript != null)
                ActorScript.TakeDamage(DamageAmount);
        }
        
        protected override void HitWall(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
            velocity = Vector3.Reflect(velocity, hitNormal);
            m_RemaingingLifeTime /= 2;
        }
        
        protected override void HitProjectile(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
            Physics.IgnoreCollision(this.collider, hitObject.collider);
        }
        
        protected override void HitPlayer(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
            Physics.IgnoreCollision(this.collider, hitObject.collider);
        }
    }
}