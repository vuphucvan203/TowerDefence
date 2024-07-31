using System.Collections.Generic;
using UnityEngine;

public class TurnManager : KennMonoBehaviour
{
    [SerializeField] protected TurnSO turnSO;
    [SerializeField] protected List<Transform> lanes;
    [SerializeField] protected List<Transform> positions;
    protected float spawnTimer;
    [SerializeField] protected float nextTurnTimer;
    [SerializeField] protected float timer;
    protected bool startTurn;
    public bool StartTurn => startTurn;
    public virtual void SetStartTurn(bool isStart) => startTurn = isStart;
    protected int index;
    [SerializeField] protected int turn;
    protected bool doneTurn;
    protected bool nextTurn;
    public bool NextTurn => nextTurn;
    public void SetNextTurn(bool next) => nextTurn = next;
    
    protected virtual void Update()
    {
        if (this.turn > this.turnSO.Turns.Count - 1) return;
        if (this.doneTurn)
        {
            this.spawnTimer = 0f;
            this.nextTurnTimer += Time.deltaTime;
        }
        float delay = this.turnSO.Turns[turn].Delay;
        if (this.nextTurnTimer >= delay && delay > 0)
        {
            this.startTurn = true;
            this.doneTurn = false;
            this.nextTurn = true;
            this.nextTurnTimer = 0f;
        }
        if (!this.startTurn) return;
        this.SpawnEnemy(turn);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTurnSO();
        this.LoadLanes();
        this.LoadPositions();
    }

    protected virtual void LoadTurnSO()
    {
        if (this.turnSO != null) return;
        this.turnSO = Resources.Load<TurnSO>("Turn");
        Debug.LogWarning(transform.name + ": LoadTurnSO", gameObject);
    }

    protected virtual void LoadLanes()
    {
        if (this.lanes.Count > 0) return;
        Transform laneRoad = transform.Find("LaneRoad").GetComponent<Transform>();
        for(int i = 0; i < laneRoad.childCount; i++)
        {
            Transform lane = laneRoad.GetChild(i);
            this.lanes.Add(lane);
        }
        Debug.LogWarning(transform.name + ": LoadLanes", gameObject);
    }

    protected virtual void LoadPositions()
    {
        if (this.positions.Count > 0) return;
        Transform spawnPositions = transform.Find("Positions").GetComponent<Transform>();
        for (int i = 0; i < spawnPositions.childCount; i++)
        {
            Transform pos = spawnPositions.GetChild(i);
            this.positions.Add(pos);
        }
        Debug.LogWarning(transform.name + ": LoadPositions", gameObject);
    }

    protected virtual void SpawnEnemy(int turnOrder)
    {
        if (turnOrder > this.turnSO.Turns.Count - 1) return;
        int enemyCount = this.turnSO.Turns[turnOrder].Enemies.Count;
        if (this.index > enemyCount - 1)
        {
            this.startTurn = false;
            this.turn++;
            this.index = 0;
            this.doneTurn = true;
        }
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Wolf")
        {
            float delay = this.turnSO.Turns[turnOrder].Enemies[index].Delay;
            if(delay > 0 && WolfSpawner.Instance.SpawnCount == 0) this.timer += Time.deltaTime;
            if (this.timer < delay && WolfSpawner.Instance.SpawnCount == 0) return;
            WolfSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (WolfSpawner.Instance.SpawnCount >= WolfSpawner.Instance.SpawnMax)
            {
                WolfSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
                this.timer = 0f;
                return;
            }

            if (WolfSpawner.Instance.SpawnCount > 0) this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer < 2f && WolfSpawner.Instance.SpawnCount > 0) return;
            WolfSpawner.Instance.Spawn(WolfSpawner.Wolf, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
            this.timer = 0f;
        }
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Goblin")
        {
            float delay = this.turnSO.Turns[turnOrder].Enemies[index].Delay;
            if (delay > 0 && GoblinSpawner.Instance.SpawnCount == 0) this.timer += Time.deltaTime;
            if (this.timer < delay && GoblinSpawner.Instance.SpawnCount == 0) return;
            GoblinSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (GoblinSpawner.Instance.SpawnCount >= GoblinSpawner.Instance.SpawnMax)
            {
                GoblinSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
                this.timer = 0f;
                return;
            }
            if (GoblinSpawner.Instance.SpawnCount > 0) this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer < 2f && GoblinSpawner.Instance.SpawnCount > 0) return;
            GoblinSpawner.Instance.Spawn(GoblinSpawner.Goblin, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
            this.timer = 0f;
        }
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Slime")
        {
            float delay = this.turnSO.Turns[turnOrder].Enemies[index].Delay;
            if (delay > 0 && SlimeSpawner.Instance.SpawnCount == 0) this.timer += Time.deltaTime;
            if (this.timer < delay && SlimeSpawner.Instance.SpawnCount == 0) return;
            
            SlimeSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (SlimeSpawner.Instance.SpawnCount >= SlimeSpawner.Instance.SpawnMax)
            {
                SlimeSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
                this.timer = 0f;
                return;
            }

            if (SlimeSpawner.Instance.SpawnCount > 0) this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer < 2f && SlimeSpawner.Instance.SpawnCount > 0) return;
            SlimeSpawner.Instance.Spawn(SlimeSpawner.Slime, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
            this.timer = 0f;
        }

        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Bee")
        {
            float delay = this.turnSO.Turns[turnOrder].Enemies[index].Delay;
            if (delay > 0 && BeeSpawner.Instance.SpawnCount == 0) this.timer += Time.deltaTime;
            if (this.timer < delay && BeeSpawner.Instance.SpawnCount == 0) return;
            BeeSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (BeeSpawner.Instance.SpawnCount >= BeeSpawner.Instance.SpawnMax)
            {
                BeeSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
                this.timer = 0f;
                return;
            }

            if (BeeSpawner.Instance.SpawnCount > 0) this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer < 2f && BeeSpawner.Instance.SpawnCount > 0) return;
            BeeSpawner.Instance.Spawn(BeeSpawner.Bee, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
            this.timer = 0f;
        }
    }

    protected virtual CheckPoint RandomLane()
    {
        int index = Random.Range(0, this.lanes.Count);
        CheckPoint lane = this.lanes[index].GetComponent<CheckPoint>();
        return lane;
    }
}
