using UnityEngine;
using System.Collections;
using Surge.Core;

namespace Surge.Weapons
{

    public class Gun : MonoBehaviour {

    	//private members
    	protected bool m_bActive;
        protected Transform GunSocket;
    	
    	// Use this for initialization
    	void Start () {
    		GunSocket = transform.FindChild("GunSocket");

            GameInfo.PlayerCtrl.PlayerDyingEvent += onPlayerDying;
            GameInfo.MusicCtrl.BeatDetectedEvent += onBeatDetected;

    		onGameStart ();
    	}
    	
    	public virtual void Shoot()
    	{
    	}

        #region Notifications and Event listeners

        protected virtual void onGameStart()
        {
            m_bActive = true;
        }

        protected virtual void onBeatDetected(int subband)
        {
            if(m_bActive)
                Shoot();
        }
        protected virtual void onPlayerDying()
        {
            m_bActive = false;
        }
        #endregion
    }
}