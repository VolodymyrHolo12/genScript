using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TileMapGen : MonoBehaviour
{
    [Range (0,100)]
    public int iniChance;

    [Range(1, 8)] 
    public int birthLimit;
    
    [Range(1, 8)] 
    public int deathLimit;

    [Range(1, 10)] 
    public int numR;
    private int count = 0;

    private int[,] terrainMap;

    public Vector3Int tmapSize;

    public Tilemap topMap;
    public Tilemap botMap;
    
    [SerializeField]
    private Tile topTile;
    [SerializeField]
    private Tile botTile;
    [SerializeField]
    private Tile blockTile;
    [SerializeField]
    private Tile beachTile;
    [SerializeField]
    private Tile rightBorderTile;
    [SerializeField]
    private Tile leftBorderTile;
    [SerializeField]
    private Tile topBorderTile;
    [SerializeField]
    private Tile botBorderTile;
    [SerializeField]
    private Tile cornerBotRight;
    [SerializeField]
    private Tile cornerBotLeft;
    [SerializeField]
    private Tile cornerTopLeft;
    [SerializeField]
    private Tile cornerTopRight;
    
    private int width;
    private int height;
    // Update is called once per frame
    public void doSim(int numR)
    {
        clearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < numR; i++)
        {
            terrainMap = genTilePos(terrainMap);
            
        }

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if ((x == 0 || x == width - 1) || (y==0 || y==height-1)) {
                    terrainMap[x, y] = 0;
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), blockTile);
                }
                else {
                    if (terrainMap[x, y] == 1) {
                        topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                        if ((x + 1 < width && x - 1 >= 0) && (y + 1 < height && y - 1 >= 0)) {
                            if (((terrainMap[x + 1, y] == 0 && terrainMap[x, y] == 1) ||
                                 (terrainMap[x - 1, y] == 0 && terrainMap[x, y] == 1)) ||
                                ((terrainMap[x, y - 1] == 0 && terrainMap[x, y] == 1) ||
                                 (terrainMap[x, y + 1] == 0 && terrainMap[x, y] == 1))) {
                                topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), beachTile);
                            }
                        }
                    }
                    else
                    {
                        if ((x + 1 < width && x - 1 >= 0) && (y + 1 < height && y - 1 >= 0)) {
                            botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                            if (((terrainMap[x + 1, y] == 1 && terrainMap[x, y] == 0)
                                 && (terrainMap[x, y - 1] == 1 && terrainMap[x, y] == 0))) {
                                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), cornerBotRight);
                            }
                            else if ((terrainMap[x - 1, y] == 1 && terrainMap[x, y] == 0)
                                     && (terrainMap[x, y + 1] == 1 && terrainMap[x, y] == 0)) {
                                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), cornerTopLeft);
                            }
                            else if ((terrainMap[x - 1, y] == 1 && terrainMap[x, y] == 0)
                                     && (terrainMap[x, y - 1] == 1 && terrainMap[x, y] == 0)) {
                                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), cornerBotLeft);
                            }
                            else if ((terrainMap[x + 1, y] == 1 && terrainMap[x, y] == 0)
                                     && (terrainMap[x, y + 1] == 1 && terrainMap[x, y] == 0)) {
                                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), cornerTopRight);
                            }
                            else {
                                if (terrainMap[x + 1, y] == 1 && terrainMap[x, y] == 0) {
                                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), rightBorderTile);
                                }
                                else if (terrainMap[x - 1, y] == 1 && terrainMap[x, y] == 0) {
                                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), leftBorderTile);
                                }
                                else if (terrainMap[x, y - 1] == 1 && terrainMap[x, y] == 0) {
                                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botBorderTile);
                                }
                                else if (terrainMap[x, y + 1] == 1 && terrainMap[x, y] == 0) {
                                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topBorderTile);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public int[,] genTilePos(int[,] oldMap) {
        int[,] newMap = new int [width, height];
        int neighb;
        BoundsInt myB=new BoundsInt(-1,-1,0,3,3,1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin) {
                    if(b.x ==0 && b.y ==0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height) {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1) {
                    if (neighb < deathLimit) newMap[x, y] = 0;
                    else {
                        newMap[x, y] = 1;
                    }
                }
                if (oldMap[x, y] == 0) {
                    if (neighb > birthLimit) newMap[x, y] = 1;
                    else {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        return newMap;
    }
    public void initPos() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                terrainMap[x,y]=Random.Range(1,101) < iniChance ? 1:0; 
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            doSim(numR);
        }
        
        if (Input.GetMouseButtonDown(1)) {
            clearMap(true);
        }

        if (Input.GetMouseButtonDown(2)) {
            SaveAssetMap();
        }
    }
    public void SaveAssetMap() {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("GRID");

        if (mf) {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath, mf)) {
                EditorUtility.DisplayDialog("Tilemap saved", "Your TileMap was saved under " + savePath, "Continue");
                count++;
            }
            else {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "AN ERROR OCCURED WHILE TRYING TO SAVE TILEMAP UNDER " + savePath, "Continue");
            }
        }
    }
    public void clearMap(bool complete) {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}
