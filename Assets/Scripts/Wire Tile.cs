using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WireTile : MonoBehaviour
{
    // These variables control what wires are created at the start of runtime
    public bool[] rightWires = new bool[3];
    public bool[] upWires = new bool[3];
    public bool[] leftWires = new bool[3];
    public bool[] downWires = new bool[3];

    public GameObject wirePrefab;
    public GameObject wirePrefab1;
    public GameObject wirePrefab2;
    private GameObject[] prefabList;

    public float wireThickness = 0.05f;

    private bool oneWireType;
    private bool Line;
    private bool XO;


    // Start is called before the first frame update
    void Start()
    {
        prefabList = new GameObject[] {wirePrefab, wirePrefab1, wirePrefab2};

        // This is true if there's only one wire type on the tile
        oneWireType = !rightWires[1] && !rightWires[2] && !upWires[1] && !upWires[2] && !leftWires[1] && !leftWires[2] && !downWires[1] && !downWires[2];

        // These variables are set to true if there are two wires that make a straight line vertically or horizontally, and only one type of wire
        Line = (leftWires[0] == rightWires[0]) && oneWireType;
        XO = ((rightWires[0] == upWires[0]) && (upWires[0] == leftWires[0]) && (leftWires[0] == downWires[0])) && oneWireType;

        float[] wireLengths = new float[] {0.5f + wireThickness/2 - 2*wireThickness, 0.5f + wireThickness/2, 0.5f + wireThickness/2 + 2*wireThickness};
        float[] relWireShifts = new float[] {0.5f - (wireLengths[0])/2, 0.5f - (wireLengths[1])/2, 0.5f - (wireLengths[2])/2};
        int[] helper = new int[] {0, 1, -1};
        GameObject wire;

        // This chain of blocks creates the wires for each tile.
        for (int r = 0; r < 3; r++) {
            if (rightWires[r]) {
                wire = Instantiate(prefabList[r], transform.position + (Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * new Vector3(relWireShifts[helper[r]+1]*this.transform.localScale.x, helper[r]*2*wireThickness, -1)), transform.rotation, this.transform);
                wire.transform.localScale = new Vector3(wireLengths[helper[r]+1], wireThickness, 1);
            }
        }
        for (int u = 0; u < 3; u++) {
            if (upWires[u]) {
                wire = Instantiate(prefabList[u], transform.position + (Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * new Vector3(-helper[u]*2*wireThickness, relWireShifts[1-helper[u]]*this.transform.localScale.y, -1)), transform.rotation * Quaternion.Euler(0, 0, 90), this.transform);
                wire.transform.localScale = new Vector3(wireLengths[1-helper[u]], wireThickness, 1);
            }
        }
        for (int l = 0; l < 3; l++) {
            if (leftWires[l]) {
                wire = Instantiate(prefabList[l], transform.position + (Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * new Vector3(-relWireShifts[1-helper[l]]*this.transform.localScale.x, helper[l]*2*wireThickness, -1)), transform.rotation * Quaternion.Euler(0, 0, 180), this.transform);
                wire.transform.localScale = new Vector3(wireLengths[1-helper[l]], wireThickness, 1);
            }
        }
        for (int d = 0; d < 3; d++) {    
            if (downWires[d]) {
                wire = Instantiate(prefabList[d], transform.position + (Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * new Vector3(-helper[d]*2*wireThickness, -relWireShifts[helper[d]+1]*this.transform.localScale.y, -1)), transform.rotation * Quaternion.Euler(0, 0, 270), this.transform);
                wire.transform.localScale = new Vector3(wireLengths[helper[d]+1], wireThickness, 1);
            }
        }

        updateTileRotation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseDown is called when a tile is clicked
    void OnMouseDown()
    {
        transform.Rotate(0, 0, 90);
        updateTileRotation();
    }

    void updateTileRotation()
    {
        // If multiple orientations of a tile can solve the puzzle, this function makes sure that all of them register as the 0 rotation.
        if (Line && transform.rotation.eulerAngles.z % 180 == 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (XO && transform.rotation.eulerAngles.z % 90 == 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
