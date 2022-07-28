using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkedToTracker : MonoBehaviour
{
    [System.Serializable]
    public class TalkGroup
    {
        public string groupName;
        public string[] names;
        [HideInInspector]
        public bool[] spokenTo;
        [HideInInspector]
        public bool groupSpokenTo = false;
        [HideInInspector]
        public bool groupEventAlreadyTriggered = false;
        public GameObject TriggerEvent;

        public void SetUp()
        {
            spokenTo = new bool[names.Length];
        }

        public void GroupSpokenTo()
        {
            if(groupSpokenTo == true)
            {
                return;
            }
            foreach (bool b in spokenTo)
            {
                if (b == false)
                {
                    groupSpokenTo = false;
                    return;
                }
            }
            groupSpokenTo = true;
        }
    }

    public TalkGroup[] talkGroup;

    //Handle non mono start bool[] bullshit
    private void Start()
    {
        foreach (TalkGroup group in talkGroup)
        {
            group.SetUp();
        }
    }

    public void spokeTo(string firstName)
    {
        foreach (TalkGroup group in talkGroup)
        {
            
            for(int i=0; i < group.names.Length; i++)
            {
                if (group.names[i] == firstName)
                {
                    group.spokenTo[i] = true;
                }
            }
        }
    }

    //should dialogue that just occured cause an event?
    public void CheckSpeachEvents()
    {
        foreach (TalkGroup group in talkGroup)
        {
            group.GroupSpokenTo();
            if(group.groupSpokenTo == true)
            {
                if(group.groupEventAlreadyTriggered == false)
                {
                    if (group.TriggerEvent)
                    {
                        group.TriggerEvent.SetActive(true);
                    }
                    group.groupEventAlreadyTriggered = true;
                }

            }
        }
    }
}
