using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class InitialVelocity : MonoBehaviour
    {
        [SerializeField] private Vector2 initialVelocity;

        private void Awake()
        {
            GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }
    }
}