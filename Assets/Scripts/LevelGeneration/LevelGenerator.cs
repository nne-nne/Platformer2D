using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    [SerializeField] Transform startPoint;
    [SerializeField] List<LevelPieceBasic> levelPrefabs = new List<LevelPieceBasic>();
    [SerializeField] LevelPieceBasic startPlatformPiece;

    private List<LevelPieceBasic> piecesOnScene = new List<LevelPieceBasic>();



    /// <summary>
    /// Dodaje losowy segment
    /// </summary>
    public void AddPiece()
    {
        if (levelPrefabs.Count == 0)
        {
            Debug.LogError("No level prefabs");
            return;
        }

        int randomIndex = Random.Range(0, levelPrefabs.Count); // max exclusive
        AddPiece(levelPrefabs[randomIndex]);
    }

    public void RemoveOldestPiece()
    {
        LevelPieceBasic oldestPiece = piecesOnScene[0];

        piecesOnScene.Remove(oldestPiece);
        Destroy(oldestPiece.gameObject);
    }

    /// <summary>
    /// Dodaje zadany segment
    /// </summary>
    /// <param name="piecePrefab">Prefab</param>
    private void AddPiece(LevelPieceBasic piecePrefab)
    {
        LevelPieceBasic piece = Instantiate(piecePrefab);
        piece.transform.SetParent(transform, false);
        PlacePieceOnEnd(piece);

        piecesOnScene.Add(piece);
    }

    private void PlacePieceOnEnd(LevelPieceBasic newPiece)
    {
        Vector3 snapPoint;
        if(piecesOnScene.Count == 0)
        {
            snapPoint = startPoint.position;
        }
        else
        {
            LevelPieceBasic lastPiece = piecesOnScene[piecesOnScene.Count - 1];
            snapPoint = lastPiece.ExitPoint.position;
        }

        Vector3 newPieceStartOffset = newPiece.StartPoint.position - newPiece.transform.position;
        newPiece.transform.position = snapPoint - newPieceStartOffset;
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        AddPiece(startPlatformPiece);
        AddPiece();
        AddPiece();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
