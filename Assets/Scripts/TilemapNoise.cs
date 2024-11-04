using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //remember to include this when working with tilemaps!

public class TilemapNoise : MonoBehaviour
{


    [Header("Config")]
    public float noiseScale = .1f;
    public float threshold = .5f;
    public int areaSize = 100;


    public Tilemap tilemap;
    public TileBase abyss;
    public TileBase wall1;
    public TileBase wall2;
    public TileBase middle;


    void Start(){
        ApplyNoise();
    }

    void ApplyNoise(){
        for(int x = 0; x<areaSize; x++){
            for(int y = 0; y<areaSize; y++){
                
                float perlinValue = Mathf.PerlinNoise((float)x*noiseScale,(float)y*noiseScale); //keep coordinates between 0 and 1
                
                if(perlinValue > threshold+.2){
                    if(perlinValue < threshold + .4){
                        tilemap.SetTile(new Vector3Int(x,y),wall1);
                        Debug.Log("Block: " + perlinValue); 
                    } else {
                        tilemap.SetTile(new Vector3Int(x,y),wall2); 
                        Debug.Log("Block2: " + perlinValue); 
                    }
                } else if (perlinValue < threshold-.2){
                    tilemap.SetTile(new Vector3Int(x, y), abyss);
                } else {
                    tilemap.SetTile(new Vector3Int(x,y), middle);
                }
            }
        }
    }


}
