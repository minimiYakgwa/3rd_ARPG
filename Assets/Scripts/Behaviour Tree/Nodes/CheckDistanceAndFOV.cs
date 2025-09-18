using UnityEngine;
using Unity.Behavior;

[System.Serializable]
[NodeDescription(
    name: "Check Distance and FOV",
    story: "Check Distance between two objects and if the target is in the field of view",
    category: "custom"
)]


public class CheckDistanceAndFOV : Condition
{
    public override bool IsTrue()
    {
        return false;
    }
}
