using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class MainMenu_Manager : MonoBehaviour
{
    #region Cinemachine Cam
    [Header("Cinemachine Cam")]
    [SerializeField]
    private CinemachineVirtualCamera mainMenuCam;
    [SerializeField]
    private CinemachineVirtualCamera arcadeCam;
    [SerializeField]
    private CinemachineVirtualCamera multiplayerCam;
    [SerializeField]
    private CinemachineVirtualCamera P1VsP2Cam;
    [SerializeField]
    private CinemachineVirtualCamera P1VsCompCam;
    #endregion


    #region Buttons
    [Header("Buttons")]
    [SerializeField]
    private Button arcadeBtn;
    [SerializeField]
    private Button multiplayerBtn;
    [SerializeField]
    private Button p1_P2_Btn;
    [SerializeField]
    private Button p1_Comp_Btn;
    [SerializeField]
    private Button backBtn;
    [SerializeField]
    private Button exitBtn;
    #endregion


    void Start()
    {
        arcadeBtn.onClick.AddListener(ArcadeSelected);
        multiplayerBtn.onClick.AddListener(MultplayerSelected);
        exitBtn.onClick.AddListener(Exit);
        p1_Comp_Btn.onClick.AddListener(P1vsCompSelected);
        p1_P2_Btn.onClick.AddListener(P1vsP2Selected);
        backBtn.onClick.AddListener(BackBtn);

        Button[] offBtn = { p1_Comp_Btn, p1_P2_Btn };
        Button[] onBtn = { arcadeBtn, multiplayerBtn, exitBtn };
        TriggerBtnSwitch(onBtn, offBtn);

        arcadeCam.gameObject.SetActive(false);
        multiplayerCam.gameObject.SetActive(false);
        P1VsCompCam.gameObject.SetActive(false);
        P1VsP2Cam.gameObject.SetActive(false);
        mainMenuCam.gameObject.SetActive(true);
    }
    public void ArcadeSelected()
    {
        TriggerCam(arcadeCam);

        Button[] onBtn = { p1_Comp_Btn, p1_P2_Btn };
        Button[] offBtn = { arcadeBtn, multiplayerBtn, exitBtn };
        TriggerBtnSwitch(onBtn,offBtn);
    }
    public void MultplayerSelected()
    {
        TriggerCam(multiplayerCam);
        GameManager_Old.instance.GameMode = GameType.Multiplayer;
        SceneManager.LoadScene("Multiplayer");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void P1vsP2Selected()
    {
        GameManager_Old.instance.GameMode = GameType.P1vsP2;
        SceneManager.LoadScene("P1VsP2_Selection");
    }
    public void P1vsCompSelected()
    {
        GameManager_Old.instance.GameMode = GameType.P1vsComp;
        SceneManager.LoadScene("P1VsCOMP_Selection");
    }
    public void BackBtn()
    {
        TriggerCam(mainMenuCam);
        Button[] offBtn = { p1_Comp_Btn, p1_P2_Btn };
        Button[] onBtn = { arcadeBtn, multiplayerBtn, exitBtn };
        TriggerBtnSwitch(onBtn, offBtn);
    }
    public void TriggerBtnSwitch(Button[] onBtn, Button[] offBtn)
    {
        foreach (Button item in onBtn)
        {
            item.gameObject.SetActive(true);   
        }
        foreach (Button item in offBtn)
        {
            item.gameObject.SetActive(false);
        }

    }
    public void TriggerCam(CinemachineVirtualCamera cam)
    {
        arcadeCam.gameObject.SetActive(false);
        multiplayerCam.gameObject.SetActive(false);
        P1VsCompCam.gameObject.SetActive(false);
        P1VsP2Cam.gameObject.SetActive(false);
        mainMenuCam.gameObject.SetActive(true);
        cam.gameObject.SetActive(true);
    }
}
