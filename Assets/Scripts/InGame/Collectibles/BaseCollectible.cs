using UnityEngine;

namespace InGame.Collectibles
{
    public class BaseCollectible : MonoBehaviour
    {
        private bool isCollected;

        virtual protected void OnCollected() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isCollected) return;

            OnCollected();
            Destroy(gameObject);
            isCollected = true;
        }
    }
}
