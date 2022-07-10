using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="Role",menuName ="RoleInfo/Role")]
public class Role : ScriptableObject
{
    public string Name;
    public string Type;
    public int Level;
    public int BulletCapacity;
    public int Food;
    public int Number;
    public Sprite RoleImage;
    public Sprite ModImage;
    public GameObject RoleGo;
    public bool IsTeam=false;
    [TextArea]
    public string Information;

}
