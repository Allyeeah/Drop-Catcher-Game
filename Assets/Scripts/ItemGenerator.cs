using TMPro;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject appleGO;
    public GameObject bombGO;
    public GameObject lemonGO;

    float span = 1.0f;
    float delta = 0.0f;
    int ratio = 2;
    float speed = -1.0f;

   public GameObject timerText;
   public GameObject scoreText;
    float time = 60.0f;
    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetParameter(float span, int ratio, float speed)
    {
        this.span = span;
        this.ratio = ratio;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if(delta > span)
        {
            //span�ʸ��� �����ϰ� ���� ����
            delta = 0;
            GameObject item;
            int dice = Random.Range(1, 21); //1~10������ ������ int �� ��ȯ
            if (dice <= ratio) item = Instantiate(bombGO);
            else if (dice <= 16)
                item = Instantiate(appleGO);
            else
                item = Instantiate(lemonGO);

            //-1 ~ 1 ������ ������ int ��
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.transform.position = new Vector3(x, 4, z);


        }
        time -= Time.deltaTime; // �ð��� �������� ����
        timerText.GetComponent<TextMeshProUGUI>().text = "Time : " + time.ToString("F1");

        if (time < 30)
            SetParameter(3.0f, 1, -1.0f);
        else
            SetParameter(1.0f, 5, -2.0f);
    }
    public void GetApple()
    {
        score += 100;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void GetBomb()
    {
        score /= 2;
    }

   public void GetLemon()
    {
        score += 50;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

}
