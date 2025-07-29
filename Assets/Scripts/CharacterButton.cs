using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI 컴포넌트")]
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Image borderImage;
    
    [Header("선택 상태 색상")]
    [SerializeField] private Color p1SelectedColor = Color.blue;
    [SerializeField] private Color p2SelectedColor = Color.red;
    [SerializeField] private Color bothSelectedColor = new Color(0.6f, 0.2f, 1f, 1f); // 보라색 (R,G,B,A)
    [SerializeField] private Color p1CursorColor = Color.blue;
    [SerializeField] private Color p2CursorColor = Color.red;
    [SerializeField] private Color bothCursorColor = Color.yellow;
    
    private CharacterType characterType;
    private int characterIndex;
    private CharacterSelection characterSelection;
    private bool isEmpty = false;
    
    // 선택 상태
    private bool isP1Selected = false;
    private bool isP2Selected = false;
    private bool isP1Cursor = false;
    private bool isP2Cursor = false;
    
    public void Initialize(CharacterType type, int index, CharacterSelection selection, CharacterData characterData)
    {
        characterType = type;
        characterIndex = index;
        characterSelection = selection;
        isEmpty = false;
        
        // UI 설정
        if (characterImage != null && characterData.characterSprite != null)
        {
            characterImage.sprite = characterData.characterSprite;
        }
        
        if (characterNameText != null)
        {
            characterNameText.text = characterData.characterName;
        }
        
        // 테두리 초기화 (처음에는 안 보이게)
        if (borderImage != null)
        {
            borderImage.gameObject.SetActive(false);
        }
        
        UpdateBorder();
    }
    
    public void InitializeEmpty(int index, CharacterSelection selection)
    {
        characterIndex = index;
        characterSelection = selection;
        isEmpty = true;
        
        // 빈 슬롯 설정
        if (characterImage != null)
        {
            characterImage.sprite = null;
            characterImage.color = new Color(0.7f, 0.7f, 0.7f, 0.5f); // 더 밝은 회색
        }
        
        if (characterNameText != null)
        {
            characterNameText.text = "";
        }
        
        // 테두리 초기화 (빈 슬롯은 항상 안 보이게)
        if (borderImage != null)
        {
            borderImage.gameObject.SetActive(false);
        }
        
        UpdateBorder();
    }
    
    public void SetP1Selected(bool selected)
    {
        if (isEmpty) return; // 빈 슬롯은 선택 불가
        
        isP1Selected = selected;
        UpdateBorder();
    }
    
    public void SetP2Selected(bool selected)
    {
        if (isEmpty) return; // 빈 슬롯은 선택 불가
        
        isP2Selected = selected;
        UpdateBorder();
    }
    
    public void SetP1Cursor(bool cursor)
    {
        if (isEmpty) return; // 빈 슬롯은 커서 불가
        
        isP1Cursor = cursor;
        UpdateBorder();
    }
    
    public void SetP2Cursor(bool cursor)
    {
        if (isEmpty) return; // 빈 슬롯은 커서 불가
        
        isP2Cursor = cursor;
        UpdateBorder();
    }
    
    private void UpdateBorder()
    {
        if (borderImage == null || isEmpty) 
        {
            if (borderImage != null)
            {
                borderImage.gameObject.SetActive(false);
            }
            return;
        }
        
        // 선택 상태 우선순위: P1 선택 > P2 선택 > P1 커서 > P2 커서
        if (isP1Selected && isP2Selected)
        {
            borderImage.color = bothSelectedColor; // 노란색 (두 플레이어가 같은 캐릭터 선택)
            borderImage.gameObject.SetActive(true);
        }
        else if (isP1Selected)
        {
            borderImage.color = p1SelectedColor; // 파란색
            borderImage.gameObject.SetActive(true);
        }
        else if (isP2Selected)
        {
            borderImage.color = p2SelectedColor; // 빨간색
            borderImage.gameObject.SetActive(true);
        }
        else if (isP1Cursor && isP2Cursor)
        {
            borderImage.color = bothCursorColor; // 노란색 커서
            borderImage.gameObject.SetActive(true);
        }
        else if (isP1Cursor)
        {
            borderImage.color = p1CursorColor; // 파란색 커서
            borderImage.gameObject.SetActive(true);
        }
        else if (isP2Cursor)
        {
            borderImage.color = p2CursorColor; // 빨간색 커서
            borderImage.gameObject.SetActive(true);
        }
        else
        {
            borderImage.gameObject.SetActive(false);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isEmpty)
        {
            return;
        }
        
        characterSelection?.OnCharacterButtonClicked(characterIndex);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isEmpty) return;
        
        characterSelection?.OnCharacterButtonHover(characterIndex);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isEmpty) return;
        
        characterSelection?.OnCharacterButtonExit();
    }
    
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
    
    public int GetCharacterIndex()
    {
        return characterIndex;
    }
    
    public bool IsEmpty()
    {
        return isEmpty;
    }
}
