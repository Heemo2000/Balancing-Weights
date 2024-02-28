using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tests
{
    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField]private Camera lookCamera;
        [SerializeField]private Cube cubePrefab;

        private Plane _lookPlane;

        private void Awake() {
            _lookPlane = new Plane(transform.forward, transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = lookCamera.ScreenPointToRay(Input.mousePosition);
                if(_lookPlane.Raycast(ray, out float distance))
                {
                    Vector3 spawnPosition = ray.origin + ray.direction * distance;
                    Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                }
            }
            
        }
    }
}
