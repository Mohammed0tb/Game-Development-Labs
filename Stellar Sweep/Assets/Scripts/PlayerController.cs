using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 100f;

    private float arenaBoundX = 24f;
    private float arenaBoundZ = 24f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotateSpeed * Time.deltaTime);

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.World);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -arenaBoundX, arenaBoundX);
        clampedPos.z = Mathf.Clamp(clampedPos.z, -arenaBoundZ, arenaBoundZ);
        transform.position = clampedPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position + transform.forward * 1.0f;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);

            bullet.GetComponent<Bullet>().SetDirection(transform.forward);
        }
    }
}