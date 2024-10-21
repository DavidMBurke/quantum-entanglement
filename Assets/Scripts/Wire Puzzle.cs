using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WirePuzzle : MonoBehaviour
{
    private int puzzleSize = 4;  // Assumes the puzzle is a square grid

    public GameObject tilePrefab;
    public GameObject wirePrefabA;
    public GameObject wirePrefabB;
    public GameObject wirePrefabC;
    public float wireThickness = 0.05f;
    private GameObject[] tiles;
    private bool solved = false;
    private WireTile tileScript;
    private int[] wirePath;

    public GameObject leftView;
    public GameObject rightView;
    public GameObject testIndicatorL;
    public GameObject testIndicatorR;

    private int RIGHT;
    private int UP;
    private int LEFT;
    private int DOWN;
    private int nullInt;

    private System.Random rnd = new System.Random();

    public GameObject numberToActivate1;
    public GameObject numberToActivate2;
    public AudioSource finishAudio;

    // Start is called before the first frame update
    void Start() {
        tiles = new GameObject[puzzleSize*puzzleSize];
        wirePath = new int[puzzleSize*puzzleSize*2 + 1];

        RIGHT = 1;
        UP = -1*puzzleSize;
        LEFT = -1;
        DOWN = puzzleSize;
        nullInt = -1*puzzleSize - 1;

        Vector3 relTileShift = new Vector3(0, 2, 0);

        // Instantiate tiles and set rotations
        int randAngle = 0;
        for (int r = 0; r < puzzleSize; r++) {
            for (int c = 0; c < puzzleSize; c++) {

                if ((r+c) % 3 == 0) {
                    randAngle = rnd.Next(2)*180 + 90;
                }
                else {
                    randAngle = rnd.Next(4)*90;
                }

                if ((r+c) % 2 == 0) {
                    tiles[puzzleSize*r+c] = Instantiate(tilePrefab, leftView.transform.position + relTileShift + new Vector3(c-1.5f, 1.5f-r, 0), leftView.transform.rotation * Quaternion.Euler(0, 0, randAngle), leftView.transform);
                }
                else {
                    tiles[puzzleSize*r+c] = Instantiate(tilePrefab, rightView.transform.position + relTileShift + new Vector3(c-1.5f, 1.5f-r, 0), rightView.transform.rotation * Quaternion.Euler(0, 0, randAngle), rightView.transform);
                }
                tileScript = tiles[puzzleSize*r+c].GetComponent<WireTile>();
                tileScript.wirePrefab = wirePrefabA;
                if (wirePrefabB != null) {
                    tileScript.wirePrefab1 = wirePrefabB;
                }
                if (wirePrefabC != null) {
                    tileScript.wirePrefab2 = wirePrefabC;
                }
                tileScript.wireThickness = wireThickness;
            }
        }


        // Debug.Log("Test 1: " + isAdjacent(new int[] {1, 2, 3, 4, 5, 6, 2, 4}, 2, 5));
        // Debug.Log("Test 2: " + isAdjacent(new int[] {1, 2, 3, 4, 5, 6, 2, 4}, 2, 3));
        // Debug.Log("Test 3: " + isAdjacent(new int[] {1, 2, 3, 4, 5, 6, 2, 4}, 2, 6));

        // Generate random path(s) and set wires for each wire prefab
        GameObject[] prefabList = new GameObject[] {wirePrefabA, wirePrefabB, wirePrefabC};
        for (int p = 0; p < 3; p++) {
            if (prefabList[p] != null) {

                bool validPath = writeRandomPath(wirePath);
                while (!validPath) {
                    validPath = writeRandomPath(wirePath);
                }

                /*
                string pathText = wirePath[0].ToString();
                for (int l = 1; l < wirePath.Length; l++) {
                    pathText = pathText + ", " + wirePath[l].ToString();
                }

                Debug.Log(pathText);
                */

                float[] wireLengths = new float[] {0.5f + wireThickness/2 - 2*wireThickness, 0.5f + wireThickness/2, 0.5f + wireThickness/2 + 2*wireThickness};
                float[] relWireShifts = new float[] {0.5f - (wireLengths[0])/2, 0.5f - (wireLengths[1])/2, 0.5f - (wireLengths[2])/2};
                int[] helper = new int[] {0, 1, -1};
                GameObject wire;

                int startCol = wirePath[0] % puzzleSize;
                Vector3 startPos = relTileShift + new Vector3(startCol-1.5f, 1.5f-4, 0) + new Vector3(-helper[p]*2*wireThickness, relWireShifts[1-helper[p]]*this.transform.localScale.y, -1);
                if ((3+startCol) % 2 == 0) {
                    wire = Instantiate(prefabList[p], leftView.transform.position + startPos, leftView.transform.rotation * Quaternion.Euler(0, 0, 90), leftView.transform);
                    wire.transform.localScale = new Vector3(wireLengths[1-helper[p]], wireThickness, 1);
                }
                else {
                    wire = Instantiate(prefabList[p], rightView.transform.position + startPos, rightView.transform.rotation * Quaternion.Euler(0, 0, 90), rightView.transform);
                    wire.transform.localScale = new Vector3(wireLengths[1-helper[p]], wireThickness, 1);
                }

                // endCol is the array index of the last tile location in the path
                int endCol = Array.IndexOf(wirePath, -5);
                if (endCol == -1) {
                    endCol = wirePath.Length - 1;
                }
                else {
                    endCol = endCol - 1;
                }
                endCol = wirePath[endCol] + puzzleSize; // endCol is now the column of the last location (a negative number between -puzzleSize and -1)
                Vector3 endPos = relTileShift + new Vector3(endCol-1.5f, 1.5f+1, 0) + new Vector3(-helper[p]*2*wireThickness, -relWireShifts[helper[p]+1]*this.transform.localScale.y, -1);
                if (endCol % 2 == 0) {
                    wire = Instantiate(prefabList[p], leftView.transform.position + endPos, leftView.transform.rotation * Quaternion.Euler(0, 0, 270), leftView.transform);
                    wire.transform.localScale = new Vector3(wireLengths[helper[p]+1], wireThickness, 1);
                }
                else {
                    wire = Instantiate(prefabList[p], rightView.transform.position + endPos, rightView.transform.rotation * Quaternion.Euler(0, 0, 270), rightView.transform);
                    wire.transform.localScale = new Vector3(wireLengths[helper[p]+1], wireThickness, 1);
                }


                setWires(wirePath, p);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the puzzle is solved every frame
        // (We should find a way to only check when the puzzle is clicked on.)
        bool currSolved = tiles[0].transform.rotation.eulerAngles.z < 1;
        for (int i = 1; i < tiles.Length; i++) {
            currSolved = currSolved && tiles[i].transform.rotation.eulerAngles.z < 1;
        }

        testIndicatorL.SetActive(currSolved);
        testIndicatorR.SetActive(currSolved);
        if (!solved && currSolved)
        {
            numberToActivate1.SetActive(true);
            numberToActivate2.SetActive(true);
            finishAudio.Play();
        }
        solved = currSolved;
    }

    int locPlusDir(int location, int direction)
    {
        // This function takes a location and calculates the location one step in the given direction, or returns the null integer if this
        // location isn't valid

        if (location % puzzleSize == 0 && direction == LEFT) {
            // If the location is on the left edge of the grid and the direction is left, return nullInt
            return nullInt;
        }

        else if ((location+1) % puzzleSize == 0 && direction == RIGHT) {
            // If the location is on the right edge of the grid and the direction is right, return nullInt
            return nullInt;
        }

        else if ((puzzleSize*puzzleSize - puzzleSize <= location) && (location < puzzleSize*puzzleSize) && (direction == DOWN)) {
            // If the location is on the bottom edge of the grid and the direction is down, return nullInt
            return nullInt;
        }

        else {
            return location + direction;
        }
    }

    bool isAdjacent(int[] path, int loc1, int loc2)
    {
        // This returns True if the two given locations appear adjacent in the given array, and False otherwise
        for (int i = 0; i < path.Length-1; i++) {
            if ((path[i] == loc1 && path[i+1] == loc2) || (path[i] == loc2 && path[i+1] == loc1)) {
                return true;
            }
        }

        return false;
    }

    bool writeRandomPath(int[] path) {
        // This generates a random path from the bottom of the puzzle grid to the top.
        // It writes the path inside of the given path array, starting with filling the array with nullInts.
        // It returns True if the path successfully reaches the top of the grid, and False otherwise.

        for (int i = 0; i < path.Length; i++) {
            path[i] = nullInt;
        }

        path[0] = rnd.Next(puzzleSize*puzzleSize, puzzleSize*(puzzleSize+1));
        // Debug.Log("Selected first location: " + path[0]);
        path[1] = locPlusDir(path[0], UP);
        // Debug.Log("Selected next location: " + path[1]);

        int[] newLocs = new int[4];
        bool[] possibleDirs = new bool[4];
        for (int j = 2; j < path.Length; j++) {
            // To append a new location to the end of the path, find all of the possible directions to move from the last location, then choose
            // one at random and enter the resulting location into the array.

            newLocs[0] = locPlusDir(path[j-1], RIGHT);
            newLocs[1] = locPlusDir(path[j-1], UP);
            newLocs[2] = locPlusDir(path[j-1], LEFT);
            newLocs[3] = locPlusDir(path[j-1], DOWN);
            
            for (int k = 0; k < 4; k++) {
                possibleDirs[k] = newLocs[k] != -1 && !(isAdjacent(path, newLocs[k], path[j-1]));
            }

            if (Array.TrueForAll(possibleDirs, d => !d)) {
                return false;
            }

            int randK = (int) (rnd.Next(7)/2 + 0.5f);
            // Debug.Log("RNG: " + randK);
            while (!possibleDirs[randK]) {
                randK = (int) (rnd.Next(7)/2 + 0.5f);
                // Debug.Log("RNG: " + randK);
            }

            path[j] = newLocs[randK];
            if (path[j] < 0 && path[j] >= -1*puzzleSize) {
                return true;
            }
        }

        return false;
    }

    void setWires(int[] path, int wireIndex)
    {
        // This sets the wire bool variables of each tile to match the generated path.
        for (int i = 1; i < path.Length; i++) {
            if (path[i] < 0) {
                break;
            }

            tileScript = tiles[path[i]].GetComponent<WireTile>();
            int deltaPri = path[i-1] - path[i];  // delta from current location to last location
            int deltaPost = path[i+1] - path[i]; // delta from current location to next location
            Debug.Assert(deltaPri != deltaPost);

            if (deltaPri == RIGHT || deltaPost == RIGHT) {
                tileScript.rightWires[wireIndex] = true;
            }
            if (deltaPri == UP || deltaPost == UP) {
                tileScript.upWires[wireIndex] = true;
            }
            if (deltaPri == LEFT || deltaPost == LEFT) {
                tileScript.leftWires[wireIndex] = true;
            }
            if (deltaPri == DOWN || deltaPost == DOWN) {
                tileScript.downWires[wireIndex] = true;
            }
        }
    }
}
