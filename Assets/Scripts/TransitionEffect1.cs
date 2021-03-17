using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TransitionEffect1 : MonoBehaviour
{
    //public static TransitionEffect1 instance;

    public Image transitionEffect;

    //singleton ideie buna dar am deja canvasuri pot folosi la death animation tho dar nu stiu

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(instance);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        //actifate transition tween
        transitionEffect.DOFade(0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator transitionFromMenuToGame()
    {
        transitionEffect.DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
    }
}
