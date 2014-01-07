using UnityEngine;
using System.Collections;

namespace Surge.Actors
{
    public class Asteroid : Enemy {

    	//public members
    	public Transform[] AsteroidChunkSockets;
    	public Transform AsteroidChunkPrefab;
    	public float AsteroidChunkSpawnSpeed;
    	public float minSpawnSpeed;
    	public float maxSpawnSpeed;

    	protected override void Initalize()
    	{
    		//Vector3 initForce;
    		//initForce = new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f));

    		velocity = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(minSpawnSpeed, maxSpawnSpeed);
    		//initForce *= 1000* RB.mass;
    		//this.AddForce(initForce);
    	}
    	
    	protected override void Explode()
    	{
    		//NotificationCenter.DefaultCenter.PostNotification(this, "onEnemyDestroyed");
    		rigidbody.detectCollisions = false;
    		SpawnChunks();
    		Destroy(gameObject);
    	}

    	 
    	private void SpawnChunks()
    	{
    		Vector3 chunkVelocity;

    		for( int k = 0; k < 4; k ++)
    		{
    			Transform t = Instantiate(AsteroidChunkPrefab, AsteroidChunkSockets[k].position, Quaternion.identity) as Transform;
    			//Asteroid_Chunk chunk = t.gameObject.AddComponent<Asteroid_Chunk>();
    			Asteroid_Chunk chunk = t.gameObject.GetComponent<Asteroid_Chunk>() as Asteroid_Chunk;

    			var direction = (t.position - transform.position).normalized;
    			chunk.velocity = direction * AsteroidChunkSpawnSpeed;

    			//chunk.AddForce(force);
    		}
    	}


    }
}