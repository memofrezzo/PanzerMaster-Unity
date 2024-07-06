using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Text player1ResourcesText;
    public Text player2ResourcesText;

    private int player1Gold = 30;
    private int player1Metal = 80;
    private int player2Gold = 30;
    private int player2Metal = 80;
    private float tiempoTranscurrido = 0;

    private float resourceTimer = 0f;
    private float resourceInterval = 1f;

    void Start()
    {
        // Crear el Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Crear el texto para los recursos del jugador 1
        GameObject player1TextGO = new GameObject("Player1ResourcesText");
        player1TextGO.transform.parent = canvasGO.transform;

        player1ResourcesText = player1TextGO.AddComponent<Text>();
        player1ResourcesText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        player1ResourcesText.fontSize = 40;
        player1ResourcesText.color = Color.white;
        player1ResourcesText.alignment = TextAnchor.MiddleCenter;

        // Posicionar el texto del jugador 1 en la parte superior central de la pantalla
        RectTransform player1RectTransform = player1ResourcesText.GetComponent<RectTransform>();
        player1RectTransform.sizeDelta = new Vector2(400, 50);
        player1RectTransform.anchorMin = new Vector2(0.5f, 1f);
        player1RectTransform.anchorMax = new Vector2(0.5f, 1f);
        player1RectTransform.pivot = new Vector2(0.5f, 1f);
        player1RectTransform.anchoredPosition = new Vector2(-680, -822);

        // Crear el texto para los recursos del jugador 2
        GameObject player2TextGO = new GameObject("Player2ResourcesText");
        player2TextGO.transform.parent = canvasGO.transform;

        player2ResourcesText = player2TextGO.AddComponent<Text>();
        player2ResourcesText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        player2ResourcesText.fontSize = 40;
        player2ResourcesText.color = Color.white;
        player2ResourcesText.alignment = TextAnchor.MiddleCenter;

        // Posicionar el texto del jugador 2 en la parte superior central de la pantalla (debajo del texto del jugador 1)
        RectTransform player2RectTransform = player2ResourcesText.GetComponent<RectTransform>();
        player2RectTransform.sizeDelta = new Vector2(400, 50);
        player2RectTransform.anchorMin = new Vector2(0.5f, 1f);
        player2RectTransform.anchorMax = new Vector2(0.5f, 1f);
        player2RectTransform.pivot = new Vector2(0.5f, 1f);
        player2RectTransform.anchoredPosition = new Vector2(675, -825);  // Ajustar la posición

        // Inicializar el texto para mostrar los recursos
        UpdateResourceUI();
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        resourceTimer += Time.deltaTime;

        if (resourceTimer >= resourceInterval)
        {
            if (tiempoTranscurrido < 60)
            {
                player1Gold += 9;
                player1Metal += 34;
                player2Gold += 9;
                player2Metal += 34;
            }
            else if (tiempoTranscurrido >= 60 && tiempoTranscurrido < 120)
            {
                player1Gold += 18;
                player1Metal += 68;
                player2Gold += 18;
                player2Metal += 68;
            }
            else if (tiempoTranscurrido >= 120 && tiempoTranscurrido < 180)
            {
                player1Gold += 27;
                player1Metal += 102;
                player2Gold += 27;
                player2Metal += 102;
            }
            else if (tiempoTranscurrido >= 180)
            {
                player1Gold += 27;
                player1Metal += 102;
                player2Gold += 27;
                player2Metal += 102;
            }

            UpdateResourceUI();
            resourceTimer = 0f;
        }
    }


    void UpdateResourceUI()
    {
        player1ResourcesText.text = $"{player1Gold}                 {player1Metal}";
        player2ResourcesText.text = $"{player2Gold}                  {player2Metal}";
    }

    public bool CanAfford(int goldCost, int metalCost, int player)
    {
        if (player == 1)
        {
            return player1Gold >= goldCost && player1Metal >= metalCost;
        }
        else if (player == 2)
        {
            return player2Gold >= goldCost && player2Metal >= metalCost;
        }
        return false;
    }

    public void SpendResources(int goldCost, int metalCost, int player)
    {
        if (player == 1)
        {
            player1Gold -= goldCost;
            player1Metal -= metalCost;
        }
        else if (player == 2)
        {
            player2Gold -= goldCost;
            player2Metal -= metalCost;
        }
        UpdateResourceUI();
    }
}