using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class EnemyBase :MonoBehaviour, IHealth
{   
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected string myName;
    [SerializeField]
    protected int damage;
    public int Damage { get { return damage; } set { damage = value; } }
    public int HP
    {
        get { return hp; }
        set {
            hp = value;
            if (hp <= 0)
            {
                this.OnDead();
            }
            }//TODO
        
    }
    public EnemyBase(int n)
    {
        hp = n;//初始化血量;
    }
    protected abstract void OnHPChange(EventArg eventArg);
    protected abstract void OnDead();
    protected abstract void Counter(Player role);
}
