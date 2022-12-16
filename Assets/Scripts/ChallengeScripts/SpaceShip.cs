using UnityEngine;

public class SpaceShip : Vehicle
{
    protected override void Update()
    {
        base.Update();

        MoveSideways();
    }

    private void MoveSideways()
    {
        float inputValue = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(inputValue, 0, 0) * moveSpeed * Time.deltaTime);
    }
}
