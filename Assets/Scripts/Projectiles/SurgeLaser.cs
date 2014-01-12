using UnityEngine;
using System.Collections;

namespace Surge.Projectiles
{

    public class SurgeLaser : SurgeProjectile {

        //TODO: use animated textures for laser beam effect

       
        private Vector3 TraceEnd;
        private LineRenderer LaserEffect;


        public float MaxDistance;

        void Awake()
        {
           // transform.parent = transform.Find("GunSocket");
            LaserEffect = gameObject.GetComponent<LineRenderer>();
            LaserEffect.SetVertexCount(2);
        }

        protected override void Initalize()
        {

        }


        public override void StartProjectile(Vector3 direction)
        {

            RaycastHit hit;
            Vector3 EndPoint;
            //Vector3 direction = transform.forward;

//            if(GetClosestHitTarget(direction ,out hit)) //something was hit
//            {
//                GameObject hitObject = hit.transform.gameObject;
//                EndPoint = hit.point;
//
//                if(hitObject.tag.Equals("Enemy"))
//                {
//                    SurgeActor HitActorScript = hitObject.GetComponent<SurgeActor>();
//                    HitActorScript.TakeDamage(DamageAmount);
//                }
//            }
//            else
//            {
                EndPoint = direction * MaxDistance;
            //}

            //start laser effect
            LaserEffect.SetPosition(0,transform.position);
            LaserEffect.SetPosition(1,EndPoint);

        }


        public bool GetClosestHitTarget(Vector3 dir, out RaycastHit ClosestHit)
        {
            float ShortestSoFar = Mathf.Infinity;
            bool bFoundHit = false;
            ClosestHit = new RaycastHit();

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, dir, MaxDistance);

            foreach( RaycastHit hit in hits)
            {
                if(Vector3.Distance(transform.position, hit.point) < ShortestSoFar)
                {
                    ClosestHit = hit;
                    bFoundHit = true;
                }
            }

            return bFoundHit;
        }
    }
}
