using UnityEngine;

public class GameplaySceneController : SceneController
{
    [Header("Services")]
    [SerializeField] private GameplayServices GameplayServices;
    private GameplayStateMachine stateMachine;

    private BaseState BaseState;
    private BuildState BuildState;
    private DemolishState DemolishState;

    // Overrides ============================================
    protected override void OnInit()
    {
        base.OnInit();

        GameplayServices.Init(this, Services);

        InitStates();
    }
    protected override void OnEnter()
    {
        EnterBaseMode();
    }
    protected override void OnExit()
    {

    }

    // State Initiation ============================================
    private void InitStates()
    {
        stateMachine = new();
        BaseState = new BaseState(Services, GameplayServices);
        BuildState = new BuildState(this, Services, GameplayServices);
        DemolishState = new DemolishState(this, Services, GameplayServices);
    }


    // State Transit ============================================
    public void EnterBuildMode()
    {
        stateMachine.SetState(BuildState);
    }

    public void EnterDemolishMode()
    {
        stateMachine.SetState(DemolishState);
    }

    public void EnterBaseMode()
    {
        stateMachine.SetState(BaseState);
    }

    // State Machine
    private void Update()
    {
        GameplayServices.CameraController.Update();
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
