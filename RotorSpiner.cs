using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSpiner : MonoBehaviour
{
        public float speed = 8f;
        public Vector3 localAxis;

        void Update()
        {
            transform.Rotate(localAxis * speed * Time.deltaTime);
        }
    
}
