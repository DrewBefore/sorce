using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {
    [SerializeField] private Transform ground;
    [SerializeField] private Transform lava;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    private float hexWidth;
    private float hexHeight;
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start() {
        hexWidth = ground.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * ground.lossyScale.x + .05f;
        hexHeight = ground.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * ground.lossyScale.z + .05f;
        CalcStartPos();
        createGrid();
    }

    private void CalcStartPos() {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0) {
            offset = hexWidth / 2;
        }    
        float x = -hexWidth * (gridWidth * .75f) - offset;
        float z = hexHeight * (gridHeight / 4);
        startPos = new Vector3(x, 0, z);
    }

    Vector3 calcWorldPos(Vector2 gridPos) {
        float offset = 0;
        if (gridPos.y % 2 != 0) {
            offset = hexWidth * .75f;
        }
        float x = startPos.x + gridPos.x * hexWidth * 1.5f + offset ;
        float z = startPos.z - gridPos.y * hexHeight / 2;
        return new Vector3(x, 0, z);
    }

    private void createGrid() {
        for (int y = 0; y < gridHeight; y++) {
            for (int x = 0; x < gridWidth; x++) {
                Transform hex = Instantiate(ground) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = calcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + " | " + y;
            }
        }
    }
}
