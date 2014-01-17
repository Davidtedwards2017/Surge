using UnityEngine;
using System.Collections;

namespace Surge.Core
{
    
    public class DirectionalForceZone : MonoBehaviour {
        
        public Vector3 Direction;
        public float ForceAmt;
        
        
        void OnTriggerStay(Collider other) 
        {
            
            if( other.transform.tag.Equals("Enemy") ||
               other.transform.tag.Equals("Player"))
            {
                
                if (other.attachedRigidbody)
                    other.attachedRigidbody.AddForce(Direction * ForceAmt);
            }
            
        }
        
        // Use this for initialization
        void Start () {
            
        }
        
        // Update is called once per frame
        void Update () {
            
        }
    }
}