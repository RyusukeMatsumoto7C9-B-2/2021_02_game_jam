using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChocoShark
{
    /// <summary>
    /// 手作りチョコレートパイ.
    /// </summary>
    public class ChocolatePie : MonoBehaviour
    {

        [SerializeField] private float hp = 100;
        private float initialHp;


        void Start()
        {
            initialHp = hp;
        }

        
        void Update()
        {
            if (0 < hp)
            {
                float ratio = hp / initialHp;
                transform.localScale = Vector3.one * (hp / initialHp);
            }
            else
            {
                transform.localScale = Vector3.zero;
            }

        }


        public void SetDamage(
            float damage)
        {
            hp -= damage;
        }
    }
}

    