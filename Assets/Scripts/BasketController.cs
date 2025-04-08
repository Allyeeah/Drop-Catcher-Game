using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip applesound;
    public AudioClip bombsound;
    AudioSource AS;
    public GameObject GM;
    ItemGenerator IG;

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
        if (Input.GetMouseButtonDown(0)) 
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("apple"))
        {
            Debug.Log("apple");
            AS.PlayOneShot(applesound);
            IG.GetApple();
        }
        if (other.CompareTag("bomb")){
            Debug.Log("bomb");
            AS.PlayOneShot(bombsound);
            IG.GetBomb();
        }
        Destroy(other.gameObject);
    }
}
