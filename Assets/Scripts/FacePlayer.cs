using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            // Rotate the canvas to face the player
            transform.LookAt(player);

            // Optionally, reverse the direction to ensure the text isn't mirrored
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
        }
    }
}
