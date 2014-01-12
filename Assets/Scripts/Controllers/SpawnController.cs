using UnityEngine;
using System.Collections;
using Surge.Core;

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
        public float MinSpawnDistance;
        public int NumberOfSpawnPlacementAttempts;
        public bool bSpawningEnabled;

		//private members
		private float m_timeTillNextSpawn;
		private ArrayList SpawnQueue;

		// Use this for initialization
		void Start () {
			SpawnQueue = new ArrayList();

            GameInfo.MusicCtrl.BeatDetectedEvent += onBeatDetected;
            GameInfo.GameStateCtrl.GameStartEvent += onGameStart;
            GameInfo.GameStateCtrl.GameEndEvent += onGameEnd;
		}

		// Update is called once per frame
		void Update () {
			if(m_timeTillNextSpawn > 0)
				m_timeTillNextSpawn -= Time.deltaTime;
		}

		private void onBeatDetected(int subband)
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
            if(!bSpawningEnabled)
                return;

			SpawnQueue.Add(GetPrefabFromSpawnType(type));
		}

		private bool SpawnNext()
		{
			if(SpawnQueue.Count == 0)
				return false;

            Vector3 spawnLoc;

            if( GetValidSpawnLocation(out spawnLoc))
                Spawn((Transform)SpawnQueue[0],spawnLoc);
            else
                Debug.Log("[SpawnController] Failed to spawn enemy, no valid spawn locations found within " +NumberOfSpawnPlacementAttempts+ " attempts");

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

        private bool GetValidSpawnLocation(out Vector3 SpawnLocation)
        {
            int attempts = 0;
            bool bValid = false;
            SpawnLocation = new Vector3(0,0,0);

            while (!bValid)
            {
                //dont return anything if we take to long to find valid location
                if( attempts++ > NumberOfSpawnPlacementAttempts)
                    return false;

                SpawnLocation = Stage.GetRandomLocationInStage();
                bValid = true;

                foreach( GameObject GO in GameObject.FindGameObjectsWithTag("Enemy") )
                {
                    if ( (SpawnLocation - GO.transform.position).magnitude < MinSpawnDistance )
                        bValid = false;
                    
                }
            }

            return true;
        }

        public void ClearEnemies()
        {
            foreach( GameObject GO in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(GO);

            SpawnQueue.Clear();
        }

        public void StartSpawning()
        {
            InvokeRepeating("AddAsteroidToQueue", 4, TimeBetweenSpawns);
        }
        public void StopSpawning()
        {
            CancelInvoke("AddAsteroidToQueue");
        }

        #region Notifications and Event listeners

        void onGameStart()
        {
            StartSpawning();
        }

        void onGameEnd()
        {
            StopSpawning();
            ClearEnemies();
        }

        #endregion
	}
}