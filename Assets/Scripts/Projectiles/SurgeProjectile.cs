using UnityEngine;
using System.Collections;

public class SurgeProjectile : MonoBehaviour {
	//public members
	public Vector3 velocity;
	public float LifeTime;
	public float DamageAmount;
	public float FlightSpeed;
	public GameObject Source;
	
	//private members
	// Use this for initialization
	void Awake()
    {

        //transform.Rotate((Random.value * 2 * RandomAngle) - RandomAngle, 0
    }

    void Start () {
		this.Initalize();
	}

   	protected virtual void Initalize()
	{
	}

    public virtual void StartProjectile(Vector3 direction)
	{
		this.velocity = direction.normalized * FlightSpeed;
	}

	// Update is called once per frame
	void Update () {
		//transform.position += velocity * Time.deltaTime;
		
		if( (LifeTime -= Time.deltaTime) <= 0)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		//if( collider.gameObject.tag.Equals("Wall"))
			Destroy(gameObject);
	}
	
}
