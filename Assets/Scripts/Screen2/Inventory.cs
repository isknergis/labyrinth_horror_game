using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    private List<GameObject> collectedPieces = new List<GameObject>();

    public int totalPiecesCount = 7; 
    public static bool AllPiecesCollected = false; 

    public void AddPiece(GameObject piece)
    {
        collectedPieces.Add(piece);
        Debug.Log(piece.name + " added to inventory!");
        CheckAllPiecesCollected(); 
    }

    public bool HasPiece(GameObject piece)
    {
        return collectedPieces.Contains(piece);
    }

    public List<GameObject> GetCollectedPieces()
    {
        return collectedPieces;
    }

    private void CheckAllPiecesCollected()
    {
        if (collectedPieces.Count >= totalPiecesCount)
        {
            AllPiecesCollected = true; 
            Debug.Log("All pieces collected.");
        }
        else
        {
            AllPiecesCollected = false; 
        }
    }
}
