using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public NavMeshAgent navMeshAgent;
    public Animator anim;
    public bool isHaunting = false;
    public GameObject cam1;
    
    public GameObject cam;
    public AudioSource scream;
    
    public float panelDisplayDelay = 3f;

    public float hauntSpeed = 3f; // Adjust this to control the ghost's speed while haunting

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = anim.GetComponent<Animator>();
        
        cam.SetActive(false);

    }

    public void Update()
    {
        if (carry.Instance.dollc != 0)
        {
            isHaunting = true;
        }

        if (isHaunting)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        if (player != null)
        {
            // Set the destination of the ghost to the player's position
            navMeshAgent.SetDestination(player.position);
            anim.Play("Scary Clown Walk");
        }
    }

    // Call this method to start the ghost's haunting behavior
    public void StartHaunting()
    {
        Debug.Log("i got called");
        isHaunting = true;
        navMeshAgent.speed = hauntSpeed;
    }

    // Call this method to stop the ghost's haunting behavior
    public void StopHaunting()
    {
        isHaunting = false;
        navMeshAgent.ResetPath();
        navMeshAgent.speed = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            cam1.SetActive(false);
            cam.SetActive(true);
            scream.Play();
            StartCoroutine(DisplayPanelAfterDelay());
          
            
            

        }
    }
    private IEnumerator DisplayPanelAfterDelay()
    {
        
        yield return new WaitForSeconds(panelDisplayDelay);
        SceneManager.LoadScene(2);

    }
    }