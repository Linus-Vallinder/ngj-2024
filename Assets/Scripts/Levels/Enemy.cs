using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy
{
    private static List<Func<List<Enemy>>> AllPatterns;
    private static Random Rand;
    private static int InputCount;
    
    public static List<Enemy> GetRandomPattern()
    {
        return AllPatterns[Rand.Next(AllPatterns.Count)].Invoke();
    }

    static Enemy()
    {
        Rand = new Random();
        AllPatterns = new List<Func<List<Enemy>>>();
        InputCount = Enum.GetValues(typeof(InputType)).Length;
        AllPatterns.Add(Patter_One);
        AllPatterns.Add(Patter_Two);
        AllPatterns.Add(Patter_Four);
        AllPatterns.Add(Patter_Three);
    }

    private static List<Enemy> Patter_One()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 0;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 2;
        enemies.Add(enemy);

        return enemies;
    }    
    
    private static List<Enemy> Patter_Two()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 1;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 3;
        enemies.Add(enemy);

        return enemies;
    }
    
    private static List<Enemy> Patter_Three()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 0;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 1;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 2;
        enemies.Add(enemy);   
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 3;
        enemies.Add(enemy);

        return enemies;
    }
    
    private static List<Enemy> Patter_Four()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.BeatPosition = 3;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 6;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);

        return enemies;
    }

    public InputType RequiredInput;
    public GameObject Object;
    public int BeatPosition;
    public int InternalPos;
    public bool Crotchet;
    public bool Active;
}