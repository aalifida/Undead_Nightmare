using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseHealth : MonoBehaviour
{
    public Slider playerHealthSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==("Player"))
        {
            playerHealthSlider.value = playerHealthSlider.value +30;
            Destroy(gameObject,.5f);
        }
}
}