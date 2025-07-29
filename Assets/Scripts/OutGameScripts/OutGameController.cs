using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutGameController : MonoBehaviour
{
    [SerializeField] private GameObject _StartScreen;
    [SerializeField] private GameObject _ResultScreen;
    [SerializeField] private GameObject _HowtoPlayScreen;
    [SerializeField] private GameObject _SelectCharacterScreen;
    [SerializeField] private GameObject _StartButton;
    [SerializeField] private GameObject _HowtoPlayButton;
    [SerializeField] private GameObject _RetryButton;
    [SerializeField] private GameObject _MainButton;
    
    // CharacterSelection 추가
    [SerializeField] private CharacterSelection _characterSelection;
    
    // 선택된 캐릭터 정보 저장
    private CharacterData _leftPlayerCharacterData;
    private CharacterData _rightPlayerCharacterData;
    
    // 승자 정보 저장
    private Team _winner = Team.Left;
    private bool _gameEnded = false;

    private void Start()
    {
        _StartScreen.SetActive(true);
        
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(false);
        }
    }

    public void OnClickStartButton()
    {
        _StartScreen.SetActive(false);
        _HowtoPlayScreen.SetActive(false);
        _SelectCharacterScreen.SetActive(true);
        
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(true);
        }
    }

    public void OnClickHowtoPlayButton()
    {
        _StartScreen.SetActive(false);
        _HowtoPlayScreen.SetActive(true);
        
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(false);
        }
    }

    public void OnClickRetryButton()
    {
        _ResultScreen.SetActive(false);
        _SelectCharacterScreen.SetActive(true);
        
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(true);
        }
    }

    public void OnClickMainButton()
    {
        _ResultScreen.SetActive(false);
        _StartScreen.SetActive(true);
        
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(false);
        }
    }
    
    // 게임 시작 함수 (CharacterSelection에서 호출)
    public void StartGame()
    {
        if (_characterSelection != null)
        {
            _characterSelection.gameObject.SetActive(false);
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayScene");
    }
    
    // 선택된 캐릭터 정보 설정
    public void SetSelectedCharacters(CharacterData leftCharacter, CharacterData rightCharacter)
    {
        _leftPlayerCharacterData = leftCharacter;
        _rightPlayerCharacterData = rightCharacter;
    }
    
    // 선택된 캐릭터 정보 가져오기
    public CharacterData GetLeftPlayerCharacterData()
    {
        return _leftPlayerCharacterData;
    }
    
    public CharacterData GetRightPlayerCharacterData()
    {
        return _rightPlayerCharacterData;
    }
    
    // 승자 정보 설정
    public void SetWinner(Team winner)
    {
        _winner = winner;
        _gameEnded = true;
    }
    
    // 승자 정보 가져오기
    public Team GetWinner()
    {
        return _winner;
    }
    
    public bool IsGameEnded()
    {
        return _gameEnded;
    }
}
