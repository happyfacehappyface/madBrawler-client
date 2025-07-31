using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public static AssetManager Instance;
    private bool _isReady = false;

    [SerializeField] private Sprite _sungjun;
    [SerializeField] private Sprite _sinni;
    [SerializeField] private Sprite _gwangho;
    [SerializeField] private Sprite _seowoo;
    [SerializeField] private Sprite _jaehyeon;

    [SerializeField] private Sprite _sungjunDefeated;
    [SerializeField] private Sprite _sinniDefeated;
    [SerializeField] private Sprite _gwanghoDefeated;
    [SerializeField] private Sprite _seowooDefeated;
    [SerializeField] private Sprite _jaehyeonDefeated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _isReady = true;
    }

    public bool IsReady()
    {
        return _isReady;
    }

    public Sprite GetCharacterSprite(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return _sungjun;
            case CharacterType.Sinni:
                return _sinni;
            case CharacterType.Gwangho:
                return _gwangho;
            case CharacterType.Seowoo:
                return _seowoo;
            case CharacterType.Jaehyeon:
                return _jaehyeon;
            default:
                return null;
        }
    }

    public Sprite GetCharacterDefeatedSprite(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return _sungjunDefeated;
            case CharacterType.Sinni:
                return _sinniDefeated;
            case CharacterType.Gwangho:
                return _gwanghoDefeated;
            case CharacterType.Seowoo:
                return _seowooDefeated;
            case CharacterType.Jaehyeon:
                return _jaehyeonDefeated;
            default:
                return null;
        }
    }

    public string GetCharacterBasicAttackName(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return "강펀치";
            case CharacterType.Sinni:
                return "야";
            case CharacterType.Gwangho:
                return "테니스공";
            case CharacterType.Seowoo:
                return "인센디오";
            case CharacterType.Jaehyeon:
                return "손가락 튕기기";
            default:
                return null;
        }
    }

    public string GetCharacterSkill0Name(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return "태클";
            case CharacterType.Sinni:
                return "노래방";
            case CharacterType.Gwangho:
                return "티라노사우르스 렉스";
            case CharacterType.Seowoo:
                return "스투페파이";
            case CharacterType.Jaehyeon:
                return "컴퓨터";
            default:
                return null;
        }
    }

    public string GetCharacterSkill1Name(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return "갈고리";
            case CharacterType.Sinni:
                return "점프";
            case CharacterType.Gwangho:
                return "의자 끌기";
            case CharacterType.Seowoo:
                return "포트키";
            case CharacterType.Jaehyeon:
                return "P2P";
            default:
                return null;
        }
    }

    public string GetCharacterSkill2Name(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return "수플렉스";
            case CharacterType.Sinni:
                return "진대";
            case CharacterType.Gwangho:
                return "주식 떡상";
            case CharacterType.Seowoo:
                return "아바다 케다브라";
            case CharacterType.Jaehyeon:
                return "블록체인";
            default:
                return null;
        }
    }

    public string GetCharacterName(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Sungjun:
                return "성준";
            case CharacterType.Sinni:
                return "신이";
            case CharacterType.Gwangho:
                return "광호";
            case CharacterType.Seowoo:
                return "서우";
            case CharacterType.Jaehyeon:
                return "재현";
            default:
                return null;
        }
    }


}
