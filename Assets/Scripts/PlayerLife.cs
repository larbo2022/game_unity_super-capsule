using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour   
{
    [SerializeField]
    private float maxLife = 100f;
    public float currentLife;

    [SerializeField]
    private Image healthBarFill;

    [SerializeField] AudioSource deathSound;             

    bool isDead = false;

    void Awake()
    {
        currentLife = maxLife;
        
    }

    private void Update()
    {
       
        if (transform.position.y < -1.5f && !isDead)
            {
              TakeDamage(); 
            }     
    }

   private void OnCollisionEnter(Collision collision)
   {
    if (collision.gameObject.CompareTag("Enemy Body"))
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;
        TakeDamage();
    }
   }

   void Die()
   { 
    deathSound.Play();
     isDead = true;

        SceneManager.LoadScene("GameOver");
      
          
   }
    public void TakeDamage()
    {
        isDead = true;
        deathSound.Play();
        currentLife -= 25; 
        UpdateHealthBarFill();

       // transform.position = new Vector3(0f, 0f, 0f);
        Invoke(nameof(ReloadLevel), 1.1f);
    }
      void ReloadLevel()
      { 
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
      }

    void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = currentLife / maxLife;
    }

}

