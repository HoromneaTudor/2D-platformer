using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//v1 static class
static public class GameMaster //: MonoBehaviour
{
    //private static GameMaster instance;
    static public Vector2 lastCheckPos;
    static public string checkpointName;
    //private static Movement inst2;
    static public bool ok;
    static public bool nextlevel=false;
    static public bool isDashing = false;
    static public bool shake = false;
    static public bool wasingame = false;
    static public bool isExitingLevel = false;
    static public bool returnToSelectStage = false;
    static public int nr_death=0;
    static public float timeInGame;
    static public bool creatingNewSave = false;
    static public Resolution savedResolution;
    //static public int currentRezolutionIndex = 0;
    static public bool isFullScreen = false;
    static public bool screenShake = false;
    static public bool enteringStage = false;
    static public bool DashChangeColor = false;
    static public bool resetDash = false;
    static public bool playCheckpointSaved = false;
    static public bool onMovingPlatform = false;
    static public bool playDeathTransition = false;
    //static public bool onMovingPlatform = false;
    //static public int SavedQuality;
    //static public bool saveSettings = true;
    //static public bool settingsFileExists = false;

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
    // Start is called before the first frame update

}

//v2 undestructible instance
//public class GameMaster : MonoBehaviour
//{
//    private static GameMaster instance;
//    public Vector2 lastCheckPos;
//    public string checkpointName;
//    //private static Movement inst2;
//    public bool ok;

//    void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(instance);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    // Start is called before the first frame update

//}
