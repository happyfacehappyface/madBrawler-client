using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectHandler : MonoBehaviour
{
    [SerializeField] private CharacterViewComponent _leftCharacterViewComponent;
    [SerializeField] private CharacterViewComponent _rightCharacterViewComponent;

    [SerializeField] private GameObject _leftCharacterSelectFrame;
    [SerializeField] private GameObject _rightCharacterSelectFrame;

    private int _leftCharacterType;
    private int _rightCharacterType;

    private bool _isLeftSelected;
    private bool _isRightSelected;

    private const float _gridSize = 210f;

    public void ManualStart()
    {

        TryChangeLeftSelectedCharacter(0);
        TryChangeRightSelectedCharacter(2);

        _isLeftSelected = false;
        _isRightSelected = false;

    }

    public void ManualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TryChangeLeftSelectedCharacter(Mathf.Clamp(_leftCharacterType - 1, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TryChangeLeftSelectedCharacter(Mathf.Clamp(_leftCharacterType + 1, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryChangeLeftSelectedCharacter(Mathf.Clamp(_leftCharacterType - 3, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TryChangeLeftSelectedCharacter(Mathf.Clamp(_leftCharacterType + 3, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleChangeLeftSelected();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryChangeRightSelectedCharacter(Mathf.Clamp(_rightCharacterType - 1, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryChangeRightSelectedCharacter(Mathf.Clamp(_rightCharacterType + 1, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryChangeRightSelectedCharacter(Mathf.Clamp(_rightCharacterType - 3, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryChangeRightSelectedCharacter(Mathf.Clamp(_rightCharacterType + 3, 0, 5));
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ToggleChangeRightSelected();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_isLeftSelected && _isRightSelected)
            {

                SceneManager.sceneLoaded += OnInGameSceneLoaded;
                SceneManager.LoadScene("InGameScene");
            }
        }
        

    }



    private void OnInGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnInGameSceneLoaded;
        GameController controller = GameObject.FindObjectOfType<GameController>();
        
        int selectedLeftCharacterIndex = _leftCharacterType;
        int selectedRightCharacterIndex = _rightCharacterType;

        if (selectedLeftCharacterIndex == 5)
        {
            selectedLeftCharacterIndex = UnityEngine.Random.Range(0, 5);
        }
        if (selectedRightCharacterIndex == 5)
        {
            selectedRightCharacterIndex = UnityEngine.Random.Range(0, 5);
        }

        CharacterType leftCharacterType = (CharacterType)selectedLeftCharacterIndex;
        CharacterType rightCharacterType = (CharacterType)selectedRightCharacterIndex;

        controller.ManualStart(leftCharacterType, rightCharacterType);
    }
    

    

    private void TryChangeLeftSelectedCharacter(int characterType)
    {
        if (_isLeftSelected)
        {
            return;
        }

        _leftCharacterType = characterType;
        _leftCharacterViewComponent.UpdateComponent(_leftCharacterType, _isLeftSelected);

        _leftCharacterSelectFrame.transform.localPosition = GetCharacterGridPosition(_leftCharacterType);

        
    }

    private Vector2 GetCharacterGridPosition(int characterType)
    {
        return new Vector2((characterType % 3 - 1) * _gridSize, (1 - (characterType / 3)) * _gridSize - 225f);
    }

    private void TryChangeRightSelectedCharacter(int characterType)
    {
        if (_isRightSelected)
        {
            return;
        }

        _rightCharacterType = characterType;
        _rightCharacterViewComponent.UpdateComponent(_rightCharacterType, _isRightSelected);

        _rightCharacterSelectFrame.transform.localPosition = GetCharacterGridPosition(_rightCharacterType);
    }

    private void ToggleChangeLeftSelected()
    {
        _isLeftSelected = !_isLeftSelected;
        _leftCharacterViewComponent.UpdateComponent(_leftCharacterType, _isLeftSelected);

        if (_isLeftSelected)
        {
            switch (_leftCharacterType)
            {
                case 0:
                SoundManager.Instance.PlayVoiceSungjunSkill0(0.0f);
                break;
                case 1:
                SoundManager.Instance.PlayVoiceSinniSkill0(0.0f);
                break;
                case 2:
                SoundManager.Instance.PlayVoiceGwanghoSkill2(0.0f);
                break;
                case 3:
                SoundManager.Instance.PlayVoiceSeowooPick(0.0f);
                break;
                case 4:
                SoundManager.Instance.PlayVoiceJaehyeonSkill0(0.0f);
                break;
            }
        }

        
    }

    private void ToggleChangeRightSelected()
    {
        _isRightSelected = !_isRightSelected;
        _rightCharacterViewComponent.UpdateComponent(_rightCharacterType, _isRightSelected);

        if (_isRightSelected)
        {
            switch (_rightCharacterType)
            {
                case 0:
                SoundManager.Instance.PlayVoiceSungjunSkill0(0.0f);
                break;
                case 1:
                SoundManager.Instance.PlayVoiceSinniSkill0(0.0f);
                break;
                case 2:
                SoundManager.Instance.PlayVoiceGwanghoSkill2(0.0f);
                break;
                case 3:
                SoundManager.Instance.PlayVoiceSeowooPick(0.0f);
                break;
                case 4:
                SoundManager.Instance.PlayVoiceJaehyeonSkill0(0.0f);
                break;
            }
        }
    }
}
