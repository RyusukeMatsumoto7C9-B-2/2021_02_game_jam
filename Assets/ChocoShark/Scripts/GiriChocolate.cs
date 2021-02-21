using System;
using UnityEngine;
using UniRx;


namespace ChocoShark
{
    /// <summary>
    /// サメに食わせるように義理チョコ.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class GiriChocolate : MonoBehaviour
    {
        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(5))
                .Subscribe(_ =>
                {
                    Destroy(gameObject);
                }).AddTo(this);
        }

        
        private void Update()
        {
        }


        private void OnCollisionEnter(
            Collision other)
        {
            var shark = other.gameObject.GetComponent<Shark>();
            if (shark)
            {
                shark.SetDamage(100);
            }
        }


        public void SetForce(
            Vector3 force)
        {
            rig.AddForce(force, ForceMode.Impulse);
            Vector3 torque = Vector3.one;
            torque.x *= UnityEngine.Random.Range(0, 3);
            torque.y *= UnityEngine.Random.Range(0, 3);
            torque.z *= UnityEngine.Random.Range(0, 3);
            rig.AddTorque(torque);
        }
        
    }
}

    