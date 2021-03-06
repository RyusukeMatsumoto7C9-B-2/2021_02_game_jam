﻿using UnityEngine;
using UniRx;

using UnityEngine.XR.MagicLeap;


namespace ChocoShark
{
    /// <summary>
    /// 手作りチョコレートパイ.
    /// </summary>
    public class ChocolatePie : MonoBehaviour
    {
        private MLPersistentCoordinateFrames.PCF aa;
        private float initialHp;
        private ReactiveProperty<float> hp = new ReactiveProperty<float>(100);

        void Start()
        {
            initialHp = hp.Value;
            hp.Subscribe(e =>
            {
                if (0 < e)
                {
                    transform.localScale = Vector3.one * (e / initialHp);
                }
                else
                {
                    transform.localScale = Vector3.zero;
                }
            });
        }

        
        void Update()
        {
            Debug.Log($"hp {hp.Value / initialHp}");
        }


        public void SetDamage(
            float damage)
        {
            hp.Value -= damage;
        }
    }
}

    