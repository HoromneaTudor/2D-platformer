using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//using UnityEditor.Animations;
using DG.Tweening;
using UnityEditor;
//using UnityEngine.Windows;
using System.IO;
//using System.Xml.Serialization;

public class MainMenu : MonoBehaviour
{
    //public int index;
    [SerializeField] bool keyDown;
    //[SerializeField] int maxIndex;
    public AudioSource audioSource;
    public GameObject PlayButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;
    public GameObject MainMenu1;
    public GameObject OptionsPannel;
    public GameObject MainMenuPannel;
    //public GameObject selectMenu;
    public static int save;
    public static bool neww = false;
    public GameObject saveslot1;
    public GameObject saveslot2;
    public GameObject saveslot3;
    public GameObject selectstage;
    public GameObject saveMenu;
    public GameObject optionsMenu;
    public GameObject lvl1btn;
    public GameObject optionsfirstbutton;
    public RawImage caracterPicPanel1;
    public RawImage caracterPicPanel2;
    public RawImage caracterPicPanel3;
    //public TMPro.TextMeshProUGUI panel1NameText;
    //public TMPro.TextMeshProUGUI panel1LastLevelText;
    //public TMPro.TextMeshProUGUI panel2NameText;
    //public TMPro.TextMeshProUGUI panel2LastLevelText;
    //public TMPro.TextMeshProUGUI panel3NameText;
    //public TMPro.TextMeshProUGUI panel3LastLevelText;
    public TMPro.TextMeshProUGUI panel1TextNewGame;
    public TMPro.TextMeshProUGUI panel2TextNewGame;
    public TMPro.TextMeshProUGUI panel3TextNewGame;
    public TMPro.TextMeshProUGUI panel1NrDeath;
    public TMPro.TextMeshProUGUI panel2NrDeath;
    public TMPro.TextMeshProUGUI panel3NrDeath;
    public TMPro.TextMeshProUGUI panel1Time;
    public TMPro.TextMeshProUGUI panel2Time;
    public TMPro.TextMeshProUGUI panel3Time;
    //public GameObject panel1;
    //public GameObject panel2;
    //public GameObject panel3;
    private bool save1Exists = false;
    private bool save2Exists = false;
    private bool save3Exists = false;
    //public bool creatingNewSave;
    private float originalPozSlot1;
    private float originalPozSlot2;
    private float originalPozSlot3;
    //public GameObject panelCreatenewSave;
    public GameObject BeginButton;
    public GameObject RenameButton;
    public GameObject BackButton;

    public GameObject ContinueButton;
    public GameObject DeleteButton;
    public GameObject HardcoreButton;

    public Image transitionEffect;

    private float originalPozSaveMenuPannel;
    private float originalPozOptionsPanel;
    private float originalPozMainMenuPanel;
    private float originalPozSelectStagePanel;
    private float originalPozBeginButton;
    private float originalPozRenameButton;
    private float originalPozBackButton;
    private float originalPozContinueButton;
    private float originalPozDeleteButton;
    private float originalPozHardcoreButton;

    private int hours;
    private int minutes;

    VirtualKeyboard vk = new VirtualKeyboard();

    public void OpenKeyboard()
    {
        {
            vk.ShowTouchKeyboard();
        }
    }

    public void CloseKeyboard()
    {
        {
            vk.HideTouchKeyboard();
        }
    }


    void Awake()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
    void Start()
    {
        //Debug.Log(Application.dataPath);
        caracterPicPanel1.enabled = false;
        caracterPicPanel2.enabled = false;
        caracterPicPanel3.enabled = false;
        panel1Time.enabled = false;
        panel2Time.enabled = false;
        panel3Time.enabled = false;
        panel1NrDeath.enabled = false;
        panel2NrDeath.enabled = false;
        panel3NrDeath.enabled = false;
        BeginButton.SetActive(false);
        RenameButton.SetActive(false);
        BackButton.SetActive(false);
        //panelCreatenewSave.SetActive(true) ;

        originalPozOptionsPanel = OptionsPannel.transform.position.x;
        originalPozMainMenuPanel = MainMenuPannel.transform.position.x;
        originalPozSaveMenuPannel = saveMenu.transform.position.x;
        originalPozSelectStagePanel = selectstage.transform.position.y;
        originalPozSlot1 = saveslot1.transform.position.y;
        originalPozSlot2 = saveslot2.transform.position.y;
        originalPozSlot3 = saveslot3.transform.position.y;
        originalPozBeginButton = BeginButton.transform.position.x;
        originalPozRenameButton = RenameButton.transform.position.x;
        originalPozBackButton = BackButton.transform.position.x;
        originalPozContinueButton = ContinueButton.transform.position.x;
        originalPozDeleteButton = DeleteButton.transform.position.x;
        originalPozHardcoreButton = HardcoreButton.transform.position.x;


        XMLManager.Ins.LoadSettings();
        Screen.SetResolution(GameMaster.savedResolution.width, GameMaster.savedResolution.height, GameMaster.isFullScreen,GameMaster.savedResolution.refreshRate);

        //activate transition tween
        transitionEffect.DOFade(0, 0.5f);

        //MainMenuPannel.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        //BeginButton.transform.position = new Vector2(1000, BeginButton.transform.position.y);
        //Button btnPlay = PlayButton.GetComponent<Button>();
        //Button btnOptions = OptionsButton.GetComponent<Button>();
        //Button btnExit = ExitButton.GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PlayButton);
        if (GameMaster.returnToSelectStage)
        {

            StartCoroutine(transitionFromGameToSelectStage());
            GameMaster.returnToSelectStage = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lvl1btn);

        }
        //string path = Application.persistentDataPath + "/player" + 1 + ".ok";
        //if (!System.IO.File.Exists(path))
        //{
        //    //Debug.Log("da");
        //    //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
        //    //saves1.text = "da";
        //    saveslot1.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        //    saveslot1.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        //    panel1TextNewGame.text = "Start new game!!";
        //    //caracterPicPanel1.CrossFadeAlpha(0,0,false);
        //}
        //else
        //{
        //    save1Exists = true;
        //    panel1TextNewGame.text = "";
        //    caracterPicPanel1.enabled = true;
        //    panel1Time.enabled = true;
        //    panel1NrDeath.enabled = true;
        //    PlayerData data = SaveSystem.LoadPlayer(1);
        //    panel1NrDeath.text = data.nr_deaths.ToString();
        //    //panel1Time.text = data.timeInGame.ToString("F3");
        //    hours = (int)(data.timeInGame / 3600);
        //    minutes = (int)((data.timeInGame % 3600));
        //    panel1Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");

        //}

        //path = Application.persistentDataPath + "/player" + 2 + ".ok";
        //if (!System.IO.File.Exists(path))
        //{
        //    //Debug.Log("da");
        //    //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
        //    //saves1.text = "da";
        //    saveslot2.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        //    saveslot2.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        //    panel2TextNewGame.text = "Start new game!!";
        //}
        //else
        //{
        //    save2Exists = true;
        //    caracterPicPanel2.enabled = true;
        //    panel2Time.enabled = true;
        //    panel2NrDeath.enabled = true;
        //    panel2TextNewGame.text = "";
        //    PlayerData data = SaveSystem.LoadPlayer(2);
        //    panel2NrDeath.text = data.nr_deaths.ToString();
        //    //panel2Time.text = data.timeInGame.ToString("F3");
        //    //panel2Time.text = ((int)(data.timeInGame / 216000)).ToString() + ":" + ((int)(data.timeInGame / 3600)).ToString() + ":" + ((int)(data.timeInGame / 60)).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        //    hours = (int)(data.timeInGame / 3600);
        //    minutes = (int)((data.timeInGame % 3600));
        //    panel2Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        //}

        //path = Application.persistentDataPath + "/player" + 3 + ".ok";
        //if (!System.IO.File.Exists(path))
        //{
        //    Debug.Log("da");
        //    //GameObject.Find("save slot 1").GetComponentInChildren<Text>().text = "Button Text";
        //    //saves1.text = "da";
        //    saveslot3.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
        //    saveslot3.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        //    panel3TextNewGame.text = "Start new game!!";
        //    //caracterPicPanel3.CrossFadeAlpha(0, 0, false);
        //    //caracterPicPanel3.color = new Color(caracterPicPanel3.color.r, caracterPicPanel3.color.g, caracterPicPanel3.color.r, 0);
        //    //caracterPicPanel3.enabled = false;
        //}
        //else
        //{
        //    save3Exists = true;
        //    panel3TextNewGame.text = "";
        //    caracterPicPanel3.enabled = true;
        //    panel3Time.enabled = true;
        //    panel3NrDeath.enabled = true;
        //    PlayerData data = SaveSystem.LoadPlayer(3);
        //    panel3NrDeath.text = data.nr_deaths.ToString();
        //    //panel3Time.text = data.timeInGame.ToString("F3");
        //    //panel3Time.text = ((int)(data.timeInGame / 216000)).ToString() + ":" + ((int)(data.timeInGame / 3600)).ToString() + ":" + ((int)(data.timeInGame / 60)).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        //    hours = (int)(data.timeInGame / 3600);
        //    minutes = (int)((data.timeInGame % 3600));
        //    panel3Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        //}
        //daca o sa fac cu xml sa ma gandesc
        //XMLManager.Ins.LoadSettings();
        //if (GameMaster.settingsFileExists)
        //{
        //    Screen.SetResolution(GameMaster.savedResolution.width, GameMaster.savedResolution.height, GameMaster.isFullScreen);
        //    Debug.Log(Screen.currentResolution);
        //    //Debug.Log(Screen.height);
        //}

    }
    //public TextMeshProUGUI saves1;
    //private GameMaster gm;


    public void PlaySlot1()
    {
        PlayerData data = SaveSystem.LoadPlayer(1);
        if (data == null)
        {
            //creatingNewSave = true;
            GameMaster.creatingNewSave = true;
            saveslot2.transform.DOMoveY(-1000, 0.5f, false);
            saveslot3.transform.DOMoveY(-1000, 0.5f, false);
            saveslot1.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setBeginRenameBack();
            //BeginButton.transform.DOMoveX(200, 0.5f, false);
            Invoke("saveslot1DisableButtons", 0.5f);
            //panelCreatenewSave.SetActive(false);
            //panelCreatenewSave.transform.DOMoveX(200, 1f, false);
            //StartCoroutine(wait());
            //StartCoroutine(saveslor1New());
            //NewGame();
            //Debug.Log(creatingNewSave);
            //Debug.Log("creatomg new file...");

        }
        else
        {
            GameMaster.enteringStage = true;
            saveslot2.transform.DOMoveY(-1000, 0.5f, false);
            saveslot3.transform.DOMoveY(-1000, 0.5f, false);
            saveslot1.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setContinueDeleteHardcore();
            Invoke("saveslot1DisableButtons", 0.5f);
        }
        ///Asta trebuie adaugata la begin
        //else if (data.in_level)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;

        //}
        //else
        //{
        //    selectstage.SetActive(true);
        //    saveMenu.SetActive(false);
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(lvl1btn);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;
        //}
        save = 1;
    }
    public void PlaySlot2()
    {
        PlayerData data = SaveSystem.LoadPlayer(2);
        if (data == null)
        {
            saveslot1.transform.DOMoveY(1000, 0.5f, false);
            saveslot3.transform.DOMoveY(-1000, 0.5f, false);
            saveslot2.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setBeginRenameBack();
            Invoke("saveslot2DisableButtons", 0.5f);
            //StartCoroutine(saveslor2New());
            //NewGame();
            //Debug.Log("creatomg new file...");
            GameMaster.creatingNewSave = true;
            //creatingNewSave = true;
        }
        else
        {
            GameMaster.enteringStage = true;
            saveslot1.transform.DOMoveY(1000, 0.5f, false);
            saveslot3.transform.DOMoveY(-1000, 0.5f, false);
            saveslot2.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setContinueDeleteHardcore();
            Invoke("saveslot2DisableButtons", 0.5f);
        }
        //else if (data.in_level)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;

        //}
        //else
        //{
        //    selectstage.SetActive(true);
        //    saveMenu.SetActive(false);
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(lvl1btn);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;
        //}
        save = 2;
    }
    public void PlaySlot3()
    {
        PlayerData data = SaveSystem.LoadPlayer(3);
        if (data == null)
        {
            saveslot1.transform.DOMoveY(1000, 0.5f, false);
            saveslot2.transform.DOMoveY(1000, 0.5f, false);
            saveslot3.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setBeginRenameBack();
            Invoke("saveslot3DisableButtons", 0.5f);
            //StartCoroutine(saveslor3New());
            //NewGame();
            //Debug.Log("creatomg new file...");
            GameMaster.creatingNewSave = true;
            //creatingNewSave = true;
        }
        else
        {
            GameMaster.enteringStage = true;
            saveslot1.transform.DOMoveY(1000, 0.5f, false);
            saveslot2.transform.DOMoveY(1000, 0.5f, false);
            saveslot3.transform.DOMoveY(this.transform.position.y, 0.5f, false);
            setContinueDeleteHardcore();
            Invoke("saveslot3DisableButtons", 0.5f);
        }
        //else if (data.in_level)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;

        //}
        //else
        //{
        //    selectstage.SetActive(true);
        //    saveMenu.SetActive(false);
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(lvl1btn);
        //    GameMaster.nr_death = data.nr_deaths;
        //    GameMaster.timeInGame = data.timeInGame;
        //}
        save = 3;
    }
    
    public void ContinueButtonClick()
    {
        PlayerData data = SaveSystem.LoadPlayer(MainMenu.save);
        if (data.in_level)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + data.level);
            StartCoroutine(transitionFromMenuToGame(data.level));
            GameMaster.nr_death = data.nr_deaths;
            GameMaster.timeInGame = data.timeInGame;

        }
        else
        {
            //selectstage.SetActive(true);
            //saveMenu.SetActive(false);
            StartCoroutine(transitionFromSaveMenuToSelectStage());
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lvl1btn);
            GameMaster.nr_death = data.nr_deaths;
            GameMaster.timeInGame = data.timeInGame;
        }
    }
    public void DeleteFile()
    {
        File.Delete(Application.persistentDataPath + "/player" + MainMenu.save + ".ok");
        saveslot1.SetActive(true);
        saveslot2.SetActive(true);
        saveslot3.SetActive(true);
        if(MainMenu.save==1)
        {
            caracterPicPanel1.enabled = false;
            panel1Time.enabled = false;
            panel1NrDeath.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(saveslot1);
            //panel2TextNewGame.text = "";
            //PlayerData data = SaveSystem.LoadPlayer(2);
            //panel2NrDeath.text = data.nr_deaths.ToString();
            //panel2Time.text = data.timeInGame.ToString("F3");
            //panel2Time.text = ((int)(data.timeInGame / 216000)).ToString() + ":" + ((int)(data.timeInGame / 3600)).ToString() + ":" + ((int)(data.timeInGame / 60)).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
            //hours = (int)(data.timeInGame / 3600);
            //minutes = (int)((data.timeInGame % 3600));
            //panel2Time.text = hours.ToString() + ":" + (minutes / 60).ToString() + ":" + (data.timeInGame % 60).ToString("F3");
        }
        else if(MainMenu.save==2)
        {
            caracterPicPanel2.enabled = false;
            panel2Time.enabled = false;
            panel2NrDeath.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(saveslot2);
        }
        else
        {
            caracterPicPanel3.enabled = false;
            panel3Time.enabled = false;
            panel3NrDeath.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(saveslot3);
        }
        //saveslot1.transform.position = originalPozSlot1;
        //saveslot2.transform.position = originalPozSlot2;
        //saveslot3.transform.position = originalPozSlot3;
        saveslot1.transform.DOMoveY(originalPozSlot1, 0.5f, false);
        saveslot2.transform.DOMoveY(originalPozSlot2, 0.5f, false);
        saveslot3.transform.DOMoveY(originalPozSlot3, 0.5f, false);
        resetContinueDeleteHardcore();
        //creatingNewSave = false;
        GameMaster.enteringStage = false;
    }

    //IEnumerator saveslor1New()
    //{
    //    saveslot2.transform.DOMoveY(-1000, 1f, false);
    //    saveslot3.transform.DOMoveY(-1000, 1f, false);
    //    saveslot1.transform.DOMoveY(200, 1f, false);
    //    yield return new WaitForSeconds(1f);
    //    saveslot2.SetActive(false);
    //    saveslot3.SetActive(false);
    //}

    //IEnumerator saveslor2New()
    //{
    //    saveslot1.transform.DOMoveY(1000, 1f, false);
    //    saveslot3.transform.DOMoveY(-1000, 1f, false);
    //    saveslot2.transform.DOMoveY(200, 1f, false);
    //    yield return new WaitForSeconds(1f);
    //    saveslot1.SetActive(false);
    //    saveslot3.SetActive(false);
    //}

    //IEnumerator saveslor3New()
    //{
    //    saveslot1.transform.DOMoveY(1000, 1f, false);
    //    saveslot2.transform.DOMoveY(1000, 1f, false);
    //    saveslot3.transform.DOMoveY(200, 1f, false);
    //    yield return new WaitForSeconds(1f);
    //    saveslot1.SetActive(false);
    //    saveslot2.SetActive(false);
    //}
    void saveslot1DisableButtons()
    {
        saveslot2.SetActive(false);
        saveslot3.SetActive(false);
    }
    void saveslot2DisableButtons()
    {
        saveslot1.SetActive(false);
        saveslot3.SetActive(false);
    }
    void saveslot3DisableButtons()
    {
        saveslot1.SetActive(false);
        saveslot2.SetActive(false);
    }
    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("merge");
    //}
    //private void createNewSave()
    //{

    //}

    void setBeginRenameBack()
    {
        BeginButton.SetActive(true);
        RenameButton.SetActive(true);
        BackButton.SetActive(true);
        BeginButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        RenameButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        BackButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
    }
    void disableBeginRenameBack()
    {
        BeginButton.SetActive(false);
        RenameButton.SetActive(false);
        BackButton.SetActive(false);
    }
    void resetBeginRenameBack()
    {
        BeginButton.transform.DOMoveX(originalPozBeginButton, 0.5f, false);
        RenameButton.transform.DOMoveX(originalPozRenameButton, 0.5f, false);
        BackButton.transform.DOMoveX(originalPozBackButton, 0.5f, false);
        Invoke("disableBeginRenameBack", 0.5f);
    }
    void setContinueDeleteHardcore()
    {
        ContinueButton.SetActive(true);
        DeleteButton.SetActive(true);
        HardcoreButton.SetActive(true);
        ContinueButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        DeleteButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        HardcoreButton.transform.DOMoveX(this.transform.position.x, 0.5f, false);
    }
    void disableContinueDeleteHardcore()
    {
        ContinueButton.SetActive(false);
        DeleteButton.SetActive(false);
        HardcoreButton.SetActive(false);
    }
    void resetContinueDeleteHardcore()
    {
        ContinueButton.transform.DOMoveX(originalPozContinueButton, 0.5f, false);
        DeleteButton.transform.DOMoveX(originalPozDeleteButton, 0.5f, false);
        HardcoreButton.transform.DOMoveX(originalPozHardcoreButton, 0.5f, false);
        Invoke("disableContinueDeleteHardcore", 0.5f);
    }
    public void NewGame()
    {
        neww = true;
        StartCoroutine(transitionFromMenuToGame(1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameMaster.lastCheckPos = new Vector2(0f, 0f);
        GameMaster.nr_death = 0;
        GameMaster.timeInGame = 0;

    }
    public void OpenSaveMenu()
    {
        saveMenu.SetActive(true);
        //MainMenuPannel.transform.DOMoveX(-1000, 0.5f, false);
        //OptionsPannel.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        StartCoroutine(transitionFromMainMenuToSaveMenu());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(saveslot1);
    }
    public void openOptionsMenu()
    {
        //optionsMenu.SetActive(true);
        //OptionsPannel.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        //Invoke("transitionFromMainMenuToOptions", 0.1f);
        StartCoroutine(transitionFromMainMenuToOptions());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsfirstbutton);
    }
    IEnumerator transitionFromMainMenuToOptions()
    {
        optionsMenu.SetActive(true);
        OptionsPannel.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        MainMenuPannel.transform.DOMoveX(-1000, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        MainMenuPannel.SetActive(false);
    }
    IEnumerator transitionFromOptionsToMainMenu()
    {
        MainMenuPannel.SetActive(true);
        MainMenuPannel.transform.DOMoveX(originalPozMainMenuPanel, 0.5f, false);
        OptionsPannel.transform.DOMoveX(originalPozOptionsPanel, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        OptionsPannel.SetActive(false);
    }
    IEnumerator transitionFromMainMenuToSaveMenu()
    {
        saveMenu.SetActive(true);
        saveMenu.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        MainMenuPannel.transform.DOMoveX(-1000, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        MainMenuPannel.SetActive(false);
    }
    IEnumerator transitionFromSaveMenuToMainMenu()
    {
        MainMenuPannel.SetActive(true);
        MainMenuPannel.transform.DOMoveX(originalPozMainMenuPanel, 0.5f, false);
        saveMenu.transform.DOMoveX(originalPozSaveMenuPannel, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        saveMenu.SetActive(false);
    }
    IEnumerator transitionFromSelectStageToSaveMenu()
    {
        saveMenu.SetActive(true);
        selectstage.transform.DOMoveY(originalPozSelectStagePanel, 0.5f, false);
        saveMenu.transform.DOMoveX(this.transform.position.x, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        selectstage.SetActive(false);
    }
    IEnumerator transitionFromGameToSelectStage()
    {
        selectstage.SetActive(true);
        MainMenuPannel.SetActive(false);
        MainMenuPannel.transform.DOMoveX(-1000, 0.2f, false);
        selectstage.transform.DOMoveY(this.transform.position.y, 0.5f, false);
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator transitionFromSaveMenuToSelectStage()
    {
        selectstage.SetActive(true);
        selectstage.transform.DOMoveY(this.transform.position.y, 0.5f, false);
        saveMenu.transform.DOMoveX(originalPozSaveMenuPannel, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        saveMenu.SetActive(false);
    }
    IEnumerator transitionFromMenuToGame(int index)
    {
        transitionEffect.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Escape"))
        {
            if (MainMenu1.activeInHierarchy)
            {
                //Debug.Log("ok");
                saveMenu.SetActive(false);
                MainMenu1.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(PlayButton);
            }
            if (saveMenu.activeInHierarchy && GameMaster.creatingNewSave == false && GameMaster.enteringStage == false)
            {
                //Debug.Log("ok");
                //Debug.Log(creatingNewSave);
                //saveMenu.SetActive(false);
                //MainMenu1.SetActive(true);
                StartCoroutine(transitionFromSaveMenuToMainMenu());
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(PlayButton);
            }
            else if (saveMenu.activeInHierarchy && GameMaster.creatingNewSave == true)
            {
                saveslot1.SetActive(true);
                saveslot2.SetActive(true);
                saveslot3.SetActive(true);
                //saveslot1.transform.position = originalPozSlot1;
                //saveslot2.transform.position = originalPozSlot2;
                //saveslot3.transform.position = originalPozSlot3;
                saveslot1.transform.DOMoveY(originalPozSlot1, 0.5f, false);
                saveslot2.transform.DOMoveY(originalPozSlot2, 0.5f, false);
                saveslot3.transform.DOMoveY(originalPozSlot3, 0.5f, false);
                resetBeginRenameBack();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(saveslot1);
                //creatingNewSave = false;
                GameMaster.creatingNewSave = false;
            }
            else if (saveMenu.activeInHierarchy && GameMaster.enteringStage == true)
            {
                saveslot1.SetActive(true);
                saveslot2.SetActive(true);
                saveslot3.SetActive(true);
                //saveslot1.transform.position = originalPozSlot1;
                //saveslot2.transform.position = originalPozSlot2;
                //saveslot3.transform.position = originalPozSlot3;
                saveslot1.transform.DOMoveY(originalPozSlot1, 0.5f, false);
                saveslot2.transform.DOMoveY(originalPozSlot2, 0.5f, false);
                saveslot3.transform.DOMoveY(originalPozSlot3, 0.5f, false);
                resetContinueDeleteHardcore();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(saveslot1);
                //creatingNewSave = false;
                GameMaster.enteringStage = false;
            }
            if (selectstage.activeInHierarchy)
            {
                //saveMenu.SetActive(true);
                //selectstage.SetActive(false);
                StartCoroutine(transitionFromSelectStageToSaveMenu());
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(saveslot1);
                GameMaster.creatingNewSave = false;
                GameMaster.enteringStage = false;
            }
            if (optionsMenu.activeInHierarchy)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //GameMaster.saveSettings = true;
                //GameMaster.isFullScreen = Screen.fullScreen;
                //GameMaster.savedResolution = Screen.currentResolution;
                //Debug.Log(Screen.fullScreen);
                //GameMaster.isFullScreen = Screen.fullScreen;
                XMLManager.Ins.SaveSettings();
                XMLManager.Ins.LoadSettings();
                StartCoroutine(transitionFromOptionsToMainMenu());
                //Screen.SetResolution(GameMaster.savedResolution.width, GameMaster.savedResolution.height, GameMaster.isFullScreen, GameMaster.savedResolution.refreshRate);
                //MainMenu1.SetActive(true);
                //optionsMenu.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(OptionsButton);
                //GameMaster.savedResolution = Screen.currentResolution;
                //GameMaster.isFullScreen = Screen.fullScreen;
                //GameMaster.saveSettings = true;
                ////GameMaster.savedResolution = Screen.currentResolution;
                //GameMaster.isFullScreen = Screen.fullScreen;
                //XMLManager.Ins.SaveSettings();
                //XMLManager.Ins.LoadSettings();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //Debug.Log(GameMaster.savedResolution);
            }

        }
    }
    public void selectStage1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameMaster.lastCheckPos = new Vector2(0, 0);

    }
    public void selectStage2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        GameMaster.lastCheckPos = new Vector2(0, 0);

    }
}
