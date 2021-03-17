using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityEditorInternal;

public class MovingPlatformsOnLanding : MonoBehaviour
{
    public static Rigidbody2D rb;
    //Vector2 respown_possition;
    Vector3 respown_position;
    //public float speed = 9f;
    public float xdirection = 0f;
    public float ydirection = 0f;
    public bool ok;
    public float Calculatedspeedx = 0f;
    public float Calculatedspeedy = 0f;
    private bool finishCourtine = true;
    private bool moveRight = false;
    private bool once = false;
    public Vector3 newPos;
    GameObject copy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pos1.position;
        //respown_possition = new Vector2(transform.position.x, transform.position.y);
        respown_position = new Vector3(transform.position.x, transform.position.y, 0);
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
    /// nu recomand folosirea acestei coroutine sunt multe probleme cu ea
    /// </summary>
    //IEnumerator MovePlatform(Vector2 spownposition)
    //{
    //    //rb.isKinematic = false;

    //    float elapsedTime = 0;
    //    Vector3 startingPos = transform.position;
    //    Vector3 goulPosition = pos2.position;

    //    //this works somewhat
    //    yield return new WaitForSeconds(0.3f); // ca sa nu porneasca chiar instant
    //    //yield return WaitForSecondsInFixedUpdate(0.3f);

    //    transform.DOMove(goulPosition, speed / 2, false);

    //    while (elapsedTime < speed/2)
    //    {
    //        transform.position = Vector3.Lerp(startingPos, goulPosition, (elapsedTime / speed ));
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }
    //    yield return new WaitForSeconds(speed / 2);
    //    //yield return WaitForSecondsInFixedUpdate(speed / 2);

    //    transform.DOMove(spownposition, speed, false);

    //    elapsedTime = 0;
    //    startingPos = transform.position;
    //    while (elapsedTime < speed)
    //    {
    //        transform.position = Vector3.Lerp(startingPos, spownposition, (elapsedTime / speed));
    //        elapsedTime += Time.deltaTime;
    //        //cred ca cu fixed deltatime dar nust
    //        //elapsedTime += Time.fixedDeltaTime;
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

    ////merge dar a trebuit sa schimb deltatimeul la 0.01 sper sa fie ok (interesant ca la celalalt moving platform nu am problema asta)
    ///
    IEnumerator MovePlatform2(Vector3 spownPosition)
    {
        Vector3 startingPos = transform.position;
        Vector3 goulPosition = pos2.position;
        
        //Vector3 startingPos = transform.position;
        //Vector3 goulPosition = pos2.position;
        
            yield return new WaitForSeconds(0.5f);
            while (transform.position != goulPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, goulPosition, speed * (Time.deltaTime * 4));
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(0.5f);
            while (transform.position != spownPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, spownPosition, speed * (Time.deltaTime * 2));
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(0.5f);
            finishCourtine = true;
            yield return new WaitForFixedUpdate();
        

    }
    //// ce am si in fixedupdate dar intr-o corutina merge dar daca ii pun un while performanta scade drastic
    IEnumerator MovePlatform3(Vector3 spownPosition)
    {
        if (transform.position == pos1.position)
        {
            //moveRight = true;
            nextpos = pos2.position;
            yield return new WaitForSeconds(0.5f);
        }
        if (transform.position == pos2.position)
        {
            // moveRight = false;
            nextpos = pos1.position;
            yield return new WaitForSeconds(0.5f);
        }
        //if(moveRight)
        //{

        //while (transform.position != pos1.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
        //    yield return new WaitForFixedUpdate();
        //}
        //yield return new WaitForFixedUpdate();

        //while (transform.position != pos2.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
        //    yield return new WaitForFixedUpdate();
        //}
        //transform.position = nextpos;
        //yield return new WaitForFixedUpdate();
        //while(transform.position!=nextpos)
        //{
            transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));
        //    yield return new WaitForFixedUpdate();

        //}
        //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));
        finishCourtine = true;
        yield return new WaitForFixedUpdate();
        //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
        //yield return new WaitForFixedUpdate();
    }

    //// nust ce are nu imi pot da seama de ce se comporta diferit
    //// mda face ce vreau dar nu arata bine ceva nu este ok si nu imi dau seama ce
    /// sooooo fixedupdate for the win
    /// adica functionalitatea este acolo dar nu imi place cum arata
    /// dar face ce vreau
    /// varianta pt update functioneaza foarte bine cred ca asta are o problema
    /// ceva nu este ok ca waitforfixedupdate se intampla o data la 50 deci ii corect
    /// dar miscarea caracterul cand sta pe platforma nu arata ok
    /// cand se misca caracterul si platforma tho arata cam la fel
    /// dar avand in vedere ca physicsul nu pare afectat daca pun fixedtimu la 0.01 devine ok
    /// sooo not a complete waste of time
    /// nust care ii problema
    /// imi place mai mult cum se misca pe platforma aici tho
    /// plus ca platforma se misca cam la fel la ambele metode 
    /// cred ca o sa cresc fixedTime la 0.01 si o pun pe asta pare mai eficient
    /// am gasit chinemachinu face ceva in cazul in care stau pe loc in fixedupdate dar nu si in newfixedupdate
    /// 
    /// Am sa folosesc coroutina eficienta mai mare si se misca mai bn din ce am vazut eu
    IEnumerator newFixedUpdate()
    {
        while(true)
        {

            if (ok)
            {
                //finishCourtine = false;
                Vector3 startingPos = transform.position;
                Vector3 goulPosition = pos2.position;
                yield return new WaitForSeconds(0.5f);
                //Debug.Log("am intrat");       //works as intended but still jittery
                while (transform.position != goulPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, goulPosition, 4*speed * (Time.deltaTime));
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSeconds(0.5f);
                while (transform.position != respown_position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, respown_position, 2*speed * (Time.deltaTime));
                    yield return new WaitForFixedUpdate();
                }
                yield return new WaitForSeconds(0.5f);
                //finishCourtine = true;
            }
            //Debug.Log(1f / Time.deltaTime);
            yield return new WaitForFixedUpdate();

            ////interesant tot jittery este in cazul asta
            //if (transform.position == pos1.position)
            //{
            //    //moveRight = true;
            //    nextpos = pos2.position;
            //}
            //if (transform.position == pos2.position)
            //{
            //    // moveRight = false;
            //    nextpos = pos1.position;
            //}
            ////if(moveRight)
            ////{
            //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.fixedDeltaTime * 4));
            //yield return new WaitForFixedUpdate();

            //if (once)
            //{
            //    //versiune care face si o pauza cand ajunge la inpeput sau destinatie
            //    if (transform.position == pos1.position && !returnMuvenment)
            //    {
            //        //moveRight = true;
            //        //nextpos = pos2.position;
            //        StartCoroutine(wait(0.5f, pos2.position, 1));

            //    }
            //    if (transform.position == pos1.position && returnMuvenment)
            //    {
            //        //moveRight = true;
            //        //nextpos = pos2.position;
            //        //StartCoroutine(wait(0.5f, pos2.position));
            //        once = false;
            //        returnMuvenment = false;
            //        //goingToPos1 = false;
            //        //goingToPos2 = false;
            //    }
            //    if (transform.position == pos2.position)
            //    {
            //        // moveRight = false;
            //        //nextpos = pos1.position;
            //        StartCoroutine(wait(0.5f, pos1.position, 2));
            //        returnMuvenment = true;

            //    }
            //    //if(moveRight)
            //    //{
            //    if (goingToPos2)
            //    {
            //        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));
            //    }
            //    else if (goingToPos1)
            //    {
            //        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));

            //    }
            //}
            ////Debug.Log(1f / Time.deltaTime);
            ////yield return null;
            //yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator WaitForSecondsInFixedUpdate(float time)
    {
        float loops = (time / 0.02f);
        for (int vlr = 0; vlr < loops; vlr++)
        {
            yield return new WaitForFixedUpdate();
        }
    }
    void OnCollisionEnter2D(Collision2D col)        //sau Stay ambele merg ok
    {
        if (col.gameObject.name.Equals("Player"))
        {
            //finishCourtine = false;
            //Invoke("DropPlatform", 0.5f);
            //StartCoroutine("MovePlatform", respown_possition);
            col.collider.transform.SetParent(transform);
            GameMaster.onMovingPlatform = true;
            ok = true;
            once = true;
            //if(ok && finishCourtine)
            //{
            //    finishCourtine = false;
            //    StartCoroutine(MovePlatform(respown_position)); //nu am vazut era lag
            //}
            //if(finishCourtine)
            //{
            //    finishCourtine = false;
            //    StartCoroutine(MovePlatform2(respown_position));
            //}
        }
    }
    //IEnumerator OnCollisionEnter2D(Collision2D col)       //coroutina oncollisionEnter2D
    //{
    //    col.collider.transform.SetParent(transform);
    //    StartCoroutine(MovePlatform2(respown_position));
    //    yield return new WaitForFixedUpdate();
    //}
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            col.collider.transform.SetParent(null);
            GameMaster.onMovingPlatform = false;
            ok = false;
        }
        //while (transform.position == pos2.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * (Time.deltaTime * 2));
        //}
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
    //bool startMovenment=false;
    bool returnMuvenment=false;
    bool goingToPos2 = false;
    bool goingToPos1 = false;

    Vector3 nextpos;

    IEnumerator wait(float time,Vector3 newpos,int nr)
    {
        yield return new WaitForSeconds(time);
        nextpos = newpos;
        //if(nr==1)
        //{
        //    goingToPos2 = true;
        //    goingToPos1 = false;
        //}
        //else
        //{
        //    goingToPos1 = true;
        //    goingToPos2 = false;
        //}
        goingToPos2 = nr == 1;
        goingToPos1 = nr == 2;
    }
    //void Start()
    //{
    //    nextpos = startpos.position;
    //}

    ////pt fixed update 0.02 asta pastreaza caracterul intr-un loc soooo are avantaje
    //void FixedUpdate() //god bless the fixedupdate
    //{

    //    ////This works really well but i dont know why coroutines didnt want to cooperate
    //    if (once)
    //    {
    //        //versiune care face si o pauza cand ajunge la inpeput sau destinatie
    //        if (transform.position == pos1.position && !returnMuvenment)
    //        {
    //            //moveRight = true;
    //            //nextpos = pos2.position;
    //            StartCoroutine(wait(0.5f, pos2.position, 1));

    //        }
    //        if (transform.position == pos1.position && returnMuvenment)
    //        {
    //            //moveRight = true;
    //            //nextpos = pos2.position;
    //            //StartCoroutine(wait(0.5f, pos2.position));
    //            once = false;
    //            returnMuvenment = false;
    //            //goingToPos1 = false;
    //            //goingToPos2 = false;
    //        }
    //        if (transform.position == pos2.position)
    //        {
    //            // moveRight = false;
    //            //nextpos = pos1.position;
    //            StartCoroutine(wait(0.5f, pos1.position, 2));
    //            returnMuvenment = true;

    //        }
    //        //if(moveRight)
    //        //{
    //        if (goingToPos2)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 4));
    //        }
    //        else if (goingToPos1)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));

    //        }



    //        //    //transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
    //        //    //}
    //        //    //if (ok && finishCourtine)
    //        //    //{
    //        //    //    finishCourtine = false;
    //        //    //    StartCoroutine(MovePlatform(respown_position));
    //        //    //}
    //        ////    if (ok && finishCourtine)
    //        ////{
    //        ////    finishCourtine = false;
    //        ////    StartCoroutine(MovePlatform2(respown_position));
    //        ////}
    //        ////if (ok && finishCourtine)
    //        ////{
    //        ////    finishCourtine = false;
    //        ////    StartCoroutine(MovePlatform3(respown_position));

    //        ////}
    //        ////if(transform.position==pos1.position)
    //        ////{
    //        ////    nextpos = pos2.position;
    //        ////    startMovenment = true;
    //        ////    returnMuvenment = false;
    //        ////}
    //        ////if(transform.position==pos2.position)
    //        ////{
    //        ////    nextpos = pos1.position;
    //        ////    returnMuvenment = true;
    //        ////    startMovenment = false;
    //        ////}
    //        ////if(startMovenment)
    //        ////{
    //        ////    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
    //        ////    if(transform.position == pos1.position)
    //        ////    {
    //        ////        finishCourtine = false;
    //        ////        StartCoroutine(wait(0.5f));
    //        ////    }
    //        ////}
    //        ////if(returnMuvenment)
    //        ////{
    //        ////    transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * (Time.deltaTime * 2));
    //        ////    if (transform.position == pos2.position)
    //        ////    {
    //        ////        finishCourtine = false;
    //        ////        StartCoroutine(wait(0.5f));
    //        ////    }
    //    }



    //}
}
