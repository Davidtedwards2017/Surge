using UnityEngine;
using System.Collections;

public class SurgeActor : MonoBehaviour {
	
	//public members
	public float MaxSpeed;
	public float Health;
	public float Mass;
	public float Drag;
	public Vector3 velocity;
	/*
	public Rigidbody RB
	{
		get
		{
			if( m_Rigidbody == null)
				m_Rigidbody = gameObject.GetComponent<Rigidbody>();

			return m_Rigidbody;
		}
	}
	*/
	//private members
	protected float m_Speed;
	protected Rigidbody m_Rigidbody;


	// Use this for initialization
	void Start () {

		this.Initalize();
	}



	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
		/*
		if(transform.position.y != 0)
		{
			Debug.Log("[SurgeActor]" + this.name + " is has invalid Y:" + transform.position.y);
			Vector3 vec;
			vec.x = transform.position.x;
			vec.z = transform.position.z;
			vec.y = 0;
			transform.position = vec;
		}
		*/
	}

	protected virtual void Initalize()
	{
	}
	
	protected void OnTriggerEnter(Collider collider)
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

	protected void OnCollisionEnter(Collision collision)
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
