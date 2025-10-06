using UnityEngine;
using UnityEngine.EventSystems;

public class FieldHoverDetector : MonoBehaviour {
    [SerializeField] private LayerMask menuZoneMask; 
    [SerializeField] private float hoverDelay = 0.25f;
    [SerializeField] private UIManager ui;

    private float timer = 0f;
    private bool armed = false;

    void Update() {
        // UI 위면 필드 감지 중단
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
            ResetHover();
            return;
        }

        // 마우스 월드좌표 (삼항연산자 X)
        Camera cam = Camera.main;
        Vector2 world = Vector2.zero;
        if (cam != null) {
            world = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // 메뉴 존 위 판정
        bool over = Physics2D.OverlapPoint(world, menuZoneMask);

        if (over) {
            timer += Time.deltaTime;
            if (!armed && timer >= hoverDelay) {
                armed = true;
                ui.ShowContextMenuAt(Input.mousePosition);
            }
        } else {
            ResetHover();
            ui.HideContextMenuIfOpen();
        }

        // ESC로 모두 닫기
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ui.HideContextMenuIfOpen();
            ui.CloseBag();
        }
    }

    private void ResetHover() { timer = 0f; armed = false; }
}
