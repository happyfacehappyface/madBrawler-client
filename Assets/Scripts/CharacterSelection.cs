using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [Header("Grid Layout")]
    [SerializeField] private GridLayoutGroup characterGrid;
    [SerializeField] private GameObject characterButtonPrefab;
    
    [Header("플레이어 선택 표시")]
    [SerializeField] private TextMeshProUGUI p1Text;
    [SerializeField] private TextMeshProUGUI p2Text;
    [SerializeField] private Image p1CharacterImage;
    [SerializeField] private Image p2CharacterImage;
    
    [Header("캐릭터 정보 UI")]
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    [Header("스탯 표시")]
    [SerializeField] private Image[] healthBars = new Image[3];        // 체력바 3개
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image[] moveSpeedBars = new Image[3];       // 이동속도바 3개
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    [SerializeField] private Image[] difficultyBars = new Image[3];    // 난이도바 3개
    [SerializeField] private TextMeshProUGUI difficultyText;

    [Header("스킬 정보")]
    [SerializeField] private Image skill0Icon;
    [SerializeField] private TextMeshProUGUI skill0Text;
    [SerializeField] private Image skill1Icon;
    [SerializeField] private TextMeshProUGUI skill1Text;
    [SerializeField] private Image skill2Icon;
    [SerializeField] private TextMeshProUGUI skill2Text;
    
    [Header("스탯 바 색상")]
    [SerializeField] private Color statBarColor = Color.gray; 
    
    [Header("스탯 색상")]
    [SerializeField] private Color statColor = Color.white; 
    
    [Header("캐릭터 데이터")]
    [SerializeField] private CharacterData sungjunData;
    [SerializeField] private CharacterData sinniData;
    [SerializeField] private CharacterData gwanghoData;
    
    [Header("게임 시작 버튼")]
    [SerializeField] private Button playButton;
    
    // 스탯 최대값 (3칸 표시용)
    private const int MAX_STAT_LEVEL = 3;
    
    private int[] selectedCharacters = new int[2] { -1, -1 };
    private int currentHoveredCharacter = -1;
    
    // 커서 위치 (3x2 그리드)
    private Vector2Int p1Cursor = Vector2Int.zero;
    private Vector2Int p2Cursor = Vector2Int.zero;
    
    // 키보드 입력 관련
    private float keyCooldown = 0.15f;
    private float lastP1KeyTime = 0f;
    private float lastP2KeyTime = 0f;
    
    // 그리드 크기 (3x2에서 5개만 사용)
    private const int GRID_WIDTH = 3;
    private const int GRID_HEIGHT = 2;
    private const int TOTAL_CHARACTERS = 5;
    
    // 캐릭터 버튼 리스트
    private List<CharacterButton> characterButtons = new List<CharacterButton>();
    
    // 캐릭터 타입 배열 (CharacterType enum에서 자동 생성)
    private CharacterType[] characterTypes;
    
    // 캐릭터 데이터 딕셔너리
    private Dictionary<CharacterType, CharacterData> characterDataDictionary;
    
    private void Start()
    {
        InitializeCharacterData();
        InitializeUI();
        InitializeStatBars();
        UpdatePlayerDisplay();
        UpdatePlayButton();
        UpdateCursors();
    }
    
    private void Update()
    {
        HandleKeyboardInput();
    }

    private void InitializeCharacterData()
    {
        // CharacterType enum에서 모든 타입 가져오기
        characterTypes = System.Enum.GetValues(typeof(CharacterType)) as CharacterType[];
        
        // 캐릭터 데이터 딕셔너리 초기화
        characterDataDictionary = new Dictionary<CharacterType, CharacterData>();
        
        // 각 CharacterType에 따른 데이터 자동 생성
        characterDataDictionary[CharacterType.Sungjun] = CreateSungjunData();
        characterDataDictionary[CharacterType.Sinni] = CreateSinniData();
        characterDataDictionary[CharacterType.Gwangho] = CreateGwanghoData();
        characterDataDictionary[CharacterType.Seowoo] = CreateSeowooData();
        characterDataDictionary[CharacterType.Jaehyeon] = CreateJaehyeonData();
    }
    
    private CharacterData CreateSungjunData()
    {
        CharacterData data = new CharacterData();
        data.characterName = "성 준";
        data.description = "";
        data.healthLevel = 3;      
        data.moveSpeedLevel = 2;   
        data.difficultyLevel = 2;  
        data.basicAttackDamage = 12f;
        data.basicAttackSpeed = 10f;
        data.basicAttackLifeTime = 2.5f;
        data.characterSprite = GetCharacterSprite(CharacterType.Sungjun);
        data.characterPrefab = GetCharacterPrefab(CharacterType.Sungjun);
        data.skills = CreateSkillDataFromClass<CharacterSungjun>();
        return data;
    }

    private CharacterData CreateSinniData()
    {
        CharacterData data = new CharacterData();
        data.characterName = "신 이";
        data.description = "";
        data.healthLevel = 2;      
        data.moveSpeedLevel = 3;   
        data.difficultyLevel = 2;  
        data.basicAttackDamage = 8f;
        data.basicAttackSpeed = 12f;
        data.basicAttackLifeTime = 2.0f;
        data.characterSprite = GetCharacterSprite(CharacterType.Sinni);
        data.characterPrefab = GetCharacterPrefab(CharacterType.Sinni);
        data.skills = CreateSkillDataFromClass<CharacterSinni>();
        return data;
    }

    private CharacterData CreateGwanghoData()
    {
        CharacterData data = new CharacterData();
        data.characterName = "광 호";
        data.description = "";
        data.healthLevel = 2;     
        data.moveSpeedLevel = 2;   
        data.difficultyLevel = 3;  
        data.basicAttackDamage = 15f;
        data.basicAttackSpeed = 6f;
        data.basicAttackLifeTime = 3.0f;
        data.characterSprite = GetCharacterSprite(CharacterType.Gwangho);
        data.characterPrefab = GetCharacterPrefab(CharacterType.Gwangho);
        data.skills = CreateSkillDataFromClass<CharacterGwangho>();
        return data;
    }

    private CharacterData CreateSeowooData()
    {
        CharacterData data = new CharacterData();
        data.characterName = "서 우";
        data.description = "";
        data.healthLevel = 2;     
        data.moveSpeedLevel = 3;   
        data.difficultyLevel = 2;  
        data.basicAttackDamage = 10f;
        data.basicAttackSpeed = 8f;
        data.basicAttackLifeTime = 2.0f;
        data.characterSprite = GetCharacterSprite(CharacterType.Seowoo);
        data.characterPrefab = GetCharacterPrefab(CharacterType.Seowoo);
        data.skills = CreateSkillDataFromClass<CharacterSeowoo>();
        return data;
    }

    private CharacterData CreateJaehyeonData()
    {
        CharacterData data = new CharacterData();
        data.characterName = "재 현";
        data.description = "";
        data.healthLevel = 2;     
        data.moveSpeedLevel = 2;   
        data.difficultyLevel = 3;  
        data.basicAttackDamage = 12f;
        data.basicAttackSpeed = 9f;
        data.basicAttackLifeTime = 2.5f;
        data.characterSprite = GetCharacterSprite(CharacterType.Jaehyeon);
        data.characterPrefab = GetCharacterPrefab(CharacterType.Jaehyeon);
        data.skills = CreateSkillDataFromClass<CharacterJaehyeon>();
        return data;
    }

    // 캐릭터별 스킬 데이터 생성 (아이콘과 이름만)
    private CharacterSkillData[] CreateSkillDataFromClass<T>() where T : Character
    {
        CharacterSkillData[] skills = new CharacterSkillData[GameConst.SkillCount];
        
        if (typeof(T) == typeof(CharacterSungjun))
        {
            // 성준의 스킬들 
            skills[0] = CreateSimpleSkillData("드라이브");
            skills[1] = CreateSimpleSkillData("기본 공격 강화");
            skills[2] = CreateSimpleSkillData("수플렉스");
        }
        else if (typeof(T) == typeof(CharacterSinni))
        {
            // 신이의 스킬들
            skills[0] = CreateSimpleSkillData("음파 공격");
            skills[1] = CreateSimpleSkillData("점프");
            skills[2] = CreateSimpleSkillData("진실의 방");
        }
        else if (typeof(T) == typeof(CharacterGwangho))
        {
            // 광호의 스킬들 
            skills[0] = CreateSimpleSkillData("티라노");
            skills[1] = CreateSimpleSkillData("의자 타기");
            skills[2] = CreateSimpleSkillData("주식 떡상");
        }
        else if (typeof(T) == typeof(CharacterSeowoo))
        {
            // 서우의 스킬들 
            skills[0] = CreateSimpleSkillData("전기 공격");
            skills[1] = CreateSimpleSkillData("포탈");
            skills[2] = CreateSimpleSkillData("기절 공격");
        }
        else if (typeof(T) == typeof(CharacterJaehyeon))
        {
            // 재현의 스킬들 
            skills[0] = CreateSimpleSkillData("컴퓨터 설치");
            skills[1] = CreateSimpleSkillData("P2P");
            skills[2] = CreateSimpleSkillData("블록체인");
        }
        
        return skills;
    }

    private CharacterSkillData CreateSimpleSkillData(string name)
    {
        CharacterSkillData skill = new CharacterSkillData();
        skill.skillName = name;
        // 나머지는 기본값 사용
        return skill;
    }
    
    // 캐릭터 데이터 가져오기 메서드들
    public CharacterData GetCharacter(CharacterType characterType)
    {
        if (characterDataDictionary.TryGetValue(characterType, out CharacterData characterData))
        {
            return characterData;
        }
        return null;
    }
    
    public CharacterData GetCharacter(int index)
    {
        if (index >= 0 && index < characterTypes.Length)
        {
            CharacterType characterType = characterTypes[index];
            return GetCharacter(characterType);
        }
        return null;
    }
    
    public CharacterData[] GetAllCharacters()
    {
        CharacterData[] dataArray = new CharacterData[characterTypes.Length];
        for (int i = 0; i < characterTypes.Length; i++)
        {
            dataArray[i] = GetCharacter(characterTypes[i]);
        }
        return dataArray;
    }

    private Sprite GetCharacterSprite(CharacterType characterType)
    {
        string spritePath = $"Sprites/{characterType}";
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        if (sprite == null)
        {
            Debug.LogWarning($"Could not load sprite for {characterType} at path: {spritePath}");
        }
        return sprite;
    }

    // 캐릭터 프리팹 가져오기
    private GameObject GetCharacterPrefab(CharacterType characterType)
    {
        string prefabPath = $"Prefabs/Character{characterType}";
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null)
        {
            // 기본 Character 프리팹 사용
            prefab = Resources.Load<GameObject>("Prefabs/Character");
            Debug.LogWarning($"Could not load prefab for {characterType}, using default Character prefab");
        }
        return prefab;
    }
    
    private void InitializeUI()
    {
        // 총 5개의 슬롯 생성 (2개는 캐릭터, 3개는 빈 상태)
        ClearCharacterButtons();
        
        for (int i = 0; i < 5; i++)
        {
            GameObject buttonObj = Instantiate(characterButtonPrefab, characterGrid.transform);
            CharacterButton characterButton = buttonObj.GetComponent<CharacterButton>();
            
            if (characterButton != null)
            {
                if (i < characterTypes.Length)
                {
                    // 캐릭터가 있는 경우
                    CharacterType characterType = characterTypes[i];
                    CharacterData characterData = GetCharacter(characterType);
                    
                    if (characterData != null)
                    {
                        characterButton.Initialize(characterType, i, this, characterData);
                    }
                    else
                    {
                        characterButton.InitializeEmpty(i, this);
                    }
                }
                else
                {
                    // 빈 슬롯인 경우
                    characterButton.InitializeEmpty(i, this);
                }
                
                characterButtons.Add(characterButton);
            }
        }
        
        // Play 버튼 초기화
        playButton.onClick.AddListener(OnPlayButtonClicked);
        
        // 스탯 바 초기화
        InitializeStatBars();
    }
    
    private void ClearCharacterButtons()
    {
        foreach (var button in characterButtons)
        {
            if (button != null)
            {
                DestroyImmediate(button.gameObject);
            }
        }
        characterButtons.Clear();
    }
    
    private void InitializeStatBars()
    {
        // 체력바 설정 (3개 개별 바)
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (healthBars[i] != null)
            {
                healthBars[i].type = Image.Type.Filled;
                healthBars[i].fillMethod = Image.FillMethod.Horizontal;
                healthBars[i].fillOrigin = (int)Image.OriginHorizontal.Left;
                healthBars[i].fillAmount = 0f; // 초기값 0%
                Debug.Log($"HealthBar[{i}] Filled 설정 완료");
            }
        }
        
        // 이동속도바 설정 (3개 개별 바)
        for (int i = 0; i < moveSpeedBars.Length; i++)
        {
            if (moveSpeedBars[i] != null)
            {
                moveSpeedBars[i].type = Image.Type.Filled;
                moveSpeedBars[i].fillMethod = Image.FillMethod.Horizontal;
                moveSpeedBars[i].fillOrigin = (int)Image.OriginHorizontal.Left;
                moveSpeedBars[i].fillAmount = 0f; // 초기값 0%
                Debug.Log($"MoveSpeedBar[{i}] Filled 설정 완료");
            }
        }
        
        // 난이도바 설정 (3개 개별 바)
        for (int i = 0; i < difficultyBars.Length; i++)
        {
            if (difficultyBars[i] != null)
            {
                difficultyBars[i].type = Image.Type.Filled;
                difficultyBars[i].fillMethod = Image.FillMethod.Horizontal;
                difficultyBars[i].fillOrigin = (int)Image.OriginHorizontal.Left;
                difficultyBars[i].fillAmount = 0f; // 초기값 0%
                Debug.Log($"DifficultyBar[{i}] Filled 설정 완료");
            }
        }
    }
    
    private void HandleKeyboardInput()
    {
        float currentTime = Time.time;
        
        // P1 키보드 입력 (WASD)
        if (currentTime - lastP1KeyTime > keyCooldown)
        {
            bool moved = false;
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                p1Cursor.y = Mathf.Max(0, p1Cursor.y - 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                p1Cursor.y = Mathf.Min(GRID_HEIGHT - 1, p1Cursor.y + 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                p1Cursor.x = Mathf.Max(0, p1Cursor.x - 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // 마지막 줄에서는 2개만 있으므로 제한
                int maxX = (p1Cursor.y == 1) ? 1 : 2; // 두 번째 줄(y=1)에서는 x=0,1만
                p1Cursor.x = Mathf.Min(maxX, p1Cursor.x + 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                int characterIndex = GridToIndex(p1Cursor);
                // 5개 캐릭터만 있으므로 범위 체크
                if (characterIndex < TOTAL_CHARACTERS)
                {
                    SelectCharacterForPlayer(0, characterIndex);
                }
            }
            
            if (moved)
            {
                lastP1KeyTime = currentTime;
                UpdateCursors();
                // 범위 체크 추가
                int index = GridToIndex(p1Cursor);
                if (index < TOTAL_CHARACTERS && index < characterButtons.Count)
                {
                    CharacterData characterData = GetCharacter(index);
                    if (characterData != null)
                    {
                        ShowCharacterInfo(characterData);
                    }
                }
            }
        }
        
        // P2 키보드 입력 (화살표 키) - 동일한 로직 적용
        if (currentTime - lastP2KeyTime > keyCooldown)
        {
            bool moved = false;
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                p2Cursor.y = Mathf.Max(0, p2Cursor.y - 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                p2Cursor.y = Mathf.Min(GRID_HEIGHT - 1, p2Cursor.y + 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                p2Cursor.x = Mathf.Max(0, p2Cursor.x - 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 마지막 줄에서는 2개만 있으므로 제한
                int maxX = (p2Cursor.y == 1) ? 1 : 2; // 두 번째 줄(y=1)에서는 x=0,1만
                p2Cursor.x = Mathf.Min(maxX, p2Cursor.x + 1);
                moved = true;
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                int characterIndex = GridToIndex(p2Cursor);
                if (characterIndex < TOTAL_CHARACTERS)
                {
                    SelectCharacterForPlayer(1, characterIndex);
                }
            }
            
            if (moved)
            {
                lastP2KeyTime = currentTime;
                UpdateCursors();
                int index = GridToIndex(p2Cursor);
                if (index < TOTAL_CHARACTERS && index < characterButtons.Count)
                {
                    CharacterData characterData = GetCharacter(index);
                    if (characterData != null)
                    {
                        ShowCharacterInfo(characterData);
                    }
                }
            }
        }
    }

    // 그리드 좌표를 배열 인덱스로 변환
    private int GridToIndex(Vector2Int gridPos)
    {
        return gridPos.y * GRID_WIDTH + gridPos.x;
    }

    // 배열 인덱스를 그리드 좌표로 변환
    private Vector2Int IndexToGrid(int index)
    {
        return new Vector2Int(index % GRID_WIDTH, index / GRID_WIDTH);
    }

    private void UpdateCursors()
    {
        // 모든 버튼의 상태 초기화
        for (int i = 0; i < characterButtons.Count; i++)
        {
            CharacterButton button = characterButtons[i];
            if (button == null) continue;

            // 빈 슬롯은 선택/커서 불가
            if (button.IsEmpty())
            {
                button.SetP1Selected(false);
                button.SetP2Selected(false);
                button.SetP1Cursor(false);
                button.SetP2Cursor(false);
                continue;
            }

            bool p1Selected = (selectedCharacters[0] == i);
            bool p2Selected = (selectedCharacters[1] == i);
            bool p1CursorHere = (GridToIndex(p1Cursor) == i);
            bool p2CursorHere = (GridToIndex(p2Cursor) == i);
            
            button.SetP1Selected(p1Selected);
            button.SetP2Selected(p2Selected);
            button.SetP1Cursor(p1CursorHere);
            button.SetP2Cursor(p2CursorHere);
        }
    }
    
    private void SelectCharacterForPlayer(int playerIndex, int characterIndex)
    {
        // 캐릭터 인덱스가 유효한지 확인
        if (characterIndex < 0 || characterIndex >= characterButtons.Count) return;
        
        // 빈 슬롯인지 확인
        if (characterButtons[characterIndex].IsEmpty()) return;
        
        selectedCharacters[playerIndex] = characterIndex;
        UpdateCursors();
        UpdatePlayerDisplay();
        UpdatePlayButton();
        
        // 선택된 캐릭터 정보 표시
        CharacterData selectedData = characterDataDictionary[characterTypes[characterIndex]];
        if (selectedData != null)
        {
            ShowCharacterInfo(selectedData);
        }
        
        // 선택 효과
        StartCoroutine(SelectionEffect(characterIndex));
    }
    
    private IEnumerator SelectionEffect(int characterIndex)
    {
        if (characterIndex < characterButtons.Count)
        {
            RectTransform buttonRect = characterButtons[characterIndex].GetComponent<RectTransform>();
            Vector3 originalScale = buttonRect.localScale;
            
            buttonRect.localScale = originalScale * 1.1f;
            yield return new WaitForSeconds(0.1f);
            buttonRect.localScale = originalScale;
        }
    }
    
    public void OnCharacterButtonClicked(int characterIndex)
    {
        // 마우스 클릭으로도 선택 가능 (P1이 우선)
        if (selectedCharacters[0] == -1)
        {
            SelectCharacterForPlayer(0, characterIndex);
        }
        else if (selectedCharacters[1] == -1)
        {
            SelectCharacterForPlayer(1, characterIndex);
        }
    }
    
    public void OnCharacterButtonHover(int characterIndex)
    {
        currentHoveredCharacter = characterIndex;
        ShowCharacterInfo(characterDataDictionary[characterTypes[characterIndex]]);
    }
    
    public void OnCharacterButtonExit()
    {
        currentHoveredCharacter = -1;
        // 마지막으로 선택된 캐릭터 정보 표시
        int lastSelectedPlayer = (selectedCharacters[1] != -1) ? 1 : 0;
        if (selectedCharacters[lastSelectedPlayer] != -1)
        {
            ShowCharacterInfo(characterDataDictionary[characterTypes[selectedCharacters[lastSelectedPlayer]]]);
        }
    }
    
    private void UpdatePlayerDisplay()
    {
        // P1 표시 업데이트
        if (selectedCharacters[0] != -1)
        {
            CharacterData p1Character = characterDataDictionary[characterTypes[selectedCharacters[0]]];
            p1Text.text = $"P1: ";
            p1Text.color = Color.blue; 
            if (p1CharacterImage != null)
            {
                p1CharacterImage.sprite = p1Character.characterSprite;
                p1CharacterImage.gameObject.SetActive(true);
            }
        }
        else
        {
            p1Text.text = "P1: ";
            p1Text.color = Color.white;
            if (p1CharacterImage != null)
            {
                p1CharacterImage.gameObject.SetActive(false);
            }
        }
        
        // P2 표시 업데이트
        if (selectedCharacters[1] != -1)
        {
            CharacterData p2Character = characterDataDictionary[characterTypes[selectedCharacters[1]]];
            p2Text.text = $"P2: ";
            p2Text.color = Color.red; 
            if (p2CharacterImage != null)
            {
                p2CharacterImage.sprite = p2Character.characterSprite;
                p2CharacterImage.gameObject.SetActive(true);
            }
        }
        else
        {
            p2Text.text = "P2: ";
            p2Text.color = Color.white;
            if (p2CharacterImage != null)
            {
                p2CharacterImage.gameObject.SetActive(false);
            }
        }
    }
    
    private void ShowCharacterInfo(CharacterData characterData)
    {
        if (characterData == null) return;
        
        Debug.Log($"Showing character info for: {characterData.characterName}");
        Debug.Log($"Character sprite: {characterData.characterSprite}");
        Debug.Log($"Skills array length: {characterData.skills?.Length ?? 0}");
        
        // 캐릭터 이미지 업데이트
        if (characterImage != null)
        {
            characterImage.sprite = characterData.characterSprite;
            characterImage.gameObject.SetActive(characterData.characterSprite != null);
        }
        
        // 캐릭터 이름 업데이트
        if (characterNameText != null)
        {
            characterNameText.text = characterData.characterName ?? "이름 없음";
        }
        
        // 캐릭터 설명 업데이트
        if (descriptionText != null)
        {
            descriptionText.text = characterData.description ?? "설명 없음";
        }
        
        UpdateStatDisplay(characterData);
        UpdateSkillDisplay(characterData);
    }
    
    private void UpdateStatDisplay(CharacterData characterData)
    {
        Debug.Log($"UpdateStatDisplay - Health: {characterData.healthLevel}, MoveSpeed: {characterData.moveSpeedLevel}, Difficulty: {characterData.difficultyLevel}");
        
        // 체력바 업데이트 (3개 개별 바)
        UpdateStatBars(healthBars, characterData.healthLevel, statBarColor);
        
        if (healthText != null)
        {
            string healthLevelText = GetStatLevelText(characterData.healthLevel);
            healthText.text = $"체력: {healthLevelText}";
            healthText.color = statColor;
        }
        
        // 이동속도바 업데이트 (3개 개별 바)
        UpdateStatBars(moveSpeedBars, characterData.moveSpeedLevel, statBarColor);
        
        if (moveSpeedText != null)
        {
            string moveSpeedLevelText = GetStatLevelText(characterData.moveSpeedLevel);
            moveSpeedText.text = $"이동속도: {moveSpeedLevelText}";
            moveSpeedText.color = statColor;
        }
        
        // 난이도바 업데이트 (3개 개별 바)
        UpdateStatBars(difficultyBars, characterData.difficultyLevel, statBarColor);
        
        if (difficultyText != null)
        {
            string difficultyLevelText = GetStatLevelText(characterData.difficultyLevel);
            difficultyText.text = $"난이도: {difficultyLevelText}";
            difficultyText.color = statColor;
        }
    }
    
    private void UpdateSkillDisplay(CharacterData characterData)
    {
        // 스킬 0 정보 업데이트
        if (skill0Icon != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 0 && 
                characterData.skills[0] != null && characterData.skills[0].skillIcon != null)
            {
                skill0Icon.sprite = characterData.skills[0].skillIcon;
                skill0Icon.gameObject.SetActive(true);
            }
            else
            {
                skill0Icon.gameObject.SetActive(false);
            }
        }
        
        if (skill0Text != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 0 && 
                characterData.skills[0] != null)
            {
                string skillName = !string.IsNullOrEmpty(characterData.skills[0].skillName) 
                    ? characterData.skills[0].skillName : "스킬 1";
                skill0Text.text = $"{skillName}";
            }
            else
            {
                skill0Text.text = "스킬 정보 없음";
            }
        }
        
        // 스킬 1 정보 업데이트
        if (skill1Icon != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 1 && 
                characterData.skills[1] != null && characterData.skills[1].skillIcon != null)
            {
                skill1Icon.sprite = characterData.skills[1].skillIcon;
                skill1Icon.gameObject.SetActive(true);
            }
            else
            {
                skill1Icon.gameObject.SetActive(false);
            }
        }
        
        if (skill1Text != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 1 && 
                characterData.skills[1] != null)
            {
                string skillName = !string.IsNullOrEmpty(characterData.skills[1].skillName) 
                    ? characterData.skills[1].skillName : "스킬 2";
                skill1Text.text = $"{skillName}";
            }
            else
            {
                skill1Text.text = "스킬 정보 없음";
            }
        }
        
        // 스킬 2 정보 업데이트
        if (skill2Icon != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 2 && 
                characterData.skills[2] != null && characterData.skills[2].skillIcon != null)
            {
                skill2Icon.sprite = characterData.skills[2].skillIcon;
                skill2Icon.gameObject.SetActive(true);
            }
            else
            {
                skill2Icon.gameObject.SetActive(false);
            }
        }
        
        if (skill2Text != null)
        {
            if (characterData.skills != null && characterData.skills.Length > 2 && 
                characterData.skills[2] != null)
            {
                string skillName = !string.IsNullOrEmpty(characterData.skills[2].skillName) 
                    ? characterData.skills[2].skillName : "스킬 3";
                skill2Text.text = $"{skillName}";
            }
            else
            {
                skill2Text.text = "스킬 정보 없음";
            }
        }
    }
    
    private void UpdatePlayButton()
    {
        playButton.interactable = (selectedCharacters[0] != -1 && selectedCharacters[1] != -1);
    }
    
    private void OnPlayButtonClicked()
    {
        CharacterData leftPlayerCharacter = characterDataDictionary[characterTypes[selectedCharacters[0]]];
        CharacterData rightPlayerCharacter = characterDataDictionary[characterTypes[selectedCharacters[1]]];
        
        // OutGameController에 선택된 캐릭터 정보 전달
        OutGameController outGameController = FindObjectOfType<OutGameController>();
        if (outGameController != null)
        {
            outGameController.SetSelectedCharacters(leftPlayerCharacter, rightPlayerCharacter);
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGameScene");
    }

    // 스탯 레벨을 텍스트로 변환하는 헬퍼 메서드
    private string GetStatLevelText(int level)
    {
        switch (level)
        {
            case 1: return "낮음";
            case 2: return "보통";
            case 3: return "높음";
            default: return "보통";
        }
    }

    // 개별 바들을 업데이트하는 헬퍼 메서드
    private void UpdateStatBars(Image[] bars, int level, Color barColor)
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (bars[i] != null)
            {
                // i+1이 level보다 작거나 같으면 채우기, 아니면 비우기
                bool shouldFill = (i + 1) <= level;
                bars[i].fillAmount = shouldFill ? 1f : 0f;
                bars[i].color = barColor;
            }
        }
    }
}

// CharacterData와 CharacterSkillData 클래스는 별도 파일로 만들거나 여기에 추가
[System.Serializable]
public class CharacterSkillData
{
    [Header("스킬 기본 정보")]
    public string skillName;
    public Sprite skillIcon;
}

[System.Serializable]
public class CharacterData
{
    [Header("캐릭터 기본 정보")]
    public string characterName;
    public string description;
    public Sprite characterSprite;
    
    [Header("캐릭터 스탯 (1=낮음, 2=보통, 3=높음)")]
    public int healthLevel = 2;        // 체력 레벨
    public int moveSpeedLevel = 2;     // 이동속도 레벨
    public int difficultyLevel = 2;    // 난이도 레벨
    
    [Header("기본 공격 설정")]
    public float basicAttackCooldown = 0.2f;
    public float basicAttackDamage = 10f;
    public float basicAttackSpeed = 8f;
    public float basicAttackLifeTime = 2.0f;
    
    [Header("캐릭터별 스킬 정보")]
    public CharacterSkillData[] skills = new CharacterSkillData[GameConst.SkillCount];
    
    [Header("캐릭터 프리팹")]
    public GameObject characterPrefab;
}