﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomGridScript : GridScript
{

    public static readonly float rockPercentage = 0.2f;         // 20% chance of rocks
    public static readonly float forestPercentage = 0.05f;      // 5% chance of forest
    public static readonly float waterPercentage = 0.1f;        // 10% chance of water

    string[] gridString;

    private void Awake()
    {
        // Make the grid be generated into gridString at random w/ the above percentages.
        MakeGrid(rockPercentage, forestPercentage, waterPercentage);
    }

    public void MakeGrid(float rockPercentage, float forestPercentage, float waterPercentage)
    {
        gridString = new string[gridHeight];


        for (int i = 0; i < gridHeight; i++)
        {
            var lineOfGrid = "";
            //make a for loop that creates a string/line
            for (int j = 0; j < gridWidth; j++)
            {
                var mapTile = UnityEngine.Random.Range(0, 100);
                if (mapTile < rockPercentage * 100) lineOfGrid += "r";//add to the string
                else if (mapTile < rockPercentage * 100 + forestPercentage * 100) lineOfGrid += "f";
                else if (mapTile < (rockPercentage + forestPercentage + waterPercentage) * 100) lineOfGrid += "w";
                else lineOfGrid += "-";
            }
            gridString[i] = lineOfGrid;
            //print(lineOfGrid);

        }
    }

    protected override Material GetMaterial(int x, int y)
    {

        var c = gridString[y].ToCharArray()[x];

        Material mat;

        switch (c)
        {
            case 'r':
                mat = mats[1];
                break;
            case 'f':
                mat = mats[2];
                break;
            case 'w':
                mat = mats[3];
                break;
            default:
                mat = mats[0];
                break;
        }

        return mat;
    }
}
