using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class Editor : EditorWindow
{
    string description="";
    string RoleName="";
    Sprite RoleImage=null;
    List<string> mTalkList = new List<string>();
    [MenuItem("Editor/XML")]
    private static void XMlEditor()
    {
        EditorWindow.CreateWindow<Editor>("DialogueSystem");
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:", GUILayout.MaxWidth(80));
        RoleName = EditorGUILayout.TextField(RoleName, GUILayout.MaxHeight(15)).ToString();
        GUILayout.EndHorizontal();

        RoleImage = EditorGUILayout.ObjectField("Image",RoleImage,typeof(Sprite),true) as Sprite;
        

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Description", GUILayout.MaxWidth(80));
        description = EditorGUILayout.TextArea(description, GUILayout.MaxHeight(60)).ToString();
        GUILayout.EndHorizontal();
        GUILayout.Label("换行输入视为同一个人多段对话");
        GUILayout.Space(30);
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        if(GUILayout.Button("创建XML", GUILayout.Width(180)))
        {
            CreateXML();

        }
        GUILayout.Space(10);
        if (GUILayout.Button("加入对话", GUILayout.Width(180)))
        {
            AddXMLData();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        if(GUILayout.Button("读取XML", GUILayout.Width(180)))
        {
            ReadXML();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("创建对话", GUILayout.Width(180)))
        {
            CreateDialogue();
        }
        GUILayout.EndHorizontal();
        
    }
    void CreateXML()
    {
        string path = Application.dataPath + "/Test.xml";
        if (!File.Exists(path))
        {
            if (description == null || RoleName == null) ShowNotification(new GUIContent("名字和内容不能为空"));
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("Root");
            XmlElement role = xml.CreateElement("Role");
            role.SetAttribute("name", RoleName);
            XmlElement roleContents = xml.CreateElement("contents");
            //XmlElement content = xml.CreateElement("content");
            string[] descriptions = description.Split('\n');
            for (int i = 0; i < descriptions.Length; i++)
            {
                XmlElement content = xml.CreateElement("content");
                content.InnerText = descriptions[i];
                roleContents.AppendChild(content);
            }
            //foreach(var s in descriptions)
            //{
            //    Debug.Log(s);
            //}
            //自底向上的保存
            //roleContents.AppendChild(content);
            role.AppendChild(roleContents);
            root.AppendChild(role);
            xml.AppendChild(root);
            xml.Save(path);
            ShowNotification(new GUIContent("创建成功,请刷新"));
        }
    }
    void AddXMLData()
    {
        string path = Application.dataPath + "/Test.xml";
        if (File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            //根节点
            XmlNode root = xml.SelectSingleNode("Root");
            XmlElement role = xml.CreateElement("Role");
            role.SetAttribute("name", RoleName);
            XmlElement roleContents = xml.CreateElement("contents");
            string[] descriptions = description.Split('\n');
            for (int i = 0; i < descriptions.Length; i++)
            {
                XmlElement content = xml.CreateElement("content");
                content.InnerText = descriptions[i];
                roleContents.AppendChild(content);
            }
            role.AppendChild(roleContents);
            root.AppendChild(role);
            xml.AppendChild(root);
            xml.Save(path);
            ShowNotification(new GUIContent("添加成功"));
        }
        else
            CreateXML();
    }
    void ReadXML()
    {
        string path = Application.dataPath + "/Test.xml";
        if (File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNode root = xml.SelectSingleNode("Root");
            XmlNodeList xmlNodeList = root.ChildNodes;
            //遍历Root节点下的Role节点
            foreach(XmlElement xl1 in xmlNodeList)
            {
                if (xl1.GetAttribute("name") == "416")
                {
                    XmlNodeList contents = xml.SelectNodes("//Role/contents/content");
                    //继续遍历每个人contents的对话内容
                    foreach(XmlElement xl2 in contents)
                    {
                        mTalkList.Add(xl2.InnerText);
                    }
                }
                if (xl1.GetAttribute("name") == "45")
                {
                    XmlNodeList contents = xml.SelectNodes("//Role/contents/content");
                    foreach (XmlElement xl2 in contents)
                    {
                        mTalkList.Add(xl2.InnerText);
                    }
                }
            }
            ShowNotification(new GUIContent("读取成功"));
        }
    }
    void CreateDialogue()
    {
        string path = "Prefabs/Panel";
        SetPanel(path);
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<DiagolueSystem>();
        ShowNotification(new GUIContent("创建成功"));
    }

    void SetPanel(string path)
    {
        GameObject go = Resources.Load<GameObject>(path);
        go = Instantiate(go);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.name = "Panel";
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        //GameObject Image = go.transform.GetChild(0).gameObject;
        //Image.transform.GetChild(0).GetComponent<Text>().text = mTalkList[0];
        //Image.transform.GetChild(1).GetComponent<Text>().text = RoleName;
    }
}
