﻿using UnityEngine;
using System.Collections;

public class Asteroid_Chunk : Asteroid {

	protected override void Initalize()
	{
	}

	protected override void Explode()
	{
		NotificationCenter.DefaultCenter.PostNotification(this, "onEnemyDestroyed");
		rigidbody.detectCollisions = false;
		Destroy(gameObject);
	}

}
