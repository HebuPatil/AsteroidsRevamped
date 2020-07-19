using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setVolume : MonoBehaviour
{
    public AudioMixer mixer;
    //public AudioMixer sfxmixer;
    public float volumelevel;
    public Slider myslider;
    public Text musictext;

    

    public Camera cam;
    //public Slider sfxslider;

    public void setLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
        
    }

    //public void setsfxLevel(float sliderValue)
    //{
    //    sfxMixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);

    //}
    void Update()
    {
        
        
        float mynum = myslider.value;
        int numint = (int)(mynum * 100);
        musictext.GetComponent<Text>().text = "Music (" + (numint).ToString() + "%)";
        
        if (Input.GetKeyDown("z"))
        {
            volumelevel = myslider.value;
            
            PlayerPrefs.SetFloat("musicVolumeLevel", volumelevel);

        }
        
    }
    void Start()
    {
        
        
            myslider.value = PlayerPrefs.GetFloat("musicVolumeLevel");
        
        
    }

    



}
