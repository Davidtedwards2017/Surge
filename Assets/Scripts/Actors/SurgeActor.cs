using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Actors
{
    public class SurgeActor : MonoBehaviour {
    	
        private Rigidbody m_RigidBody;

    	//public members
    	public float MaxSpeed;
    	public float Health;
    	public Vector3 velocity;
        public float mass;

        public Rigidbody RB
        {
            get
            {
                if(m_RigidBody == null)
                    m_RigidBody = GetComponent<Rigidbody>() as Rigidbody;

                return m_RigidBody;
            }
        }
        public int PointRewardAmt;
    	public float Speed;
    	
    	// Use this for initialization
    	void Start () {

    		this.Initalize();
    	}
        // Update is called once per frame
    	void Update () {

            //UpdateMovement();
    	}

        public void SetVelocity(Vector3 vect)
        {
            Speed = vect.magnitude;
            SetDirection(vect.normalized);
        }

        public void SetDirection(Vector3 Direction)
        {
            transform.rotation = Quaternion.Euler(Direction);
        }
        
        protected virtual void UpdateMovement()
        {
            Vector3 location = transform.position;

            location += transform.forward * Speed * Time.deltaTime;
                //velocity * Time.deltaTime;
            location.y = 0;
            transform.position = location;
        }

    	protected virtual void Initalize()
    	{
    	}
    	

        /*
    	protected virtual void OnTriggerEnter(Collider collider)
    	{
    		if(collider.gameObject.tag.Equals("Projectile"))
    		{
    			SurgeProjectile projectile = collider.gameObject.GetComponent<SurgeProjectile>();

    			//ignore collisions from your own projectiles
    			if(projectile.Source == this.gameObject)
    				return;

    			TakeDamage(projectile.dmg);
    		} 
    	}
    */
    	protected virtual void OnCollisionEnter(Collision collision)
    	{
    		//hit another Enemy or Wall
    		if(collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Wall"))
    		{
                Bounce(collision.contacts[0].normal);
    		}
    	}

        protected virtual void Bounce(Vector3 normal)
        {
            Vector3 newDirection = Vector3.Reflect(transform.forward, normal);
            transform.rotation = Quaternion.Euler(newDirection);
        }

    	public void TakeDamage(float amount)
        {
    		Health -= amount;

    		if(Health <= 0)
    			Explode();
    	}

    	protected virtual void Explode()
    	{

    	}

    }
}
