using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MouseManager : MonoBehaviour
{
    [SerializeField] private bool mIsSelect = false;
    private  Stack<Transform> mPathStack;
    private Transform mPlayerPos;
    private bool mIsReach=false;
    public GameObject Clicked;

    private void Awake()
    {
        mPathStack = new Stack<Transform>();
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = GetMouseHitInfo();
            //没有选中
            if (!mIsSelect)
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    CheckObstacle(hit);//检测是否有人物
                    //如果是机场
                    //TODO:部署or移动
                    if (hit.collider.GetComponent<Vertex>().Data >= 2&&!mIsSelect)
                    {
                        UIManager.Instance.ShowPanel("SelectPanel");
                        UIManager.Instance.LauchPos = hit.collider.transform.GetChild(0);
                    }
                }
            }
            //选中某个单位后
            else
            {
                
                //如果点到路径点以外
                if (hit.collider == null)
                {
                    mPathStack.Clear();//清空当前的位置信息
                    mIsSelect = false;
                    Clicked.SetActive(false);
                    Debug.Log("Clear");
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if(PathManager.Instance.Martixs.IsEdge(mPathStack.Peek().GetComponent<Vertex>(), hit.collider.GetComponent<Vertex>())&&hit.collider.GetComponent<Vertex>().Data==1&&!mIsReach)//选中路径点
                    {
                        mIsReach = true;
                        mPlayerPos.DOMove(hit.collider.transform.position, 1.5f);
                        Clicked.transform.position = hit.collider.transform.position;
                        //mPathStack.Pop();
                        mPathStack.Push(hit.collider.transform);
                        StartCoroutine(Wait());
                        
                    }
                    
                }
            }

        }
    }
    /// <summary>
    /// 检测路径点上的物体
    /// </summary>
    /// <param name="hitInfo"></param>
    private void CheckObstacle(RaycastHit2D hitInfo)
    {
        Collider2D collider = Physics2D.OverlapCircle(hitInfo.transform.position, hitInfo.collider.GetComponent<SpriteRenderer>().bounds.extents.x, 1 << LayerMask.NameToLayer("Player"));
        if (collider != null)
        {
            Debug.Log(collider.name);
            mIsSelect = true;
            Clicked.SetActive(true);
            Clicked.transform.position = hitInfo.collider.transform.position;
            mPathStack.Push(hitInfo.collider.transform);//存储选中时的单位位置信息
            mPlayerPos = collider.transform;//存储选中单位
            
        }
        else
        {
            return;
        }

    }
    private RaycastHit2D GetMouseHitInfo()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, 1 << LayerMask.NameToLayer("PathWay"));
        return hit;
    }
    IEnumerator Wait()
    {   

        yield return new WaitForSeconds(1.5f);
        mIsReach = false;
    }
}
