using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int pointValue = 1; //Value to increase points by
    public float rotateSpeed = 5.0f; // controls speed of rotation
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // IF the "other" collider hits this game object, check tag to see if it is "Player"
            Destroy(this.gameObject); //Destroy pickup 

    } //OnTriggerEnter is called when a Trigger collider collides with another collider

    private void OnDestroy()
    {
        GameManager.Instance.UpdateScore(pointValue);
        Destroy(this.gameObject);//Destroy pickup 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * rotateSpeed * Time.deltaTime);//Time.deltaTime makes the object frame dependent
    }
}
