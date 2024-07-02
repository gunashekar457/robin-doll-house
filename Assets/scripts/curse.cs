using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class curse : MonoBehaviour
{
    [SerializeField] GameObject doll1;
    [SerializeField] GameObject doll2;
    [SerializeField] GameObject doll3;
    [SerializeField] GameObject doll4;
    public GameObject ghostprefab;
    public GameObject devil;
    public Transform tr;
    public Light targetLight;
    public float flickerDuration = 0.1f;
    public int flickerCount = 5;
    public float intervalBetweenIterations = 2f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doll1&&doll2&&doll3&&doll4.activeInHierarchy)
        {
            transform.position+= new Vector3(0f,0.2f,0f)*Time.deltaTime;
            Instantiate(devil,tr.position,Quaternion.identity);
            StartCoroutine(FlickerLight1());
            ghostprefab.SetActive(false);

        }






    }
    private IEnumerator FlickerLight1()
    {
        while (true)
        {

            {
                targetLight.enabled = true;
                yield return new WaitForSeconds(flickerDuration / 2);
                targetLight.enabled = false;
                yield return new WaitForSeconds(flickerDuration / 2);
            }


            yield return new WaitForSeconds(intervalBetweenIterations);
            SceneManager.LoadScene(3);
           
        }
    }
}
