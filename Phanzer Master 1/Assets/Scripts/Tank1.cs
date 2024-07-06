using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] player1TankPrefabs;
    [SerializeField] private GameObject[] player2TankPrefabs;
    [SerializeField] private Transform[] base1SpawnPoints;
    [SerializeField] private Transform[] base2SpawnPoints;
    [SerializeField] private ResourceManager resourceManager; // Añadir referencia al ResourceManager

    private int lastBase1SpawnPoint = -1;
    private int lastBase2SpawnPoint = -1;

    void Update()
    {
        // Controles para base 1                                                                                                  tankName,          health, range, damage, speed, faction
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SpawnTank(GetRandomSpawnPoint(base1SpawnPoints, ref lastBase1SpawnPoint), 0, player1TankPrefabs, "Tank1", 100, 5, 20, 2, "Base1", 19, 80); } //Ofensivo
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SpawnTank(GetRandomSpawnPoint(base1SpawnPoints, ref lastBase1SpawnPoint), 1, player1TankPrefabs, "Tank2", 300, 4, 8, 1, "Base1", 30, 140); } //Defensivo
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SpawnTank(GetRandomSpawnPoint(base1SpawnPoints, ref lastBase1SpawnPoint), 2, player1TankPrefabs, "Tank3", 150, 6, 17, 1.5f, "Base1", 18, 110); } //Normal
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SpawnTank(GetRandomSpawnPoint(base1SpawnPoints, ref lastBase1SpawnPoint), 3, player1TankPrefabs, "Tank4", 45, 5, 30, 2.5f, "Base1", 19, 55); } //Soporte
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SpawnTank(GetRandomSpawnPoint(base1SpawnPoints, ref lastBase1SpawnPoint), 4, player1TankPrefabs, "Tank5", 2, 8, 100, 2, "Base1", 55, 140); } //Especial

        // Controles para base 2
        if (Input.GetKeyDown(KeyCode.G)) { SpawnTank(GetRandomSpawnPoint(base2SpawnPoints, ref lastBase2SpawnPoint), 0, player2TankPrefabs, "EnemyTank1", 95, 5, 22, 2, "Base2", 17, 90); } //Ofensivo
        if (Input.GetKeyDown(KeyCode.H)) { SpawnTank(GetRandomSpawnPoint(base2SpawnPoints, ref lastBase2SpawnPoint), 1, player2TankPrefabs, "EnemyTank2", 300, 4, 7, 1, "Base2", 32, 135); } //Defensivo
        if (Input.GetKeyDown(KeyCode.J)) { SpawnTank(GetRandomSpawnPoint(base2SpawnPoints, ref lastBase2SpawnPoint), 2, player2TankPrefabs, "EnemyTank3", 160, 6, 15, 1.5f, "Base2", 20, 100); } //Normal
        if (Input.GetKeyDown(KeyCode.K)) { SpawnTank(GetRandomSpawnPoint(base2SpawnPoints, ref lastBase2SpawnPoint), 3, player2TankPrefabs, "EnemyTank4", 60, 5, 15, 2.5f, "Base2", 18, 60); } //Soporte
        if (Input.GetKeyDown(KeyCode.L)) { SpawnTank(GetRandomSpawnPoint(base2SpawnPoints, ref lastBase2SpawnPoint), 4, player2TankPrefabs, "EnemyTank5", 2, 8, 100, 2, "Base2", 52, 145); } //Especial
    }

    Transform GetRandomSpawnPoint(Transform[] spawnPoints, ref int lastSpawnPoint)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
        } while (randomIndex == lastSpawnPoint);
        lastSpawnPoint = randomIndex;
        return spawnPoints[randomIndex];
    }

    void SpawnTank(Transform spawnPoint, int tankIndex, GameObject[] tankPrefabs, string tankName, float health, float range, float damage, float speed, string faction, int goldCost, int metalCost)
    {
        if (tankIndex < 0 || tankIndex >= tankPrefabs.Length)
        {
            Debug.LogError("Tank index is out of range.");
            return;
        }

        int player = faction == "Base1" ? 1 : 2;

        if (resourceManager.CanAfford(goldCost, metalCost, player))
        {
            GameObject newTank = Instantiate(tankPrefabs[tankIndex], spawnPoint.position, spawnPoint.rotation);
            Tank tankComponent = newTank.GetComponent<Tank>();
            tankComponent.Initialize(tankName, health, range, damage, speed, faction, goldCost, metalCost);
            resourceManager.SpendResources(goldCost, metalCost, player);
        }
        else
        {
            Debug.Log("No tienes suficientes recursos para desplegar este tanque.");
        }
    }
}
