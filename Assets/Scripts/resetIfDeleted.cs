using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class resetIfDeleted : MonoBehaviour
{
    public TMPro.TextMeshProUGUI panel1TextNewGame;
    public TMPro.TextMeshProUGUI panel2TextNewGame;
    public TMPro.TextMeshProUGUI panel3TextNewGame;
    public TMPro.TextMeshProUGUI panel1NrDeath;
    public TMPro.TextMeshProUGUI panel2NrDeath;
    public TMPro.TextMeshProUGUI panel3NrDeath;
    public TMPro.TextMeshProUGUI panel1Time;
    public TMPro.TextMeshProUGUI panel2Time;
    public TMPro.TextMeshProUGUI panel3Time;
    public GameObject saveslot1;
    public GameObject saveslot2;
    public GameObject saveslot3;
    private bool save1Exists = false;
    private bool save2Exists = false;
    private bool save3Exists = false;
    public RawImage caracterPicPanel1;
    public RawImage caracterPicPanel2;
    public RawImage caracterPicPanel3;

    private int hours;
    private int minutes;
    void OnEnable()
    {
        string path = Application.persistentDataPath + "/player" + 1 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            //Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot1.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
            saveslot1.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            panel1TextNewGame.text = "Start new game!!";
            //caracterPicPanel1.CrossFadeAlpha(0,0,false);
        }
        else
        {
            save1Exists = true;
            panel1TextNewGame.text = "";
            caracterPicPanel1.enabled = true;
            panel1Time.enabled = true;
            panel1NrDeath.enabled = true;
            PlayerData data = SaveSystem.LoadPlayer(1);
            panel1NrDeath.text = data.nr_deaths.ToString();
            //panel1Time.text = data.timeInGame.ToString("F3");
            hours = (int)(data.timeInGame / 3600);
            minutes = (int)((data.timeInGame % 3600));
            panel1Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");

        }

        path = Application.persistentDataPath + "/player" + 2 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            //Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot2.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
            saveslot2.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            panel2TextNewGame.text = "Start new game!!";
        }
        else
        {
            save2Exists = true;
            caracterPicPanel2.enabled = true;
            panel2Time.enabled = true;
            panel2NrDeath.enabled = true;
            panel2TextNewGame.text = "";
            PlayerData data = SaveSystem.LoadPlayer(2);
            panel2NrDeath.text = data.nr_deaths.ToString();
            //panel2Time.text = data.timeInGame.ToString("F3");
            //panel2Time.text = ((int)(data.timeInGame / 216000)).ToString() + ":" + ((int)(data.timeInGame / 3600)).ToString() + ":" + ((int)(data.timeInGame / 60)).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
            hours = (int)(data.timeInGame / 3600);
            minutes = (int)((data.timeInGame % 3600));
            panel2Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        }

        path = Application.persistentDataPath + "/player" + 3 + ".ok";
        if (!System.IO.File.Exists(path))
        {
            Debug.Log("da");
            //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
            //saves1.text = "da";
            saveslot3.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
            saveslot3.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            panel3TextNewGame.text = "Start new game!!";
            //caracterPicPanel3.CrossFadeAlpha(0, 0, false);
            //caracterPicPanel3.color = new Color(caracterPicPanel3.color.r, caracterPicPanel3.color.g, caracterPicPanel3.color.r, 0);
            //caracterPicPanel3.enabled = false;
        }
        else
        {
            save3Exists = true;
            panel3TextNewGame.text = "";
            caracterPicPanel3.enabled = true;
            panel3Time.enabled = true;
            panel3NrDeath.enabled = true;
            PlayerData data = SaveSystem.LoadPlayer(3);
            panel3NrDeath.text = data.nr_deaths.ToString();
            //panel3Time.text = data.timeInGame.ToString("F3");
            //panel3Time.text = ((int)(data.timeInGame / 216000)).ToString() + ":" + ((int)(data.timeInGame / 3600)).ToString() + ":" + ((int)(data.timeInGame / 60)).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
            hours = (int)(data.timeInGame / 3600);
            minutes = (int)((data.timeInGame % 3600));
            panel3Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        }
    }
  
}
