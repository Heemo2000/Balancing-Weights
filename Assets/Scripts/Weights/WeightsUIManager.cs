using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Weights
{
    public class WeightsUIManager : MonoBehaviour
    {
        [SerializeField]private WeightsContainer leftContainer;
        [SerializeField]private WeightsContainer rightContainer;

        [SerializeField]private Button resetButton;


        private void ResetWeights()
        {
            leftContainer.ResetWeights();
            rightContainer.ResetWeights();
        }
        // Start is called before the first frame update
        void Start()
        {
            resetButton.onClick.AddListener(()=> ResetWeights());
        }

        private void OnDestroy() 
        {
            resetButton.onClick.RemoveAllListeners();    
        }
    }
}
