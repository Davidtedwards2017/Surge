using UnityEngine;
using System.Collections;

namespace Surge.Weapons
{
    public class LaserGun : Gun 
    {
        private LineRenderer m_LaserEffect;
        //private bool b

        public float EffectDuration;
        public float Range;

        void Awake()
        {
            m_LaserEffect = transform.FindChild("Laser Effect").GetComponent<LineRenderer>();
        }

        void Update()
        {
            m_LaserEffect.SetPosition(0, GunSocket.transform.position);
            m_LaserEffect.SetPosition(1, (transform.forward).normalized * Range);
        }

        public override void Shoot()
        {

        }
    }
}