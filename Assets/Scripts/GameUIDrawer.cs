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
    [SerializeField] private TextMeshProUGUI _leftPlayerSpecialPointNameText;


    [SerializeField] private Image _rightPlayerPortrait;
    [SerializeField] private TextMeshProUGUI _rightPlayerNameText;
    [SerializeField] private Image _rightPlayerHitPointBar;
    [SerializeField] private TextMeshProUGUI _rightPlayerHitPointText;
    [SerializeField] private Image _rightPlayerSpecialPointBar;
    [SerializeField] private TextMeshProUGUI _rightPlayerSpecialPointText;
    [SerializeField] private TextMeshProUGUI _rightPlayerSpecialPointNameText;


    [SerializeField] private GameObject _playerSkillComponent;
    [SerializeField] private Transform _leftPlayerSkillComponentParent;
    [SerializeField] private Transform _rightPlayerSkillComponentParent;


    private InGameSkillComponent _leftPlayerBasicAttackComponent;
    private InGameSkillComponent[] _leftPlayerSkillComponent;
    private InGameSkillComponent _rightPlayerBasicAttackComponent;
    private InGameSkillComponent[] _rightPlayerSkillComponent;


    public void ManualStart()
    {
        ClearSkillComponent();

        _leftPlayerBasicAttackComponent = Instantiate(_playerSkillComponent, _leftPlayerSkillComponentParent).GetComponent<InGameSkillComponent>();
        _leftPlayerSkillComponent = new InGameSkillComponent[3];
        _leftPlayerBasicAttackComponent.ManualStart(GameController.Instance.LeftPlayerCharacter.GetBasicAttackName(), GetButtonText(Team.Left, 0));
        for (int i = 0; i < 3; i++)
        {
            _leftPlayerSkillComponent[i] = Instantiate(_playerSkillComponent, _leftPlayerSkillComponentParent).GetComponent<InGameSkillComponent>();

            _leftPlayerSkillComponent[i].ManualStart(GameController.Instance.LeftPlayerCharacter.GetSkillName(i), GetButtonText(Team.Left, i + 1));
        }

        _rightPlayerBasicAttackComponent = Instantiate(_playerSkillComponent, _rightPlayerSkillComponentParent).GetComponent<InGameSkillComponent>();
        _rightPlayerSkillComponent = new InGameSkillComponent[3];
        _rightPlayerBasicAttackComponent.ManualStart(GameController.Instance.RightPlayerCharacter.GetBasicAttackName(), GetButtonText(Team.Right, 0));

        for (int i = 0; i < 3; i++)
        {
            _rightPlayerSkillComponent[i] = Instantiate(_playerSkillComponent, _rightPlayerSkillComponentParent).GetComponent<InGameSkillComponent>();
            _rightPlayerSkillComponent[i].ManualStart(GameController.Instance.RightPlayerCharacter.GetSkillName(i), GetButtonText(Team.Right, i + 1));
        }

        _leftPlayerNameText.text = GameController.Instance.LeftPlayerCharacter.GetCharacterName();
        _rightPlayerNameText.text = GameController.Instance.RightPlayerCharacter.GetCharacterName();

        _leftPlayerSpecialPointNameText.text = GameController.Instance.LeftPlayerCharacter.GetSpecialPointName();
        _rightPlayerSpecialPointNameText.text = GameController.Instance.RightPlayerCharacter.GetSpecialPointName();


        _leftPlayerPortrait.sprite = AssetManager.Instance.GetCharacterSprite(GameController.Instance.LeftPlayerCharacter.GetCharacterType());
        _rightPlayerPortrait.sprite = AssetManager.Instance.GetCharacterSprite(GameController.Instance.RightPlayerCharacter.GetCharacterType());
    }

    private void ClearSkillComponent()
    {
        foreach (Transform component in _leftPlayerSkillComponentParent)
        {
            Destroy(component.gameObject);
        }
        foreach (Transform component in _rightPlayerSkillComponentParent)
        {
            Destroy(component.gameObject);
        }
    }

    public void OnLeftPlayerDefeated()
    {
        _leftPlayerPortrait.sprite = AssetManager.Instance.GetCharacterDefeatedSprite(GameController.Instance.LeftPlayerCharacter.GetCharacterType());
    }

    public void OnRightPlayerDefeated()
    {
        _rightPlayerPortrait.sprite = AssetManager.Instance.GetCharacterDefeatedSprite(GameController.Instance.RightPlayerCharacter.GetCharacterType());
    }

    private string GetButtonText(Team team, int index)
    {
        if (team == Team.Left)
        {
            if (index == 0)
            {
                return "Space";
            }
            else if (index == 1)
            {
                return "B";
            }
            else if (index == 2)
            {
                return "N";
            }
            else
            {
                return "M";
            }
        }
        else
        {
            if (index == 0)
            {
                return "0";
            }
            else if (index == 1)
            {
                return "1";
            }
            else if (index == 2)
            {
                return "2";
            }
            else
            {
                return "3";
            }
        }
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

            bool isLeftPlayerAble;
            bool isRightPlayerAble;
            if (i == 0)
            {
                isLeftPlayerAble = GameController.Instance.LeftPlayerCharacter.IsBasicAttackAble();
                isRightPlayerAble = GameController.Instance.RightPlayerCharacter.IsBasicAttackAble();
            }
            else if (i == 1)
            {
                isLeftPlayerAble = GameController.Instance.LeftPlayerCharacter.IsSkill0Able();
                isRightPlayerAble = GameController.Instance.RightPlayerCharacter.IsSkill0Able();
            }
            else if (i == 2)
            {
                isLeftPlayerAble = GameController.Instance.LeftPlayerCharacter.IsSkill1Able();
                isRightPlayerAble = GameController.Instance.RightPlayerCharacter.IsSkill1Able();
            }
            else
            {
                isLeftPlayerAble = GameController.Instance.LeftPlayerCharacter.IsSkill2Able();
                isRightPlayerAble = GameController.Instance.RightPlayerCharacter.IsSkill2Able();
            }

            if (i == 0)
            {
                _leftPlayerBasicAttackComponent.ManualUpdate(GameController.Instance.LeftPlayerCharacter.GetCoolTimeRatio(i), GameController.Instance.LeftPlayerCharacter.GetCoolTime(i), !isLeftPlayerAble);
                _rightPlayerBasicAttackComponent.ManualUpdate(GameController.Instance.RightPlayerCharacter.GetCoolTimeRatio(i), GameController.Instance.RightPlayerCharacter.GetCoolTime(i), !isRightPlayerAble);
            }
            else
            {
                _leftPlayerSkillComponent[i-1].ManualUpdate(GameController.Instance.LeftPlayerCharacter.GetCoolTimeRatio(i), GameController.Instance.LeftPlayerCharacter.GetCoolTime(i), !isLeftPlayerAble);
                _rightPlayerSkillComponent[i-1].ManualUpdate(GameController.Instance.RightPlayerCharacter.GetCoolTimeRatio(i), GameController.Instance.RightPlayerCharacter.GetCoolTime(i), !isRightPlayerAble);
            }

            
        }

    }
}
