using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace InGame.UI
{
    public class CollectedPapersCounter : MonoBehaviour
    {

        private static CollectedPapersCounter Instance;

        public static void AddToCounter()
        {
            if (Instance == null) return;

            Instance.collectedPapers++;
            Instance.UpdateText();

            if(Instance.collectedPapers >= Instance.totalPapers)
            {
                Instance.allCollectablesCollected.Invoke();
            }
        }

        [SerializeField] private UnityEvent allCollectablesCollected;
        [Header("Format string for counting papers")]
        [SerializeField] private string counterFormat = "Collected papers: {0} / {1}";
        [Header("How many papers are in this level?")]
        [SerializeField] private int totalPapers = 6;
        [Header("The text entity itself")]
        [SerializeField] private TMP_Text textCounter;

        private float collectedPapers;

        private void Awake()
        {
            Instance = this;

            UpdateText();
        }

        private void UpdateText()
        {
            textCounter.text = string.Format(counterFormat, collectedPapers, totalPapers);
        }
    }
}
