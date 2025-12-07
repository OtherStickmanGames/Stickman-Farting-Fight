using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnSound;
    [SerializeField] Button btnPrivacy;
    [SerializeField] GameObject iconMute;
    [SerializeField] TMP_Text txtMaxStage;

    private void Start()
    {
        btnPlay.onClick.AddListener(Play_Clicked);
        btnSound.onClick.AddListener(Sound_Clicked);
        btnPrivacy.onClick.AddListener(OpenPrivacyPolicy);

        iconMute.SetActive(AudioManager.Mute);

        txtMaxStage.text = $"Max Stage {Statistics.MaxStage}";
    }

    private void Sound_Clicked()
    {
        AudioManager.Mute = !AudioManager.Mute;
        iconMute.SetActive(AudioManager.Mute);
    }

    private void Play_Clicked()
    {
        SceneManager.LoadScene(UnityEngine.Random.Range(1, 3));
    }

    private void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://wogergames.github.io/StickmanFarting/");
    }

    
}
