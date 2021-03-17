using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 respown_possition;
    private bool finishCourtime = true;
    //GameObject copy;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respown_possition = new Vector2(transform.position.x, transform.position.y);
        finishCourtime = true;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name.Equals("Player")&& finishCourtime)
        {
            finishCourtime = false;
            //rb.transform.DOShakePosition(0.2f,1f,0,0,true);
            //Invoke("DropPlatform", 1f);
            StartCoroutine(RespownPlatform(respown_possition));
        }
        
        //StartCoroutine("RespownPlatform",respown_possition);
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
    }
    IEnumerator RespownPlatform(Vector2 spownposition)
    {
        transform.DOShakePosition(0.2f, 0.1f, 20, 0, false); //perfect (false este daca vreau fadeout in cazul asta nu)
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        yield return new WaitForSeconds(4f);
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        gameObject.transform.position = spownposition;
        finishCourtime = true;
        //Instantiate(copy, spownposition, copy.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
