using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class curse : MonoBehaviour
{
   // [SerializeField] private GameObject doll1, doll2, doll3, doll4; // Not used anymore but can be kept for visuals if needed
    public GameObject ghostprefab;
    public GameObject devil;
    public GameObject playercam, exorismcam;
    public Transform tr;
    public Light targetLight;
    public float flickerDuration = 0.1f;
    public int flickerCount = 5;
    public string nextSceneName = "gamecompleted";
    public AudioSource exorcismAudio;
    public GameObject fireEffectPrefab;

    private bool triggered = false;
    private GameObject spawnedDevil;

    void Start()
    {
        exorismcam.SetActive(false);
        playercam.SetActive(true);
    }

    // 🔥 Trigger this from carry script when 4 dolls are fired
    public void StartExorcism()
    {
        if (triggered) return;

        triggered = true;

        // Spawn devil with offset
        Vector3 offset = new Vector3(1.5f, -1f, 0.1f);
        Vector3 spawnPos = tr.position + offset;

        spawnedDevil = Instantiate(devil, spawnPos, Quaternion.identity);

        // Face exorcism camera
        Vector3 lookDirection = exorismcam.transform.position - spawnedDevil.transform.position;
        lookDirection.y = 0f;
        spawnedDevil.transform.rotation = Quaternion.LookRotation(lookDirection);

        // Disable ghost and start exorcism
        if (ghostprefab != null)
            ghostprefab.SetActive(false);

        StartCoroutine(FlickerLightAndExorcise());
    }

    private IEnumerator FlickerLightAndExorcise()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            targetLight.enabled = true;
            yield return new WaitForSeconds(flickerDuration / 2);
            targetLight.enabled = false;
            yield return new WaitForSeconds(flickerDuration / 2);
        }

        // Switch to exorcism camera and play audio
        playercam.SetActive(false);
        exorismcam.SetActive(true);
        exorcismAudio.Play();

        yield return new WaitForSeconds(2f);

        // Fire effect on devil
        if (spawnedDevil != null)
        {
            Vector3 firePosition = spawnedDevil.transform.position;
            GameObject fire = Instantiate(fireEffectPrefab, firePosition, Quaternion.identity);
            fire.transform.SetParent(spawnedDevil.transform);
        }

        // Wait for audio + buffer
        float remainingTime = exorcismAudio.clip.length - 2f;
        if (remainingTime > 0)
            yield return new WaitForSeconds(remainingTime + 1f);

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
