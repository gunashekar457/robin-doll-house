using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenemanager : MonoBehaviour
{
    public GameObject cam1, cam2;
    public GameObject panel1, panel2,panel3;
    public AudioSource clip,buttonclick;
    public Slider volumeSlider;
    public Toggle musicToggle;
    
    


    private float defaultVolume = 0.5f;
    private bool isMusicEnabled = true;
    public void Start()
    {
        cam2.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        clip.Play();
        LoadOptions();
        Cursor.lockState = CursorLockMode.None;

        // Set UI elements to initial values
        volumeSlider.value = defaultVolume;
        musicToggle.isOn = isMusicEnabled;

        // Add event listeners for UI elements
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);

        // Apply the initial volume setting
        OnVolumeChanged(defaultVolume);
        

}
    private void OnVolumeChanged(float volume)
    {
        // Update the volume based on the slider value
        AudioListener.volume = volume;
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        
        isMusicEnabled = isOn;
        if (isMusicEnabled)
        {
            clip.Play();
            Debug.Log("Music enabled");
        }
        else
        {
            clip.Pause();
            Debug.Log("Music disabled");
        }
    }

    
    private void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("MusicEnabled", isMusicEnabled ? 1 : 0);
    }

    // Load the options from PlayerPrefs or a file
    private void LoadOptions()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            defaultVolume = PlayerPrefs.GetFloat("Volume");
        }

        if (PlayerPrefs.HasKey("MusicEnabled"))
        {
            isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1;
        }
    }
    public void quit()
    {
        Application.Quit();
    }
    public void playagain()
    {
        SceneManager.LoadScene("hemanth");
    }
    

    public void Play()
    {
       
        Debug.Log("pressed play");
        clip.Pause();
        buttonclick.Play();
        panel3.SetActive(true);
        



    }
    
    public void options()
    {
        buttonclick.Play();
        panel1.SetActive(false);
        cam1.SetActive(false);
        cam2.SetActive(true);
        panel2.SetActive(true);
        
    }
    
        
    
    public void ApplyChanges()
    {
        SaveOptions();
        
        Debug.Log("Options applied");
        buttonclick.Play();
    }

    
    public void CloseOptions()
    {
        cam2.SetActive(false);
        panel2.SetActive(false);
        cam1.SetActive(true);
        panel1.SetActive(true);
        buttonclick.Play();

        Debug.Log("Options window closed");
    }
    public void proceed()
    {
        SceneManager.LoadScene(+1);
    }
    
}
