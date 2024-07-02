using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Animator anim;
    public int score1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //if()
      //  {
           
      //  }
     

      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player"&& carry.Instance.score == 1)
        {
            anim.SetBool("dooropen", true);

        }
    }
}
