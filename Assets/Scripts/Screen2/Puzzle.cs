using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> listPuzzlePieces;
    [SerializeField] private Transform player;
    [SerializeField] private Inventory playerInventory; // Envanter referansý
    public float activationDistance = 3.0f;

    private void Update()
    {
        CheckForPieceCollection();
    }

    private void CheckForPieceCollection()
    {
        foreach (GameObject piece in listPuzzlePieces)
        {
            if (piece.activeSelf && Vector3.Distance(player.position, piece.transform.position) <= activationDistance)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    CollectPiece(piece);
                }
            }
        }
    }

    private void CollectPiece(GameObject piece)
    {
        piece.SetActive(false);
        playerInventory.AddPiece(piece);
        Debug.Log(piece.name + " collected and added to inventory!");
    }
}
