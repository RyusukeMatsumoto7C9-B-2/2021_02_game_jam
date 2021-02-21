using System;
using UnityEngine;
using UniRx;

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
                    Debug.Log("逃げに移るはず");
                    state.Value = SharkState.Escape;
                    animator.SetBool(SharkAnimState.Eating, false);
                }
            }
        }
        
        
        [SerializeField] private float correctPower;
        [SerializeField,Range(1f, 4f)] private float attenuation = 1f;
        [SerializeField,Range(1f, 10)] private float attackPoint = 0.1f;


        private ReactiveProperty<SharkState> state = new ReactiveProperty<SharkState>(SharkState.TargetChase);

        private Rigidbody rig;
        private Animator animator;
        private ChocolatePie chocolatePie;
        private IDisposable attackDisposable = null;
        private Vector3 spawnedPosition;
        private Vector3 targetPosition;


        private void Start()
        {
            spawnedPosition = transform.position;
            rig = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            state.Subscribe(value =>
            {
                animator.SetBool(SharkAnimState.Eating, value == SharkState.Eating);
                if (value == SharkState.Eating)
                {
                    attackDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                        .Subscribe(_ => chocolatePie?.SetDamage(attackPoint));
                }
                else
                {
                    attackDisposable?.Dispose();                    
                }
            });
        }

        
        private void Update()
        {

            switch (state.Value)
            {
                case SharkState.TargetChase:
                    transform.LookAt(targetPosition);
                    AddSpringForceExtra(targetPosition);
                    break;
                
                case SharkState.Eating:
                    // 食べる処理.
                    break;
     
                case SharkState.Escape:
                    transform.LookAt(spawnedPosition);
                    AddSpringForceExtra(spawnedPosition);
                    if (Vector3.Distance(spawnedPosition, transform.position) < 0.1f)
                    {
                        Destroy(gameObject);
                    }
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
            if (state.Value == SharkState.Escape) return;

            var pie = other.gameObject.GetComponent<ChocolatePie>();
            if (pie != null)
            {
                chocolatePie = pie;
                state.Value = SharkState.Eating;
            }
            else
            {
                chocolatePie = null;
            }
        }

        
        private void OnCollisionStay(
            Collision other)
        {
            if (state.Value == SharkState.Escape) return;
            if (chocolatePie != null) return;
            
            var pie = other.gameObject.GetComponent<ChocolatePie>();
            if (pie != null)
            {
                chocolatePie = pie;
                state.Value = SharkState.Eating;
            }
            else
            {
                chocolatePie = null;
            }
        }


        private void OnCollisionExit(
            Collision other)
        {
            if (state.Value == SharkState.Escape) return;
            state.Value = SharkState.TargetChase;
        }


        public void SetDamage(float damage) => Hp -= damage;

        public void SetTargetPosition(Vector3 position) => targetPosition = position;
    }
}

    