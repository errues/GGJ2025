using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour {
    [SerializeField] private Vector2 randomAppearTimeLimits;

    private List<GarbagePiece> inactiveGarbagePieces;
    private List<GarbagePiece> activeGarbagePieces;

    private float nextGarbageAppearTime;

    private void Awake() {
        inactiveGarbagePieces = GetComponentsInChildren<GarbagePiece>().ToList();
        activeGarbagePieces = new List<GarbagePiece>();

        nextGarbageAppearTime = Random.Range(randomAppearTimeLimits.x, randomAppearTimeLimits.y);
    }

    private void Update() {
        if (Time.time > nextGarbageAppearTime) {
            nextGarbageAppearTime = Time.time + Random.Range(randomAppearTimeLimits.x, randomAppearTimeLimits.y);
            ActivateRandomGarbagePiece();
        }
    }

    private void ActivateRandomGarbagePiece() {
        if (inactiveGarbagePieces.Count > 0) {
            GarbagePiece piece = inactiveGarbagePieces[Random.Range(0, inactiveGarbagePieces.Count)];
            inactiveGarbagePieces.Remove(piece);
            activeGarbagePieces.Add(piece);
            piece.Appear();
        }
    }

    public void SetGarbagePieceInactive(GarbagePiece piece) {
        if (activeGarbagePieces.Remove(piece)) {
            inactiveGarbagePieces.Add(piece);
        }
    }
}
