using System.Collections.Generic;
using UnityEngine;

public class TurnManager : KennMonoBehaviour
{
    [SerializeField] protected TurnSO turnSO;
    [SerializeField] protected List<Transform> lanes;
    [SerializeField] protected List<Transform> positions;
    protected float spawnTimer;
    protected float nextTurnTimer;
    protected bool startTurn;
    public bool StartTurn => startTurn;
    public virtual void SetStartTurn(bool isStart) => startTurn = isStart;
    protected int index;
    protected int turn;
    protected bool doneTurn;
    protected bool nextTurn;
    public bool NextTurn => nextTurn;
    public void SetNextTurn(bool next) => nextTurn = next;
    
    protected virtual void Update()
    {
        if (this.doneTurn)
        {
            this.spawnTimer = 0f;
            this.nextTurnTimer += Time.deltaTime;
        }
        if (this.nextTurnTimer >= 20f)
        {
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
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Minion")
        {
            MinionSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (MinionSpawner.Instance.SpawnCount >= MinionSpawner.Instance.SpawnMax)
            {
                MinionSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
            }
            this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer <= 1f) return;
            MinionSpawner.Instance.Spawn(MinionSpawner.Minion, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
        }
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Goblin")
        {
            GoblinSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (GoblinSpawner.Instance.SpawnCount >= GoblinSpawner.Instance.SpawnMax)
            {
                GoblinSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
            }
            this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer <= 1f) return;
            GoblinSpawner.Instance.Spawn(GoblinSpawner.Goblin, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
        }
        if (this.turnSO.Turns[turnOrder].Enemies[index].EnemyTypeToName() == "Enemy_Orc")
        {
            OrcSpawner.Instance.SetSpawnMax(this.turnSO.Turns[turnOrder].Enemies[index].Amount);
            if (OrcSpawner.Instance.SpawnCount >= OrcSpawner.Instance.SpawnMax)
            {
                OrcSpawner.Instance.SetSpawnCount(0);
                this.spawnTimer = 0f;
                this.index++;
            }

            this.spawnTimer += Time.deltaTime;
            if (this.spawnTimer <= 3f) return;
            OrcSpawner.Instance.Spawn(OrcSpawner.Orc, positions[0].position, this.RandomLane());
            this.spawnTimer = 0f;
        }

    }

    protected virtual CheckPoint RandomLane()
    {
        int index = Random.Range(0, this.lanes.Count);
        CheckPoint lane = this.lanes[index].GetComponent<CheckPoint>();
        return lane;
    }
}
