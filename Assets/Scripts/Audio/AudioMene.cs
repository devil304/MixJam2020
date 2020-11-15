using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioMene : MonoBehaviour
{
    [SerializeField] AudioSource HeadAct, HeadNext, AmbAct, AmbNext;
    bool amb = true, next = false, switching = false;
    [SerializeField] UnityEvent UE;
    float HeadLastTime=0, AmbLastTime=0, HLTStamp, ALTStamp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UE.Invoke();
        }
    }

    public void SwichHA(float time)
    {
        if(!next && !switching)
        {
            switching = true;
            StartCoroutine(SHA(time));
        }

    }

    IEnumerator SHA(float t)
    {
        if (amb)
        {
            HeadAct.volume = 0f;
            HeadAct.enabled = true;
            if (HeadLastTime != 0)
                HeadAct.time = (Time.time + HLTStamp + HeadLastTime) % HeadAct.clip.length;
            HeadAct.time = AmbAct.time;
            while (HeadAct.volume < 1f || AmbAct.volume > 0f)
            {
                HeadAct.volume += (1f / t)*Time.deltaTime;
                HeadAct.volume = HeadAct.volume > 1f ? 1f : HeadAct.volume;
                AmbAct.volume -= (1f / t) * Time.deltaTime;
                AmbAct.volume = AmbAct.volume > 1f ? 1f : AmbAct.volume;
                yield return null;
            }
            AmbLastTime = AmbAct.time;
            ALTStamp = Time.time;
            AmbAct.enabled = false;
            amb = false;
        }
        else
        {
            AmbAct.volume = 0f;
            AmbAct.enabled = true;
            if (AmbLastTime != 0)
                AmbAct.time = (Time.time + ALTStamp + AmbLastTime) % AmbAct.clip.length;
            AmbAct.time = HeadAct.time;
            while (AmbAct.volume < 1f || HeadAct.volume > 0f)
            {
                AmbAct.volume += (1f / t) * Time.deltaTime;
                AmbAct.volume = AmbAct.volume > 1f ? 1f : AmbAct.volume;
                HeadAct.volume -= (1f / t) * Time.deltaTime;
                HeadAct.volume = HeadAct.volume > 1f ? 1f : HeadAct.volume;
                yield return null;
            }
            HeadLastTime = HeadAct.time;
            HLTStamp = Time.time;
            HeadAct.enabled = false;
            amb = true;
        }
        switching = false;
    }
}
