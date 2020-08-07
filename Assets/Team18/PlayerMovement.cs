using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatrixJam.Team18
{
    public class PlayerMovement : MonoBehaviour
    {
        public mainPlayerController controller;
        public float runSpeed = 40f;

        private float horizontalMove = 0f;
        private bool jump = false;

        // Update is called once per frame
        void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        private void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
}
