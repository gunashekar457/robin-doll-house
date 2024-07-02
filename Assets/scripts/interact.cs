using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public bool key=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,20f))
        {
            if(hit.collider.gameObject.tag=="interactable"&& hit.collider.gameObject.layer==3&&key==true)
            {
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.tag == "interactable" && hit.collider.gameObject.layer == 3&& key==false)
                {
                Debug.Log("door is locked need a key");
            }
            if(hit.collider.gameObject.tag=="interactable" && hit.collider.gameObject.layer==6)
            {
                Debug.Log("key");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    key = true;
                    Debug.Log("u have taken the key");
                }
                
            }
            if (hit.collider.gameObject.tag=="cursedoll")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("doll accquired");
                    Destroy(hit.collider.gameObject);
                }
                
            }
        }
        
    }
}
