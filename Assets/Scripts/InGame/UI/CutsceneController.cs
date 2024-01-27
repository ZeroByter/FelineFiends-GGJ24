using Management;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InGame.UI
{
    [Serializable]
    public struct CutsceneFrameData
    {
        public Sprite frame;
        public float delay;
    }

    public class CutsceneController : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCutsceneEnd;

        [SerializeField] private Image[] images;

        [SerializeField] private bool startWithFullBackground;
        [SerializeField] private float postLastFrameDelay = 3;

        [SerializeField] private List<CutsceneFrameData> frames = new List<CutsceneFrameData>();

        private int activeImageIndex = -1;

        private void Update()
        {
            for (int i = 0; i < images.Length; i++)
            {
                var image = images[i];

                if (i == activeImageIndex)
                {
                    image.color = Color.Lerp(image.color, Color.white, 5f * Time.deltaTime);
                }
                else
                {
                    image.color = Color.Lerp(image.color, new Color(0, 0, 0, 0), 5f * Time.deltaTime);
                }
            }
        }

        private void OnEnable()
        {
            StartCoroutine(StartCutscene());
        }

        private IEnumerator StartCutscene()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                var frameData = frames[i];

                yield return new WaitForSeconds(frameData.delay);

                activeImageIndex = i % 2;
                images[activeImageIndex].sprite = frameData.frame;
            }

            yield return new WaitForSeconds(postLastFrameDelay);

            activeImageIndex = -1;

            onCutsceneEnd.Invoke();
        }

        public void LoadScene(string sceneName)
        {
			SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		}
    }
}
