using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] Button btnSpawn;
    [SerializeField] Button btnJump;
    [SerializeField] Button btnPunch;
    [SerializeField] Joystick joystick;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] TMP_Text labelCurrentStage;
    [SerializeField] Animator panelCurrentStage;
    [SerializeField] GameObject panelFail;
    [SerializeField] Button btnLoadMenu;
    [SerializeField] TMP_Text txtMaxStage;

    [Space]

    [SerializeField] GameManager gameManager;
    

    
    void Start()
    {
        panelCurrentStage.gameObject.SetActive(true);
        panelFail.SetActive(false);

        btnSpawn.onClick.AddListener(Spawn_Clicked);
        btnJump.onClick.AddListener(Jump_Clicked);
        btnPunch.onClick.AddListener(Punch_Clicked);
        btnLoadMenu.onClick.AddListener(LoadMenu_Clicked);

        gameManager.onPlayerHPChanged += PlayerHP_Changed;
        gameManager.onLevelComplete += Stage_Completed;
        gameManager.onCirdyick += Cirdicked;

        Statistics.onMaxStageChanged += MaxStage_Changed;

        MaxStage_Changed();
    }

    private void MaxStage_Changed()
    {
        txtMaxStage.text = $"Max Stage {Statistics.MaxStage}";
    }

    private void Cirdicked()
    {
        panelFail.SetActive(true);
    }

    private void LoadMenu_Clicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Stage_Completed(int level)
    {
        labelCurrentStage.text = $"Stage {level}";
        panelCurrentStage.SetTrigger("Start");
    }

    private void Update()
    {
        if(joystick.Direction != Vector2.zero)
        {
            gameManager.Move(joystick.Direction);
        }
    }

    private void Spawn_Clicked()
    {
        gameManager.SpawnMeat();
    }

    private void Punch_Clicked()
    {
        gameManager.Punch();
    }

    private void Jump_Clicked()
    {
        gameManager.Jump();
    }

    private void PlayerHP_Changed(float value)
    {
        progressBar.value = value;
    }
}
