using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using UnityStandardAssets.Characters.FirstPerson;

public class Flickering : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CVC;
    [SerializeField] Animator animL;
    [SerializeField] VisualEffect VE;
    [SerializeField] FirstPersonController FPC;
    [SerializeField] AudioSource AS;
    [SerializeField] Light l;
    [SerializeField] AudioMene AM;
    [SerializeField] UnityEvent StartF, StopF;
    float OldWalk, Xsen, Ysen;
    public void starflick(float time)
    {
        OldWalk = FPC.m_WalkSpeed;
        Xsen = FPC.m_MouseLook.XSensitivity;
        Ysen = FPC.m_MouseLook.YSensitivity;
        StartCoroutine(flicker(time));
    }

    public void chamgeAnim(RuntimeAnimatorController RAC)
    {
        animL.runtimeAnimatorController = RAC;
    }
    IEnumerator flicker(float t)
    {
        FPC.m_WalkSpeed = 0;
        FPC.m_MouseLook.XSensitivity = 0;
        FPC.m_MouseLook.YSensitivity = 0;
        while (AM.switching)
        {
            yield return null;
        }
        AM.disableSwitching = true;
        StartF.Invoke();
        bool tmp = AM.amb;
        AM.SwitchToA(0.75f);
        if(!tmp)
            yield return new WaitForSecondsRealtime(0.5f);
        AS.PlayOneShot(AS.clip);
        CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        VE.enabled = true;
        animL.enabled = true;

        yield return new WaitForSecondsRealtime(t);
        VE.enabled = false;
        animL.enabled = false;
        l.enabled = true;
        CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        StopF.Invoke();
        FPC.m_WalkSpeed = OldWalk;
        FPC.m_MouseLook.XSensitivity = Xsen;
        FPC.m_MouseLook.YSensitivity = Ysen;
        AM.disableSwitching = false;
    }
}
