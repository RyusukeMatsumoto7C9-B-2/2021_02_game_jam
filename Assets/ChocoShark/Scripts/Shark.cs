using System;
using UnityEngine;


namespace ChocoShark
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shark : MonoBehaviour
    {
        private static class SharkAnimState
        {
            public static readonly string Eating = "IsEating";
        }


        
        public enum SharkState
        {
            TargetChase,
            Eating,
            Escape
        }


        [SerializeField, Range(100, 1000)]　private float hp;
        public float Hp
        {
            get => hp;

            private set
            {
                hp = value;
                if (hp <= 0)
                {
                    // 逃げに移る.            
                    animator.SetBool(SharkAnimState.Eating, false);
                }
            }
        }
        
        
        [SerializeField] private float correctPower;
        [SerializeField,Range(1f, 4f)] private float attenuation = 1f;
        [SerializeField] private Transform currentTarget;

        public SharkState State { get; private set; } = SharkState.TargetChase;
        private Rigidbody rig;
        private Animator animator;
        private ChocolatePie chocolatePie;        
        

        private void Start()
        {
            rig = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        
        private void Update()
        {
            transform.LookAt(currentTarget);

            switch (State)
            {
                case SharkState.TargetChase:
                    AddSpringForceExtra(currentTarget.position);
                    break;
                
                case SharkState.Eating:
                    // 食べる処理.
                    AddSpringForceExtra(currentTarget.position);
                    chocolatePie?.SetDamage(1f);                    
                    break;
     
                case SharkState.Escape:
                    AddSpringForceExtra(currentTarget.position);
                    break;
            }
        }

        
        /// <summary>
        /// オーバーシュートしないばね力を加える.
        /// </summary>
        /// <param name="targetPos"></param>
        private void AddSpringForceExtra(
            Vector3 targetPos )
        {
            float r = rig.mass * rig.drag * rig.drag / attenuation;
            Vector3 force = ( targetPos - transform.position ) * r;
            rig.velocity = force * correctPower;
        }


        private void OnCollisionEnter(
            Collision other)
        {
            chocolatePie = other.gameObject.GetComponent<ChocolatePie>();
            if (chocolatePie != null)
            {
                State = SharkState.Eating;
                animator.SetBool(SharkAnimState.Eating, true);
            }
        }


        private void OnCollisionExit(
            Collision other)
        {
            State = SharkState.TargetChase;
            animator.SetBool(SharkAnimState.Eating, false);
        }
    }
}

    