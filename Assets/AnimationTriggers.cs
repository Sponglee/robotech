using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    public GameObject stepFx;

    public Transform[] LeftFeet;
    public Transform[] RightFeet;

    private int leftFeetIndex;
    private int rightFeetIndex;

    public void StepTriggerredLeft()
    {
        Instantiate(stepFx, LeftFeet[leftFeetIndex].position, Quaternion.identity);

        leftFeetIndex++;
        if (leftFeetIndex >= LeftFeet.Length)
        {
            leftFeetIndex = 0;
        }
    }

    public void StepTriggerredRight()
    {
        Instantiate(stepFx, RightFeet[rightFeetIndex].position, Quaternion.identity);

        rightFeetIndex++;
        if (rightFeetIndex >= RightFeet.Length)
        {
            rightFeetIndex = 0;
        }
    }
}