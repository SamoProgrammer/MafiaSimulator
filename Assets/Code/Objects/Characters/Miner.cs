using UnityEngine;

public class Miner : Character
{
    private float workTimer;
    [SerializeField] float workTime = 7;
    public MinerStates minerState = MinerStates.Mining;
    public Building mine;

    protected override void PerformUpdate()
    {
        Mining();
    }

    public void Mining()
    {
        if (mine.money > 0)
        {
            workTimer += Time.deltaTime;
            if (workTimer > workTime)
            {
                money += 10;
                mine.money -= 10;
                workTimer = 0;
            }
        }
    }
}
