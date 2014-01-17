using UnityEngine;
using System.Collections;

namespace Surge.Core
{
    public class ForceExplosion : MonoBehaviour {


        private SphereCollider m_Collider;
        private float m_ExplosionRadius;
        private float m_ExplosionSpeed;
        private float m_ExplosionTime;
        private bool bStarted = false;

        public float MaxExplosiveRadius;
        public float ExplosiveForceAmount;
        public float Duration;

        public SphereCollider ExplosionCollider
        {
            get
            {
                if( m_Collider == null)
                {
                    m_Collider = gameObject.AddComponent<SphereCollider>();
                    m_Collider.isTrigger = true;
                }

                return m_Collider;
            }
        }

        public float ExplosionRadius
        {
            get
            {
                return m_ExplosionRadius;
            }
            set
            {
                transform.FindChild("Sphere").transform.localScale = new Vector3(value,value,value);
                ExplosionCollider.radius = value;
                m_ExplosionRadius = value;
            }
        }

        void Awake()
        {

        }
    	// Use this for initialization
    	void Start () {
    	
    	}
    	
    	// Update is called once per frame
    	void Update () {

            if(!bStarted)
                return;

            m_ExplosionTime += Time.deltaTime;

            ExplosionRadius = Mathf.Lerp(0, MaxExplosiveRadius, m_ExplosionSpeed * m_ExplosionTime);

            if(m_ExplosionTime >= Duration)
                EndExplosion();
    	}

        public void StartExplosion()
        {
            m_ExplosionSpeed = (MaxExplosiveRadius / Duration) * Time.deltaTime;
            bStarted = true;
        }

        public void EndExplosion()
        {
            Destroy(this.gameObject);
        }

        void OnTriggerStay(Collider other) 
        {

            if( other.transform.tag.Equals("Enemy") ||
               other.transform.tag.Equals("Player"))
            {
                if (other.attachedRigidbody)
                    other.attachedRigidbody.AddExplosionForce(ExplosiveForceAmount, transform.position, ExplosionRadius);
            }
            
        }
    }
}
