using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenemanager : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject cam1;
    public GameObject cam2;

    [Header("UI Panels")]
    public GameObject panel1;  // Main menu panel
    public GameObject panel2;  // Options panel
    public GameObject panel3;  // Confirmation/start panel

    [Header("Audio")]
    public AudioSource clip;        // Background music
    public AudioSource buttonclick; // Button click sound

    [Header("Settings UI")]
    public Slider volumeSlider;
    public Toggle musicToggle;

    private float defaultVolume = 0.5f;
    private bool isMusicEnabled = true;

    void Start()
    {
        // Disable cam2 and related panels initially if assigned
        if (cam2 != null) cam2.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);
        if (panel3 != null) panel3.SetActive(false);

        // Play music if AudioSource assigned
        if (clip != null) clip.Play();

        LoadOptions();

        Cursor.lockState = CursorLockMode.None;

        // Setup UI initial values if UI elements assigned
        if (volumeSlider != null)
        {
            volumeSlider.value = defaultVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (musicToggle != null)
        {
            musicToggle.isOn = isMusicEnabled;
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }

        // Apply initial volume setting
        OnVolumeChanged(defaultVolume);
    }

    private void OnVolumeChanged(float volume)
    {
        AudioListener.volume = volume;
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        isMusicEnabled = isOn;
        if (clip != null)
        {
            if (isMusicEnabled) clip.Play();
            else clip.Pause();
        }
        Debug.Log(isMusicEnabled ? "Music enabled" : "Music disabled");
    }

    private void SaveOptions()
    {
        if (volumeSlider != null)
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("MusicEnabled", isMusicEnabled ? 1 : 0);
    }

    private void LoadOptions()
    {
        if (PlayerPrefs.HasKey("Volume"))
            defaultVolume = PlayerPrefs.GetFloat("Volume");

        if (PlayerPrefs.HasKey("MusicEnabled"))
            isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("hemanth");  // Replace "hemanth" with your game scene name
    }

    public void Play()
    {
        Debug.Log("Play button pressed");

        if (clip != null) clip.Pause();
        if (buttonclick != null) buttonclick.Play();
        if (panel3 != null) panel3.SetActive(true);
    }

    public void Options()
    {
        if (buttonclick != null) buttonclick.Play();
        if (panel1 != null) panel1.SetActive(false);
        if (cam1 != null) cam1.SetActive(false);
        if (cam2 != null) cam2.SetActive(true);
        if (panel2 != null) panel2.SetActive(true);
    }

    public void ApplyChanges()
    {
        SaveOptions();
        Debug.Log("Options applied");
        if (buttonclick != null) buttonclick.Play();
    }

    public void CloseOptions()
    {
        if (cam2 != null) cam2.SetActive(false);
        if (panel2 != null) panel2.SetActive(false);
        if (cam1 != null) cam1.SetActive(true);
        if (panel1 != null) panel1.SetActive(true);
        if (buttonclick != null) buttonclick.Play();
        Debug.Log("Options window closed");
    }

    public void Proceed()
    {
        // Example to load next scene by build index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            Debug.LogWarning("No next scene in build settings");
    }
}
