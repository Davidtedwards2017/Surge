using UnityEngine;
using System.Collections;

namespace Surge.Actors.Enemies
{
    public class Asteroid_Chunk : Asteroid {

    	protected override void Initalize()
    	{
    	}

    	protected override void Explode()
    	{
            AwardPoints();
    		NotificationCenter.DefaultCenter.PostNotification(this, "onEnemyDestroyed");
    		rigidbody.detectCollisions = false;
    		Destroy(gameObject);
    	}

    }
}