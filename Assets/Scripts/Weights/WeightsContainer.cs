using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weights
{
    public class WeightsContainer : MonoBehaviour
    {
        [SerializeField]private Vector3 offset;
        [SerializeField]private Vector3 checkSize = Vector3.one;
        [SerializeField]private LayerMask detectMask;
        [SerializeField]private Transform connectedLocation;


        private LineRenderer _wireRenderer;

        public void ResetWeights()
        {
            var colliders = Physics.OverlapBox(transform.position + offset, checkSize/2f, transform.rotation, detectMask.value);
            for(int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i].gameObject);
            }
        }

        public List<Rigidbody> GetAllWeights()
        {
            var result = new List<Rigidbody>();
            var colliders = Physics.OverlapBox(transform.position + offset, checkSize/2f, transform.rotation, detectMask.value);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].transform.TryGetComponent<Rigidbody>(out Rigidbody weightRB))
                {
                    result.Add(weightRB);
                }
            }
            return result;
        }

        public float GetTotalMass(List<Rigidbody> weights)
        {
            float result = 0.0f;
            for(int i = 0; i < weights.Count; i++)
            {
                var current = weights[i];
                result += current.mass;
            }
            return result;
        }

        public float GetTotalMass()
        {
            float result = 0.0f;
            var colliders = Physics.OverlapBox(transform.position + offset, checkSize/2f, transform.rotation, detectMask.value);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].transform.TryGetComponent<Rigidbody>(out Rigidbody weightRB))
                {
                    result += weightRB.mass;
                }
            }

            return result;
        }

        private void Awake() {
            _wireRenderer = GetComponent<LineRenderer>();
        }

        private void Update() 
        {
            
            _wireRenderer.SetPosition(0, transform.position);
            _wireRenderer.SetPosition(1, connectedLocation.position);    
        }
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + offset, checkSize);
        }
        
    }
}
