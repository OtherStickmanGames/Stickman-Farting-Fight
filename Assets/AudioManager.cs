using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] fartings;

    AudioSource audioSource;
    GameManager gameManager;

    public static bool Mute
    {
        get
        {
            if (PlayerPrefs.HasKey("mute"))
            {
                return bool.Parse(PlayerPrefs.GetString("mute"));
            }
            else
            {
                return false;
            }
        }
        set
        {
            PlayerPrefs.SetString("mute", value.ToString());
            PlayerPrefs.Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.aiDamaged += AI_Damaged;
    }

    private void AI_Damaged(AIStickman ai)
    {
        if (Mute)
            return;

        var clip = fartings[UnityEngine.Random.Range(0, fartings.Length)];

        AudioSource.PlayClipAtPoint(clip, ai.transform.GetChild(0).transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
