using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuIU;
    public GameObject resumeButton;
    public GameObject pauseMenuPanel;
    public GameObject optionMenuPanel;
    public GameObject optionMenu1StButton;
    public GameObject PausePanel;
    public GameObject PauseMenuOptionsButton;
    public Image transitionEffect;
    //public GameObject textCheckpointSaved;
    public TMPro.TextMeshProUGUI playCheckpointSaved;

    private float originalPosPauseMenuPanel;
    private float originalPosOptionsMenuPanel;
    private bool finishCourtine=false;
    private bool finishCourtineOptions = true;

    // Update is called once per frame
    void Start()
    {
        //Debug.Log("start");
        originalPosPauseMenuPanel = pauseMenuPanel.transform.position.x;
        originalPosOptionsMenuPanel = optionMenuPanel.transform.position.x;
        transitionEffect.DOFade(0, 0.5f);
        playCheckpointSaved.DOFade(0, 0.01f);
    }
    //void OnEnable()
    //{
    //    XMLManager.Ins.SaveSettings();
    //}
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Escape"))
        //{
        //    if (GameIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Escape"))
        {
            if(!pauseMenuPanel.activeInHierarchy)
            {
                Pause();
            }
            if (pauseMenuPanel.activeInHierarchy && finishCourtine && finishCourtineOptions)
            {
                Resume();
                finishCourtine = false;
            }
            if (optionMenuPanel.activeInHierarchy)
            {
                XMLManager.Ins.SaveSettings();
                StartCoroutine(transitionFromOptionsMenuToPauseMenu());
            }
        }
        if(GameMaster.playCheckpointSaved)
        {
            GameMaster.playCheckpointSaved = false;
            StartCoroutine(playCheckpointSavedCourtine());

        }
        if(GameMaster.playDeathTransition)
        {
            GameMaster.playDeathTransition = false;
            StartCoroutine(PlayDeathTransition());
        }
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        //pauseMenuIU.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        StartCoroutine(transitionFromPauseToGame());
    }
    void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        // pauseMenuIU.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        StartCoroutine(transitionFromGameToPauseMenu());
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameMaster.wasingame = true;
        GameMaster.isExitingLevel = true;
        StartCoroutine(transitionFromGameToMainMenu());
        //SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        //Debug.Log("Quitting");
        //Time.timeScale = 1f;
        //GameMaster.wasingame = true;
        //GameMaster.isExitingLevel = true ;
        Application.Quit();
        //SceneManager.LoadScene("MainMenu");
    }
    public void SelectStage()
    {
        Time.timeScale = 1f;
        GameMaster.wasingame = false;
        GameMaster.isExitingLevel = true;
        GameMaster.returnToSelectStage = true;
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(transitionFromGameToMainMenu());
    }

    public void openOptionMenu()
    {
        StartCoroutine(transitionFromPauseMenuToOptionsMenu());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionMenu1StButton);
    }
    IEnumerator transitionFromPauseMenuToOptionsMenu()
    {
        finishCourtineOptions = false;
        optionMenuPanel.SetActive(true);
        optionMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        pauseMenuPanel.transform.DOMoveX(originalPosPauseMenuPanel, 0.5f, false).SetUpdate(true);
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.5f));
        pauseMenuPanel.SetActive(false);
    }
    IEnumerator transitionFromGameToPauseMenu()
    {
        pauseMenuIU.SetActive(true);
        //optionMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        pauseMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.5f));
        //pauseMenuPanel.SetActive(false);
        finishCourtine = true;
    }
    IEnumerator transitionFromPauseToGame()
    {
        //optionMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        pauseMenuPanel.transform.DOMoveX(originalPosPauseMenuPanel, 0.3f, false).SetUpdate(true);
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.5f));
        pauseMenuPanel.SetActive(false);

    }
    IEnumerator transitionFromOptionsMenuToPauseMenu()
    {
        //optionMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        pauseMenuPanel.SetActive(true);
        optionMenuPanel.transform.DOMoveX(originalPosOptionsMenuPanel, 0.3f, false).SetUpdate(true);
        pauseMenuPanel.transform.DOMoveX(this.transform.position.x, 0.5f, false).SetUpdate(true);
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.5f));
        optionMenuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseMenuOptionsButton);
        finishCourtineOptions = true;

    }
    IEnumerator transitionFromGameToMainMenu()
    {
        transitionEffect.DOFade(1, 0.5f).SetUpdate(true);
        yield return StartCoroutine(ButtonEffectsPauseMenu.CoroutineUtil.WaitForRealSeconds(0.5f));
        SceneManager.LoadScene("MainMenu");

    }
    IEnumerator playCheckpointSavedCourtine()
    {
        playCheckpointSaved.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        playCheckpointSaved.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        playCheckpointSaved.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        playCheckpointSaved.DOFade(0, 0.5f);
        //yield return new WaitForSeconds(0.5f);
    }
    IEnumerator PlayDeathTransition()
    {
        transitionEffect.DOFade(1, 0.4f);
        yield return new WaitForSeconds(0.4f);
        transitionEffect.DOFade(0, 0.4f);
    }


}
