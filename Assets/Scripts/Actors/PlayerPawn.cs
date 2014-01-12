using UnityEngine;
using System.Collections;
using Surge.Controllers;

namespace Surge.Actors
{
    public class PlayerPawn : SurgeActor 
    {
        //private members
    

        //public members
        public PlayerController PlayerCtrl;
        public float TurningRate;
        public float ThrustAmt;
        public float DyingTime;

    	// Update is called once per frame
    	void Update ()  
        {

    	}
           
        private void Dying()
        {
            PlayerCtrl.PawnDying();
            RB.detectCollisions = false;
            Invoke("Death", DyingTime);    
        }
        private void Death()
        {
            PlayerCtrl.PawnDeath();
        }

        public void ActivateThrusters()
        {
            if (!RB)
                return;

            RB.AddForce(transform.forward * ThrustAmt);
        }

        public void RotatePawn(Vector3 Direction)
        {
            Direction.y = 0;
            transform.rotation = Quaternion.Lerp(
                gameObject.transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * TurningRate);
        }

        #region Events and Notifications

        protected virtual void OnTriggerEnter(Collider collider)
        {
           
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            //hit by enemy
            if(collision.gameObject.tag.Equals("Enemy"))
            {
                Dying();
            }
        }

        #endregion

    }
}
