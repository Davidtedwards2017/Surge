using UnityEngine;
using System.Collections;
using Surge.Core;

public class SurgeActor : MonoBehaviour {
	
	//public members
	public float MaxSpeed;
	public float Health;
	public Vector3 velocity;
    public Rigidbody RB;
    public int PointRewardAmt;

	//private members
	protected float m_Speed;
	
	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>() as Rigidbody;
		this.Initalize();
	}



	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}

	protected virtual void Initalize()
	{
	}
	
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

	protected virtual void OnCollisionEnter(Collision collision)
	{
		//hit another Enemy or Wall
		if(collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Wall"))
		{
			velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
		}
	}

	protected void TakeDamage(float amount)
	{
		Health -= amount;

		if(Health <= 0)
			Explode();
	}

	protected virtual void Explode()
	{

	}




	
}
