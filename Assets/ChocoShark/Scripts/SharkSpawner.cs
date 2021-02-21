using System;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

namespace ChocoShark
{
    public class SharkSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject sharkPrefab;
        [SerializeField] private ChocolatePie chocolatePie;

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(Random.Range(3, 5)))
                .Subscribe(_ =>
                {
                    // サメを1 ~ 3匹生成.
                    int sharkCount = Random.Range(1, 4);
                    for (var i = 0; i < sharkCount; ++i)
                    {
                        CreateShark();
                    }
                })
                .AddTo(this);
        }

        
        private void Update()
        {
            
        }


        private void CreateShark()
        {
            var shark = Instantiate(sharkPrefab).GetComponent<Shark>();
            Vector3 spawnPosition = Random.onUnitSphere * 3;
            if (spawnPosition.y < 0)
            {
                spawnPosition.y += 3;
            }
            shark.transform.position = spawnPosition;
            shark.SetTargetPosition(chocolatePie.transform.position);
        }

    }

}

    