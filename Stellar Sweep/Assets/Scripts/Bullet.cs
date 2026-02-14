using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float arenaBound = 26f; 

    private Vector3 moveDirection;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) > arenaBound ||
            Mathf.Abs(transform.position.z) > arenaBound)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            gameObject.SetActive(false);
        }
    }
}
