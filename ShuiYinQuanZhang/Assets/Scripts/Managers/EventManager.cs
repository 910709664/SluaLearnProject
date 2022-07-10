using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventArg : EventArgs
{
    public int StepRange;
    public int AttackRange;
    public int Damage;
    public string Name;
    public Vector3Int vector;
    public Status status;
}
public class EventManager : MonoBehaviour
{
    //public static EventManager instance;
    private Dictionary<string, Action<EventArg>> eventDic;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    //print("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDic == null)
        {
            eventDic = new Dictionary<string, Action<EventArg>>();
        }
    }
    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);
        
    }
    
    private void Start()
    {
        
    }
    public static void StartListening(string eventName,Action<EventArg> listener)
    {
        
        if(instance.eventDic.ContainsKey(eventName))//如果存在则更新
        {
            
            instance.eventDic[eventName] = listener;
        }
        else
        {
            instance.eventDic.Add(eventName, listener);
        }
    }
    public static void StopListening(string eventName,Action<EventArg> listener)
    {
        if (instance == null) return;
        if(instance.eventDic.ContainsKey(eventName))//如果存在则更新
        {
            instance.eventDic[eventName] -= listener;
        }
    }
    public static void TriggerEvent(string eventName,EventArg eventArg)
    {
        Action<EventArg> thisEvent = null;
        if(instance.eventDic.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.Invoke(eventArg);
        }
    }
}
