using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //public bool ok = false;
    //private GameMaster gm;
    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameMaster.lastCheckPos = new Vector2(0, 0);
            transform.position = GameMaster.lastCheckPos;
            GameMaster.nextlevel = true;
            //Player.instance.SavePlayer();
            //Player.instance.LoadPlayer();
            //Player.instance.SavePlayer();
        }

    }
}
