using DG.Tweening;
using Febucci.UI.Effects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;
    
    [SerializeField]
    private SpriteRenderer _Renderer;
    
    [SerializeField]
    private Sprite _IdleFrame;
    
    [SerializeField]
    private Image AttackLockOut;

    private float TimeOut;

    [SerializeField] 
    private ParticleSystem BLOOD;
    
    void Awake()
    {
        _Renderer.sprite = _IdleFrame;
    }

    public void Stab(bool miss)
    {
        if(miss)
        {
            _Animator.SetTrigger("StabMiss");
        }
        else
        {
            _Animator.SetTrigger("StabHit");
        }
    }

    public void Lockout(float duration)
    {
        AttackLockOut.fillAmount = 1;
        DOTween.To(() => AttackLockOut.fillAmount, value => AttackLockOut.fillAmount = value, 0, duration);
    }

    public void Idle()
    {
        _Animator.SetTrigger("Idle");
    }

    public void BLOOD_ALL_THE_BLOOD()
    {
        BLOOD.Play();
    }
}
