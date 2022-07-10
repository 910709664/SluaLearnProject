using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Formation",menuName ="Formation/Formation")]
public class Format : ScriptableObject
{
    public int TeamNum;
    public Role[] FormatNum=new Role[9];
}
