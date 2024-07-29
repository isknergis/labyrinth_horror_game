using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{

    [SerializeField] private GameObject keyPrefab;

    public int keyNumber=1;
    private Vector3 planeSize;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject keyHeightReference;

    public LayerMask Ground;

    private void Start()
    {

        planeSize = plane.GetComponent<Renderer>().bounds.size;
        SpawnItems(keyPrefab, keyNumber, keyHeightReference);
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
            if (!Physics.Raycast(position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity))
            {
                return true;
            }
        }
        return false;
    }

}
