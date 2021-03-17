using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//incercare de implementare dar sunt probleme la controllerul caracterului trebuie adaugate noi conditii in functie de criterii
//merge dar nu cum mi-as dori soooo.....
public class MovingPlatforms : MonoBehaviour
{
    public static Rigidbody2D rb;
    Vector2 respown_possition;
    //public float speed = 9f;
    public float xdirection = 0f;
    public float ydirection = 0f;
    public bool ok;
    public float Calculatedspeedx = 0f;
    public float Calculatedspeedy = 0f;
    private bool finishCourtine = true;
    private bool moveRight = false;
    public Vector3 newPos;
    GameObject copy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respown_possition = new Vector2(transform.position.x, transform.position.y);
        //Calculatedspeedx = (xdirection - respown_possition.x) / (speed/2);
        //Calculatedspeedx = (ydirection - respown_possition.y) / (speed);
        finishCourtine = true;
        StartCoroutine(newFixedUpdate());
    }


    ////mehhhh dar ma apropi
    /// <summary>
    /// 
    /// Concluzie am reusit folosind un while ce are ca yield return waitforfixedupdate si domove sa fac o platforma programabila care respecta si physicsurile
    /// si arata fluid
    /// domove da in-trun fel overrite la while
    /// 
    /// </summary>


    IEnumerator newFixedUpdate()
    {
        while (true)
        {
            if (transform.position == pos1.position)
            {
                //moveRight = true;
                nextpos = pos2.position;
            }
            if (transform.position == pos2.position)
            {
                // moveRight = false;
                nextpos = pos1.position;
            }
            //if(moveRight)
            //{
            transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));
            yield return new WaitForFixedUpdate();
        }
    }
    //IEnumerator MovePlatform(Vector2 spownposition)
    //{
    //    //rb.isKinematic = false;

    //    float elapsedTime = 0;
    //    Vector3 startingPos = transform.position;
    //    Vector3 goulPosition = pos2.position;

    //    //this works somewhat
    //    yield return new WaitForSeconds(0.3f); // ca sa nu porneasca chiar instant
    //    //yield return WaitForSecondsInFixedUpdate(0.3f);
    //    transform.DOMove(goulPosition, speed/2, false);
    //    while (elapsedTime < speed/2)
    //    {
    //        transform.position = Vector3.Lerp(startingPos, goulPosition, (elapsedTime / speed/2));
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }
    //    yield return new WaitForSeconds(speed/2);
    //    //yield return WaitForSecondsInFixedUpdate(speed / 2);
    //    transform.DOMove(respown_possition, speed, false);
    //    elapsedTime = 0;
    //    startingPos = transform.position;
    //    while (elapsedTime < speed)
    //    {
    //        transform.position = Vector3.Lerp(startingPos, spownposition, (elapsedTime / speed));
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }
    //    //transform.position = Vector2.Lerp(transform.position, spownposition, 0.5f);
    //    yield return new WaitForSeconds(speed);
    //    //yield return WaitForSecondsInFixedUpdate(speed);
    //    transform.position = spownposition;
    //    finishCourtine = true;
    //    //yield return new WaitForFixedUpdate();
    //    //yield return new WaitForFixedUpdate();

    //    //rb.isKinematic = true;
    //    //gameObject.transform.position = spownposition;
    //    //Instantiate(copy, spownposition, copy.transform.rotation);
    //}

    ////merge la si face aceeasi chestie ca in fixedupdate
    //IEnumerator incercare()
    //{
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
    //    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
    //    yield return new WaitForFixedUpdate();
    //    finishCourtine = true;
    //}
    public IEnumerator WaitForSecondsInFixedUpdate(float time)
    {
        float loops = (time / 0.02f);
        for (int vlr = 0; vlr < loops; vlr++)
        {
            yield return new WaitForFixedUpdate();
        }
    }
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
    //void FixedUpdate()
    //{
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
    //    //if (ok && finishCourtine)
    //    //{
    //    //    finishCourtine = false;
    //    //    StartCoroutine(MovePlatform(respown_possition));
    //    //}
    //    //}
    //}
    public Transform pos1, pos2;
    public float speed;
    public Transform startpos;

    Vector3 nextpos;

    //void Start()
    //{
    //    nextpos = startpos.position;
    //}
    //void FixedUpdate() //god bless the fixedupdate
    //{
    //    //if (ok)
    //    //{
    //        if (transform.position == pos1.position)
    //        {
    //            //moveRight = true;
    //            nextpos = pos2.position;
    //        }
    //        if (transform.position == pos2.position)
    //        {
    //            // moveRight = false;
    //            nextpos = pos1.position;
    //        }
    //        //if(moveRight)
    //        //{
    //        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));

    //        ////merge si face aceeasi chestie ca si in fixedupdate
    //        //if(finishCourtine)
    //        //{
    //        //    finishCourtine = false;
    //        //    StartCoroutine(incercare());

    //        //}
    //        //metoda pentru accelerare a platformei intr-o directie
    //        //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime *5));
    //        //}
    //        //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    //        //else
    //        //{
    //        //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    //        //}

    //        //if (ok && finishCourtine)
    //        //{
    //        //    finishCourtine = false;
    //        //    StartCoroutine(MovePlatform(respown_possition));
    //        //}
    //    }
}
//private void onDrawGizmos()
//{
//    Gizmos.DrawLine(pos1.position, pos2.position);
//}
//}
