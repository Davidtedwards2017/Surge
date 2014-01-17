using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Actors.Enemies
{
    public class Asteroid : Enemy {

    	//public members
    	public Transform[] AsteroidChunkSockets;
    	public Transform AsteroidChunkPrefab;
    	public float AsteroidChunkSpawnSpeed;
    	public float minSpawnSpeed;
    	public float maxSpawnSpeed;

        public float minStartingForce;
        public float maxStartingForce;
        public float minStartingSpin;
        public float maxStartingSpin;
        public Transform ExposionPrefab;

    	protected override void Initalize()
    	{

            Vector3 initForce = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized* Random.Range(minStartingForce, maxStartingForce);

            Vector3 RandomPoint = new Vector3(Random.Range(-1, 1),0,0);
            Quaternion quat = Quaternion.AngleAxis(Random.Range(1, 360),Vector3.forward);
            RandomPoint = transform.position + (quat * RandomPoint);

            RandomPoint = transform.position;
            RandomPoint.x += 2;

    		RB.AddForceAtPosition(initForce, RandomPoint);
    	}
    	
    	protected override void Explode()
    	{
            AwardPoints();
    		rigidbody.detectCollisions = false;
    		SpawnChunks();
    		Destroy(gameObject);
    	}

    	 
    	private void SpawnChunks()
    	{
    	    for( int k = 0; k < 4; k ++)
    		{
    			Transform t = Instantiate(AsteroidChunkPrefab, AsteroidChunkSockets[k].position, Quaternion.identity) as Transform;
    			Asteroid_Chunk chunk = t.gameObject.GetComponent<Asteroid_Chunk>() as Asteroid_Chunk;

    			var direction = (t.position - transform.position).normalized;

                //chunk.SetVelocity(direction * AsteroidChunkSpawnSpeed);

    		}

            Transform Explosion = Instantiate(ExposionPrefab, transform.position, Quaternion.identity) as Transform;
            ForceExplosion explosion = Explosion.gameObject.AddComponent<ForceExplosion>();
            explosion.ExplosiveForceAmount = 200;
            explosion.MaxExplosiveRadius = 10;
            explosion.Duration = 0.5f;
            explosion.StartExplosion();


    	}


    }
}