using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatrixJam.Team18
{
    public class deletePlayer : MonoBehaviour
    {
        public GameObject player;
        public Camera normalCamera;
        public Camera rampCamera;

        void DestroyGameObject()
        {
            Destroy(player);
        }
        void DestroyComponent()
        {
            // Removes the rigidbody from the game object
            Destroy(GetComponent<ScriptableObject>());
        }

        private void SwitchToNormalCamera()
        {
            rampCamera.enabled = false;
            normalCamera.enabled = true;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //DestroyComponent();
            
            SwitchToNormalCamera();
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
