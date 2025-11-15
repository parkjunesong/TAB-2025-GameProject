using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BattleSceneLoader : MonoBehaviour
{
    // --- 인스펙터에서 연결할 변수들 ---
    public Image whiteFlashImage; 
    public Camera mainCamera; 
    public string battleSceneName = "Battle Scene"; 

    // 줌 아웃에 걸리는 시간 (초)
    public float zoomDuration = 0.7f; 

    // 줌 아웃 시 도달할 최종 카메라 크기 (값이 클수록 멀어짐)
    public float targetOrthographicSize = 10f; 

    // --- 테스트용 시작 함수 ---
    void Start()
    {
        // (테스트용) 1초 뒤 배틀 전환 시작
        Invoke("StartBattleTransition", 1.0f); 
    }

    // --- 메인 함수 ---
    public void StartBattleTransition()
    {
        StopAllCoroutines();
        StartCoroutine(BattleTransitionSequence());
    }

    IEnumerator BattleTransitionSequence()
    {
        // ----------------------------------------
        // 2. 휜 화면으로 번쩍번쩍하는 기능
        // ----------------------------------------
        yield return StartCoroutine(FlashScreen(3, 0.08f)); 

        // ----------------------------------------
        // 3. 줌 아웃 및 휜 화면으로 전환 (Orthographic 카메라용)
        // ----------------------------------------
        if (mainCamera == null)
        {
            Debug.LogError("!!! BattleSceneLoader ERROR: 'Main Camera'가 연결되지 않았습니다!");
        }
        else
        {
            float timer = 0f;
            // 'fieldOfView' 대신 'orthographicSize'를 사용합니다.
            float originalSize = mainCamera.orthographicSize; 
            Color flashColor = whiteFlashImage.color; 

            while (timer < zoomDuration)
            {
                timer += Time.deltaTime;
                float t = timer / zoomDuration; 

                // 'fieldOfView' 대신 'orthographicSize'를 변경합니다.
                mainCamera.orthographicSize = Mathf.Lerp(originalSize, targetOrthographicSize, t);
                
                flashColor.a = Mathf.Lerp(0, 1, t);
                whiteFlashImage.color = flashColor;
                yield return null; 
            }
        }

        // ----------------------------------------
        // 4. 배틀씬으로 자연스럽게 전환
        // ----------------------------------------
        SceneManager.LoadScene(battleSceneName);
    }

    // 2번 기능을 위한 깜빡임 효과 헬퍼 함수
    IEnumerator FlashScreen(int flashCount, float flashDuration)
    {
        Color flashColor = whiteFlashImage.color;
        for (int i = 0; i < flashCount; i++)
        {
            flashColor.a = 1f;
            whiteFlashImage.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            flashColor.a = 0f;
            whiteFlashImage.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}