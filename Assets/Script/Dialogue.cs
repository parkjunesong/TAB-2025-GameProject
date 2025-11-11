using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TMP 사용을 위해 반드시 필요합니다.

public class DialogueManager : MonoBehaviour
{
    // 대화 텍스트가 표시될 TextMeshPro 컴포넌트
    public TextMeshProUGUI dialogueText;
    
    // 다음 대화로 넘어감을 표시하는 화살표 (UI.Image 또는 RectTransform)
    public GameObject arrowIndicator;

    // 대화 내용 배열
    [TextArea(3, 10)] // 인스펙터 창에서 여러 줄 입력이 가능하게 설정
    public string[] dialogueLines;

    // 타이핑 속도 (글자당 딜레이 시간)
    [Range(0.01f, 0.1f)]
    public float typingSpeed = 0.05f;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    // --- 유니티 시작 시 첫 대화 시작 ---
    void Start()
    {
        // TextMeshPro 컴포넌트가 할당되었는지 확인
        if (dialogueText == null)
        {
            Debug.LogError("Dialogue Text Component가 할당되지 않았습니다. 인스펙터에서 설정해주세요.");
            return;
        }
        
        // 화살표 숨기기
        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(false);
        }

        // 첫 대화 시작
        if (dialogueLines.Length > 0)
        {
            StartDialogue(dialogueLines[currentLineIndex]);
        }
    }

    // --- 업데이트: 입력 처리 (스페이스바 또는 엔터) ---
    void Update()
    {
        // 마우스 클릭 대신 스페이스바 또는 엔터로 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            HandleInput();
        }
    }

    // --- 입력 처리 로직 ---
    public void HandleInput()
    {
        if (isTyping)
        {
            // 1. 타이핑 중일 때: 텍스트 즉시 완료
            CompleteLine();
        }
        else
        {
            // 2. 타이핑이 완료되었을 때: 다음 줄로 이동
            NextDialogueLine();
        }
    }

    // --- 대화 시작/다음 대화로 이동 ---
    public void StartDialogue(string line)
    {
        // 기존 코루틴 중지
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 텍스트 초기화 및 타이핑 코루틴 시작
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypewriterEffect(line));
    }

    // --- 핵심: 타이핑 효과 구현 (코루틴) ---
    IEnumerator TypewriterEffect(string fullText)
    {
        isTyping = true;
        arrowIndicator.SetActive(false); // 타이핑 시작 시 화살표 숨기기

        int charIndex = 0;
        
        // 전체 텍스트를 순회하며 한 글자씩 출력
        while (charIndex < fullText.Length)
        {
            // 글자를 추가합니다. (TMP는 유니코드 문자를 포함하여 처리합니다.)
            dialogueText.text += fullText[charIndex];
            charIndex++;
            
            // 지정된 딜레이 시간만큼 대기합니다.
            yield return new WaitForSeconds(typingSpeed);
        }

        // 타이핑 완료
        isTyping = false;
        // 다음 대화로 넘어갈 수 있음을 표시
        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(true);
        }
    }

    // --- 현재 줄 텍스트 즉시 완료 ---
    private void CompleteLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        // 현재 대화 텍스트를 전체 내용으로 채웁니다.
        dialogueText.text = dialogueLines[currentLineIndex];
        isTyping = false;
        
        // 다음 대화로 넘어갈 수 있음을 표시
        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(true);
        }
    }

    // --- 다음 대화 줄로 이동 ---
    private void NextDialogueLine()
    {
        currentLineIndex++;
        
        if (currentLineIndex < dialogueLines.Length)
        {
            // 다음 대화 시작
            StartDialogue(dialogueLines[currentLineIndex]);
        }
        else
        {
            // 모든 대화가 끝났을 때
            dialogueText.text = "대화 종료...";
            if (arrowIndicator != null)
            {
                arrowIndicator.SetActive(false);
            }
            // 이후 전투 장면 전환 등의 추가 로직을 구현합니다.
            Debug.Log("Dialogue Ended.");
        }
    }
}