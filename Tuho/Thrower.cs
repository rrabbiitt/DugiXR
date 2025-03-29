using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thrower : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject Projectile;
    public float launchSpeed = 10f;

    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.01f;

    public AudioClip throwSound1;
    public AudioClip throwSound2;
    public AudioClip throwSound3;
    public AudioClip throwSound4;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void LaunchProjectile()
    {
        AudioClip[] throwSounds = { throwSound1, throwSound2, throwSound3, throwSound4 };

        int randomIndex = Random.Range(0, throwSounds.Length);

        if (throwSounds[randomIndex] != null)
        {
            audioSource.clip = throwSounds[randomIndex];
            audioSource.Play();
        }

        var _projectile = Instantiate(Projectile, launchPoint.position, launchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchSpeed * launchPoint.up;

        //DrawTrajectory(); // 포물선 그리는 함수 호출

        Destroy(gameObject);
    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
