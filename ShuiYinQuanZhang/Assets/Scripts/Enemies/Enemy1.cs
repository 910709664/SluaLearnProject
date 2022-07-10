using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Enemy1 : EnemyBase
{
    Action<EventArg> OnDeadEvent;
    public Enemy1(string name,int hp) : base(hp)
    {
        this.hp = hp;
        this.name = name;
    }
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        EventManager.StartListening("OnHPChange", OnHPChange);
    }
    private void OnDisable()
    {
        EventManager.StopListening("OnHPChange", OnHPChange);
    }
    protected override void Counter(Player role)
    {   
        //TODO 受击动画
        role.HP -= Damage;
    }

    protected override void OnDead()
    {   
        Debug.Log("Dead");
        Destroy(this.gameObject);
    }

    protected override void OnHPChange(EventArg eventArg)
    {
        if (eventArg.Name == this.myName)
        {
            HP -= eventArg.Damage;
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            transform.DOMoveX(transform.position.x + 0.3f, 0.3f);
            StartCoroutine(Change());
            Debug.Log("Enemy: " + HP);
        }

    }
    IEnumerator Change()
    {
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        transform.DOMoveX(transform.position.x - 0.3f, 0.3f);
    }
}
