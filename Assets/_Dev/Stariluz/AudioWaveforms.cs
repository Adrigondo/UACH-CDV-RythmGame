using System;
using UnityEngine;

namespace Stariluz
{
    public class AudioWaveforms : MonoBehaviour
    {
        #region "Fields"
        public AudioClip audioClip;
        public AudioSource audioSource;
        public LineRenderer lineRenderer;
        public NewPlayerBehavior PlayerBehavior;
        public float samplesPerUnit = 10f; // Desired samples per unit
        private Vector3[] waveformPoints;
        #endregion

        #region "LyfeCycle Methods"
        protected void Start()
        {
            if (audioClip != null && PlayerBehavior != null)
            {
                transform.position=PlayerBehavior.StartPositionScript.GetStartPosition();
                float[] samples = new float[audioClip.samples * audioClip.channels];
                audioClip.GetData(samples, 0);
                waveformPoints = GenerateWaveform(samples);
                audioSource.clip = audioClip;
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = waveformPoints.Length;
                    lineRenderer.SetPositions(waveformPoints);
                    audioSource.Play();
                }
            }
        }
        protected void OnDrawGizmos()
        {
            if (audioClip != null && waveformPoints == null && PlayerBehavior != null)
            {
                transform.position=PlayerBehavior.StartPositionScript.GetStartPosition();
                float[] samples = new float[audioClip.samples * audioClip.channels];
                audioClip.GetData(samples, 0);
                waveformPoints = GenerateWaveform(samples);
            }

            if (waveformPoints != null)
            {
                Vector3 scale = lineRenderer.transform.localScale;
                Gizmos.color = Color.magenta;

                for (int i = 0; i < waveformPoints.Length - 1; i++)
                {
                    Vector3 origin = transform.position + new Vector3(waveformPoints[i].x, waveformPoints[i].y * scale.y, waveformPoints[i].z);
                    Vector3 destiny = transform.position + new Vector3(waveformPoints[i + 1].x, waveformPoints[i + 1].y * scale.y, waveformPoints[i + 1].z);

                    Gizmos.DrawLine(origin, destiny);
                }
            }
        }
        #endregion

        #region "Protected Methods"
        Vector3[] GenerateWaveform(float[] samples)
        {
            float duration = audioClip.length;
            float totalUnits = duration * PlayerBehavior.MovementSpeed;
            int totalSamples = Mathf.CeilToInt(samplesPerUnit * totalUnits);
            int packSize = Mathf.CeilToInt((float)samples.Length / totalSamples);

            Vector3[] points = new Vector3[totalSamples];

            for (int i = 0; i < totalSamples; i++)
            {
                float sum = 0f;
                int sampleCount = 0;

                for (int j = 0; j < packSize; j++)
                {
                    int sampleIndex = (i * packSize) + j;
                    if (sampleIndex < samples.Length)
                    {
                        sum += Mathf.Abs(samples[sampleIndex]);
                        sampleCount++;
                    }
                }

                float average = sampleCount > 0 ? sum / sampleCount : 0f;
                float xPosition = (float)i / samplesPerUnit;
                points[i] = new Vector3(xPosition, average, 0);
            }

            return points;
        }
        #endregion
    }
}
