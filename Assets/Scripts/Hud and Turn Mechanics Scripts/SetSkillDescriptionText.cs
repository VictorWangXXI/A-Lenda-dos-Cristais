using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SetSkillDescriptionText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public TextMeshProUGUI skillText;
    public string skillDescription;

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillText.text = skillDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillText.text = "";
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        skillText.text = "";
    }
}
