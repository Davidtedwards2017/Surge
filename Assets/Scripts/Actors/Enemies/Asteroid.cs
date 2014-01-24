using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Actors.Enemies
{
    public class Asteroid : Enemy {

        private float SPAWN_MOMENTUM_SCALE = 15;
    	//public members
    	public Transform[] AsteroidChunkSockets;
    	public Transform AsteroidChunkPrefab;
    	public float AsteroidChunkSpawnSpeed;
    	
        public float minStartingForce;
        public float maxStartingForce;
        public float minStartingTorque;
        public float maxStartingTorque;

        public Transform ExposionPrefab;

    	protected override void Initalize()
    	{
            Vector3 initForce = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized* Random.Range(minStartingForce, maxStartingForce);
            Vector3 initTorque = new Vector3(0, 1, 0) * Random.Range(minStartingTorque, maxStartingTorque);
           
            RB.AddForce(initForce);
            RB.AddTorque(initTorque);
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
            Vector3 parentMomentum;

            parentMomentum = RB.velocity * RB.mass * SPAWN_MOMENTUM_SCALE;

    	    for( int k = 0; k < 4; k ++)
    		{
    			Transform t = Instantiate(AsteroidChunkPrefab, AsteroidChunkSockets[k].position, Quaternion.identity) as Transform;
    			Asteroid_Chunk chunk = t.gameObject.GetComponent<Asteroid_Chunk>() as Asteroid_Chunk;

    			var direction = (t.position - transform.position).normalized;

                chunk.RB.AddForce( parentMomentum / chunk.RB.mass);
                //chunk.SetVelocity(direction * AsteroidChunkSpawnSpeed);

    		}

            Transform Explosion = Instantiate(ExposionPrefab, transform.position, Quaternion.identity) as Transform;
            ForceExplosion explosion = Explosion.gameObject.AddComponent<ForceExplosion>();
            explosion.ExplosiveForceAmount = 500;
            explosion.MaxExplosiveRadius = 15;
            explosion.Duration = 0.05f;
            explosion.StartExplosion();


    	}


    }
}