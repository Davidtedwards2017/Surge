using UnityEngine;
using System.Collections;

namespace Surge.Projectiles
{

    public class SurgeProjectile : MonoBehaviour {
    	//public members
    	public float LifeTime;
        public float DamageAmount;
        public float StartingSpeed;
    	public float CurrentSpeed;
    	public GameObject Source;


        public Vector3 direction;
        //public Vector3 velocity;
            
        public Vector3 velocity
        {
            get
            {
                return direction * CurrentSpeed;
            }
            set
            {
                direction = value.normalized;
                CurrentSpeed = value.magnitude;
            }
        }


    	
    	//private members
        protected float m_RemaingingLifeTime;

    	void Awake()
        {
            CurrentSpeed = StartingSpeed;
            m_RemaingingLifeTime = LifeTime;
            //transform.Rotate((Random.value * 2 * RandomAngle) - RandomAngle, 0
        }

        void Start () {
    	}


        public virtual void StartProjectile(Vector3 direction)
    	{
            //velocity = direction * CurrentSpeed;
            this.direction = direction;
    	}

    	// Update is called once per frame
    	void Update () {

            //make sure bullet wont miss collision if traveling fast
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, CurrentSpeed * Time.deltaTime + 0.1f))
                ProjectileHit( hit.collider.gameObject, hit.normal, hit.normal);

            transform.position += velocity * Time.deltaTime;
                		
            if( (m_RemaingingLifeTime -= Time.deltaTime) <= 0)
    			Destroy(gameObject);
    	}

        protected void OnCollisionEnter (Collision collision) 
        {
            ProjectileHit( collision.gameObject, collision.contacts[0].point, collision.contacts[0].normal);
            return;

            if (collision.gameObject.tag == "Projectile" ||
                collision.gameObject.tag == "Player") 
            {
                Physics.IgnoreCollision(collision.collider, collider);    

            } else if( collision.gameObject.tag == "Enemy" ||
                       collision.gameObject.tag == "Wall")
            {

                velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
            } 
        }

        private void ProjectileHit(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
            switch (hitObject.tag)
            {
                case "Wall":
                    HitWall(hitObject, hitLoc, hitNormal);
                    return;
                case "Player":
                    HitProjectile(hitObject, hitLoc, hitNormal);
                    return;
                case "Enemy":
                    HitEnemy(hitObject, hitLoc, hitNormal);
                    return;
                case "Projectile":
                    HitProjectile(hitObject, hitLoc, hitNormal);
                    return;
            }
        }

        protected virtual void HitEnemy(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
        }

        protected virtual void HitWall(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
        }

        protected virtual void HitProjectile(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
        }

        protected virtual void HitPlayer(GameObject hitObject, Vector3 hitLoc, Vector3 hitNormal)
        {
        }

               


    	
    	void OnTriggerEnter(Collider collider)
    	{

    	}
    	
    }
}