using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security;

public class XMLManager : MonoBehaviour
{
    public static XMLManager Ins;

    void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(Ins);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveSettings()
    {
        Settings set = new Settings();
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        //string path = Application.persistentDataPath + "/settings";
        string path = Application.persistentDataPath + "/settings";
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, set);
        stream.Close();
    }
    public void LoadSettings()
    {
        Settings set = new Settings();
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        //string path = Application.persistentDataPath + "/settings";
        string path = Application.persistentDataPath + "/settings";
        if (File.Exists(path))
        {
            //Debug.Log("aici");
            FileStream stream = new FileStream(path, FileMode.Open);
            set = serializer.Deserialize(stream) as Settings;
            GameMaster.savedResolution = set.savedResolution;
            GameMaster.isFullScreen = set.isFullScreen;
            //GameMaster.currentRezolutionIndex = set.RezolutionIndex;
            GameMaster.screenShake = set.ScreenShake;
            //GameMaster.settingsFileExists = true;
            stream.Close();
        }
        else
        {
            //Debug.Log("aici2");
            FileStream stream = new FileStream(path, FileMode.Create);
            set.savedResolution = Screen.currentResolution;
            set.isFullScreen = true;    //asa cred ca las
            //set.RezolutionIndex = Screen.resolutions.Length - 1;
            set.ScreenShake = true;
            GameMaster.savedResolution = set.savedResolution;
            GameMaster.isFullScreen = set.isFullScreen;
            //GameMaster.currentRezolutionIndex = set.RezolutionIndex;
            GameMaster.screenShake = set.ScreenShake;
            serializer.Serialize(stream, set);
            stream.Close();
        }
        //FileStream stream = new FileStream(path, FileMode.Open);
        //if (stream != null)
        //{
        //    GameMaster.settingsFileExists = true;
        //    set = serializer.Deserialize(stream) as Settings;
        //    stream.Close();
        //    GameMaster.savedResolution = set.savedResolution;
        //    GameMaster.isFullScreen = set.isFullScreen;
        //    GameMaster.currentRezolutionIndex = set.RezolutionIndex;
        //    stream.Close();
        //}
        //else
        //{
        //    stream = new FileStream(path, FileMode.Create);
        //    set.savedResolution = Screen.currentResolution;
        //    set.isFullScreen = Screen.fullScreen;
        //    set.RezolutionIndex = 21;
        //    serializer.Serialize(stream, set);
        //    stream.Close();
        //}

    }
    void Update()
    {
        //if (GameMaster.saveSettings)
        //{
        //    SaveSettings();
        //    GameMaster.saveSettings = false;
        //}
    }

}
[System.Serializable]
public class Settings
{
    public Resolution savedResolution;
    public bool isFullScreen;
    //public int RezolutionIndex;
    public bool ScreenShake;
    public Settings()
    {
        savedResolution = GameMaster.savedResolution;
        isFullScreen = GameMaster.isFullScreen;
        //RezolutionIndex = GameMaster.currentRezolutionIndex;
        ScreenShake = GameMaster.screenShake;
    }
}
