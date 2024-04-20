using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar
{
    public List<Enemy> Enemies;
    
    public Bar()
    {
        Enemies = new List<Enemy>();
    }
        
    public static List<Bar> GetRandomStage(int length)
    {
        List<Bar> stage = new List<Bar>();
        for (int i = 0; i < length; i++)
        {
            Bar bar = new Bar();
            bar.Enemies = Enemy.GetRandomPattern();
            stage.Add(bar);
        }

        return stage;
    }
}
