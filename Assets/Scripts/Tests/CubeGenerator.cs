using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tests
{
    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField]private Camera lookCamera;
        
        [SerializeField]private Cube[] cubePrefabs;

        private Plane lookPlane;
        private int cubeIndex = 0;

        public void SetCubeToGenerateIndex(int index)
        {
            cubeIndex = index;
        }

        private void Awake() {
            lookPlane = new Plane(transform.forward, transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(1))
            {
                Ray ray = lookCamera.ScreenPointToRay(Input.mousePosition);
                if(lookPlane.Raycast(ray, out float distance))
                {
                    Vector3 spawnPosition = ray.origin + ray.direction * distance;
                    Instantiate(cubePrefabs[cubeIndex], spawnPosition, Quaternion.identity);
                }
            }
            
        }
    }
}
