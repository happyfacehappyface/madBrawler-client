using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScreenController : MonoBehaviour
{
    [Header("결과 화면 UI")]
    [SerializeField] private GameObject resultScreen;
    
    [Header("캐릭터 아이콘")]
    [SerializeField] private Image leftPlayerCharacterIcon;
    [SerializeField] private Image rightPlayerCharacterIcon;
    
    [Header("플레이어 이름")]
    [SerializeField] private TextMeshProUGUI leftPlayerNameText;
    [SerializeField] private TextMeshProUGUI rightPlayerNameText;
    
    [Header("승자 표시")]
    [SerializeField] private GameObject leftPlayerWinnerIndicator;
    [SerializeField] private GameObject rightPlayerWinnerIndicator;
    
    [Header("버튼")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    
    private OutGameController outGameController;
    
    private void Start()
    {
        // OutGameController 찾기
        outGameController = FindObjectOfType<OutGameController>();
        
        // 버튼 이벤트 연결
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
        
        // 초기 상태 설정
        if (resultScreen != null)
        {
            resultScreen.SetActive(false);
        }
    }
    
    // 결과 화면 표시
    public void ShowResultScreen()
    {
        if (resultScreen != null)
        {
            resultScreen.SetActive(true);
            UpdateCharacterIcons();
            UpdateWinnerDisplay();
        }
    }
    
    // 결과 화면 숨기기
    public void HideResultScreen()
    {
        if (resultScreen != null)
        {
            resultScreen.SetActive(false);
        }
    }
    
    // 캐릭터 아이콘 업데이트
    private void UpdateCharacterIcons()
    {
        if (outGameController == null) return;
        
        // 왼쪽 플레이어 캐릭터 정보
        CharacterData leftCharacter = outGameController.GetLeftPlayerCharacterData();
        if (leftCharacter != null)
        {
            if (leftPlayerCharacterIcon != null)
            {
                leftPlayerCharacterIcon.sprite = leftCharacter.characterSprite;
                leftPlayerCharacterIcon.gameObject.SetActive(true);
            }
            
            if (leftPlayerNameText != null)
            {
                leftPlayerNameText.text = leftCharacter.characterName;
            }
        }
        else
        {
            if (leftPlayerCharacterIcon != null)
            {
                leftPlayerCharacterIcon.gameObject.SetActive(false);
            }
            
            if (leftPlayerNameText != null)
            {
                leftPlayerNameText.text = "선택되지 않음";
            }
        }
        
        // 오른쪽 플레이어 캐릭터 정보
        CharacterData rightCharacter = outGameController.GetRightPlayerCharacterData();
        if (rightCharacter != null)
        {
            if (rightPlayerCharacterIcon != null)
            {
                rightPlayerCharacterIcon.sprite = rightCharacter.characterSprite;
                rightPlayerCharacterIcon.gameObject.SetActive(true);
            }
            
            if (rightPlayerNameText != null)
            {
                rightPlayerNameText.text = rightCharacter.characterName;
            }
        }
        else
        {
            if (rightPlayerCharacterIcon != null)
            {
                rightPlayerCharacterIcon.gameObject.SetActive(false);
            }
            
            if (rightPlayerNameText != null)
            {
                rightPlayerNameText.text = "선택되지 않음";
            }
        }
    }
    
    // 승자 표시 업데이트
    private void UpdateWinnerDisplay()
    {
        if (outGameController == null) return;
        
        Team winner = outGameController.GetWinner();
        
        // 왼쪽 플레이어 승자 표시
        if (leftPlayerWinnerIndicator != null)
        {
            leftPlayerWinnerIndicator.SetActive(winner == Team.Left);
        }
        
        // 오른쪽 플레이어 승자 표시
        if (rightPlayerWinnerIndicator != null)
        {
            rightPlayerWinnerIndicator.SetActive(winner == Team.Right);
        }
    }
    
    // 재시작 버튼 클릭
    private void OnRetryButtonClicked()
    {
        if (outGameController != null)
        {
            outGameController.OnClickRetryButton();
        }
    }
    
    // 메인 메뉴 버튼 클릭
    private void OnMainMenuButtonClicked()
    {
        if (outGameController != null)
        {
            outGameController.OnClickMainButton();
        }
    }
    
    // 외부에서 호출할 수 있는 공개 메서드들
    public void SetWinner(Team winner)
    {
        if (outGameController != null)
        {
            outGameController.SetWinner(winner);
        }
    }
    
    public Team GetWinner()
    {
        if (outGameController != null)
        {
            return outGameController.GetWinner();
        }
        return Team.Left; // 기본값
    }
}