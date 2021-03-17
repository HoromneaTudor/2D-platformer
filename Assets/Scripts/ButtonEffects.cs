using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;


//i should have used tween not animation but it works for the moment

public class ButtonEffects : MonoBehaviour,ISelectHandler//,IDeselectHandler//, IPointerEnterHandler
{
    GameObject currentSelected;
    public TMPro.TMP_ColorGradient oldColor;
    public TMPro.TMP_ColorGradient newcolor1;
    public TMPro.TMP_ColorGradient newcolor2;
    public TMPro.TextMeshProUGUI actuall;
    public GameObject button;
    private bool isCourtimeReady = true;
    private bool once = true;
    private bool reset = false;
    private float oldPoz;
    //private float originalPos;
    //private float pos // trebuie sa fac in asa fac sa mearga transformul
    //private float timer;
    // Start is called before the first frame update
    void Start()
    {
        //currentSelected = EventSystem.current.currentSelectedGameObject;
        //while(currentSelected == button)
        //{
        //    StartCoroutine(colorshift());

        //}
        //if (currentSelected != button)
        //{
        //    actuall.colorGradientPreset = oldColor;
        //}
        isCourtimeReady = true;
        once = true;
        oldPoz = button.transform.position.x;
        
        //originalPos = button.transform.position.x;
    }

    void OnEnable()
    {
        isCourtimeReady = true;
        once = true;
        //button.transform.position = new Vector2(originalPos-50, button.transform.position.y);
    }
    

    // Update is called once per frame
    void Update()
    {
        //currentSelected = EventSystem.current.currentSelectedGameObject;
        ////    if (currentSelected.name == this.name)

        ////        //this.transform.position = new Vector2(this.transform.position.x + 10f, this.transform.position.y);// nu in loop
        ////        //this.GetComponentInChildren<TextMeshProUGUI>().colorGradientPreset = newcolor;
        ////        StartCoroutine(colorshift());
        ////currentSelected = EventSystem.current.currentSelectedGameObject;
        //if (currentSelected == button)
        //{
        //    //Debug.Log("de ce?");
        //    //StartCoroutine(colorshift());
        //    //yield return new WaitForSeconds(0.2f);
        //    //timer = Time.deltaTime+1f;

        //    actuall.colorGradientPreset = newcolor1;
        //}
        //else
        //{
        //    actuall.colorGradientPreset = oldColor;
        //}



        //this works but too much too late maybe i will use it but for the moment no


        currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == button && isCourtimeReady)
        {
            isCourtimeReady = false;
            StartCoroutine(colorshift());

        }
        if (currentSelected == button && once)
        {
            button.transform.DOComplete();
            //button.transform.DOShakePosition(0.1f, new Vector3(20,0,0), 20, 20, false, true);
            button.transform.DOShakePosition(0.1f, new Vector3(20, 0, 0), 40, 0, false, true);
            //button.transform.DOMoveX(oldPoz+50,0.15f, false);
            //button.transform.DOScale(1.25f, 0.1f);
            actuall.fontSize = Mathf.Lerp(100, 80, 0.15f);
            once = false;

        }
        else if (currentSelected != button)
        {
            actuall.colorGradientPreset = oldColor;
            //button.transform.DOMoveX(oldPoz, 0.15f, false);
            //button.transform.DOScale(0.8f, 0.1f);
            actuall.fontSize = Mathf.Lerp(80, 100, 0.15f);
            //reset = true;
        }
        //if (currentSelected == button && once)
        //{
        //    transform.position = new Vector2(transform.position.x + 20, transform.position.y);
        //    once = false;

        //}
        //if (reset)
        //{
        //    transform.position = new Vector2(transform.position.x - 20, transform.position.y);
        //    reset = false;
        //}




    }
    IEnumerator colorshift()
    {
        //while (currentSelected.name == this.name)
        //{
        //Debug.Log("am intrat");
        //  yield return new WaitForSeconds(1f);
        //this.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        //yield return new WaitForSeconds(4f);
        //this.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        //actual_color = newcolor;
        actuall.colorGradientPreset = newcolor1;
        yield return new WaitForSeconds(0.15f);
        actuall.colorGradientPreset = newcolor2;
        yield return new WaitForSeconds(0.15f);
        //Debug.Log("merge");
        isCourtimeReady = true;
        //yield return new WaitForSeconds(2f);

        //actual_color = oldColor;
        // yield return new WaitForSeconds(1f);
        //}
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    //StartCoroutine(colorshift());

    //    //transform.position = new Vector2(transform.position.x + 20, transform.position.y);
    //}

    //merge si asta binisor
    //public void OnDeselect(BaseEventData eventdata)
    //{
    //    //transform.position = new Vector2(transform.position.x - 30, transform.position.y);

    //}
    public void OnSelect(BaseEventData eventData)
    {
        once = true;
        //currentSelected = EventSystem.current.currentSelectedGameObject;
        //while(true)
        //{
        //    StartCoroutine(colorshift());
        //    if (currentSelected != button)
        //        break;
        //}
        //{

        //}

        //transform.position = new Vector2(transform.position.x + 30, transform.position.y);

        //merge dar nu cum vreau

        //transform.DOMoveX(200, 2f);
        //transform.DOScale(1.2f, 0.1f);
    }

}
