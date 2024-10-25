using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人移动控制器
/// </summary>
public class PeopleMoveController : MonoBehaviour
{
    public float moveSpeed = 5f;           // 移动速度
    public float rotationSpeed = 100f;     // 旋转速度
    public float stepHeight = 0.3f;        // 可跨越的台阶高度
    public float stepSmooth = 0.1f;        // 爬台阶的平滑系数
    public LayerMask groundLayer;          // 检测地面或楼梯的图层

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;          // 防止角色旋转
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();        // 获取输入
        HandleAnimation(); // 控制动画
    }

    void FixedUpdate()
    {
        HandleRotation();  // 控制旋转
        MoveCharacter();   // 控制移动
        HandleStepClimb(); // 爬台阶检测
    }

    /// <summary>
    /// 获取键盘输入
    /// </summary>
    private void GetInput()
    {
        movementInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) movementInput += transform.forward;
        if (Input.GetKey(KeyCode.S)) movementInput -= transform.forward;
    }

    /// <summary>
    /// 控制动画播放
    /// </summary>
    private void HandleAnimation()
    {
        bool isWalking = movementInput != Vector3.zero;
        animator.SetTrigger("walk");
    }

    /// <summary>
    /// 使用 Rigidbody.velocity 移动角色
    /// </summary>
    private void MoveCharacter()
    {
        Vector3 velocity = movementInput * moveSpeed;
        velocity.y = rb.velocity.y; // 保留垂直方向上的速度（如重力作用）
        rb.velocity = velocity;
    }

    /// <summary>
    /// 控制角色旋转
    /// </summary>
    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // 只在有按键时进行旋转
            float rotationDirection = 0f;

            if (Input.GetKey(KeyCode.A)) rotationDirection = -1f; // 左旋转
            if (Input.GetKey(KeyCode.D)) rotationDirection = 1f;  // 右旋转

            // 计算目标旋转
            Quaternion targetRotation = Quaternion.Euler(0, rotationDirection * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= targetRotation; // 旋转角色
        }
    }

    /// <summary>
    /// 处理爬台阶逻辑
    /// </summary>
    private void HandleStepClimb()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.7f; // 增加检测高度

        // 检测前方台阶
        if (Physics.Raycast(rayStart, transform.forward, out hit, 1f, groundLayer))
        {
            float stepDifference = hit.point.y - transform.position.y;

            // 仅当台阶高度在允许范围内时，才改变位置
            if (stepDifference > 0 && stepDifference <= stepHeight)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, stepSmooth)); // 平滑移动
                return; // 直接返回，避免后续重力处理影响
            }
        }

        // 检测下方地面，避免踩空气
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 1f, groundLayer))
        {
            rb.useGravity = false; // 有地面，禁用重力
            rb.MovePosition(new Vector3(transform.position.x, hit.point.y, transform.position.z)); // 确保与地面对齐
        }
        else
        {
            rb.useGravity = true; // 没有地面，启用重力
        }
    }
}