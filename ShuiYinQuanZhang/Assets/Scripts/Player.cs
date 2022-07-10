using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour,IHealth
{

    public Status status = Status.None;
    public Type type = Type.Melee;
    [SerializeField]
    private bool isAction = false;
    [SerializeField]
    private int _stepRange;
    [SerializeField]
    private int _attackRange;
    [SerializeField]
    private int hp;
    private Vector3Int[] range;
    [SerializeField]
    private int _attackDamage;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                this.OnDead();
            }
        }
    }
    public bool IsAction
    {
        get { return isAction; }
        set
        {
            isAction = value;
            if (isAction)
            {
                this.Action();
            }
            else
            {
                this.ReAction();
            }
        }
    }
    public int StepRange{ get { return _stepRange; } set { _stepRange = value; } }
    public int AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public Vector3Int[] Range { get { return range; } }
    public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }

    public void OnDead()
    {
        Destroy(this.gameObject);
    }
    public void Action()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(0.65f, 0.65f, 0.65f);
    }
    private void ReAction()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}
public enum Status
{   
    None,
    Select
}
public enum Type
{
    Melee,//近战
    Remote//远程
}
