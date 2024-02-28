using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weights
{
    public class WeightsContainer : MonoBehaviour
    {
        [SerializeField]private float checkInterval = 1;

        [SerializeField]private Vector3 offset;
        [SerializeField]private Vector3 checkSize = Vector3.one;
        [SerializeField]private Transform forceOrigin;
        [SerializeField]private float distanceFromForceOrigin = 2.0f;
        private Collider[] _colliders;

        private int _collidersCount = 0;

        public Transform ForceOrigin { get => forceOrigin; }

        public float GetTotalMass()
        {
            float result = 0.0f;
            for(int i = 0; i < _collidersCount; i++)
            {
                if(_colliders[i].transform.TryGetComponent<Rigidbody>(out Rigidbody weightRB))
                {
                    result += weightRB.mass;
                }
            }

            return result;
        }

        private IEnumerator ScanWeights()
        {
            while(this.enabled)
            {
                _collidersCount = Physics.OverlapBoxNonAlloc(transform.position + offset, checkSize/2f, _colliders, transform.rotation);
                yield return new WaitForSeconds(checkInterval);
            }
        }

        private void Awake() {
            _colliders = new Collider[10];
        }

        private void Start() 
        {
            StartCoroutine(ScanWeights());    
        }

        private void FixedUpdate() {
            transform.position = forceOrigin.position + Vector3.up * distanceFromForceOrigin;
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + offset, checkSize);    
            Gizmos.color = Color.red;
            Gizmos.DrawLine(forceOrigin.position, forceOrigin.position + Vector3.up * distanceFromForceOrigin);
        }
        
    }
}
