using UnityEngine;

public class UIManager : MonoBehaviour {
    [Header("Refs")]
    [SerializeField] private Canvas canvas;            
    [SerializeField] private RectTransform contextMenu;  
    [SerializeField] private RectTransform bagPanel;     

    void Awake() {
        if (contextMenu) contextMenu.gameObject.SetActive(false);
        if (bagPanel)    bagPanel.gameObject.SetActive(false);
    }

    public void ShowContextMenuAt(Vector2 screenPos) {
        if (!contextMenu) return;
        PositionAtScreen(contextMenu, screenPos);
        contextMenu.gameObject.SetActive(true);
        if (bagPanel) bagPanel.gameObject.SetActive(false);
    }

    public void HideContextMenuIfOpen() {
        if (contextMenu) contextMenu.gameObject.SetActive(false);
    }

    public void OpenBag() {
        if (contextMenu) contextMenu.gameObject.SetActive(false);
        if (bagPanel)    bagPanel.gameObject.SetActive(true);
    }

    public void CloseBag() {
        if (bagPanel) bagPanel.gameObject.SetActive(false);
    }

    private void PositionAtScreen(RectTransform rt, Vector2 screenPos) {
        var canvasRect = (RectTransform)canvas.transform;

        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay) {
            cam = Camera.main;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            cam,
            out var anchored
        );

        Vector2 size = rt.sizeDelta;
        Vector2 half = size * 0.5f;
        Vector2 min = canvasRect.rect.min + half;
        Vector2 max = canvasRect.rect.max - half;
        anchored.x = Mathf.Clamp(anchored.x, min.x, max.x);
        anchored.y = Mathf.Clamp(anchored.y, min.y, max.y);

        rt.anchoredPosition = anchored;
    }
}
