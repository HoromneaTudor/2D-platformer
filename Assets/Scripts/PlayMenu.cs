using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayMenu : MonoBehaviour
{
    //public Player player;
    public static int save;
    public static bool neww = false;
    public GameObject saveslot1;
    public GameObject saveslot2;
    public GameObject saveslot3;
    public GameObject selectstage;
    public GameObject saveMenu;
    //public GameObject savepannel1;
    //public GameObject savepannel2;
    //public GameObject savepannel3;
    //public TextMeshProUGUI saves1;
    //private GameMaster gm;
    void Start()
    {
        string path = Application.persistentDataPath + "/player" + 1 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            //Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot1.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
            //savepannel1.GetComponentInChildren<TextMeshProUGUI>().text = "nume";
        }
        path = Application.persistentDataPath + "/player" + 2 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            //Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot2.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        }
        path = Application.persistentDataPath + "/player" + 3 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot3.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        }
    }
    public void PlaySlot1()
    {
        PlayerData data = SaveSystem.LoadPlayer(1);
        if (data == null)
        {
            NewGame();
            Debug.Log("creatomg new file...");
        }
        else if(data.in_level)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        }
        else
        {
            selectstage.SetActive(true);
            saveMenu.SetActive(false);
        }
        save = 1;
    }
    public void PlaySlot2()
    {
        PlayerData data = SaveSystem.LoadPlayer(2);
        if (data == null)
        {
            NewGame();
            Debug.Log("creatomg new file...");
        }
        else if (data.in_level)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        }
        else
        {
            selectstage.SetActive(true);
            saveMenu.SetActive(false);
        }
        save = 2;
    }
    public void PlaySlot3()
    {
        PlayerData data = SaveSystem.LoadPlayer(3);
        if (data == null)
        {
            NewGame();
            Debug.Log("creatomg new file...");
        }
        else if (data.in_level)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        }
        else
        {
            selectstage.SetActive(true);
            saveMenu.SetActive(false);
        }
        save = 3;
    }
    void NewGame()
    {
        neww = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameMaster.lastCheckPos = new Vector2(0f, 0f);

    }
}
