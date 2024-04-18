using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public GameObject label_moedas;
    public Animator anim;
    public GameObject bosta;

    public float velocidade = 5f; // Velocidade de movimento
    public Vector2 limiteMin; // Canto inferior esquerdo da �rea
    public Vector2 limiteMax; // Canto superior direito da �rea

    public string state = "Idle";


    private float idleTime = 0;
    private float idleTimeMax = 4;
    private Vector2 lastPos = new Vector2 (0, 0);
    private Vector2 walkingTo = new Vector2(0,0);

    private Vector2 destinoAleatorio;


    [SerializeField] private string stat_Comida = "Cheio";
    [SerializeField] private string stat_Diversao = "Muito Feliz";
    [SerializeField] private string stat_Agua = "Cheio";
    [SerializeField] private string stat_Sujeira = "Limpo";




    [SerializeField] private DateTime lastComida;
    [SerializeField] private DateTime lastDiversao;
    [SerializeField] private DateTime lastAgua;
    [SerializeField] private DateTime lastSujeira;
    [SerializeField] private DateTime lastEnergia;
    [SerializeField] private DateTime lastCagar;
    [SerializeField] private int energyPoints = 10;
    [SerializeField] private List<GameObject> poops = new List<GameObject>();

    private void Start()
    {
        transform.position = new Vector2(0, 0);
        if (lastComida.Year == 1)
        {
            DateTime date = DateTime.Now;
            lastComida = date.AddHours(-3);
            lastDiversao = date.AddHours(-3);
            lastAgua = date.AddHours(-3);
            lastSujeira = date.AddHours(-3);
            lastEnergia = date;
            lastCagar = date.AddHours(-3);

            var diarreia = true;
            do
            {
                diarreia = Pooping(PosRandom());
            } while (diarreia);
            
        }
        
    }

    private void Update()
    {
        CheckStats();

        Pooping(transform.position);

        MachineState();

        
    }

    private Vector2 PosRandom()
    {
        float posx = Random.Range(-20f,20f);
        float yrange = (20-math.abs(posx))/2;
        float posy = Random.Range(-yrange, yrange);
        return new Vector2(posx,posy);
    }

    private bool Pooping(Vector2 pos)
    {
        var cagar = false;
        DateTime dat = DateTime.Now;
        DateTime datSemCagar = dat.AddTicks(-lastCagar.Ticks);
        var horaspracagar = (datSemCagar.Hour) + (datSemCagar.Day * 24) + (datSemCagar.Month * 720) + (datSemCagar.Year * 8760) - 8760 - 720 - 24;
        if (stat_Comida == "Gordo")
        {
            if (horaspracagar >= 4)
            {
                lastCagar = AddInHours(lastCagar, 4);
                cagar = true;
            }
        }
        if (stat_Comida == "Cheio" || stat_Comida == "Neutro")
        {
            if (horaspracagar >= 8)
            {
                lastCagar = AddInHours(lastCagar, 8);
                cagar = true;
            }
        }
        if (cagar)
        {
            GameObject bostaInst = Instantiate(bosta, pos, Quaternion.identity);
            poops.Add(bostaInst);
            return true;
        }
        else return false;
    }


    private DateTime AddInHours(DateTime date, int hours)
    {
        if(hours>0)
        {
            DateTime datatemp = new DateTime();
            datatemp = datatemp.AddHours(hours);
            return date.AddTicks(datatemp.Ticks);
        }
        else
        {
            return date.AddHours(-120);
        }
    }


    private void MachineState()
    {
        if (state == "Idle")
        {
            if (idleTime <= idleTimeMax)
            {
                anim.SetBool("Idle", true);
                idleTime += Time.deltaTime;
            }
            else
            {
                if (Vector2.Distance(transform.position, destinoAleatorio) < 0.1f)
                {
                    idleTime = 0;
                    destinoAleatorio = PosRandom();
                }
                else
                {
                    anim.SetBool("Idle", false);
                    var posx = transform.position.x - destinoAleatorio.x;
                    var posy = transform.position.y - destinoAleatorio.y;
                    anim.SetFloat("MovementX", -posx);
                    anim.SetFloat("MovementY", -posy);
                    transform.position = Vector2.MoveTowards(transform.position, destinoAleatorio, velocidade * Time.deltaTime);
                }
            }
        }
    }

    private void CheckStats()
    {

        DateTime dat = DateTime.Now;

        DateTime datSemComida = dat.AddTicks(-lastComida.Ticks);
        if (datSemComida.Hour < 3)
        {
            stat_Comida = "Gordo";
        }
        if (datSemComida.Hour >= 3)
        {
            stat_Comida = "Cheio";
        }
        if (datSemComida.Hour >= 6)
        {
            stat_Comida = "Neutro";
        }
        if (datSemComida.Hour >= 12)
        {
            stat_Comida = "Com Fome";
        }
        if (datSemComida.Hour >= 24)
        {
            stat_Comida = "Esfomeado";
        }
        if (datSemComida.Hour >= 72)
        {
            stat_Comida = "Morto - Fome";
        }

        DateTime datSemDiversao = dat.AddTicks(-lastDiversao.Ticks);
        if (datSemDiversao.Hour < 3)
        {
            stat_Diversao = "Excitado";
        }
        if (datSemDiversao.Hour >= 3)
        {
            stat_Diversao = "Feliz";
        }
        if (datSemDiversao.Hour >= 8)
        {
            stat_Diversao = "Neutro";
        }
        if (datSemDiversao.Hour >= 16)
        {
            stat_Diversao = "Triste";
        }
        if (datSemDiversao.Hour >= 32)
        {
            stat_Diversao = "Depressivo";
        }

        DateTime datSemAgua = dat.AddTicks(-lastAgua.Ticks);
        if (datSemAgua.Hour < 3)
        {
            stat_Agua = "Cheio";
        }
        if (datSemAgua.Hour >= 8)
        {
            stat_Agua = "Neutro";
        }
        if (datSemAgua.Hour >= 16)
        {
            stat_Agua = "Com Sede";
        }
        if (datSemAgua.Hour >= 32)
        {
            stat_Agua = "Muita sede";
        }
        if (datSemAgua.Hour >= 72)
        {
            stat_Agua = "Morto - Sede";
        }

        DateTime datSemBanho = dat.AddTicks(-lastSujeira.Ticks);
        if (datSemBanho.Hour < 3)
        {
            stat_Sujeira = "Brilhando";
        }
        if (datSemBanho.Hour >= 8)
        {
            stat_Sujeira = "Limpo";
        }
        if (datSemBanho.Hour >= 16)
        {
            stat_Sujeira = "Neutro";
        }
        if (datSemBanho.Hour >= 24)
        {
            stat_Sujeira = "Sujo";
        }
        if (datSemBanho.Hour >= 48)
        {
            stat_Sujeira = "Podre";
        }

        DateTime datSemEnergia = dat.AddTicks(-lastEnergia.Ticks);
        if (datSemEnergia.Hour >= 1)
        {
            energyPoints += 1;
            if (energyPoints > 10) energyPoints = 10;
            lastEnergia = lastEnergia.AddHours(1);
        }
    }
}


