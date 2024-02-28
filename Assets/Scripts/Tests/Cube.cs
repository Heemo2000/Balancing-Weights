using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tests
{
    public class Cube : MonoBehaviour
    {
        [SerializeField]private float destroyTime = 5.0f;
        [SerializeField]private Vector3 centreOfMass = Vector3.zero;

        private Rigidbody cubeRB;
        private IEnumerator DestroyCube()
        {
            yield return new WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }
        private void Awake() {
            cubeRB = GetComponent<Rigidbody>();
        }
        // Start is called before the first frame update
        void Start()
        {
            cubeRB.centerOfMass = centreOfMass;
            StartCoroutine(DestroyCube());
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + centreOfMass,Vector3.one * 0.1f);    
        }
    }
}
