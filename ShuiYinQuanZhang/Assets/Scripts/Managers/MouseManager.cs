using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
public class MouseManager : MonoBehaviour
{
    
    EventArg showRangeArg;
    EventArg closeRangeArg;
    EventArg DamageArg;
    [SerializeField]
    private Status mPlayerStatus=Status.None;
    private Transform mPlayer;
    private bool RoleIsAction;
    private Hexagon nowHex;
    private Hexagon targetHex;
    //private Vector3Int[] moveRange;
    private List<Vector3Int> moveRange;
    private List<Vector3Int> attackRange;
    public bool isClick=true;
    public bool isTalk;
    private static MouseManager mInstance;
    public static MouseManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    private void Awake()
    {
        mInstance = this;
    }
    private void Start()
    {
        showRangeArg = new EventArg();
        DamageArg = new EventArg();
        nowHex = new Hexagon(Vector3Int.zero);
        targetHex = new Hexagon(Vector3Int.zero);
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!isTalk)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3Int vector = tilemap.WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));
            //RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero, 10f, 1 << LayerMask.NameToLayer("Terrain"));
            if (mPlayerStatus != Status.Select&&isClick)
            {
                CheckObstacle(hit, ray);
            }
            else
            {
                CheckObstacle(hit, ray);
                //TODO 近战移动角色到敌人面前攻击
                if (hit.transform.tag == Tag.TERRAIN&&isClick)
                {
                    PlayerMoveTo(hit, ray);
                }

            }

        }
    }
    public void HitToPlayer(Collider2D col, Ray ray)
    {
        mPlayer = col.transform;
        RoleIsAction = mPlayer.GetComponent<Player>().IsAction;
        if (!RoleIsAction)
        {
            //TODO 敌人不显示在移动范围
            mPlayerStatus = Status.Select;//角色为选中状态
            //显示当前角色可移动范围and攻击范围
            showRangeArg.StepRange = mPlayer.GetComponent<Player>().StepRange;
            showRangeArg.AttackRange = mPlayer.GetComponent<Player>().AttackRange;
            Collider2D collider = Physics2D.OverlapCircle(col.transform.position, 0.1f, 1 << LayerMask.NameToLayer(Tag.TERRAIN));
            showRangeArg.vector = collider.transform.GetComponent<Tilemap>().WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));//获取所在tile坐标
            nowHex.vector = showRangeArg.vector;//存储当前位置
            moveRange = GameManager.Instance.GetMoveRange(showRangeArg.vector, showRangeArg.StepRange);//存储当前移动范围坐标信息
            attackRange = GameManager.Instance.GetAttackRange(moveRange, showRangeArg.vector, showRangeArg.StepRange + showRangeArg.AttackRange);//存储当前攻击范围坐标
            EventManager.TriggerEvent("ShowStepRange", showRangeArg);
            UIManager.Instance.ShowRoleMeg(mPlayer);
        }
        else
        {
            Debug.Log("This Role has action");
        }
    }
    public void PlayerMoveTo(RaycastHit2D hit, Ray ray)
    {
        isClick = false;//禁止鼠标点击
        targetHex.vector = hit.transform.GetComponent<Tilemap>().WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));//获取目标
        Debug.Log(targetHex.vector);
        if (!moveRange.Contains(targetHex.vector)) { Debug.Log("Can't reach it ");isClick = true; return; }
        if (nowHex.vector != targetHex.vector)
        {
            GameManager.Instance.PlayerMoveToTarget(mPlayer, GameManager.Instance.AstarSearch(nowHex, targetHex), hit);//寻路移动
        }
        Debug.Log("now: "+nowHex.vector);
        mPlayerStatus = Status.None;
        EventManager.TriggerEvent("CloseStepRange", showRangeArg);
        UIManager.Instance.CloseRoleMsg();
        
    }
    public void Attack(Collider2D col,Ray ray,int damage)
    {
        //TODO 战斗动画
        Collider2D collider = Physics2D.OverlapCircle(col.transform.position, 0.2f, 1 << LayerMask.NameToLayer(Tag.TERRAIN));
        DamageArg.Name = col.name;
        DamageArg.vector = collider.transform.GetComponent<Tilemap>().WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));//获取所在tile坐标
        if (attackRange.Contains(DamageArg.vector))//如果目标在攻击范围内
        {
            DamageArg.Damage = damage;
            EventManager.TriggerEvent("OnHPChange", DamageArg);//TODO 反击
            mPlayerStatus = Status.None;
            mPlayer.GetComponent<Player>().IsAction = true;
            EventManager.TriggerEvent("CloseStepRange", showRangeArg);
        }
        else
        {
            Debug.Log("Can't Attack Enemy");
        }
    }
    public void HitToEnemy(Collider2D col, Ray ray)
    {
        UIManager.Instance.ShowRoleMeg(col.transform);
    }
    public void CheckObstacle(RaycastHit2D hitInfo,Ray ray)
    {
        Collider2D[] colliders = new Collider2D[10];
        colliders = Physics2D.OverlapCircleAll(ray.origin, 0.2f,1<<LayerMask.NameToLayer("Player")|1<<LayerMask.NameToLayer("Enemy"));
        if (colliders != null)
        {
            foreach (var col in colliders)
            {
                Debug.Log(col.name);
                if (col.tag == Tag.PLAYER && isClick)
                {
                    HitToPlayer(col, ray);
                }
                if (col.tag == Tag.ENEMY && mPlayerStatus == Status.Select && isClick)
                {
                    Debug.Log("Attack");
                    Attack(col,ray,mPlayer.GetComponent<Player>().AttackDamage);
                }
                if (col.tag == Tag.ENEMY && mPlayerStatus == Status.None)
                {
                    HitToEnemy(col,ray);
                }
            
            }
        }
        //int Num = Physics2D.OverlapCircleNonAlloc(hitInfo.transform.position, 0.3f, colliders);//缓存
    }
}
