using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public GameObject noSkillPointsText;

    public void HideNoSkillPointsText()
    {
        noSkillPointsText.SetActive(false);
    }
}
