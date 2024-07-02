using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carry : MonoBehaviour
{
    public int maxkeys = 2;

    public GameObject doll1;
    public GameObject doll2;
    public GameObject doll3;
    public GameObject doll4;
    public int maxdoll = 4;
    public int mindoll = 0;
    public Text dolltext;
    public int dollc;
    public Text interacttext;
    private GameObject nearbyObject;
    private bool canInteract = false;
    public AudioSource scream, equip;
    public GameObject letterpanel1, letterpanel2;





    public static carry Instance
    {
        get { return s_instance; }
    }
    public Text keytext;
    public int score = 0;
    private static carry s_instance;
    void Start()
    {

    }
    private void Awake()
    {
        s_instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        
        
        
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            equip.Play();
            // Perform the interaction when the interact key is pressed
            if (nearbyObject.CompareTag("key"))
            {
                score += 1;
                Destroy(nearbyObject);
            }
            else if (nearbyObject.CompareTag("door1") && score >= 1)
            {
                score -= 1;
            }
            else if (nearbyObject.CompareTag("cursedoll"))
            {
                dollc += 1;
                scream.Play();
                Destroy(nearbyObject);
            }
            else if(nearbyObject.CompareTag("TABLE"))
            {
                if (dollc >= 1)
                {
                    doll1.SetActive(true);

                }
                if (dollc >= 2)
                {
                    doll2.SetActive(true);

                }
                if (dollc >= 3)
                {
                    doll3.SetActive(true);

                }
                if (dollc >= 4)
                {
                    doll4.SetActive(true);

                }
            }
            else if(nearbyObject.CompareTag("letter1"))
            {
                letterpanel1.SetActive(true);
            }
            else if (nearbyObject.CompareTag("letter2"))
            {
                letterpanel2.SetActive(true);
            }

            // Reset the nearbyObject and disable the interaction until the player moves away
            nearbyObject = null;
            canInteract = false;
            interacttext.gameObject.SetActive(false);
            


        }
        if (score > maxkeys)
        {
            score = maxkeys;
        }
        keytext.text = score.ToString();
        if(dollc>maxdoll)
        {
            dollc=maxdoll;
        }
        dolltext.text=dollc.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="key"|| other.gameObject.tag == "door1"|| other.gameObject.tag == "cursedoll"|| other.gameObject.tag == "TABLE"|| other.gameObject.tag == "letter1"|| other.gameObject.tag == "letter2")
       
        {
            if (!canInteract)
            {

                interacttext.gameObject.SetActive(true);
                // Store the nearby object and enable interaction
                nearbyObject = other.gameObject;
                canInteract = true;
            }

        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        letterpanel1.SetActive(false);
        letterpanel2.SetActive(false);
        if (other.gameObject.tag == "key" || other.gameObject.tag == "door1" || other.gameObject.tag == "cursedoll" || other.gameObject.tag == "TABLE" || other.gameObject.tag == "letter1" || other.gameObject.tag == "letter2")
        {
            // Reset the nearbyObject and disable interaction when the player moves away
            if (other.gameObject == nearbyObject)
            {
                interacttext.gameObject.SetActive(false);
                nearbyObject = null;
                canInteract = false;
                
            }
           

        }
            
    }

}
