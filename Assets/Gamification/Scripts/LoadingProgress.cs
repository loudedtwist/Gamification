using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour {
    private RectTransform progressRect;
    public Image progressBar;
    private RectTransform progressBarRect;
    public float duration = 10.0f;
    float hight = 8.0f;  
 
    private float width;

	void OnEnable() {
        progressRect = GetComponent<RectTransform>(); 
 
        progressBarRect = progressBar.GetComponent<RectTransform>();
        progressBarRect.sizeDelta = new Vector2(0.0f,hight); 
    } 

    public void StartLoadingAnimation(){
        width = progressRect.rect.width * 2 ;
        StartCoroutine(AnimatePr(duration));
    }

    public IEnumerator AnimatePr(float loadingTime)
    {  
        float loadingProgress = 0f;
        while(loadingProgress < width)
        {   
            float delta = Time.deltaTime * width / loadingTime ; 
            if (delta <= 0) yield return null;
            loadingProgress += delta;
            //Debug.LogError("T : " + loadingProgress + " DELTA: " + Time.deltaTime * width / loadingTime);
            progressBarRect.sizeDelta = new Vector2(loadingProgress, hight); 
            yield return null;
        }
    }
}
