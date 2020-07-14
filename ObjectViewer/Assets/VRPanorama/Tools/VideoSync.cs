using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace VRPanorama {
    public class VideoSync : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            videoPlayer.Play();
            videoPlayer.Pause();
            StartCoroutine(SyncVideo());
        }

        IEnumerator SyncVideo()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                videoPlayer.StepForward();
                videoPlayer.Pause();
            }
        }
    }
}
