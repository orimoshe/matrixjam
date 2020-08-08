using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatrixJam.Team18
{
    
    public class music : MonoBehaviour
    {
        /*
        BoxCollider2D collider;
        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
        }*/
        AudioSource audio;

        public void OnTriggerEnter2D(Collider2D other)
        {
            audio = GetComponent<AudioSource>();
            audio.PlayDelayed(0.2f);
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            audio.Stop();
        }
    }
}
