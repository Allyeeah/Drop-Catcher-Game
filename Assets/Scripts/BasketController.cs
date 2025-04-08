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
        //pos -> x, y, z --> 0, 0, 0 => ��ġ �ʱ�ȭ
        transform.position = Vector3.zero;
        AS = GetComponent<AudioSource>();
        IG = GM.GetComponent<ItemGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            // ���콺 ��Ŭ�� 0 , ��Ŭ�� 1, ���콺 ��Ŭ�� 2
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {  // out -> ���� ����
                // -1, 0, 1 
                float x = Mathf.RoundToInt(hit.point.x); //��ǥ �ݿø�
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
