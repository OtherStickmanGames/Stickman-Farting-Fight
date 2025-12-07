using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    [SerializeField] StickmanController ragdoll;
    [SerializeField] Animator animator;
    [SerializeField] string anim;
    [SerializeField] public bool ebala;
    [SerializeField] float kickCooldown = 0.5f;
    [SerializeField] float forceUp = 50;
    [SerializeField] int healthPoint = 10;

    public StickmanController Ragdoll => ragdoll;
    public bool IsJump { get; set; }

    

    DamageDetector[] detectors;
    GameManager gameManager;

    int maxHealthPoint;
    int countPunches = 8;
    int oldID = int.MinValue;
    
    float timeKickDown;

    void Start()
    {
        if(!string.IsNullOrEmpty(anim))
            animator.SetTrigger(anim);

        maxHealthPoint = healthPoint;

        gameManager = FindObjectOfType<GameManager>();
        gameManager.onJump += Jump;
        gameManager.onPunch += Punch;
        gameManager.onLevelComplete += Level_Completed;

        ragdoll.IsPlayer = ebala;

        if (ebala)
        {
            detectors = GetComponentsInChildren<DamageDetector>();
            gameManager.onMove += Move;
        }

        
    }

    private void Level_Completed(int level)
    {
        healthPoint = maxHealthPoint;
        gameManager.PlayerHPChanged(1);
    }

    void Punch()
    {
        if (ebala)
        {
            if(timeKickDown > kickCooldown && !IsJump)
            {
                animator.SetTrigger("Kick");

                var id = Random.Range(0, countPunches);

                while (oldID == id)
                {
                    id = Random.Range(0, countPunches);
                }

                animator.SetInteger("PunchID", id);

                oldID = id;

                timeKickDown = 0;

                StartCoroutine(BackEbashilovo());

            }
        }
    }

    void Jump()
    {
        if (!IsJump && ebala)
            animator.SetTrigger("Jump");
    }

    private void Move(Vector2 dir)
    {
        if(dir.x > 0)
        {
            ragdoll.MoveRight();
        }
        else if(dir.x < 0)
        {
            ragdoll.MoveLeft();
        }
    }

    public void Damage(int value)
    {
        healthPoint -= value;

        healthPoint = Mathf.Clamp(healthPoint, 0, int.MaxValue);

        var barValue = (float)healthPoint / (float)maxHealthPoint;
        gameManager.PlayerHPChanged(barValue);

        if (healthPoint == 0)
        {
            gameManager.Cirdick();
            ragdoll.SetMusclesPower(0);
        }

    }

    void Update()
    {
        timeKickDown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Punch();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

    }

    // From Animation
    public void ForceUp()
    {
        var body = GetComponentInChildren<StickmanController>().muscles[2];
        body.bone.AddForce(Vector2.up * body.bone.mass * forceUp, ForceMode2D.Impulse);
    }

    IEnumerator BackEbashilovo()
    {
        SetEbashilovo(true);

        yield return new WaitForSeconds(0.5f);

        SetEbashilovo(false);
    }

    public void SetEbashilovo(bool value)
    {
        foreach (var detector in detectors)
        {
            detector.EbashilovoAcces = value;
        }
    }
}
