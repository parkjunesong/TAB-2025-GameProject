using UnityEngine;
using UnityEngine.EventSystems;

public class FieldHoverDetector : MonoBehaviour {
    [SerializeField] private LayerMask menuZoneMask;
    [SerializeField] private float hoverDelay = 0.25f;
    [SerializeField] private UiManager ui;  

    private float timer = 0f;
    private bool armed = false;

    void Update() {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
            ResetHover();
            return;
        }

        Camera cam = Camera.main;
        Vector2 world = Vector2.zero;
        if (cam != null) {
            world = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        }

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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ui.HideContextMenuIfOpen();
            ui.CloseBag();
        }
    }

    private void ResetHover() { timer = 0f; armed = false; }
}
