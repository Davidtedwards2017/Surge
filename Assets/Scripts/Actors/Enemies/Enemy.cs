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
    }
}