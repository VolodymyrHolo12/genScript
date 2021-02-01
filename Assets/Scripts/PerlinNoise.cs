using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    public Tilemap topMap;
    public Tilemap botMap;
    public Tile topTile;
    public Tile botTile;
    
    public int height = 256;
    public int width = 256;

    public float scale = 20f;

    public float offSetX;
    public float offSetY;

    void Start()
    {
        offSetX = Random.Range(0f, 99999f);
        offSetY = Random.Range(0f, 99999f);
    }

    void Update()
    {
        // Renderer renderer = GetComponent<Renderer>();
        // renderer.material.mainTexture = GenerateTexture();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(textureCalculate(x,y)<0.5)
                {
                    topMap.SetTile(new Vector3Int(-x+width/2,-y+height/2,0), topTile);
                }
                else
                {
                    botMap.SetTile(new Vector3Int(-x+width/2,-y+height/2,0), botTile);
                }
            }
        }
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width , height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x,y,color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x , int y)
    {
        float xCoord = (float) x / width * scale+offSetX;
        float yCoord = (float) y / height * scale+offSetY;
        
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return  new Color(sample,sample,sample);
    }

    float textureCalculate(int x, int y)
    {
        float xCoord = (float) x / width * scale+offSetX;
        float yCoord = (float) y / height * scale+offSetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return sample;
    }
}
