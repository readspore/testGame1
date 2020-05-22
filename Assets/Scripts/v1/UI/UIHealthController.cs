using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Radio;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public Image healtProgresshWrap;
    public Image healtProgress;
    [SerializeField]
    int maxHPValue;
    [SerializeField]
    int lastHPValue;
    // Start is called before the first frame update
    void Start()
    {
        Radio.Radio.OnUpdateHealthHandler += UpdateHealthHandler;
    }

    void UpdateHealthHandler(int newHealth, int maxHP)
    {
        var rectHPWrap = healtProgresshWrap.GetComponent<RectTransform>();
        var rectHPProgress = healtProgress.GetComponent<RectTransform>();
        lastHPValue = newHealth;
        var currentHPInPercent = (newHealth * maxHP) / 100;
        var newHealthBarOffset = rectHPWrap.sizeDelta.x - ( (rectHPWrap.sizeDelta.x * currentHPInPercent) / 100);
        rectHPProgress.offsetMax = new Vector2(-newHealthBarOffset, rectHPProgress.offsetMax.y);
    }

}
