using System;
using Core.Brick.Interfaces;
using Core.Services.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour, IDamageable, IDestructible
{
   [SerializeField]
   [Min(0)]
   public int strength;
   [SerializeField]
   protected internal bool isEndlessStrength;

   [Header("Damage Animation Settings")]
   [SerializeField] private float animationDuration;
   [SerializeField] private float minAlpha;

   private MeshRenderer _meshRenderer;
   private bool _isDestroyed;
   private TMP_Text _counterText;

   protected virtual void Awake()
   {
      if (isEndlessStrength)
      {
         _counterText.enabled = false;
      }
      else
      {
         UpdateText();
      }

      _meshRenderer = GetComponent<MeshRenderer>();
   }

   public virtual void TakeDamage()
   {
      if(strength is 0 || isEndlessStrength) return;

      strength--;

      UpdateText();

      CheckRemainsStrength();

      PlayDamageAnimation();
   }

   public virtual void TakeDamage(int damage)
   {
      if(strength is 0 || isEndlessStrength) return;

      strength -= damage;

      UpdateText();

      CheckRemainsStrength();

      PlayDamageAnimation();
   }

   public virtual void Destruct()
   {
      Destroy(gameObject);
   }
 
   public void UpdateText()
   {
      if (_counterText == null)
      {
         _counterText = GetComponentInChildren<TMP_Text>();
         _counterText.text = strength.ToString();  
      }
      else
      {
         _counterText.text = strength.ToString();  
      }
   }

   private void CheckRemainsStrength()
   {
      if(strength > 0) return;

      if (!_isDestroyed)
      {
         Destruct();  
      }
      
      _isDestroyed = true;
   }
   
   protected virtual void PlayDamageAnimation()
   {
      foreach (var material in _meshRenderer.materials)
      {
         material.DOFade(minAlpha, animationDuration).OnComplete(() =>
         {
            material.DOFade(1f, animationDuration);
         });   
      }
   }
}
