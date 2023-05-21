using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManajer : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;
    [SerializeField] GameObject finishWindow;
    [SerializeField] TMP_Text finishText;
    [SerializeField] TMP_Text shootCountText;

    bool isBallOutside;
    bool isBallTeleporting;
    bool isGoal;
    Vector3 lastBallPosition;

    private void OnEnable()
    {
        ballController.onBallShooted.AddListener(UpdateSHootCount);
    }
    private void OnDisable()
    {
        ballController.onBallShooted.RemoveListener(UpdateSHootCount);
    }
    private void Update()
    {
        if(ballController.ShootingMode)
        {
            lastBallPosition = ballController.transform.position;
        }

        var inputActive = Input.GetMouseButton(1)
            && ballController.IsMove() == false
            && ballController.ShootingMode == false
            && isBallOutside == false;

        camController.SetInputActive(inputActive);
    }
    public void OnBallGoalEnter()
    {
        isGoal = true;
        ballController.enabled = false;
        
        finishWindow.gameObject.SetActive(true);
        finishText.text = "Finished!!!\n" + "Shoot Count: " + ballController.ShootCount;
    }

    public void OnBallOutside()
    {
        if(isGoal)
        return;

        if(isBallTeleporting == false)
            Invoke("TeleportBallLastPosition", 3);
        
        ballController.enabled = false;
        isBallOutside = true;
        isBallTeleporting = true;
    }

    public void TeleportBallLastPosition()
    {
        TeleportBall(lastBallPosition);
    }

    public void TeleportBall(Vector3 targetPosition)
    {
        var rb = ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballController.transform.position = targetPosition;
        rb.isKinematic = false;

        ballController.enabled = true;
        isBallOutside = false;
        isBallTeleporting = false;
    }
    public void MoveBallLastPosition()
    {
        ballController.transform.position = lastBallPosition;
        isBallOutside = false;
    }
    public void UpdateSHootCount(int shootCount)
    {
        shootCountText.text = shootCount.ToString();
    }
}
