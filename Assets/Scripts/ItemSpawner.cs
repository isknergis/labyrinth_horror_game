using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public GameObject batteryPrefab;
    public GameObject keyPrefab;


    public int numberOfHealthBars = 5;
    public int numberOfBatteries = 5;
    public int numberOfKeys = 3;

    public GameObject plane; 
    private Vector3 planeSize;

    public GameObject healthBarHeightReference;
    public GameObject batteryHeightReference;
    public GameObject keyHeightReference;


    public LayerMask Ground; 
    public LayerMask wallLayer; 

    void Start()
    {
       
        planeSize = plane.GetComponent<Renderer>().bounds.size;
        SpawnItems(healthBarPrefab, numberOfHealthBars, healthBarHeightReference);
        SpawnItems(batteryPrefab, numberOfBatteries, batteryHeightReference);
        SpawnItems(keyPrefab, numberOfKeys, keyHeightReference);
    }

    void SpawnItems(GameObject itemPrefab, int itemCount, GameObject heightReference)
    {
        for (int i = 0; i < itemCount; i++)
        {
            Vector3 randomPosition;
            do
            {
                randomPosition = GetRandomPositionOnPlane(heightReference);
            } while (!IsValidPosition(randomPosition));

            Instantiate(itemPrefab, randomPosition, Quaternion.identity);
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
        // Zemini kontrol et
        if (Physics.Raycast(position + Vector3.up * 10, Vector3.down, out RaycastHit hit, Mathf.Infinity, Ground))
        {
            // Duvar olup olmadýðýný kontrol et
            if (!Physics.Raycast(position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, wallLayer))
            {
                return true;
            }
        }
        return false;
    }
}
