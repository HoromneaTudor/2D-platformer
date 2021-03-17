using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Am fost nevoit sa creez acest script on loc de a folosi navigatorul din unity deoarece varianta de dropdown box textMeshPro imi lasa dupa inchiderea meniului Optiuni niste 
/// linii de navigare ce imi creaza erori dupa distrugerea "inchiderea si deschiderea jocului"
/// </summary>
public class OptionsMenuDropdownTextMeshProNavigationFix : MonoBehaviour
{
    GameObject currentSelected;
    public GameObject RezolutionDropDown;
    public GameObject GraphicsDropDown;
    public GameObject FullScreenToggle;
    private bool ok = false;

    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropDown;
    // Start is called before the first frame update
    void Start()
    {
        ok = true;


        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i=0 ; i<resolutions.Length;  i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            //if (GameMaster.settingsFileExists && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            //{
            //    currentResolutionIndex = i;
            //}

            //if (!GameMaster.settingsFileExists && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            //{
            //    currentResolutionIndex = i;
            //}
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        float yRaw = Input.GetAxis("Vertical");
        bool yDownKey = Input.GetKeyDown(KeyCode.DownArrow);
        bool yUpKey = Input.GetKeyDown(KeyCode.UpArrow);
        currentSelected = EventSystem.current.currentSelectedGameObject;
        if ((yRaw < -0.9 || yDownKey) && currentSelected == RezolutionDropDown && ok)
        {
            ok = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GraphicsDropDown);
            StartCoroutine(wait());
            yRaw = 0;
        }
        if ((yRaw > 0.9 || yUpKey) && currentSelected == GraphicsDropDown && ok)
        {
            ok = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(RezolutionDropDown);
            StartCoroutine(wait());
            yRaw = 0;
        }
        if ((yRaw < -0.9 || yDownKey) && currentSelected == GraphicsDropDown && ok)
        {
            ok = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FullScreenToggle);
            StartCoroutine(wait());
            yRaw = 0;
        }
        if ((yRaw > 0.9 || yUpKey) && currentSelected == FullScreenToggle && ok)
        {
            ok = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GraphicsDropDown);
            StartCoroutine(wait());
            yRaw = 0;
        }


    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        ok = true;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        //GameMaster.isFullScreen = isFullScreen;
    }
    public void setRezolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log(Screen.currentResolution);
        //GameMaster.savedResolution = resolution;
        XMLManager.Ins.SaveSettings();
    }
}
