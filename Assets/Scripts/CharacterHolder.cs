using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    [SerializeField] private Transform _spinnedTransform;
    public Transform SpinnedTransform => _spinnedTransform;
    [SerializeField] private Transform _notSpinnedTransform;
    public Transform NotSpinnedTransform => _notSpinnedTransform;
    [SerializeField] private Transform _bodyTransform;
    public Transform BodyTransform => _bodyTransform;


    [SerializeField] private GameObject _stunEffect;
    [SerializeField] private GameObject _bondEffect;
    [SerializeField] private GameObject _moveSpeedUpEffect;
    [SerializeField] private GameObject _moveSpeedDownEffect;

    [SerializeField] private SpriteRenderer _characterImage;

    private CharacterType _characterType;

    public void ManualStart(CharacterType characterType)
    {
        _characterType = characterType;
        switch (characterType)
        {
            case CharacterType.Sungjun:
                gameObject.AddComponent<CharacterSungjun>();
                break;
            case CharacterType.Sinni:
                gameObject.AddComponent<CharacterSinni>();
                break;
            case CharacterType.Gwangho:
                gameObject.AddComponent<CharacterGwangho>();
                break;
            case CharacterType.Seowoo:
                gameObject.AddComponent<CharacterSeowoo>();
                break;
            case CharacterType.Jaehyeon:
                gameObject.AddComponent<CharacterJaehyeon>();
                break;
        }

        _characterImage.sprite = AssetManager.Instance.GetCharacterSprite(characterType);
    }

    public void SetCharacterImageToDefeated()
    {
        _characterImage.sprite = AssetManager.Instance.GetCharacterDefeatedSprite(_characterType);
    }

    public void SetActiveStunEffect(bool isActive) => _stunEffect.SetActive(isActive);
    public void SetActiveBondEffect(bool isActive) => _bondEffect.SetActive(isActive);
    public void SetActiveMoveSpeedUpEffect(bool isActive) => _moveSpeedUpEffect.SetActive(isActive);
    public void SetActiveMoveSpeedDownEffect(bool isActive) => _moveSpeedDownEffect.SetActive(isActive);
}
