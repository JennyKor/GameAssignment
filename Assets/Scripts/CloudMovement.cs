using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float speed;
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(.1f, .5f);
        size = Random.Range(.9f, 1.8f);
        transform.localScale = new Vector2(size, size);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < -11)
        {
            Destroy(gameObject);
            Debug.Log("Cloud destroyed!");
        }
    }
}
