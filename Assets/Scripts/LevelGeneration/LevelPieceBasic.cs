using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPieceBasic : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform exitPoint;

    public Transform StartPoint => startPoint;
    public Transform ExitPoint => exitPoint;
}
