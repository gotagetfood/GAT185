using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    public float rotationRate = 180;
    public GameObject prefab;
    public Transform bulletOrigin;

    private void Awake()
    {
        Debug.Log("awake");
    }

    void Start()
    {
        Debug.Log("start");
    }

    void Update()
    {
        

        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * rotate;
        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")){ 
            //GetComponent<AudioSource>().Play();
            //Instantiate(prefab, transform.position, transform.rotation);
            GameObject go = Instantiate(prefab, bulletOrigin.position, transform.rotation);
        }
    }


}
