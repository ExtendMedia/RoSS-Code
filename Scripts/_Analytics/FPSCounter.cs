using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RoSS
{
    public class FPSCounter : MonoBehaviour
    {
        public TMP_Text fpsCounterText;

        public int MaxFrames = 60;

        private List<float> frameTimes = new List<float>();

        void Start()
        {
            frameTimes.Clear();
        }

        void Update()
        {
            AddFrame();
            fpsCounterText.text = "FPS: " + GetFPS().ToString();
        }

        private void AddFrame()
        {
            frameTimes.Add(Time.unscaledDeltaTime);
            if (frameTimes.Count > MaxFrames)
            {
                frameTimes.RemoveAt(0);
            }
        }

        private float GetFPS()
        {
            float newFPS = 0f;

            float totalTimeOfAllFrames = 0f;
            foreach (float frame in frameTimes)
            {
                totalTimeOfAllFrames += frame;
            }
            newFPS = ((float)(frameTimes.Count)) / totalTimeOfAllFrames;

            return Mathf.Round(newFPS);
        }

    }
}