using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Collectibles
{
    public class BaseCollectible : MonoBehaviour
    {
        [Header("The visual renderer itself")]
        [SerializeField] private Transform visualRenderer;
        [Header("Visual animation properties")]
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField] private float animationScale = 0.25f;

        private bool isCollected;

        virtual protected void OnCollected() { }

        private void Update()
        {
            visualRenderer.localPosition = new Vector2(0, (Mathf.Sin(Time.time * animationSpeed) + 1f) * animationScale);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isCollected) return;

            OnCollected();
            Destroy(gameObject);
            isCollected = true;
        }
    }
}
