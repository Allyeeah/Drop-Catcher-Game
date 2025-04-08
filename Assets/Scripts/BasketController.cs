using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasketController : MonoBehaviour
{
    public AudioClip applesound;
    public AudioClip bombsound;
    public AudioClip lemonsound;
    AudioSource AS;
    public GameObject GM;
    ItemGenerator IG;
    public float moveSpeed = 5f; // 이동 속도

    public Image blindOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pos -> x, y, z --> 0, 0, 0 => 위치 초기화
        transform.position = Vector3.zero;
        AS = GetComponent<AudioSource>();
        IG = GM.GetComponent<ItemGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        //키보드 입력으로 이동
        float moveX = 0f;
        float moveZ = 0f;

        // 좌우 이동 (A / D)
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // 상하 이동 (W / S)
        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveZ = -1f;

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;

        // 스테이지 밖 이동 방지
        float clampedX = Mathf.Clamp(transform.position.x, -1f, 1f);
        float clampedZ = Mathf.Clamp(transform.position.z, -1f, 1f);
        transform.position = new Vector3(clampedX, 0, clampedZ);


        /* 마우스 클릭으로 이동할 때
         * if (Input.GetMouseButtonDown(0)) 
             // 마우스 좌클릭 0 , 휠클릭 1, 마우스 우클릭 2
         {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if(Physics.Raycast(ray, out hit, Mathf.Infinity))
             {  // out -> 참조 변수
                 // -1, 0, 1 
                 float x = Mathf.RoundToInt(hit.point.x); //좌표 반올림
                 float z = Mathf.RoundToInt(hit.point.z);

                 transform.position = new Vector3(x, 0, z);
             }
         }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("apple"))
        {
            Debug.Log("apple");
            AS.PlayOneShot(applesound);
            IG.GetApple();
        }
        else if (other.CompareTag("bomb")){
            Debug.Log("bomb");
            AS.PlayOneShot(bombsound);
            IG.GetBomb();
        }
        else if (other.CompareTag("lemon"))
        {
            Debug.Log("레몬! 보너스 점수!");
            AS.PlayOneShot(lemonsound);
            IG.GetLemon();
            StartCoroutine(BlindEffect());
        }

        Destroy(other.gameObject);
    }
    // 시야 가리기 효과를 일시적으로 보여줌
    IEnumerator BlindEffect()
    {
        // 화면 전체에 노란 오버레이를 켠다 (시야가 가려짐)
        blindOverlay.enabled = true;
        // 3초 동안 오버레이 유지 (3초 기다림)
        yield return new WaitForSeconds(3f);
        // 3초 후 오버레이를 끄기
        blindOverlay.enabled = false;
    }



}
