using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlideIco : MonoBehaviour {

    public GameObject[] GOIco;
    private List<RectTransform> RTIco = new List<RectTransform>();
    private List<IcoAnimation> IcoScript = new List<IcoAnimation>();
    private int IcoNum;
    private Vector3 V3This;
    private Text GameName;
    private int NameNum;
    private GuiEvent SceneName;
//    private RectTransform RTThisIco;

    void Awake(){
        IcoNum = GOIco.Length;
        V3This = this.transform.TransformPoint(0, 0, 0);
        for (int i=0;i<IcoNum;i++){
            GameObject GOIcoClone = Instantiate(GOIco[i], V3This, new Quaternion(0, 0, 0, 0)) as GameObject;
            GOIcoClone.transform.SetParent(this.transform, false);
            RTIco.Add(GOIcoClone.GetComponent <RectTransform>());
            IcoScript.Add(GOIcoClone.GetComponent<IcoAnimation>());
        }
        GameName = GameObject.Find("/Canvas/NameText").GetComponent<Text>();
        SceneName = GameObject.Find("/GUIEvent").GetComponent <GuiEvent>();
        NameNum = 1;
    }

	void Start () {
        for (int i = 0; i < IcoNum; i++) {
            RTIco[i].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, (50 * (i - 1)), 170);
            RTIco[i].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 170);
            RTIco[i].localPosition = new Vector3(RTIco[i].localPosition.x, RTIco[i].localPosition.y, Mathf.Abs(20 * (i - 1)));
            if (i <= 1) {
                RTIco[i].SetSiblingIndex(IcoNum - 2 + i); 
            } else {
                RTIco[i].SetSiblingIndex(IcoNum - 1 - i);
            }
        }
        GameName.text = IcoScript[NameNum].GameName;
	}

	void Update () {
        if ((Input.GetKeyDown(KeyCode.W))&&(!NowSlideIco())) {
            UpSlideIco();
            SetName(-1);
//            Debug.LogWarning("Up!");
        } else if ((Input.GetKeyDown(KeyCode.S))&&(!NowSlideIco())) {
            DownSlideIco();
            SetName(1);
//            Debug.LogWarning("Down!");
        }
	}

    void SetName(int i){
        NameNum += i;
        if (NameNum < 0) {
            NameNum = IcoNum - 1;
        } else if (NameNum >= IcoNum) {
            NameNum = 0;
        }
        GameName.text = IcoScript[NameNum].GameName;
        SceneName.SetSceneName(IcoScript[NameNum].SceneName);
    }

    bool NowSlideIco(){
        foreach (IcoAnimation i in IcoScript) {
            if (i.GetNowSlide()) {
                return true;
            }
        }
        return false;
    }

    void UpSlideIco(){
        float IcoHeight = RTIco[0].anchoredPosition.y;
        float IcoDepth = RTIco[0].localPosition.z;
        int IcoIndex = RTIco[0].GetSiblingIndex();
        for (int i = 0; i < IcoNum - 1; i++) {
            IcoScript[i].SetPosition(RTIco[i + 1].anchoredPosition.y,
                RTIco[i + 1].localPosition.z, RTIco[i + 1].GetSiblingIndex());
        }
        IcoScript[IcoNum - 1].SetPosition(IcoHeight, IcoDepth, IcoIndex);
    }

    void DownSlideIco(){
        float IcoHeight = RTIco[IcoNum - 1].anchoredPosition.y;
        float IcoDepth = RTIco[IcoNum - 1].localPosition.z;
        int IcoIndex = RTIco[IcoNum - 1].GetSiblingIndex();
        for (int i = IcoNum - 1; i > 0; i--) {
            IcoScript[i].SetPosition(RTIco[i - 1].anchoredPosition.y,
                RTIco[i - 1].localPosition.z, RTIco[i - 1].GetSiblingIndex());
        }
        IcoScript[0].SetPosition(IcoHeight, IcoDepth, IcoIndex);
    }
}
