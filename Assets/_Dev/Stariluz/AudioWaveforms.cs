using UnityEngine;

namespace Stariluz
{

    public class AudioWaveforms : MonoBehaviour
    {
        public AudioClip audioClip;
        public AudioSource audioSource;
        public LineRenderer lineRenderer;
        public NewPlayerBehavior PlayerBehavior;
        public int width = 1024; // Number of points in the waveform

        private UnityEngine.Vector3[] waveformPoints;
        void Start()
        {
            if (audioClip != null)
            {
                float[] samples = new float[audioClip.samples * audioClip.channels];
                audioClip.GetData(samples, 0);
                waveformPoints = GenerateWaveform(samples);
                audioSource.clip=audioClip;
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = waveformPoints.Length;
                    lineRenderer.SetPositions(waveformPoints);
                    audioSource.Play();
                }
            }
        }


        void OnDrawGizmos()
        {
            if (audioClip != null && waveformPoints == null)
            {
                float[] samples = new float[audioClip.samples * audioClip.channels];
                audioClip.GetData(samples, 0);
                waveformPoints = GenerateWaveform(samples);
            }

            if (waveformPoints != null)
            {
                Vector2 scale=transform.localScale;
                Gizmos.color = Color.magenta;
                for (int i = 0; i < waveformPoints.Length - 1; i++)
                {
                    Vector2 origin=transform.position + waveformPoints[i];
                    Vector2 destiny=transform.position + waveformPoints[i + 1];
                    origin.y*=scale.y;
                    destiny.y*=scale.y;

                    Gizmos.DrawLine(origin, destiny);
                }
            }
        }

        Vector3[] GenerateWaveform(float[] samples)
        {
            int packSize = Mathf.CeilToInt((float)samples.Length / width);
            Vector3[] points = new Vector3[width];
            float duration = audioClip.length;
            float totalUnits = duration * PlayerBehavior.MovementSpeed;

            for (int i = 0; i < width; i++)
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
                float xPosition = (float)i / width * totalUnits;
                points[i] = new Vector3(xPosition, average, 0);
            }

            return points;
        }

    }

}
