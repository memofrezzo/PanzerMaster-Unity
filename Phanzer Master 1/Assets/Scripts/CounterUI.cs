using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUIText : MonoBehaviour
{
    private Text counterText;
    private float elapsedTime = 0f;

    void Start()
    {
        // Crear el Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Crear el objeto Text
        GameObject textGO = new GameObject("CounterText");
        textGO.transform.parent = canvasGO.transform;

        counterText = textGO.AddComponent<Text>();
        counterText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        counterText.fontSize = 44;
        counterText.color = Color.white;
        counterText.alignment = TextAnchor.MiddleCenter;

        // Posicionar el texto en la parte superior central de la pantalla
        RectTransform rectTransform = counterText.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 50);

        // Ajustar el anclaje para que esté en la parte superior central
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);

        // Posicionar el texto con un pequeño margen desde la parte superior
        rectTransform.anchoredPosition = new Vector2(4, -9);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        counterText.text = Mathf.FloorToInt(elapsedTime).ToString();
    }
}
