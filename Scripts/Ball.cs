using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private float minMagnitude = 7;
    private float maxMagnitude = 8;

    private Gameplay gameplay;
    private AudioSource GameplayAudio;

    void Start()
    {
        gameplay = GameObject.Find("Gameplay").GetComponent<Gameplay>();

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 350);
    }

    private Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }


    private void FixedUpdate()
    {
        rb.velocity = ClampMagnitude(rb.velocity, maxMagnitude, minMagnitude);
        if(rb.velocity.magnitude < 1)
            rb.AddForce(Vector2.up * 350);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "FalloutPanel")
        {
            gameplay.SoundSource.PlayOneShot(gameplay.Ball_lose);
            Destroy(gameObject);
        }

        Figure figure = collision.gameObject.GetComponent<Figure>();
        if (figure != null)
        {
            int randomHit = Random.Range(0, 3);
            gameplay.SoundSource.PlayOneShot(gameplay.Hit_figure[randomHit]);

            figure.strength--;
            if (figure.strength <= 0)
            {
                gameplay.SoundSource.PlayOneShot(gameplay.Figure_broken);
                Destroy(figure.gameObject);
            }
        }

        if(collision.gameObject.tag == "Wall")
        {
            gameplay.SoundSource.PlayOneShot(gameplay.Hit_wall);
        }
        if (collision.gameObject.tag == "Platform")
        {
            gameplay.SoundSource.PlayOneShot(gameplay.Hit_wall);
            collision.gameObject.GetComponent<Animator>().Play("Base Layer.Platform", -1, 0);
        }
    }
}
