using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackground : MonoBehaviour
{
    private AudioSource piano;
    public AudioClip BGM;
    float Slider;
    // Start is called before the first frame update
    void Start()
    {
        Slider = 0.7f;
        piano = GetComponent<AudioSource>();
        NowMusic(BGM, piano);
    }

    public static void NowMusic(AudioClip clip, AudioSource audioplayer)
    {
        audioplayer.Stop();
        audioplayer.clip = clip;
        audioplayer.loop = true;
        audioplayer.time = 0;
        audioplayer.Play();
    }
    void OnGUI()
    {
        Slider = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), Slider, 0.0F, 1.0F);
        piano.volume = Slider;
    }
}
