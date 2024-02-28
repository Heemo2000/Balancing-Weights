using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weights
{
    public class BalancingWeight : MonoBehaviour
    {
        [SerializeField]private WeightsContainer leftContainer;
        [SerializeField]private WeightsContainer rightContainer;

        [SerializeField]private float gravity = 9.8f;

        private Rigidbody _balanceRB;

        private float GetRequiredAcceleration(float leftMass, float rightMass)
        {
            if((leftMass + rightMass) == 0)
            {
                return 0.0f;
            }
            return gravity * (rightMass - leftMass)/(leftMass + rightMass);
        }
        
        private void Awake() {
            _balanceRB = GetComponent<Rigidbody>();
        }
        

        // Update is called once per frame
        void FixedUpdate()
        {
            float leftMass = leftContainer.GetTotalMass();
            float rightMass = rightContainer.GetTotalMass();
            
            _balanceRB.AddForceAtPosition(Vector3.down * leftMass * GetRequiredAcceleration(rightMass, leftMass), 
                                            leftContainer.ForceOrigin.position);
            _balanceRB.AddForceAtPosition(Vector3.down * rightMass * GetRequiredAcceleration(leftMass, rightMass), 
                                            rightContainer.ForceOrigin.position);
        }
    }
}
