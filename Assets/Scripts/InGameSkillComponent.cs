using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameSkillComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _skillButtonText;

    [SerializeField] private Image _skillCoolTimeBar;
    [SerializeField] private TextMeshProUGUI _skillCoolTimeText;
    [SerializeField] private GameObject _skillCannotUse;

    [SerializeField] private GameObject _skillCoolTimeTextObject;


    public void ManualStart(string skillName, string skillButtonText)
    {
        _skillNameText.text = skillName;
        _skillButtonText.text = skillButtonText;
    }

    public void ManualUpdate(float coolTimeRatio, string coolTimeText, bool isCannotUse)
    {
        _skillCoolTimeBar.fillAmount = coolTimeRatio;
        _skillCoolTimeText.text = coolTimeText;
        _skillCannotUse.SetActive(isCannotUse);
        _skillCoolTimeTextObject.SetActive(coolTimeText != "0.0");
    }




}
