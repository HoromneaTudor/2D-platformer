using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDash : MonoBehaviour
{
    public bool ok = false;
    // Start is called before the first frame update
    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ok = true;
            //GameMaster.lastCheckPos = transform.position;
            //GameMaster.checkpointName = this.gameObject.name;
            //GameMaster.ok = true;
            GameMaster.resetDash = true;
            Debug.Log(GameMaster.resetDash);
        }
        //else
        //{
        //    GameMaster.ok = false;
        //    ok = false;
        //}
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (ok)
    //    {
    //        ok = false;
    //    }
    //}
}
