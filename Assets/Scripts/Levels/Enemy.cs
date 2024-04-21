using System;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

public class Enemy
{
    private static List<Func<List<Enemy>>> NormalPatterns;
    private static List<Func<List<Enemy>>> AllPatterns;
    private static Random Rand;
    private static int InputCount;
    
    public static List<Enemy> GetRandomPattern()
    {
        int i = Rand.Next(3);
        
        if(i > 1)
        {
            return AllPatterns[Rand.Next(AllPatterns.Count)].Invoke();
        }
        else
        {
            return NormalPatterns[Rand.Next(NormalPatterns.Count)].Invoke();
        }
    }

    public static List<Enemy> GetNormalPattern()
    {
        return NormalPatterns[Rand.Next(NormalPatterns.Count)].Invoke();
    }

    static Enemy()
    {
        Rand = new Random();
        AllPatterns = new List<Func<List<Enemy>>>();
        NormalPatterns = new List<Func<List<Enemy>>>();
        InputCount = Enum.GetValues(typeof(InputType)).Length;
        
        NormalPatterns.Add(Patter_Two);
        NormalPatterns.Add(Patter_One);
        NormalPatterns.Add(Patter_Five);
        
        AllPatterns.Add(Patter_Two);
        AllPatterns.Add(Patter_One);
        AllPatterns.Add(Patter_Three);
        AllPatterns.Add(Patter_Two);
        AllPatterns.Add(Patter_One);
        AllPatterns.Add(Patter_Three);
        AllPatterns.Add(Patter_Four);
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
        enemy.BeatPosition = 1;
        enemies.Add(enemy);

        return enemies;
    }    
    
    private static List<Enemy> Patter_Two()
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
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 5;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 6;
        enemies.Add(enemy);

        return enemies;
    }
    
    private static List<Enemy> Patter_Five()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 0;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 1;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.Crotchet = true;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 2;
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
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 4;
        enemies.Add(enemy);   
        
        enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 5;
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 6;
        enemies.Add(enemy);   
        
        enemy = new Enemy();
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemy.BeatPosition = 7;
        enemies.Add(enemy);

        return enemies;
    }
    
    private static List<Enemy> Patter_Four()
    {
        List<Enemy> enemies = new List<Enemy>();
        
        Enemy enemy = new Enemy();
        enemy.BeatPosition = 0;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 1;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);

        enemy = new Enemy();
        enemy.BeatPosition = 2;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);

        enemy = new Enemy();
        enemy.BeatPosition = 3;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);

        enemy = new Enemy();
        enemy.BeatPosition = 4;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 5;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 6;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        
        enemy = new Enemy();
        enemy.BeatPosition = 7;
        enemy.RequiredInput = (InputType)Rand.Next(InputCount);
        enemies.Add(enemy);
        

        return enemies;
    }

    public InputType RequiredInput;
    public EnemyWorldObject Object;
    public int BeatPosition;
    public int InternalPos;
    public bool Crotchet;
    public bool Active;
}