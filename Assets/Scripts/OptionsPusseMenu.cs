using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class OptionsPusseMenu : MonoBehaviour
{
    private bool ok = false;
    GameObject currentSelected;
    public GameObject RezolutionButton;
    public GameObject GraphicsButton;
    public Toggle FullScreenToggle;
    public Toggle ScreenShakeToggle;
    // private bool ok = false;

    //Resolution[] resolutions;

    //public TMPro.TMP_Dropdown resolutionDropDown;
    // Start is called before the first frame update


    #region Player Pref Key Constants

    private const string RESOLUTION_PREF_KEY = "resolution";

    #endregion

    #region Resolution

    [SerializeField]
    private TMPro.TextMeshProUGUI resolutionText;

    private Resolution[] resolutions;

    private int currentResolutionIndex = 0;

    #endregion
    void Start()
    {
        resolutions = Screen.resolutions;

        XMLManager.Ins.LoadSettings();

        //currentResolutionIndex = GameMaster.currentRezolutionIndex;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (GameMaster.savedResolution.width == resolutions[i].width && GameMaster.savedResolution.height == resolutions[i].height && GameMaster.savedResolution.refreshRate == resolutions[i].refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        FullScreenToggle.isOn = GameMaster.isFullScreen;

        ScreenShakeToggle.isOn = GameMaster.screenShake;

        //ApplyChanges();

        SetResolutionText(resolutions[currentResolutionIndex]);

        ok = true;

    }

    #region Rezolution Cycling

    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = resolution.width + "x" + resolution.height + "   " + resolution.refreshRate + "Hz";
    }

    public void SetNextResolution()
    {
        currentResolutionIndex = getNextWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions[currentResolutionIndex]);
        //GameMaster.currentRezolutionIndex = currentResolutionIndex;
    }
    public void SetPreviousRezolution()
    {
        currentResolutionIndex = getPreviousWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions[currentResolutionIndex]);
        //GameMaster.currentRezolutionIndex = currentResolutionIndex;
    }

    #endregion

    #region Apply Resolution

    private void SetAndApplyResolution(int newResolutionIndex)
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(resolutions[currentResolutionIndex]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);

        //Screen.SetResolution(resolution.width, resolution.height, GameMaster.isFullScreen);
        //PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
        //GameMaster.savedResolution = resolution;
        //GameMaster.currentRezolutionIndex = currentResolutionIndex;
        //XMLManager.Ins.SaveSettings();
    }
    #endregion

    #region Misc Helpers

    #region Index Wrap Helpers

    private int getNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int getPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    #endregion

    #endregion

    public void ApplyChanges()
    {
        SetAndApplyResolution(currentResolutionIndex);
    }


    // Update is called once per frame
    void Update()
    {
        float xRaw = Input.GetAxis("Horizontal");
        bool xLeftKey = Input.GetKeyDown(KeyCode.LeftArrow);
        bool xRightKey = Input.GetKeyDown(KeyCode.RightArrow);
        currentSelected = EventSystem.current.currentSelectedGameObject;
        if ((xRaw < 0 || xLeftKey) && currentSelected == RezolutionButton && ok)
        {
            ok = false;
            SetPreviousRezolution();
            ApplyChanges();
            SetAndApplyResolution(currentResolutionIndex);
            GameMaster.savedResolution = resolutions[currentResolutionIndex];
            Screen.SetResolution(GameMaster.savedResolution.width, GameMaster.savedResolution.height, GameMaster.isFullScreen, GameMaster.savedResolution.refreshRate);
            StartCoroutine(wait());
            XMLManager.Ins.SaveSettings();
            xRaw = 0;
        }
        if ((xRaw > 0 || xRightKey) && currentSelected == RezolutionButton && ok)
        {
            ok = false;
            SetNextResolution();
            ApplyChanges();
            SetAndApplyResolution(currentResolutionIndex);
            GameMaster.savedResolution = resolutions[currentResolutionIndex];
            Screen.SetResolution(GameMaster.savedResolution.width, GameMaster.savedResolution.height, GameMaster.isFullScreen, GameMaster.savedResolution.refreshRate);
            StartCoroutine(wait());
            XMLManager.Ins.SaveSettings();
            xRaw = 0;
        }
        //if ((yRaw < -0.9 || yDownKey) && currentSelected == GraphicsDropDown && ok)
        //{
        //    ok = false;
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(FullScreenToggle);
        //    StartCoroutine(wait());
        //    yRaw = 0;
        //}
        //if ((yRaw > 0.9 || yUpKey) && currentSelected == FullScreenToggle && ok)
        //{
        //    ok = false;
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(GraphicsDropDown);
        //    StartCoroutine(wait());
        //    yRaw = 0;
        //}


    }
    IEnumerator wait()
    {
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.15f));
        ok = true;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        GameMaster.isFullScreen = isFullScreen;
        //XMLManager.Ins.SaveSettings();
    }

    public void ShakeScreen(bool isShakeScreen)
    {
        GameMaster.screenShake = isShakeScreen;
        //XMLManager.Ins.SaveSettings();
    }
    public void setRezolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log(Screen.currentResolution);
        //GameMaster.currentRezolutionIndex = currentResolutionIndex;
        //GameMaster.savedResolution = resolution;
        //Debug.Log(GameMaster.savedResolution);
        //XMLManager.Ins.SaveSettings();
    }
}
