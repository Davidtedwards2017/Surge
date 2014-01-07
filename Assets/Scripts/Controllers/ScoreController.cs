using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Controllers
{

    public class ScoreController : MonoBehaviour {

        //private members
        public int m_Score;
        private int m_Multiplier;

        public int MaxMultiplier;

        //public members
        public int Score
        { 
            get { return m_Score; }
            set { m_Score = value; }
        }
        public int Multiplier
        { 
            get { return m_Multiplier; } 
            set { m_Multiplier = value; }
        }

    	// Use this for initialization
    	void Start () {
            GameInfo.GameStateCtrl.GameStartEvent += onGameStart;
            GameInfo.GameStateCtrl.GameEndEvent += onGameEnd;
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        public void IncreaseMultipier(int amt)
        {
            if (Multiplier < MaxMultiplier)
                Multiplier += amt;
        }

        public void ResetMultipier()
        {
            m_Multiplier = 1;
        }

        public void IncreaseScore(int amt)
        {
            m_Score += (amt * Multiplier);
        }

        public void ResetScore()
        {
            Score = 0;
        }

        #region Notifications and Event listeners
        
        void onGameStart()
        {
            ResetScore();
            ResetMultipier();
        }
        
        void onGameEnd()
        {
            ResetMultipier();
        }
        
        #endregion
    }
}