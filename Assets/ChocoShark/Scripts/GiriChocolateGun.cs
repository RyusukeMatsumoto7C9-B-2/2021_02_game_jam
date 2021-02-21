using UnityEngine;

namespace ChocoShark
{
    /// <summary>
    /// 義理チョコを放つ銃.
    /// </summary>
    public class GiriChocolateGun : MonoBehaviour
    {
        [SerializeField] private GameObject giriChocoPrefab;
        [SerializeField] private float shootPowerMin = 0.1f;
        [SerializeField] private float shootPowerMax = 0.3f;


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
            giriChoko.SetForce(transform.forward * Random.Range(shootPowerMin, shootPowerMax));
        }

        
        /// <summary>
        /// アプリを終了する.
        /// </summary>
        public void OnShutDown()
        {
            Application.Quit();
        }

    }
}

    