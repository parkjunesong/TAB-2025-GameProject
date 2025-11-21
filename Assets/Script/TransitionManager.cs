using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 

public class TransitionManager : MonoBehaviour
{
    // 싱글톤 인스턴스: 다른 스크립트에서 쉽게 접근 가능하게 합니다.
    public static TransitionManager Instance;

    [Header("Settings")]
    // Inspector에서 Panel (Image) 오브젝트를 연결해야 합니다.
    public Image transitionImage; 
    
    // 암전/명전이 진행되는 시간 (초)
    public float transitionDuration = 1.0f; 

    // 쉐이더의 _Progress 값을 제어하기 위한 Material 인스턴스
    private Material transitionMat;
    
    // 암전 진행 중인지 확인하는 상태 변수 (선택 사항)
    private bool isTransitioning = false; 

    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (Instance == null)
        {
            Instance = this;
            // 씬이 바뀌어도 오브젝트가 파괴되지 않게 유지합니다. (선택 사항)
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (transitionImage != null)
        {
            // Material 인스턴스 생성 및 연결 (매우 중요)
            transitionMat = Instantiate(transitionImage.material);
            transitionImage.material = transitionMat;
        }
        else
        {
            Debug.LogError("Transition Image (Panel)이 연결되지 않았습니다. Inspector를 확인하세요!");
        }
    }

    // ===============================================
    // === 암전 효과 시각 테스트를 위한 임시 호출 코드 ===
    // *주의: 실제 게임을 만들 때는 이 Start() 함수를 반드시 제거해야 합니다.
    // ===============================================
    void Start()
    {
        if (transitionMat == null) return; // Material이 없으면 실행하지 않음

        // 게임 시작 시, 암전 효과 시퀀스를 즉시 실행합니다.
        Debug.Log("게임 시작! 암전 효과 테스트를 바로 시작합니다.");
        LoadScene("TestScene"); 
    }
    // ===============================================


    // 외부에서 씬 전환을 요청하는 함수
    public void LoadScene(string sceneName)
    {
        if (isTransitioning) return;
        isTransitioning = true;
        StartCoroutine(TransitionRoutine(sceneName));
    }

    // 암전->맵 로드->명전 시퀀스를 처리하는 코루틴
    private IEnumerator TransitionRoutine(string sceneName)
    {
        // 1. 암전 시작 (Fade Out)
        yield return StartCoroutine(Fade(0f, 1f));

        // 2. 맵 로드 대신 임시 대기 (암전 상태 유지)
        Debug.Log(">> 맵 로드 대신 임시 대기 (0.5초) <<");
        yield return new WaitForSeconds(0.5f); 

        // 3. (실제 맵 로드가 이루어질 위치)
        // SceneManager.LoadScene(sceneName); // 실제 씬 로드 시 이 코드를 사용

        // 4. 화면 밝아짐 (Fade In)
        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;
        Debug.Log("암전 테스트 완료.");
    }

    // 쉐이더의 _Progress 값을 시간에 따라 부드럽게 변경하는 코루틴
    private IEnumerator Fade(float startProgress, float endProgress)
    {
        float currentTime = 0f;
        
        while (currentTime < transitionDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / transitionDuration;
            
            // Lerp로 현재 진행도 계산
            float currentProgress = Mathf.Lerp(startProgress, endProgress, t);
            
            // 쉐이더의 _Progress 값 변경
            transitionMat.SetFloat("_Progress", currentProgress);
            
            yield return null;
        }
        
        // 확실하게 최종값으로 고정
        transitionMat.SetFloat("_Progress", endProgress);
    }
}