using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;  
using UnityEngine.UI;

public class GameUIDrawer : MonoBehaviour
{
    [SerializeField] private Image _leftPlayerPortrait;
    [SerializeField] private TextMeshProUGUI _leftPlayerNameText;
    [SerializeField] private Image _leftPlayerHitPointBar;
    [SerializeField] private TextMeshProUGUI _leftPlayerHitPointText;
    [SerializeField] private Image _leftPlayerSpecialPointBar;
    [SerializeField] private TextMeshProUGUI _leftPlayerSpecialPointText;

    [SerializeField] private Image[] _leftPlayerSkillCooldownBar;
    [SerializeField] private TextMeshProUGUI[] _leftPlayerSkillCooldownText;
    [SerializeField] private GameObject[] _leftPlayerSkillAvailable;
    [SerializeField] private TextMeshProUGUI[] _leftPlayerSkillButtonText;

    [SerializeField] private Image _rightPlayerPortrait;
    [SerializeField] private TextMeshProUGUI _rightPlayerNameText;
    [SerializeField] private Image _rightPlayerHitPointBar;
    [SerializeField] private TextMeshProUGUI _rightPlayerHitPointText;
    [SerializeField] private Image _rightPlayerSpecialPointBar;
    [SerializeField] private TextMeshProUGUI _rightPlayerSpecialPointText;

    [SerializeField] private Image[] _rightPlayerSkillCooldownBar;
    [SerializeField] private TextMeshProUGUI[] _rightPlayerSkillCooldownText;
    [SerializeField] private GameObject[] _rightPlayerSkillAvailable;
    [SerializeField] private TextMeshProUGUI[] _rightPlayerSkillButtonText;


    public void ManualStart()
    {
        //_leftPlayerPortrait.sprite = GameController.Instance.LeftPlayerCharacter.Portrait;
        //_rightPlayerPortrait.sprite = GameController.Instance.RightPlayerCharacter.Portrait;

        //_leftPlayerNameText.text = GameController.Instance.LeftPlayerCharacter.Name;
        //_rightPlayerNameText.text = GameController.Instance.RightPlayerCharacter.Name;

    }

    public void ManualUpdate()
    {
        _leftPlayerHitPointBar.fillAmount = GameController.Instance.LeftPlayerCharacter.HitPointRatio();
        _leftPlayerHitPointText.text = GameController.Instance.LeftPlayerCharacter.GetHitPoint().ToString();
        _rightPlayerHitPointBar.fillAmount = GameController.Instance.RightPlayerCharacter.HitPointRatio();
        _rightPlayerHitPointText.text = GameController.Instance.RightPlayerCharacter.GetHitPoint().ToString();

        _leftPlayerSpecialPointBar.fillAmount = GameController.Instance.LeftPlayerCharacter.SpecialPointRatio();
        _leftPlayerSpecialPointText.text = GameController.Instance.LeftPlayerCharacter.GetSpecialPoint().ToString();
        _rightPlayerSpecialPointBar.fillAmount = GameController.Instance.RightPlayerCharacter.SpecialPointRatio();
        _rightPlayerSpecialPointText.text = GameController.Instance.RightPlayerCharacter.GetSpecialPoint().ToString();

        for (int i = 0; i < 4; i++)
        {
            _leftPlayerSkillCooldownBar[i].fillAmount = GameController.Instance.LeftPlayerCharacter.GetCoolTimeRatio(i);
            _leftPlayerSkillCooldownText[i].text = GameController.Instance.LeftPlayerCharacter.GetCoolTime(i).ToString();
            _leftPlayerSkillAvailable[i].SetActive(GameController.Instance.LeftPlayerCharacter.GetCoolTime(i) <= 0);

            _rightPlayerSkillCooldownBar[i].fillAmount = GameController.Instance.RightPlayerCharacter.GetCoolTimeRatio(i);
            _rightPlayerSkillCooldownText[i].text = GameController.Instance.RightPlayerCharacter.GetCoolTime(i).ToString();
            _rightPlayerSkillAvailable[i].SetActive(GameController.Instance.RightPlayerCharacter.GetCoolTime(i) <= 0);
        }

    }
}
