using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorldObject : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer InputRenderer;

    public void Init(Enemy model)
    {
        float angleOffset;
        switch (model.RequiredInput)
        {
            case InputType.DOWN:
                angleOffset = 180;
                break;
            
            case InputType.LEFT:
                angleOffset = 90;
                break;
            
            case InputType.RIGHT:
                angleOffset = 270;
                break;
            
            default:
                angleOffset = 0;
                break;
        }
        
        InputRenderer.transform.Rotate(0, 0, angleOffset);
    }
    
}
