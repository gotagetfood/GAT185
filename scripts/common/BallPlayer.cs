using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallPlayer : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField] private float maxForce = 10;

    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;

    private int score = 0;
    private Vector3 force;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = Camera.main.transform;
        Camera.main.GetComponent<BallCam>().SetTarget(transform);

        GetComponent<Health>().onDamage += OnDamage;
        GetComponent<Health>().onDeath += OnDeath;
        GetComponent<Health>().onHeal += OnHeal;
        BallGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = viewSpace * (direction * maxForce);

        Ray ray = new Ray(transform.position, Vector3.down);
        bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
        Debug.DrawRay(transform.position, ray.direction * groundRayLength);

        if (onGround && Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force);
    }

    public void AddPoints(int points) {

        score += points;
        BallGameManager.Instance.SetScore(score);   
    }

    public void OnDamage() {
        BallGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnHeal()
    {
        BallGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnDeath() {
        BallGameManager.Instance.SetGameOver();

        Destroy(gameObject);
    }
}
