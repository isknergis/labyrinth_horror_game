using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerTwoScene : MonoBehaviour
{

    public GameObject healthBarPrefab;
    public GameObject batteryPrefab;
    public int numberOfHealthBars = 10;
    public int numberOfBatteries = 10;
    public GameObject plane;
    private Vector3 planeSize;
    public GameObject healthBarHeightReference;
    public GameObject batteryHeightReference;
    public LayerMask Ground;

    void Start()
    {
        Debug.Log("Start method called");

        if (plane == null)
        {
            Debug.LogError("Plane is not assigned!");
            return;
        }

        // Plane bir Terrain bileþeni içeriyorsa
        Terrain terrain = plane.GetComponent<Terrain>();
        if (terrain != null)
        {
            planeSize = terrain.terrainData.size;
            Debug.Log("Plane size from Terrain: " + planeSize);
        }
        else
        {
            MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();
            if (planeRenderer != null)
            {
                planeSize = planeRenderer.bounds.size;
                Debug.Log("Plane size from MeshRenderer: " + planeSize);
            }
            else
            {
                Debug.LogError("Plane does not have a MeshRenderer or Terrain component!");
                return;
            }
        }

        SpawnItems(healthBarPrefab, numberOfHealthBars, healthBarHeightReference);
        SpawnItems(batteryPrefab, numberOfBatteries, batteryHeightReference);
    }

    void SpawnItems(GameObject itemPrefab, int itemCount, GameObject heightReference)
    {
        Debug.Log("SpawnItems called for: " + itemPrefab.name);

        for (int i = 0; i < itemCount; i++)
        {
            Vector3 randomPosition;
            int maxAttempts = 100; // Pozisyon bulma deneme sayýsý sýnýrý
            int attempts = 0;
            do
            {
                randomPosition = GetRandomPositionOnPlane(heightReference);
                attempts++;
            } while (!IsValidPosition(randomPosition) && attempts < maxAttempts);

            if (attempts < maxAttempts)
            {
                Debug.Log("Spawning item at position: " + randomPosition);
                Instantiate(itemPrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Valid position not found for item after maximum attempts.");
            }
        }
    }

    Vector3 GetRandomPositionOnPlane(GameObject heightReference)
    {
        float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
        float y = heightReference.transform.position.y;
        return new Vector3(x, y, z);
    }

    bool IsValidPosition(Vector3 position)
    {
        Debug.Log("Checking position: " + position);

        // Zemini kontrol et
        if (Physics.Raycast(position + Vector3.up * 10, Vector3.down, out RaycastHit hit, Mathf.Infinity, Ground))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " at position: " + hit.point);
            // Eðer zemin ile temas ediyorsa pozisyon geçerli
            if (hit.collider != null && hit.collider.gameObject == plane)
            {
                Debug.Log("Valid position found at: " + position);
                return true;
            }
        }
        Debug.Log("Invalid position at: " + position);
        return false;
    }

}
