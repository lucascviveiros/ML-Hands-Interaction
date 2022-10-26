using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScript : MonoBehaviour
{
    public UnityEngine.Video.VideoClip videoClip;

    void Start()
    {
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip; 
    }

    public void setPlayVideo()
    {
        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.Play();
    }

    public void setPauseVideo()
    {
        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.Pause();
    }

    public void setStopVideo() 
    {
        var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.Stop();
    }
}
