using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    bool once = true;
    float updateTimer = 0;
    int updateFps = 0;
    void Start()
    {
        StartCoroutine(testupdate());
    }

    // Update is called once per frame

    IEnumerator testupdate()
    {
        while(true)
        {
            //Debug.Log(1f / Time.deltaTime);
            //Debug.Log(count);
            yield return new WaitForFixedUpdate();
        }
    }

    void Update()
    {
        //Debug.Log(1f / Time.deltaTime);
    }
}
