using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class EnemyWorldObject : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer Renderer;
    
    [SerializeField]
    protected Animator _Animator;
    
    [SerializeField]
    protected Rigidbody2D Rbody;
    
    [SerializeField]
    protected Sprite DeathSprite;
    
    public void Init(Enemy model)
    {
        // float angleOffset;
        // switch (model.RequiredInput)
        // {
        //     case InputType.DOWN:
        //         angleOffset = 180;
        //         break;
        //     
        //     case InputType.LEFT:
        //         angleOffset = 90;
        //         break;
        //     
        //     case InputType.RIGHT:
        //         angleOffset = 270;
        //         break;
        //     
        //     default:
        //         angleOffset = 0;
        //         break;
        // }
        //
        // InputRenderer.transform.Rotate(0, 0, angleOffset);
    }

    public void AttackAnimation()
    {
        _Animator.SetTrigger("Attack");   
    }

    public void Death()
    {
        _Animator.enabled = false;
        Renderer.sprite = DeathSprite;
        Random rand = new Random();
        Vector2 force = new Vector2(1 + rand.Next(7), 1 + rand.Next(7));
        float angularForce = rand.Next(45);
        Rbody.simulated = true;
        Rbody.AddForce(force * 4, ForceMode2D.Impulse);
        Rbody.AddTorque(angularForce * 4, ForceMode2D.Impulse);
        Rbody.transform.DOScale(Vector3.zero, 1.2f).OnComplete(() => Destroy(this.gameObject));
    }
}
