using UnityEngine;
using System.Collections;

public class SurgeProjectile : MonoBehaviour {
	//public members
	public Vector3 velocity;
	public float LifeTime;
	public float dmg;
	public float FlightSpeed;
	public GameObject Source;
	
	//private members
	// Use this for initialization
	void Start () {
		this.Initalize();
	}

	protected virtual void Initalize()
	{
	}

	public void StartProjectile(Vector3 direction)
	{
		this.velocity = direction.normalized * FlightSpeed;
	}

	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
		
		if( (LifeTime -= Time.deltaTime) <= 0)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		//if( collider.gameObject.tag.Equals("Wall"))
			Destroy(gameObject);
	}
	
	public void Bounce(Vector3 normal)
	{
		velocity = Vector3.Reflect(velocity, normal);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if( collision.gameObject.tag.Equals("Wall"))
		{
			Bounce(collision.contacts[0].normal);
		}
	}
}
