using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private Shield shield;

    private void OnEnable()
    {
        StarterAssetsInputs.onCastSpell1Click += ShieldToggle;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.onCastSpell1Click -= ShieldToggle;
    }
    
    private void ShieldToggle()
    {
        shield.OpenCloseShield();
    }
}
