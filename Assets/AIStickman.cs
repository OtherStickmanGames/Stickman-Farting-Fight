//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStickman : MonoBehaviour
{
    [SerializeField] StickmanController ragdoll;
    [SerializeField] Transform animationHolder;
    [SerializeField] Animator animator;
    [SerializeField] float distanceToPlayer = 1.5f;
    [SerializeField] float attackRange = 0.7f;
    

    AIDamager[] damagers;
    Coroutine attackState;
    Stickman player;

    public int healthPoint;
    int dir;
    float firstDelay;

    public System.Action<AIStickman> onDestroyed;
    public System.Action<AIStickman> onDamage;

    void Start()
    {
        player = FindObjectOfType<DamageDetector>().GetComponentInParent<Stickman>();
        
        damagers = GetComponentsInChildren<AIDamager>();

        healthPoint = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        firstDelay += Time.deltaTime;
        
        if(ragdoll.gameObject.layer == 6 || ragdoll.gameObject.layer == 9)
            CheckDirection();

        CheckDistance();
    }

    private void CheckDistance()
    {
        if (firstDelay < 1f)
            return;

        var distance = Vector2.Distance(ragdoll.transform.position, player.Ragdoll.transform.position);

        if (distance > distanceToPlayer)
        {
            animator.SetBool("Walk", true);

            if (attackState != null)
            {
                StopCoroutine(attackState);
                attackState = null;
            }
        }
        else 
        {
            animator.SetBool("Walk", false);

            if (attackState == null)
                attackState = StartCoroutine(AttackState());
        }
    }

    private IEnumerator AttackState()
    {
        float cooldown = float.MaxValue;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            cooldown += Time.deltaTime;

            if(cooldown > attackRange)
            {
                cooldown = 0;
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        int countPunches = 5;

        animator.SetTrigger("Kick");

        var id = Random.Range(0, countPunches);

        //while (oldID == id)
        //{
        //    id = Random.Range(0, countPunches);
        //}

        animator.SetInteger("PunchID", id);

        //oldID = id;

        //timeKickDown = 0;

        StartCoroutine(BackEbashilovo());

        IEnumerator BackEbashilovo()
        {
            foreach (var damager in damagers)
            {
                damager.EbashitMojno = true;
            }

            yield return new WaitForSeconds(0.5f);

            foreach (var damager in damagers)
            {
                damager.EbashitMojno = false;
            }
        }
    }

    void CheckDirection()
    {
        if (ragdoll.transform.position.x > player.Ragdoll.transform.position.x)
        {
            if (dir != -1)
            {
                dir = -1;
                Rotate();
            }
        }
        else if (dir != 1)
        {
            dir = 1;
            Rotate();
        }

        void Rotate()
        {
            //print("-+-+--+-+");
            ragdoll.transform.localScale = new Vector3(dir, 1, 1);
            animationHolder.localScale = new Vector3(dir, 1, 1);
        }
    }

    public void SetLayer(int layer = 6)
    {
        ragdoll.gameObject.layer = layer;
        foreach (var muscle in ragdoll.muscles)
        {
            muscle.bone.gameObject.layer = layer;
        }
    }

    public void Damage(int value = 1)
    {
        if (healthPoint <= 0)
            return;

        healthPoint -= value;
        onDamage?.Invoke(this);

        ragdoll.Xyi(0);
        SetLayer(7);

        if (healthPoint > 0)
            StartCoroutine(Slab());
        else
            onDestroyed?.Invoke(this);

        IEnumerator Slab()
        {
            yield return new WaitForSeconds(3.5f);

            if (healthPoint > 0)
            {
                ragdoll.Xyi(10);
                SetLayer();
            }

            yield return new WaitForSeconds(0.8f);

            if(healthPoint > 0)
                ragdoll.Xyi(30);

            yield return new WaitForSeconds(0.8f);

            if (healthPoint > 0)
                ragdoll.Xyi(50);

            yield return new WaitForSeconds(0.8f);

            if (healthPoint > 0)
                ragdoll.Xyi(70);
        }
    }
}
