using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float maxAngleDifference = 45f; // 人和狗之间的最大角度差
    public float fallDuration = 2f; // 僵持时间，超过这个时间就会摔倒
    public float maxRopeLength = 2f; // 绳子最大长度
    public float runFallDuration = 4f; // 人跑动超过这个时间会摔倒
    public float dogRunFallDuration = 2f; // 狗跑动超过这个时间会摔倒

    private Transform dogTransform;
    private CharacterController characterController;

    private float fallStartTime;
    private float runStartTime;
    private bool isStruggling = false;
    private bool isRunning = false;
    private bool isDogRunning = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        dogTransform = GameObject.Find("Dog").transform; // 从场景中获取狗的位置
    }

    private void Update()
    {
        Vector3 playerDirection = characterController.velocity.normalized;
        Vector3 dogDirection = dogTransform.forward;
        float angleDifference = Vector3.Angle(playerDirection, dogDirection);
        float distanceToDog = Vector3.Distance(transform.position, dogTransform.position);

        if (characterController.velocity.magnitude > 0.1f) // 确保人在移动
        {
            if (angleDifference > maxAngleDifference)
            {
                if (!isStruggling)
                {
                    isStruggling = true;
                    fallStartTime = Time.time;
                }
                else if (Time.time - fallStartTime >= fallDuration)
                {
                    OnFall();
                }
            }
            else if (distanceToDog > maxRopeLength)
            {
                if (!isStruggling)
                {
                    isStruggling = true;
                    fallStartTime = Time.time;
                }
                else if (Time.time - fallStartTime >= fallDuration)
                {
                    OnFall();
                }
            }
            else
            {
                isStruggling = false;
            }

            if (!isRunning)
            {
                isRunning = true;
                runStartTime = Time.time;
            }
            else if (Time.time - runStartTime >= runFallDuration)
            {
                OnFall();
            }
        }
        else
        {
            isStruggling = false;
            isRunning = false;
        }

        // if (dogTransform.GetComponent<DogController>().isRunning) // 假设狗有一个DogController脚本来判断是否在跑步
        // {
        //     if (!isDogRunning)
        //     {
        //         isDogRunning = true;
        //         if (!isRunning)
        //         {
        //             OnFall();
        //         }
        //         else
        //         {
        //             runStartTime = Time.time; // 重置跑步计时
        //         }
        //     }
        //     else if (Time.time - runStartTime >= dogRunFallDuration)
        //     {
        //         OnFall();
        //     }
        // }
        // else
        // {
        //     isDogRunning = false;
        // }
    }

    private void OnFall()
    {
        Debug.Log("人摔倒了！");
        ResetLevel();
    }

    private void ResetLevel()
    {
        // 重置游戏逻辑
    }
}
