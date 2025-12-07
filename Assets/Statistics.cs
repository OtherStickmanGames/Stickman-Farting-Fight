using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static System.Action onMaxStageChanged;

    public static int MaxStage
    {
        get
        {
            if (PlayerPrefs.HasKey("MaxStage"))
            {
                return PlayerPrefs.GetInt("MaxStage");
            }
            else
            {
                return 1;
            }
           
        }

        set
        {
            PlayerPrefs.SetInt("MaxStage", value);
            PlayerPrefs.Save();

            onMaxStageChanged?.Invoke();
        }
    }
}
