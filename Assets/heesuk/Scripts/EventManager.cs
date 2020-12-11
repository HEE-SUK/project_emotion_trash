using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE {

    // UI관련
    START_CHOICE,
    END_CHOICE,
    
    PLAYER_DEAD,



}

public class EventManager: Singleton<EventManager> 
{
    public delegate void OnEvent(EVENT_TYPE eventType, Component sender, object param = null);

    // 리스너 오브젝트 딕셔너리 or 배열
    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
    
    // 리스너 배열에 리스너 추가
    public static void on(EVENT_TYPE eventType, OnEvent listener)
    {
        List<OnEvent> ListenList = null;

        if(Instance.Listeners.TryGetValue(eventType, out ListenList))
        {
            ListenList.Add(listener);
            return;
        }

        ListenList = new List<OnEvent>();
        ListenList.Add(listener);
        Instance.Listeners.Add(eventType, ListenList);

    }

    // 이벤트를 리스너에게 전달
    public static void emit(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        List<OnEvent> ListenList = null;
        if (!Instance.Listeners.TryGetValue(EventType, out ListenList))
        {
            return;
        }

        for(int i = 0; i < ListenList.Count; i++)
        {
            if(!ListenList[i].Equals(null))
            {
                ListenList[i](EventType, Sender, Param);
            }
        }
    }

    public static void off(EVENT_TYPE eventType, OnEvent target = null)
    {
        if(target == null)
        {        
            // 없으면 이벤트 제거
            Instance.Listeners.Remove(eventType);    
        }
        else
        {
            // 타겟 있으면 타겟만 제거
            List<OnEvent> ListenList = null;
            if (!Instance.Listeners.TryGetValue(eventType, out ListenList))
            {
                return;
            }
            
            for(int i = 0; i < ListenList.Count; i++)
            {
                if(!ListenList[i].Equals(null))
                {
                    Instance.Listeners[eventType].Remove(target);
                }
            }
        }
    }

    public static void clear()
    {
        Instance.Listeners.Clear();
    }

    public static void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

        foreach(KeyValuePair<EVENT_TYPE, List<OnEvent>> Item in Instance.Listeners)
        {
            for(int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if(Item.Value[i].Equals(null))
                {
                    Item.Value.RemoveAt(i);
                }
            }

            if(Item.Value.Count > 0)
            {
                TmpListeners.Add(Item.Key, Item.Value);
            }
        }
        Instance.Listeners = TmpListeners;
    }

    private static void OnLevelFinishedLoading()
    {
        RemoveRedundancies();
    }
}