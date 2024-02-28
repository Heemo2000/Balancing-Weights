using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Game.Weights
{
    public class EquationMaker : MonoBehaviour
    {
        [Header("Equation Settings")]
        [SerializeField]private WeightsContainer leftContainer;
        [SerializeField]private WeightsContainer rightContainer;
        [SerializeField]private TMP_Text equationText;
        [Min(0.5f)]
        [SerializeField]private float checkInterval = 1.0f;


        private string GenerateEquationPart(List<Rigidbody> weights)
        {
            if(weights.Count == 0)
            {
                return "0";
            }

            if(weights.Count == 1)
            {
                return weights[0].mass.ToString();
            }

            string result = "";
            for(int i = 0; i < weights.Count - 1; i++)
            {
                var current = weights[i].mass;
                result += current.ToString() + " + ";
            }

            result += weights[weights.Count - 1].mass.ToString();
            return result;
        }

        private IEnumerator SolveEquation()
        {
            
            while(this.enabled)
            {
                var weightsLeft = leftContainer.GetAllWeights();
                var weightsRight = rightContainer.GetAllWeights();
                 
                var leftWeight = leftContainer.GetTotalMass(weightsLeft);
                var rightWeight = rightContainer.GetTotalMass(weightsRight);

                string equation = "";
                string equationPart1 = GenerateEquationPart(weightsLeft);
                string equationPart2 = GenerateEquationPart(weightsRight);

                if(leftWeight == rightWeight)
                {
                    equation = equationPart1.ToString() + " = " + equationPart2.ToString();
                }
                else if(leftWeight > rightWeight)
                {
                    equation = equationPart1.ToString() + " > " + equationPart2.ToString();
                }
                else
                {
                    equation = equationPart1.ToString() + " < " + equationPart2.ToString();
                }

                equationText.text = equation;
                yield return new WaitForSeconds(checkInterval);
            }
        }       
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SolveEquation());
        }
    }
}
