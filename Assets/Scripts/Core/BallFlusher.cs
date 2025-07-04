using System;
using Gameplay;
using UnityEngine;

namespace Core
{
    public class BallFlusher: MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if(other.collider.CompareTag("Ball"))
                other.collider.GetComponent<Ball>().Despawn();
        }
    }
}