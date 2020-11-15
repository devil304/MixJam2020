using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AudioMene : MonoBehaviour
{
    [SerializeField] AudioSource HeadAct, HeadNext, AmbAct, AmbNext;
    [HideInInspector] public bool amb = true, next = false, switching = false;
    [SerializeField] UnityEvent UE, HOn, HOff;
    float HeadLastTime=0, AmbLastTime=0, HLTStamp, ALTStamp;
    [HideInInspector] public bool disableSwitching = true, nextA = false;
    [SerializeField] TextMeshProUGUI TMP;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !disableSwitching)
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

    public void SwitchToH(float time)
    {
        if (!next && !switching && amb)
        {
            switching = true;
            StartCoroutine(SHA(time));
        }

    }

    public void SwitchToA(float time)
    {
        if (!next && !switching && !amb)
        {
            switching = true;
            StartCoroutine(SHA(time));
        }

    }

    public void setNextAMB(AudioClip AC)
    {
        AmbNext.clip = AC;
        AmbNext.volume = 0;
    }

    public void setNextHead(AudioClip AC)
    {
        HeadNext.clip = AC;
        HeadNext.volume = 0;
    }

    public void Snext(float Time)
    {
        StartCoroutine(NextIE(Time));
    }

    IEnumerator NextIE(float t)
    {
        disableSwitching = true;
        if (amb)
        {
            AmbNext.volume = 0f;
            AmbNext.enabled = true;
            while (AmbNext.volume < 1f || AmbAct.volume > 0f)
            {
                AmbNext.volume += (1f / t) * Time.deltaTime;
                AmbNext.volume = AmbNext.volume > 1f ? 1f : AmbNext.volume;
                AmbAct.volume -= (1f / t) * Time.deltaTime;
                AmbAct.volume = AmbAct.volume > 1f ? 1f : AmbAct.volume;
                yield return null;
            }
            AmbAct.clip = AmbNext.clip;
            AmbAct.time = AmbNext.time;
            AmbAct.volume = AmbNext.volume;
            AmbAct.Play();
            //AmbAct.enabled = true;
            AmbNext.enabled = false;
        }
        else
        {
            HeadNext.volume = 0f;
            HeadNext.enabled = true;
            while (HeadNext.volume < 1f || HeadAct.volume > 0f)
            {
                HeadNext.volume += (1f / t) * Time.deltaTime;
                HeadNext.volume = HeadNext.volume > 1f ? 1f : HeadNext.volume;
                HeadAct.volume -= (1f / t) * Time.deltaTime;
                HeadAct.volume = HeadAct.volume > 1f ? 1f : HeadAct.volume;
                yield return null;
            }
            HeadAct.clip = HeadNext.clip;
            HeadAct.time = HeadNext.time;
            HeadAct.volume = HeadNext.volume;
            HeadAct.Play();
            //HeadAct.enabled = true;
            HeadNext.enabled = false;
        }
        disableSwitching = false;
    }

    IEnumerator SHA(float t)
    {
        if (amb)
        {
            TMP.text = "Headphones: ON<br>[Press SPACE to switch]";
            HOn.Invoke();
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
            TMP.text = "Headphones: OFF<br>[Press SPACE to switch]";
            HOff.Invoke();
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
