using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private static List<Func<List<Enemy>>> AllPatterns;
    public static List<Enemy> GetPattern()
    {
        return null;
    }

    static Enemy()
    {
        AllPatterns = new List<Func<List<Enemy>>>();
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
        enemy.BeatPosition = 0;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 2;
        enemies.Add(enemy);

        // enemy.Object =  GameObject.Instantiate(_EnemyPrefab, this.transform);
        // enemy.Object.transform.position = _StartPosition.position;

        return enemies;
    }    
    
    private static List<Enemy> Patter_Two()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 1;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 3;
        enemies.Add(enemy);
        

        // enemy.Object =  GameObject.Instantiate(_EnemyPrefab, this.transform);
        // enemy.Object.transform.position = _StartPosition.position;

        return enemies;
    }
    
    private static List<Enemy> Patter_Three()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 0;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 1;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 2;
        enemies.Add(enemy);   
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.BeatPosition = 3;
        enemies.Add(enemy);
        

        // enemy.Object =  GameObject.Instantiate(_EnemyPrefab, this.transform);
        // enemy.Object.transform.position = _StartPosition.position;

        return enemies;
    }
    
    private static List<Enemy> Patter_Four()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.BeatPosition = 3;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 6;
        enemies.Add(enemy);

        // enemy.Object =  GameObject.Instantiate(_EnemyPrefab, this.transform);
        // enemy.Object.transform.position = _StartPosition.position;

        return enemies;
    }

    public GameObject Object;
    public int BeatPosition;
    public bool Crotchet;
    public int InternalPos;

}