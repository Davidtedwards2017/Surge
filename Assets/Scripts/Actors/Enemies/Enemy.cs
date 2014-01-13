using UnityEngine;
using Surge.Core;

namespace Surge.Actors.Enemies
{
    public class Enemy : SurgeActor {

        int PointAwardAmt;	
        
        protected void AwardPoints()
        {
            Debug.Log("[Enemy]" + name +" Exploded, awarding " +PointRewardAmt+ " points");
            GameInfo.ScoreCtrl.IncreaseScore(PointRewardAmt);
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