using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class RopeController : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private EdgeCollider2D edgeCollider;
    }
}
