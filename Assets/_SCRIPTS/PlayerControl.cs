using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
	public AudioClip bateuNoBicho, apanhouDoBicho, entrouNoCano, saiudoCano;			
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

	//private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
    private bool inimigo = false;
    private Animator anim;					// Reference to the player's animator component.

    private Rigidbody2D rb;
    private bool canMove = true;

    public CanvasGroup tutorial;
    public CanvasGroup msgFaltaChupeta; 
    private int chupetas = 0;
    private int totalChupetas = 0;

    public Color azul, rosa;
    public Tilemap ceu;

    private bool jaCriei = false;
    public GameObject chupetaProCeu;
    Princesa prin;

    void Awake()
	{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Setting up references.
        //groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (msgFaltaChupeta)
        {
            msgFaltaChupeta.alpha = 0;
        }
        if (tutorial)
        {
            tutorial.alpha = 1;
            StartCoroutine(SomeMsg(tutorial, 3.0f));
        }
        if (chupetaProCeu)
        {
            chupetaProCeu.SetActive(false);
        }
        totalChupetas += GameObject.FindGameObjectsWithTag("chupeta").Length;
        totalChupetas += GameObject.FindGameObjectsWithTag("cubo").Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "chupeta":
                if (collision.transform.name.Contains("azul"))
                {
                    ceu.color = azul;
                }
                else if (collision.transform.name.Contains("rosa"))
                {
                    ceu.color = rosa;
                }
                chupetas++;
                Destroy(collision.gameObject);
                break;
            case "enemy":
				AudioSource.PlayClipAtPoint(bateuNoBicho, transform.position);
                Destroy(collision.gameObject);
                break;
            case "cano":
                if (collision.name.Contains("rosa"))
                {
                    
                    //cam.maxXAndY = new Vector2(cam.maxXAndY.x, cam.maxXAndY.y);
                    
                    jumpForce = jumpForce * 1.5f;
                }
                else
                {
					AudioSource.PlayClipAtPoint(entrouNoCano, transform.position);
                    CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
                    cam.minXAndY = new Vector2(cam.minXAndY.x, -4.8f);
                }
                
                break;
            case "Finish":
                SceneManager.LoadScene("TheEnd", LoadSceneMode.Single);
                break;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "cano":
                if (collision.name.Contains("rosa"))
                {
                    jumpForce = jumpForce / 1.5f;
					AudioSource.PlayClipAtPoint(saiudoCano, transform.position);
                }
                else
                {
                    CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
                    //cam.maxXAndY = new Vector2(cam.maxXAndY.x, cam.maxXAndY.y);
                    cam.minXAndY = new Vector2(cam.minXAndY.x, 0.5f);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "enemy":
				AudioSource.PlayClipAtPoint(apanhouDoBicho, transform.position);
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                direction = direction * -1;
                StartCoroutine(TomouDano(0.7f));
                rb.AddForce(new Vector2(direction.x * 10f, 3f), ForceMode2D.Impulse);
                break;
            case "Finish":
                if (chupetas < totalChupetas)
                {
                    StartCoroutine(SomeMsg(msgFaltaChupeta, 1.0f));                   
                }
                else
                {
                    Animator temp = collision.gameObject.GetComponent<Animator>();
                    temp.SetTrigger("Abrir");
                    StartCoroutine(AbrindoPorta(collision));
                }
                break;
            case "Respawn":
                canMove = false;
                anim.SetTrigger("Entregando");
                prin = collision.gameObject.GetComponent<Princesa>();
                prin.canMove = false;
                prin.anim.SetTrigger("Entregando");
                chupetaProCeu.SetActive(true);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!grounded)
        {
            StartCoroutine(TomouDano(0.5f));
        }
    }

    IEnumerator SomeMsg(CanvasGroup canvas, float tempo)
    {
        canvas.alpha = 1;
        yield return new WaitForSeconds(tempo);
        while (canvas.alpha > 0)
        {
            yield return new WaitForSeconds(0.1f);
            canvas.alpha -= 0.05f;
        }
    }

    IEnumerator TomouDano(float tempo)
    {
        canMove = false;
        yield return new WaitForSeconds(tempo);
        canMove = true;
    }

    IEnumerator AbrindoPorta(Collision2D coll)
    {       
        yield return new WaitForSeconds(0.7f);
        coll.collider.enabled = false;
    }

    //IEnumerator playSomeNovoLevel()
    //{
    //    AudioClip som = porrada.clip;
    //    porrada.Play();
    //    anim.SetTrigger("Die");
    //    yield return new WaitForSeconds(som.length);
    //    //SceneManager.LoadScene("TheEnd", LoadSceneMode.Single);
    //}

    void ParteFinal()
    {
        SorteioChupeta sort = chupetaProCeu.GetComponent<SorteioChupeta>();
        if (sort.panel.alpha >= 1f)
        {
            anim.SetTrigger("Beijo");
            prin.anim.SetTrigger("Beijo");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TheEnd")
        {
            ParteFinal();
        }

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.5f, 1 << LayerMask.NameToLayer("Ground"));
        grounded = hit ? true : false;
        //print(grounded);
        if (canMove)
        {
            // If the jump button is pressed and the player is grounded then the player should jump.
            if (Input.GetButtonDown("Jump") && grounded)
            {
                jump = true;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}

	void FixedUpdate ()
	{
        float h = 0;
        if (canMove)
        {
            // Cache the horizontal input.
            h = Input.GetAxis("Horizontal");
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Andando", Mathf.Abs(h));
        
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rb.velocity.x < maxSpeed)
            // ... add a force to the player.
            rb.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rb.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
		}
	}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
