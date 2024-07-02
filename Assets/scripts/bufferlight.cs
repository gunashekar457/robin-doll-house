using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class bufferlight : MonoBehaviour
{
    // public float timecount = 0;
    // [SerializeField] GameObject empty;

    public Light targetLight;
    public float flickerDuration = 0.1f;
    public int flickerCount = 5;
    public float intervalBetweenIterations = 2f;

    private void Start()
    {


        //if(empty.activeInHierarchy)
        //{
        //    //StartCoroutine(FlickerLight());
        //}
        StartCoroutine(FlickerLight());


    }
    //private void Update()
    //{
    //    timecount+=Time.deltaTime;
    //    if (timecount >= 10)
    //    {
    //        empty.SetActive(false);


    //    }
    //}

    private IEnumerator FlickerLight()
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
        }
    }

}
