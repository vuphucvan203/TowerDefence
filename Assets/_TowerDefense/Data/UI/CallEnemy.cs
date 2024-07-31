using UnityEngine;
using UnityEngine.UI;

public class CallEnemy : KennMonoBehaviour
{
    [SerializeField] protected TurnManager turnManager;
    [SerializeField] protected Button button;

    protected virtual void Update()
    {
        //if (this.turnManager.NextTurn) this.button.gameObject.SetActive(true); 
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
        this.LoadButton();
    }

    protected virtual void LoadSpawner()
    {
        if (this.turnManager != null) return;
        this.turnManager = Transform.FindAnyObjectByType<TurnManager>();
        Debug.LogWarning(transform.name + ":LoadSpawner", gameObject);
    }

    protected virtual void LoadButton()
    {
        if(this.button != null) return;
        this.button = transform.Find("Button").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadButton", gameObject);
    }

    public virtual void OnClick()
    {
        this.turnManager.SetStartTurn(true);
        this.turnManager.SetNextTurn(false);
        this.button.gameObject.SetActive(false);
    }
}
