using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    public Slider playerHealthSlider; 
    public AudioClip grunt;
    private AudioSource _audioSource;

    public ParticleSystem ps;
    

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ps.Play();
            playerHealthSlider.value = playerHealthSlider.value - 5;
            _audioSource.clip = grunt;
            _audioSource.Play();
            
            if (playerHealthSlider.value<=5)
            {
                SceneManager.LoadScene(4);
            }
        }
        else if(other.CompareTag("FirstAid"))
        {
            playerHealthSlider.value = playerHealthSlider.value +30;
           
            Destroy(other.gameObject,0f);
        }
    }
}