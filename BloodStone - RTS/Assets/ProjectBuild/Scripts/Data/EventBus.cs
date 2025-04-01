using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    public IReadOnlyDictionary<string, List<Action>> Actions => actions;
    public Dictionary<string, List<Action>> actions { get; private set; }


    public void Sub(string name, Action action)
    {
        if (actions == null)
        {
            Init();
        }

        if(!actions.ContainsKey(name))
        {
            actions[name] = new List<Action>();
        }

        actions[name].Add(action);
    }


    public void Invoke(string name)
    {
        if (actions == null) return;

        if(actions.ContainsKey(name))
        {
            foreach(Action action in actions[name])
            {
                action();
            }
        }

    }


    public void UnSub(string name, Action action)
    {
        if (actions == null) return;


        try
        {
            if (actions.ContainsKey(name))
            {
                actions[name].Remove(action);
            }
        }
        catch (Exception)
        {

            Debug.LogWarning("Error");
            
        }

    }



    private void Init()
    {
        actions = new Dictionary<string, List<Action>>();
    }

}
