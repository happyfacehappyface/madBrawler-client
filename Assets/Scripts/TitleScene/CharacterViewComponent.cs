using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterViewComponent : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _portrait;
    [SerializeField] private Sprite _defaultPortrait;

    [SerializeField] private TextMeshProUGUI _characterDescriptionText;
    [SerializeField] private TextMeshProUGUI _basicAttackNameText;
    [SerializeField] private TextMeshProUGUI _skill0NameText;
    [SerializeField] private TextMeshProUGUI _skill1NameText;
    [SerializeField] private TextMeshProUGUI _skill2NameText;

    [SerializeField] private GameObject _selectFrame;

    

    


    public void UpdateComponent(int characterIndex, bool isSelected)
    {

        if (characterIndex < 5)
        {
            CharacterType characterType = (CharacterType)characterIndex;

            _nameText.text = AssetManager.Instance.GetCharacterName(characterType);
            _portrait.sprite = AssetManager.Instance.GetCharacterSprite(characterType);
            _characterDescriptionText.text = GetCharacterDescription(characterType);
            _basicAttackNameText.text = AssetManager.Instance.GetCharacterBasicAttackName(characterType);
            _skill0NameText.text = AssetManager.Instance.GetCharacterSkill0Name(characterType);
            _skill1NameText.text = AssetManager.Instance.GetCharacterSkill1Name(characterType);
            _skill2NameText.text = AssetManager.Instance.GetCharacterSkill2Name(characterType);

            _selectFrame.SetActive(isSelected);
        }
        else
        {
            _nameText.text = "랜덤";
            _portrait.sprite = _defaultPortrait;
            _characterDescriptionText.text = "";
            _basicAttackNameText.text = "";
            _skill0NameText.text = "";
            _skill1NameText.text = "";
            _skill2NameText.text = "";

            _selectFrame.SetActive(isSelected);
        }
    }

    private string GetCharacterDescription(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
            return "돌진과 갈고리로 상대와의 거리를 좁힌 후, 강력한 수플렉스로 마무리 하세요!";
            case CharacterType.Sinni:
            return "빠르게 전장을 누비며 노래로 상대를 공격하세요! 진대를 통해 상대를 도망칠 수 없게 하세요!";
            case CharacterType.Gwangho:
            return "테니스공과 공룡으로 일방적으로 상대를 공격하고, 주식을 떡상시켜 더 강한 공격을 퍼부으세요!";
            case CharacterType.Seowoo:
            return "강력한 마법들로 상대를 기절시킨 후, 용서받지 못할 마법으로 상대를 공격하세요!";
            case CharacterType.Jaehyeon:
            return "컴퓨터를 설치하고, P2P 통신과 블록체인을 통해 상대를 공격하세요!";
            default:
            return "";
        }
    }

}
