using UnityEngine;

public class BasketKinect : MonoBehaviour
{
    [Tooltip(
        "Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc."
        )]
    public int playerIndex = 0;

    public bool usesHorizontalDetection;

    [Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
    public Camera foregroundCamera;

    // the KinectManager instance
    private KinectManager manager;

    // the foreground texture
    private Texture2D foregroundTex;

    // rectangle taken by the foreground texture (in pixels)
    private Rect foregroundGuiRect;
    private Rect foregroundImgRect;

    // game objects to contain the joint colliders
    private GameObject[] jointColliders;
    private int numColliders;

    private int depthImageWidth;
    private int depthImageHeight;

    public float KinectMovementSensitivity;


    private void Start()
    {
        manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            var sensorData = manager.GetSensorData();

            if (sensorData != null && sensorData.sensorInterface != null && foregroundCamera != null)
            {
                // get depth image size
                depthImageWidth = sensorData.depthImageWidth;
                depthImageHeight = sensorData.depthImageHeight;

                // calculate the foreground rectangles
                var cameraRect = foregroundCamera.pixelRect;
                var rectHeight = cameraRect.height;
                var rectWidth = cameraRect.width;

                if (rectWidth > rectHeight)
                    rectWidth = rectHeight * depthImageWidth / depthImageHeight;
                else
                    rectHeight = rectWidth * depthImageHeight / depthImageWidth;

                var foregroundOfsX = (cameraRect.width - rectWidth) / 2;
                var foregroundOfsY = (cameraRect.height - rectHeight) / 2;
                foregroundImgRect = new Rect(foregroundOfsX, foregroundOfsY, rectWidth, rectHeight);
                foregroundGuiRect = new Rect(foregroundOfsX, cameraRect.height - foregroundOfsY, rectWidth, -rectHeight);

                // create joint colliders
                numColliders = sensorData.jointCount;
                jointColliders = new GameObject[numColliders];

                for (var i = 0; i < numColliders; i++)
                {
                    var sColObjectName = (KinectInterop.JointType)i + "Collider";
                    jointColliders[i] = new GameObject(sColObjectName);
                    jointColliders[i].transform.parent = transform;

                    var collider = jointColliders[i].AddComponent<SphereCollider>();
                    collider.radius = 0.2f;
                }
            }
        }
    }

    private void Update()
    {
        // get the users texture
        if (manager && manager.IsInitialized())
        {
            foregroundTex = manager.GetUsersLblTex();
        }

        if (manager && manager.IsUserDetected() && foregroundCamera)
        {
            var userId = manager.GetUserIdByIndex(playerIndex); // manager.GetPrimaryUserID();

            // update colliders
            for (var i = 0; i < numColliders; i++)
            {
                if (manager.IsJointTracked(userId, i))
                {
                    var posCollider = manager.GetJointPosDepthOverlay(userId, i, foregroundCamera, foregroundImgRect);
                    jointColliders[i].transform.position = posCollider;
                    if (jointColliders[i].name == "SpineBaseCollider")
                    {
                        if (usesHorizontalDetection)
                        {
                            transform.position =
                                new Vector3(jointColliders[i].transform.position.x * KinectMovementSensitivity,
                                    transform.position.y, transform.position.z);
                        }
                        else
                        {
                            transform.position = new Vector3(jointColliders[i].transform.position.z * (KinectMovementSensitivity * 6) - 30f, transform.position.y,
                                transform.position.z);
                        }
                        Debug.Log(jointColliders[i].name + " is SpineBaseCollider");
                    }
                    else Debug.Log(jointColliders[i].name + " is not SpineBaseCollider");
                }
            }
        }
    }
}