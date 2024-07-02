using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursedoll : MonoBehaviour
{
   
    
    public GameObject[] dolls = new GameObject[4];
    public Transform[] spawnpoints;
    private void Start()
    {
        spawndolls();
    }
    public void spawndolls()
    {
        int i = 0;
        foreach(Transform spawnPoint in spawnpoints)
        {
            Instantiate(dolls[i], spawnPoint.position, spawnPoint.rotation);
            
        }
           

    }
}
