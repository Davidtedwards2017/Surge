using UnityEngine;
using System.Collections;

namespace Surge.Controllers
{

	public class SpawnController : MonoBehaviour {

		public enum SpawnType
		{
			Asteroid
		}
		//public members
		public Transform AsteroidPrefab;
		public float TimeBetweenSpawns;

		//private members
		private float m_timeTillNextSpawn;
		private ArrayList SpawnQueue;

		// Use this for initialization
		void Start () {
			SpawnQueue = new ArrayList();

			NotificationCenter.DefaultCenter.AddObserver(this, "onBeatDetected");
			InvokeRepeating("AddAsteroidToQueue", 4, TimeBetweenSpawns);
		}

		// Update is called once per frame
		void Update () {
			if(m_timeTillNextSpawn > 0)
				m_timeTillNextSpawn -= Time.deltaTime;
		}

		private void onBeatDetected()
		{
			if(m_timeTillNextSpawn > 0)
				return;

			if(SpawnNext() && SpawnQueue.Count > 0) 
				m_timeTillNextSpawn = TimeBetweenSpawns;
		}

		public void AddAsteroidToQueue()
		{
			AddToQueue(SpawnType.Asteroid);
		}

		public void AddToQueue(SpawnType type)
		{
			SpawnQueue.Add(GetPrefabFromSpawnType(type));
		}

		private bool SpawnNext()
		{
			if(SpawnQueue.Count == 0)
				return false;

			Spawn((Transform)SpawnQueue[0],Stage.GetRandomLocationInStage());

			SpawnQueue.RemoveAt(0);
			return true;
		}

		private void Spawn(Transform SpawnPrefab, Vector3 location)
		{
			Instantiate(SpawnPrefab, location, Quaternion.identity);
		}

		private Transform GetPrefabFromSpawnType(SpawnType type)
		{
			switch(type)
			{
			case SpawnType.Asteroid:
				return AsteroidPrefab;
			default:
				return null;
			}

		}

	}
}