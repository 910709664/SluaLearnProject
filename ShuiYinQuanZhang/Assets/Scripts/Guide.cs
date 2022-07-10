using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using UnityEngine.EventSystems;
public class Guide : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Icon;
    Image mImage;
    public Text Dialogue;
    List<string> mTalkList;
    //固定Image
    public Sprite HK416;
    int index;
    private void Start()
    {
        mTalkList = new List<string>();
        ShowGuide();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (++index < mTalkList.Count - 1)
                Dialogue.text = mTalkList[index];
            else
            {
                UIManager.Instance.ClosePanel("GuidePanel");
                UIManager.Instance.ShowPanel("RoundPanel");
                MouseManager.Instance.isTalk = false;
            }
                

        }
    }
    void LoadXml()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.dataPath + "/Scripts/XMLFile1.xml");
        XmlNodeList xmlNodeList = xml.SelectNodes("//dialogs/dialog");//  //省略根节点
        foreach(XmlElement xmlElement in xmlNodeList)
        {
            //if (xmlElement.GetAttribute("name") == "416")
            //{
            //    mImage.sprite = HK416;
            //}
            if (!mTalkList.Contains(xmlElement.InnerText))
            {
                mTalkList.Add(xmlElement.InnerText);
                
            }
        }
    }
    void ShowGuide()
    {
        LoadXml();
        UIManager.Instance.ShowPanel("GuidePanel");
        Dialogue.text = mTalkList[0];
    }
}
