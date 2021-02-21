using UnityEngine;

namespace ChocoShark
{
    /// <summary>
    /// 義理チョコを放つ銃.
    /// </summary>
    public class GiriChocolateGun : MonoBehaviour
    {
        [SerializeField] private GameObject giriChocoPrefab;
        
        private void Start()
        {
            
        }


        private void Update()
        {
            
        }


        /// <summary>
        /// コントローラのトリガーを引いた.
        /// ControllerInputに登録.
        /// </summary>
        public void OnControllerTriggerEnter()
        {
            var giriChoko = Instantiate(giriChocoPrefab).GetComponent<GiriChocolate>();
            giriChoko.transform.position = transform.position;
            giriChoko.SetForce(transform.forward * Random.Range(1, 2));
        }

    }
}

    