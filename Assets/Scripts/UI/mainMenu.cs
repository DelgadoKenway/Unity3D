using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volSlider;

    public void playGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void exitButton()
    {
        Application.Quit();
    }
    public void changeVolume(float vol)
    {
        mixer.SetFloat("Master", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("MasterVol", vol);
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            volSlider.value = PlayerPrefs.GetFloat("MasterVol");
            mixer.SetFloat("Master", Mathf.Log10(volSlider.value) * 20);
        }
    }
}
