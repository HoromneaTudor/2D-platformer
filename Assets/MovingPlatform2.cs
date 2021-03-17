using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//incercare de implementare dar sunt probleme la controllerul caracterului trebuie adaugate noi conditii in functie de criterii
//merge dar nu cum mi-as dori soooo.....
public class MovingPlatform2 : MonoBehaviour
{
    public static Rigidbody2D rb;
    Vector2 respown_possition;
    public float speed = 9f;
    public float xdirection = 0f;
    public float ydirection = 0f;
    public bool ok;
    public float Calculatedspeedx = 0f;
    public float Calculatedspeedy = 0f;
    private bool finishCourtine = true;
    private bool moveRight = false;
    GameObject copy;

    // Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    respown_possition = new Vector2(transform.position.x, transform.position.y);
    //    //Calculatedspeedx = (xdirection - respown_possition.x) / (speed/2);
    //    //Calculatedspeedx = (ydirection - respown_possition.y) / (speed);
    //    finishCourtine = true;
    //}


    ////mehhhh dar ma apropi
    IEnumerator MovePlatform(Vector2 spownposition)
    {
        //rb.isKinematic = false;

        //this works somewhat
        yield return CoroutineUtil.WaitForFixedUpdate(1); // ca sa nu porneasca chiar instant
        transform.DOMove(new Vector2(transform.position.x + xdirection, transform.position.y + ydirection), speed / 2, false);
        yield return CoroutineUtil.WaitForFixedUpdate(1);
        transform.DOMove(respown_possition, speed, false);
        yield return CoroutineUtil.WaitForFixedUpdate(1) ;
        transform.position = spownposition;
        finishCourtine = true;
        //yield return new WaitForFixedUpdate();

        //rb.isKinematic = true;
        //gameObject.transform.position = spownposition;
        //Instantiate(copy, spownposition, copy.transform.rotation);
    }
    //public IEnumerator WaitForSecondsInFixedUpdate(float time)
    //{
    //    float loops = (time / 0.02f);
    //    for (int vlr = 0; vlr < loops; vlr++)
    //    {
    //        yield return new WaitForFixedUpdate();
    //    }
    //}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //finishCourtine = false;
            //Invoke("DropPlatform", 0.5f);
            //StartCoroutine("MovePlatform", respown_possition);
            col.collider.transform.SetParent(transform);
            GameMaster.onMovingPlatform = true;
            ok = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            col.collider.transform.SetParent(null);
            GameMaster.onMovingPlatform = false;
            ok = false;
        }
    }
    void FixedUpdate()
    {
        //    //if (ok)
        //    //{
        //    if (transform.position.x > 4f)
        //    {
        //        moveRight = false;
        //    }
        //    if (transform.position.x < -4f)
        //    {
        //        moveRight = true;
        //    }
        //    if (moveRight)
        //    {
        //        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        //        //rb.MovePosition(transform.position + transform.right * Time.deltaTime);

        //    }
        //    else
        //    {
        //        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        //        //rb.MovePosition(transform.position - transform.right * Time.deltaTime);

        //    }
        if (ok && finishCourtine)
        {
            finishCourtine = false;
            StartCoroutine(MovePlatform(respown_possition));
        }
        //    //}
    }
    //public Transform pos1, pos2;
    //public float speed;
    //public Transform startpos;

    //Vector3 nextpos;

    //void Start()
    //{
    //    nextpos = startpos.position;
    //}
    //void FixedUpdate() //god bless the fixedupdate
    //{
    //    //if (ok)
    //    //{
    //    if (transform.position == pos1.position)
    //    {
    //        //moveRight = true;
    //        nextpos = pos2.position;
    //    }
    //    if (transform.position == pos2.position)
    //    {
    //        // moveRight = false;
    //        nextpos = pos1.position;
    //    }
    //    //if(moveRight)
    //    //{
    //    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime));

    //    //metoda pentru accelerare a platformei intr-o directie
    //    //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime *5));
    //    //}
    //    //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    //    //else
    //    //{
    //    //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    //    //}

    //    //if(ok && finishCourtine)
    //    //{
    //    //    finishCourtine = false;
    //    //    StartCoroutine(MovePlatform(respown_possition));
    //    //}
    //    //}
    //}
    ////private void onDrawGizmos()
    ////{
    ////    Gizmos.DrawLine(pos1.position, pos2.position);
    ////}
}

public static class CoroutineUtil
{
     public static IEnumerator WaitForFixedUpdate(int aCount)
 {
     for(int i = 0; i < aCount; i++)
         yield return new WaitForFixedUpdate();
 }
}
