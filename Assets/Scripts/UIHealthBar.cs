using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Image mask;
    float originalSize;

    private void Awake()
    {
        instance = this;
        originalSize = mask.rectTransform.rect.width;
    }
    // Start is called before the first frame update
    void Start()
    {  
    }
    
    public void SetValue(float value)
    {
        Debug.Log("originalSize = " + originalSize);
        Debug.Log(value + "new size = " + originalSize*value);
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
