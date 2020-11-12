using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // PUBLIC //
    public GameObject objectToSwitch;

    //sprite to switch to when the SWITCH is activated
    public Sprite switchDownSprite;

    //denotes if the switch deactivates or activates upon trigger
    public bool deactivatesOnSwitch;



    // PRIVATE //
    
    private SpriteRenderer spriteRenderer;

    //was the switch used
    private bool hasSwitched;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasSwitched)
        {
            //mark switched
            hasSwitched = true;

            //deactivate the door
            objectToSwitch.SetActive(deactivatesOnSwitch ? false : true);

            //change the switch sprite to switch-down
            spriteRenderer.sprite = switchDownSprite;
        }
    }
}
