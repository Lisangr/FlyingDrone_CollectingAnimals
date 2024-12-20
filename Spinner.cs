using UnityEngine;

    public class Spinner : MonoBehaviour
    {
        private float speed = 8f; 
        public Vector3 localAxis; 

        void Update()
        {
            transform.Rotate(localAxis * speed * Time.deltaTime);
        }
    }