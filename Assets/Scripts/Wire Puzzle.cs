using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WirePuzzle : MonoBehaviour
{
    public GameObject tilePrefab;
    /*
    public int width = 4
    public int height = 4
    */

    private GameObject[] tiles = new GameObject[16];
    private bool solved = false;
    private WireTile tileScript;

    public GameObject leftView;
    public GameObject rightView;
    public GameObject testIndicatorL;
    public GameObject testIndicatorR;
    public GameObject numberToActivate1;
    public GameObject numberToActivate2;

    // Start is called before the first frame update
    void Start() {
        System.Random rnd = new System.Random();
        int randAngle;
        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 4; c++) {
                if ((r+c) % 3 == 0) {
                    randAngle = rnd.Next(2)*180 + 90;
                }
                else {
                    randAngle = rnd.Next(4)*90;
                }

                if ((r+c) % 2 == 0) {
                    tiles[4*r+c] = Instantiate(tilePrefab, transform.position + new Vector3(c-1.5f-4.5f, 1.5f-r, 0), transform.rotation * Quaternion.Euler(0, 0, randAngle), leftView.transform);
                }
                else {
                    tiles[4*r+c] = Instantiate(tilePrefab, transform.position + new Vector3(c-1.5f+4.5f, 1.5f-r, 0), transform.rotation * Quaternion.Euler(0, 0, randAngle), rightView.transform);
                }
            }
        }

        // Hardcoding a wire path for now...
        tileScript = tiles[0].GetComponent<WireTile>();
        tileScript.rightWire = true;
        tileScript.downWire = true;

        tileScript = tiles[1].GetComponent<WireTile>();
        tileScript.leftWire = true;
        tileScript.downWire = true;

        tileScript = tiles[2].GetComponent<WireTile>();
        tileScript.rightWire = true;
        tileScript.upWire = true;

        tileScript = tiles[3].GetComponent<WireTile>();
        tileScript.leftWire = true;
        tileScript.downWire = true;

        tileScript = tiles[4].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.downWire = true;

        tileScript = tiles[5].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.downWire = true;
        
        tileScript = tiles[6].GetComponent<WireTile>();
        tileScript.rightWire = true;
        tileScript.downWire = true;

        tileScript = tiles[7].GetComponent<WireTile>();
        tileScript.leftWire = true;
        tileScript.upWire = true;

        tileScript = tiles[8].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.downWire = true;

        tileScript = tiles[9].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.downWire = true;

        tileScript = tiles[10].GetComponent<WireTile>();
        tileScript.rightWire = true;
        tileScript.upWire = true;

        tileScript = tiles[11].GetComponent<WireTile>();
        tileScript.leftWire = true;
        tileScript.downWire = true;

        tileScript = tiles[12].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.downWire = true;

        tileScript = tiles[13].GetComponent<WireTile>();
        tileScript.upWire = true;
        tileScript.rightWire = true;
        
        tileScript = tiles[14].GetComponent<WireTile>();
        tileScript.rightWire = true;
        tileScript.leftWire = true;

        tileScript = tiles[15].GetComponent<WireTile>();
        tileScript.leftWire = true;
        tileScript.upWire = true;
    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the puzzle is solved every frame
        // (We should find a way to only check when the puzzle is clicked on.)
        solved = tiles[0].transform.rotation.eulerAngles.z < 1;
        for (int i = 1; i < tiles.Length; i++) {
            solved = solved && tiles[i].transform.rotation.eulerAngles.z < 1;
        }

        testIndicatorL.SetActive(solved);
        testIndicatorR.SetActive(solved);
        if (solved)
        {
            numberToActivate1.SetActive(true);
            numberToActivate2.SetActive(true);
        }
    }
}
