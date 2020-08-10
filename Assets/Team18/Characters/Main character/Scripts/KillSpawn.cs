using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatrixJam.Team18
{
    public class KillSpawn : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //if (collision.transform.CompareTag("love"))
            //{
                collision.transform.position = spawnPoint.position;
            //}
        }
    }
}
