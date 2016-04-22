using UnityEngine;
using System.Collections;

public class IcoAnimation : MonoBehaviour {

    public string GameName;
    public string SceneName;
    private float IcoHeight;
    private float IcoDepth;
    private int IcoIndex;
    private bool SlideStart = false;
    private RectTransform ThisPosition;
    private float StartTime;

    public void SetPosition(float SetHeight,float SetDepth,int SetIndex){
        IcoHeight = SetHeight;
        IcoDepth = SetDepth;
        IcoIndex = SetIndex;
        SlideStart = true;
    }
    public bool GetNowSlide(){
        return SlideStart;
    }

    void Awake(){
        ThisPosition = this.GetComponent <RectTransform>();
    }

	void Update () {
        if (SlideStart) {
            ThisPosition.anchoredPosition = new Vector2(ThisPosition.anchoredPosition.x,
                Mathf.Lerp(ThisPosition.anchoredPosition.y, IcoHeight, (Time.time - StartTime) * 2));
            ThisPosition.localPosition = new Vector3(ThisPosition.localPosition.x, ThisPosition.localPosition.y,
                Mathf.Lerp(ThisPosition.localPosition.z, IcoDepth, (Time.time - StartTime) * 2));
            if ((Time.time - StartTime) < 0.25f) {
                ThisPosition.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(0, 9, (Time.time - StartTime) * 2), 0, 0));
            } else {
                ThisPosition.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(9, 0, (Time.time - StartTime) * 2), 0, 0));
            }
            if ((ThisPosition.GetSiblingIndex() != IcoIndex) && ((Time.time - StartTime) > 0.05f)) {
                ThisPosition.SetSiblingIndex(IcoIndex);
            }
            if ((Mathf.Abs(ThisPosition.anchoredPosition.y - IcoHeight) < 2.5f)
                && (Mathf.Abs(ThisPosition.localPosition.z - IcoDepth)< 1.0f)) {
                ThisPosition.anchoredPosition = new Vector2(ThisPosition.anchoredPosition.x, IcoHeight);
                ThisPosition.localPosition = new Vector3(ThisPosition.localPosition.x, ThisPosition.localPosition.y, IcoDepth);
                ThisPosition.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                SlideStart = false;
            }
        } else {
            StartTime = Time.time;
        }
	}
}
