using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class carry : MonoBehaviour
{
    public int maxkeys = 2;
    public int maxdoll = 4;
    public GameObject dollPrefab;
    public Transform barrelSpawnPoint;
    public ParticleSystem fireEffectPrefab;

    public Text dolltext, interacttext, keytext;
    public GameObject letterpanel1, letterpanel2;
    public AudioSource scream, equip;

    public GameObject nearbyObject;
    private bool canInteract = false;

    public int score = 0;
    public int dollc = 0;

    public static int firedDollCount = 0; // 🆕 Track fired dolls
    public curse curseScript; // 🆕 Reference to curse script

    private static carry s_instance;
    public static carry Instance { get { return s_instance; } }

    private void Awake()
    {
        s_instance = this;
    }

    void Update()
{
    // Safety check: if canInteract is true but nearbyObject is null, reset interaction
    if (canInteract && nearbyObject == null)
    {
        canInteract = false;
        interacttext.gameObject.SetActive(false);
        return;
    }

    if (canInteract && Input.GetKeyDown(KeyCode.E))
    {
        if (equip != null) equip.Play();

        if (nearbyObject.CompareTag("key"))
        {
            score += 1;
            Destroy(nearbyObject);
            nearbyObject = null;
            canInteract = false;
        }
        else if (nearbyObject.CompareTag("door1") && score >= 1)
        {
            score -= 1;
            // Optionally open door animation here
            nearbyObject = null;
            canInteract = false;
        }
        else if (nearbyObject.CompareTag("cursedoll"))
        {
            dollc += 1;
            if (scream != null)
            {
                if (scream.isPlaying) scream.Stop();
                scream.Play();
            }
            Destroy(nearbyObject);
            nearbyObject = null;
            canInteract = false;
        }
        else if (nearbyObject.CompareTag("BARREL"))
        {
            if (dollc > 0)
            {
                FireDollFromBarrel();
                dollc -= 1;
            }
            nearbyObject = null;
            canInteract = false;
        }
        else if (nearbyObject.CompareTag("letter1"))
        {
            letterpanel1.SetActive(true);
            nearbyObject = null;
            canInteract = false;
        }
        else if (nearbyObject.CompareTag("letter2"))
        {
            letterpanel2.SetActive(true);
            nearbyObject = null;
            canInteract = false;
        }
    }

    // Clamp values
    score = Mathf.Min(score, maxkeys);
    dollc = Mathf.Min(dollc, maxdoll);

    // UI updates
    keytext.text = score.ToString();
    dolltext.text = dollc.ToString();
}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key") || other.CompareTag("door1") || other.CompareTag("cursedoll") ||
            other.CompareTag("BARREL") || other.CompareTag("letter1") || other.CompareTag("letter2"))
        {
            if (!canInteract)
            {
                interacttext.gameObject.SetActive(true);
                nearbyObject = other.gameObject;
                canInteract = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        letterpanel1.SetActive(false);
        letterpanel2.SetActive(false);

        if (other.gameObject == nearbyObject)
        {
            interacttext.gameObject.SetActive(false);
            nearbyObject = null;
            canInteract = false;
        }
    }

    private void FireDollFromBarrel()
    {
        GameObject newDoll = Instantiate(dollPrefab, barrelSpawnPoint.position, barrelSpawnPoint.rotation);

        // Fire effect
        if (fireEffectPrefab != null)
        {
            ParticleSystem fire = Instantiate(fireEffectPrefab, newDoll.transform.position, Quaternion.identity, newDoll.transform);
            fire.Play();
        }

        // Launch
        Rigidbody rb = newDoll.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(barrelSpawnPoint.forward * 500f + Vector3.up * 200f);
        }

        Destroy(newDoll, 3f); // Destroy after 3s

        // Count fired dolls
        firedDollCount++;

        // Start exorcism if 4 dolls fired
        if (firedDollCount == 4 && curseScript != null)
        {
            curseScript.StartExorcism(); // 👈 Call custom method
        }
    }
}
