using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public GameObject batteryPrefab;
    public int numberOfHealthBars = 5;
    public int numberOfBatteries = 5;

    public GameObject plane; // Plane nesnesini buraya s�r�kleyip b�rakacaks�n�z
    private Vector3 planeSize; // Plane'in boyutlar�n� saklamak i�in

    public GameObject healthBarHeightReference; // HealthBar y�ksekli�i referans�
    public GameObject batteryHeightReference; // Battery y�ksekli�i referans�

    public LayerMask Ground; // Zemin katman�n� belirleyin
    public LayerMask wallLayer; // Duvar katman�n� belirleyin

    void Start()
    {
        // Plane nesnesinin boyutlar�n� al
        planeSize = plane.GetComponent<Renderer>().bounds.size;
        SpawnItems(healthBarPrefab, numberOfHealthBars, healthBarHeightReference);
        SpawnItems(batteryPrefab, numberOfBatteries, batteryHeightReference);
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
            // Duvar olup olmad���n� kontrol et
            if (!Physics.Raycast(position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, wallLayer))
            {
                return true;
            }
        }
        return false;
    }
}
