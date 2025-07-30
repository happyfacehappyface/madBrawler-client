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


}
