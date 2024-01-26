using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        [SerializeField] private Image background;

        [SerializeField] private Image[] images;

        [SerializeField] private bool startWithFullBackground;

        [SerializeField] private List<CutsceneFrameData> frames = new List<CutsceneFrameData>();

        private int activeImageIndex = -1;

        private bool cutsceneActive;

        private void Awake()
        {
            background.color = new Color(0, 0, 0, 0);
        }

        private void Update()
        {
            if (cutsceneActive)
            {
                background.color = Color.Lerp(background.color, Color.black, 5f * Time.deltaTime);
            }
            else
            {
                background.color = Color.Lerp(background.color, new Color(0, 0, 0, 0), 5f * Time.deltaTime);
            }

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
            cutsceneActive = true;

            if (startWithFullBackground)
            {
                background.color = new Color(0, 0, 0, 1);
            }

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

            yield return new WaitForSeconds(frames[frames.Count - 1].delay);

            activeImageIndex = -1;

            onCutsceneEnd.Invoke();
        }

        public void StartMakeCutsceneInactive()
        {
            cutsceneActive = false;
            StartCoroutine(MakeCutsceneInactive());
        }

        private IEnumerator MakeCutsceneInactive()
        {
            yield return new WaitUntil(() => background.color.a < 0.05f);

            background.color = new Color(0, 0, 0, 0);
            this.enabled = false;
        }
    }
}
