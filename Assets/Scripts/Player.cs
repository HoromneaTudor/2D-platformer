using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public static Player instance { get; set; }
    public int level;
    public string checkpoint;
    //public Vector2 position;
    //public GameMaster gm;
    //public Transform cp0;
    
    //instanta pt moment trebuie distrusa asa ca merge dar camera nu va putea da fallow
    //pentru a acesa din afara clasei foloseste in cazul asta Player.instance.numele_fct_dorite()

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(instance);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    void Start()
    {
        if (GameMaster.nextlevel)
        {
            SavePlayer();
            LoadPlayer();
            SavePlayer();
            GameMaster.nextlevel = false;
        }
        else
        {
            LoadPlayer();
            SavePlayer();
        }
        //LoadPlayer();
        //cp0 = GetComponent<Transform>();
        //Debug.Log(cp0);
        //if (PlayMenu.neww)
        //{
        //    transform.position = cp0.position;
        //    PlayMenu.neww = false;
        //}
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //Debug.Log(Application.persistentDataPath);
    }

    void Update()
    {
        if (GameMaster.ok)
        {
            level = SceneManager.GetActiveScene().buildIndex;
            //Debug.Log(level);
            checkpoint = GameMaster.checkpointName;
            //transform.position = GameMaster.lastCheckPos;
            GameMaster.ok = false;
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayer();
        }
        if(GameMaster.isExitingLevel)
        {
            transform.position = GameMaster.lastCheckPos;
            SavePlayer();
            GameMaster.isExitingLevel = false;
            //GameMaster.returnToSelectStage = true;
        }
        if (GameMaster.returnToSelectStage)
        {
            transform.position = new Vector2(0, 0);
            SavePlayer();
            //GameMaster.returnToSelectStage = true;
        }

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this,MainMenu.save);
        //Debug.Log(MainMenu.save);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(MainMenu.save);
        if (data == null)
        {
            SaveSystem.SavePlayer(this, MainMenu.save);
            data = SaveSystem.LoadPlayer(MainMenu.save);
        }
        //Debug.Log(MainMenu.save);
        //daca nu gaseste imi da eroare tre sa fac un if aici
        level = data.level;
        checkpoint = data.checkpoint;
        Vector2 position1;
        position1.x = data.position[0];
        //Debug.Log(data.position[0]);
        position1.y = data.position[1];
        //Debug.Log(data.position[1]);
        transform.position = position1;
        //GameMaster.nr_death = data.nr_deaths;
        
    }
}
