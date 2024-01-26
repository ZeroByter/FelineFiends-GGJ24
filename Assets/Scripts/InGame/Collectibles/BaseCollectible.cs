using System.Collections.Generic;
using UnityEngine;
using InGame.Player;

namespace InGame.Collectibles
{
    public class BaseCollectible : MonoBehaviour
    {
        [SerializeField] private List<Behaviour> disableBehaviours;
		[SerializeField] private List<GameObject> disableGameObjects;

		virtual protected void OnCollected() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
			disableBehaviours.ForEach(b => b.enabled = false);
			disableGameObjects.ForEach(o => o.SetActive(false));
            PlayerManager.Instance.OnCollectItem(this);
			OnCollected();
        }
    }
}
