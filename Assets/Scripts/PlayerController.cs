using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    [Header ("References")]
    public TextMeshProUGUI countText;
    public TextMeshProUGUI fuseText;
    public GameObject winTextObject;
    public GameObject doorObject;
    public GameObject door2Object;
    public GameObject door3Object;
    public GameObject Explosion;
    public Transform parentTransform;
    
    [Header ("Sounds")]
    public AudioClip pickupSound;
    public AudioClip BombJump;
    public AudioClip Boom;
    public AudioClip Dash;
    private AudioSource audioSource;
    
    [Header ("Stats")]
    public float speed = 0;
    public float jumpForce = 5f;
    public float dashSpeed = 10f;
    public int fuse = 3;
    public float cooldownTime = 3.0f;
    private float nextDashTime = 0f;
    private bool isGrounded;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        fuseText.text = "Fuse: " + fuse.ToString();
    }
   
    void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            audioSource.PlayOneShot(BombJump);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnDash()
    {
        Vector3 dashDirection = new Vector3(movementX, 0, movementY).normalized;
        if (dashDirection == Vector3.zero) dashDirection = transform.forward;
        rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
        audioSource.PlayOneShot(Dash);
    }

    void OnExplode()
    {
        if (fuse > 0)
        {
            Debug.Log("BOOM");
            audioSource.PlayOneShot(Boom);
            Instantiate(Explosion, parentTransform.position, parentTransform.rotation, parentTransform);
            fuse --;
            fuseText.text = "Fuse: " + fuse.ToString();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 5 && doorObject != null)
        {
            doorObject.SetActive(false);
        }

        if (count >= 9 && door2Object != null)
        {
            door2Object.SetActive(false);
        }

        if (count >= 13 && door3Object != null)
        {
            door3Object.SetActive(false);
        }

        if (count >= 16) // Win Condition
        {
            winTextObject.SetActive(true);

            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";            
        }
    }
}