using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Inventory",menuName ="RoleInfo/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Role> RoleList = new List<Role>();
    public List<Format> FormatList = new List<Format>();
}
