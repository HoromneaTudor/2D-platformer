using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineShake : MonoBehaviour
{
    //public static CinemachineShake Instance { get; private set; } //metoda cu instanta
    private CinemachineVirtualCamera cinemamachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;
    public void Awake()
    {
        //Instance = this;
        cinemamachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemamachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    //metoda cu instanta
    //public void ShakeCamera(float intensity,float time)
    //{
    //        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    //        shakeTimer = time;
        
    //}
    private void Update()
    {
        //metoda cu instanta
        //if (shakeTimer > 0)
        //{
        //    shakeTimer -= Time.deltaTime;
        //    if (shakeTimer <= 0f)
        //    {
        //        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        //    }
        //}
        if (GameMaster.shake && GameMaster.screenShake)
        {
            StartCoroutine(shake());
            GameMaster.shake = false;
        }
        else
        {
            GameMaster.shake = false;
        }
    }

    IEnumerator shake()
    {
        DOVirtual.Float(3, 0, .3f, m_aplitude);
        yield return new WaitForSeconds(0.2f);
    }
    void m_aplitude(float x)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x;
    }
}

