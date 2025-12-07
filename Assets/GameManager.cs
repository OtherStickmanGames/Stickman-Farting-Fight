using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Stickman stickman;
    [SerializeField] bool autoSpawn;
    [SerializeField] int maxEnemySpawn = 18;


    public Action onJump;
    public Action onPunch;
    public Action onCirdyick;
    public Action<Vector2> onMove;
    public Action<float> onPlayerHPChanged;
    public Action<int> onLevelComplete;
    public Action<AIStickman> aiDamaged;

    List<AIStickman> destroyedAI;

    int countSpawned;
    int lastSpawnerID = 0;
    [SerializeField] // For Tests
    int stage = 1;
    int countDestroyed;

    int babcki = 0;

    Stickman player;

    void Start()
    {

        if (PlayerPrefs.HasKey("Stage"))
        {
            stage = PlayerPrefs.GetInt("Stage");
        }

        destroyedAI = new List<AIStickman>();

        if (autoSpawn)
        {
            onLevelComplete?.Invoke(stage);
            StartCoroutine(AutoSpawn());
        }

        player = FindObjectOfType<DamageDetector>().GetComponentInParent<Stickman>();
    }

    IEnumerator AutoSpawn()
    {
        
        yield return new WaitForSeconds(1.78f);

        while (stage > countSpawned)
        {
            while(countSpawned - countDestroyed > maxEnemySpawn)
            {
                CleanBattleground();

                yield return new WaitForSeconds(0.5f);
            }

            SpawnMeat();
            countSpawned++;

            if(destroyedAI.Count > maxEnemySpawn / 2)
            {
                CleanBattleground();
            }

            yield return new WaitForSeconds(1.1f);
        }
    }

    public void SpawnMeat()
    {
        var point = GetSpawnPos();

        var instance = Instantiate(stickman, point, Quaternion.identity);
        var ai = instance.GetComponent<AIStickman>();
        //ai.SetLayer(9);
        ai.onDestroyed += AI_Destroyed;
        ai.onDamage += AI_Damaged;
    }

    private void AI_Damaged(AIStickman ai)
    {
        aiDamaged?.Invoke(ai);
    }

    private void AI_Destroyed(AIStickman ai)
    {
        countDestroyed++;
        destroyedAI.Add(ai);

        if (countDestroyed >= stage)
        {
            stage++;

            if(Statistics.MaxStage < stage)
            {
                Statistics.MaxStage = stage;
            }

            countSpawned = 0;
            countDestroyed = 0;
            onLevelComplete?.Invoke(stage);
            StartCoroutine(AutoSpawn());

            babcki++;

            if(babcki > 3)
            {
                Advertising.ShowAds();
                babcki = 0;
            }
        }
    }

    public void Jump()
    {
        onJump?.Invoke();
    }

    public void Move(Vector2 dir)
    {
        onMove?.Invoke(dir);
    }

    public void Punch() => onPunch?.Invoke();

    public void PlayerHPChanged(float value)
    {
        onPlayerHPChanged?.Invoke(value);
    }

    public void Cirdick()
    {
        onCirdyick?.Invoke();
        PlayerPrefs.DeleteKey("Stage");
    }

    void CleanBattleground()
    {
        if(destroyedAI.Count > 5)
        {
            var telo = destroyedAI[0];
            if (telo)
            {
                telo.gameObject.SetActive(false);
            }
            else
            {
                print("пропал без вести, нах");
            }
            destroyedAI.RemoveAt(0);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Stage", stage);
        PlayerPrefs.Save();
    }

    Vector3 GetSpawnPos()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        Vector3 result = default;

        if (scene.buildIndex == 1)
        {
            result = spawnPoints[lastSpawnerID].position;
            lastSpawnerID++;
            if (lastSpawnerID >= spawnPoints.Length)
            {
                lastSpawnerID = 0;
            }
        }
        else
        {
            result = player.transform.GetChild(0).position + new Vector3(10 + countSpawned, 0, 0);
            result.x = Mathf.Clamp(result.x, float.MinValue, 235);
        }

        return result;
    }
}
