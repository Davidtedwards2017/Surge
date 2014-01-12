using UnityEngine;
using System.Collections;
using Surge.Actors;
using Surge.Core;

namespace Surge.Actors.Enemies
{
    public class Seeker : Enemy 
    {
        private const float SEEK_TARGET_SEARCH_INTERVAL = 3.0f;

        private GameObject m_SeekingTarget;
        private Vector3 m_TargetVelocity;
        private bool bFoundSeekingTarget;
        
        public bool bSeeking;
        public float TurningRate;
        public float Thrust;

    	// Use this for initialization
        protected override void Initalize()
        {
            bFoundSeekingTarget = false;
            InvokeRepeating("SearchForSeekingTarget", 0, SEEK_TARGET_SEARCH_INTERVAL);

            GameInfo.PlayerCtrl.PlayerDyingEvent += OnPlayerDying;
    	}
    	
    	// Update is called once per frame
    	void Update () {

            if(bSeeking && bFoundSeekingTarget)
                UpdateDirection();

            UpdateMovement();
    	}

        void UpdateDirection()
        {
            Vector3 Direction = (m_SeekingTarget.transform.position - transform.position).normalized;

            transform.rotation = Quaternion.Lerp(
                gameObject.transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * TurningRate);
        }

        void SearchForSeekingTarget()
        {
            m_SeekingTarget = GameInfo.Player;
                
            if( m_SeekingTarget != null)
            {
                CancelInvoke("SearchForSeekingTarget");
                bFoundSeekingTarget = true;
            }

        }

        protected override void UpdateMovement()
        {

            if(RB == null)
                return;


            RB.AddForce(transform.forward * Thrust);

        }

        #region Events and Notifications
        
        protected void OnPlayerDying()
        {
            bSeeking = false;
        }

        
        #endregion
    }
}