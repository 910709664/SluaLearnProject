using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
public class DiagolueSystem : MonoBehaviour
{
    List<string> mTalkList = new List<string>();
    List<Say> SayList = new List<Say>();
    [SerializeField]
    int index = 0;
    [SerializeField]
    int talkIndex = 0;
    [SerializeField]
    GameObject panel;
    Text mRoleName;
    Text mRoleText;
    private void Start()
    {
        LoadXML();
        Init();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
           
        }
        //mRoleName.text = SayList[index].RoleName;
        
    }
    void Init()
    {
        panel = GameObject.Find("Panel").transform.GetChild(0).gameObject;
        mRoleName = panel.transform.GetChild(1).GetComponent<Text>();
        mRoleText = panel.transform.GetChild(0).GetComponent<Text>();

        if (SayList.Count > 0)
        {
            mRoleName.text = SayList[0].RoleName;
            mRoleText.text = SayList[0].TalkList[0];
        }

    }
    private void ContinueDialogue()
    {
        if (index > SayList.Count - 1) return;

        if (talkIndex < SayList[index].TalkList.Count - 1)
        {
            mRoleText.text = SayList[index].TalkList[++talkIndex];

        }
        else
        {
            index++;
            if (index > SayList.Count - 1) return;
            talkIndex = 0;
            mRoleText.text = SayList[index].TalkList[talkIndex];
            mRoleName.text = SayList[index].RoleName;
        }
    }
    void LoadXML()
    {
        string path = Application.dataPath + "/Test.xml";
        if (File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.SelectSingleNode("Root");
            XmlNodeList xmlNodeList = root.ChildNodes;
            //遍历Root节点下的Role节点
            foreach (XmlElement xl1 in xmlNodeList)
            {
                //if (xl1.GetAttribute("name") == "416")
                //{
                //    XmlNodeList contents = xml.SelectNodes("//Role/contents/content");
                //    //继续遍历每个人contents的对话内容
                //    foreach (XmlElement xl2 in contents)
                //    {
                //        mTalkList.Add(xl2.InnerText);
                //    }
                //}
                //if (xl1.GetAttribute("name") == "45")
                //{
                //    XmlNodeList contents = xml.SelectNodes("//Role/contents/content");
                //    foreach (XmlElement xl2 in contents)
                //    {
                //        mTalkList.Add(xl2.InnerText);
                //    }
                //}
                Say tempSay = new Say();
                tempSay.RoleName = xl1.GetAttribute("name");
                
                XmlNode contents = xl1.FirstChild;
                foreach(XmlElement xl2 in contents)
                {
                    tempSay.TalkList.Add(xl2.InnerText);
                }
                SayList.Add(tempSay);
            }
            Debug.Log("读取成功");
            
        }
    }}
