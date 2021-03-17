using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VolumeProfile : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Vignette vignette1;
    UnityEngine.Rendering.Universal.ChromaticAberration chAberation;
    //public float buf;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) 
            throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        // You can leave this variable out of your function, so you can reuse it throughout your class.
        UnityEngine.Rendering.Universal.Vignette vignette;
        UnityEngine.Rendering.Universal.ChromaticAberration aberration;

        if (!volumeProfile.TryGet(out vignette)) 
            throw new System.NullReferenceException(nameof(vignette));
        if (!volumeProfile.TryGet(out aberration))
            throw new System.NullReferenceException(nameof(vignette));
        vignette1 = vignette;
        chAberation = aberration;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.isDashing)
        {
            //Debug.Log(GameMaster.isDashing);
            //chAberation.intensity.Override(1f);
            //vignette1.intensity.Override(0.5f);
            StartCoroutine(dash());
            GameMaster.isDashing = false;
        }
    }
    IEnumerator dash ()
    {
        DOVirtual.Float(0, 0.8f, .3f,chAberation1);
        DOVirtual.Float(0, 0.2f, .3f, vignete1);
        yield return new WaitForSeconds(0.2f);
        DOVirtual.Float(0.8f, 0, .3f, chAberation1);
        DOVirtual.Float(0.2f, 0, .3f, vignete1);
        yield return new WaitForSeconds(0.2f);
    }
    void chAberation1(float x)
    {
        chAberation.intensity.Override(x) ;
    }
    void vignete1(float x)
    {
        vignette1.intensity.Override(x);
    }
}
