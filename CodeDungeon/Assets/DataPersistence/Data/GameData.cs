using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public string piriquito;
    public int energyPoints = 10;
    public List<Vector2> poops;
    public string lastComida;
    public string lastDiversao;
    public string lastAgua;
    public string lastSujeira;
    public string lastEnergia;
    public string lastCagar;
    public int moedas;
    public bool tijelaEnxida;
    public List<int> FoodCount;
    public int recordeMinigame1;
    public int recordeMinigame2;

    public GameData() 
    {
        piriquito = "";
        energyPoints = 10;
        poops = new List<Vector2>();
        lastComida = "00010101-000000";
        lastDiversao = "00010101-000000";
        lastAgua = "00010101-000000";
        lastSujeira = "00010101-000000";
        lastEnergia = "00010101-000000";
        lastCagar = "00010101-000000";
        moedas = 0;
        tijelaEnxida = false;
        FoodCount = new List<int>();
        recordeMinigame1 = 0;
        recordeMinigame2 = 0;
    }
}
