using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject[] tntBoxes;
    public GameObject affectedPlatform;
    public float explosionForce = 5f;
    public float rotationTorque = 10f;
    
    public void OnBoomerangHit()
    {
        Debug.Log("Boomerang Hit");
        Explode();
    }

    void Explode()
    {
        if (affectedPlatform != null)
        {
            SplitPlatform(affectedPlatform);
        }
        Destroy(this.gameObject);
        foreach (var box in tntBoxes)
        {
            Destroy(box);
        }
    }

    void SplitPlatform(GameObject platform)
    {
        Vector3 originalScale = platform.transform.localScale;
        Vector3 originalPosition = platform.transform.position;
        
        GameObject leftHalf = Instantiate(platform, originalPosition, Quaternion.identity);
        leftHalf.transform.localScale = new Vector3(originalScale.x/2, originalScale.y, originalScale.z);
        leftHalf.transform.position = originalPosition + new Vector3(-originalScale.x/4, 0, 0);
        
        GameObject rightHalf = Instantiate(platform, originalPosition, Quaternion.identity);
        rightHalf.transform.localScale = new Vector3(originalScale.x / 2, originalScale.y, originalScale.z);
        rightHalf.transform.position = originalPosition + new Vector3(originalScale.x / 4, 0, 0);

        Rigidbody2D rbLeft = leftHalf.AddComponent<Rigidbody2D>();
        Rigidbody2D rbRight = rightHalf.AddComponent<Rigidbody2D>();
        
        rbLeft.AddForce(new Vector2(-explosionForce, explosionForce), ForceMode2D.Impulse);
        rbRight.AddForce(new Vector2(explosionForce, -explosionForce), ForceMode2D.Impulse);

        rbLeft.AddTorque(rotationTorque, ForceMode2D.Impulse);
        rbRight.AddTorque(-rotationTorque, ForceMode2D.Impulse);

        Destroy(platform);
    }
}
