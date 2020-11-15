using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Flickering : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CVC;
    [SerializeField] Animator anim;
    [SerializeField] FirstPersonController FPC;
    float OldWalk, Xsen, Ysen;
    public void starflick(float time)
    {
        OldWalk = FPC.m_WalkSpeed;
        Xsen = FPC.m_MouseLook.XSensitivity;
        Ysen = FPC.m_MouseLook.YSensitivity;
        StartCoroutine(flicker(time));
    }
    IEnumerator flicker(float t)
    {
        CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        //anim.SetBool("Fall",true);
        FPC.m_WalkSpeed=0;
        FPC.m_MouseLook.XSensitivity=0;
        FPC.m_MouseLook.YSensitivity=0;
        yield return new WaitForSecondsRealtime(t);
        //anim.SetBool("Fall", false);
        CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        FPC.m_WalkSpeed = OldWalk;
        FPC.m_MouseLook.XSensitivity = Xsen;
        FPC.m_MouseLook.YSensitivity = Ysen;
    }
}
