using Surge.Core;

namespace Surge.Actors
{
    public class Enemy : SurgeActor {

        int PointAwardAmt;	
        
        protected override void Explode()
        {
            GameInfo.ScoreCtrl.IncreaseScore(PointRewardAmt);
        }
    }
}