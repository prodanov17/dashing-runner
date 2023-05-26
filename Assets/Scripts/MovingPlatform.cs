using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Range(1, 10)]
    public float platformSpeed = 3f;
    private bool changePos = false;


    [SerializeField] public PlatformType platformType;
    public enum PlatformType
    {
        LeftToRight,
        UpDown,
        Diagonal
    }

    void FixedUpdate()
    {
        if(platformType == PlatformType.LeftToRight)
        {
            Vector3 platformMovement = new Vector3(1f, 0f, 0f);

            transform.position += platformMovement * platformSpeed * Time.fixedDeltaTime;

            if (changePos)
            {
                platformSpeed *= -1;
                changePos = false;
            }
        }

        if (platformType == PlatformType.UpDown)
        {
            Vector3 platformMovement = new Vector3(0f, 1f, 0f);

            transform.position += platformMovement * platformSpeed * Time.fixedDeltaTime;

            if (changePos)
            {
                platformSpeed *= -1;
                changePos = false;
            }
        }

        if (platformType == PlatformType.Diagonal)
        {
            Vector3 platformMovement = new Vector3(1f, 1f, 0f);

            transform.position += platformMovement * platformSpeed * Time.fixedDeltaTime;

            if (changePos)
            {
                platformSpeed *= -1;
                changePos = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MovingPlatform")
        {
            changePos = true;
        }
    }
}
