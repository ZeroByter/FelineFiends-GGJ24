using InGame.UI;
using Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGame
{
    public class TheEndTrigger : MonoBehaviour
    {
        [SerializeField] private int totalCollectables = 6;
        [SerializeField] private GameObject signRenderer;
        [SerializeField] private BoxCollider2D boxCollider;

        private void Awake()
        {
            signRenderer.SetActive(false);
            boxCollider.enabled = false;
        }

        public void AllCollected()
        {
            signRenderer.SetActive(true);
            boxCollider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
			SceneManager.LoadSceneAsync("Ending", LoadSceneMode.Single);
		}
    }
}
