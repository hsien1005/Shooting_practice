using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    public GameObject shootCamGameObj;
    public GameObject crossGameObj;
    public LayerMask layerMask;
    public Transform bulletSpawnTransform;//生成的位置(左手的指關節)
    public GameObject bulletPrefab;//生成bullet

    private StarterAssetsInputs _sai; //Player Input裡的Actions
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        this._sai = this.GetComponent<StarterAssetsInputs>();//瞄準的動作
        this._anim = this.GetComponent<Animator>();//人的動作
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aimWorldPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if(Physics.Raycast(ray, out RaycastHit rh, 999f, this.layerMask))
        {
            aimWorldPos = rh.point;//射線打到的位置記錄下來
        }
        else
        {
            aimWorldPos = ray.direction * 100;
        }

        if(this._sai.aim)
        {
            this.shootCamGameObj.SetActive(true);
            this.crossGameObj.SetActive(true);
            this._anim.SetLayerWeight(1, Mathf.Lerp(this._anim.GetLayerWeight(1), 1f, Time.deltaTime * 10));
            // SetLayerWeight(0):Base Layer;SetLayerWeight(1):Shoot Layer
            //Mathf.Lerp():第一個參數為原值、第二個為設定要到達的參數、第三個為經過的時間
            Vector3 temp = aimWorldPos;
            temp.y = transform.position.y;
            Vector3 aimDirection = (temp - transform.position).normalized;//準新瞄準的方向
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50);
            //從原本的方向移動到準新瞄準的方向
            if(this._sai.shoot)
            {
                Vector3 bulletDirection = (aimWorldPos - this.bulletSpawnTransform.position).normalized;//減掉生成位置的座標，取單位向量
                GameObject go = Instantiate(this.bulletPrefab, this.bulletSpawnTransform.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
                //Instantiate(a,b,c):a是生成什麼，b是生成的位置，c是轉向
                go.name = "Bullet";
                go.SetActive(true);
                this._sai.shoot = false;
            }

        }
        else
        {
            this.shootCamGameObj.SetActive(false);
            this.crossGameObj.SetActive(false);
            this._anim.SetLayerWeight(1, Mathf.Lerp(this._anim.GetLayerWeight(1), 0f, Time.deltaTime * 10));
        }
    }
}
