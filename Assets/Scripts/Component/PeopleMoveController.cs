using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人移动控制器
/// </summary>
public class PeopleMoveController : MonoBehaviour
{
    public float moveSpeed = 2f;           // 移动速度
    public float rotationSpeed = 100f;     // 旋转速度
    public float stepHeight = 0.3f;        // 可跨越的台阶高度
    public float stepSmooth = 0.1f;        // 平滑爬台阶速度
    public float recoveryTime = 2f;        // 摔倒后的恢复时间

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = true; // 防止刚体自动旋转
        rb.useGravity = true;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("walk");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("fall");
        }
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        // 检测前后移动键（W/S），仅在有前后按键时生成移动向量
        if (Input.GetKey(KeyCode.W)) movement += transform.forward;
        if (Input.GetKey(KeyCode.S)) movement -= transform.forward;

        if (movement != Vector3.zero)
        {
            HandleStepClimb(); // 只有移动时检查台阶
            rb.AddForce(movement.normalized * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        //else
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0); // 停止水平移动
        //}
    }

    private void HandleRotation()
    {
        float rotation = 0f;

        // 检测旋转按键（A/D），仅用于旋转，不影响移动
        if (Input.GetKey(KeyCode.A)) rotation = -1f;
        if (Input.GetKey(KeyCode.D)) rotation = 1f;

        if (rotation != 0)
        {
            transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
        }
    }

    private void HandleStepClimb()
    {
        RaycastHit hit;

        // 向前方发射射线检测台阶
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, 1f))
        {
            // 计算台阶的高度差
            float stepDifference = hit.point.y - transform.position.y;

            // 如果在可爬升的高度内并且前方有台阶
            if (stepDifference > 0 && stepDifference <= stepHeight)
            {
                // 计算目标位置
                Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);

                // 使用 Rigidbody.MovePosition 移动
                rb.MovePosition(targetPosition);
            }
        }
    }
}
