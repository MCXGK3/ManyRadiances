namespace Any2;

internal class BeamSweeperClone : MonoBehaviour
{
    private PlayMakerFSM _control;

    private void Awake()
    {
        Log("Added BeamSweeperClone MonoBehaviour");
        _control = base.gameObject.LocateMyFSM("Control");
    }

    private void Start()
    {
        _control.GetAction<GetOwner>("Init", 0).storeGameObject = base.gameObject;
        _control.ChangeTransition("Idle", "BEAM SWEEP L", "Beam Sweep R");
        _control.ChangeTransition("Idle", "BEAM SWEEP R", "Beam Sweep L");
        _control.ChangeTransition("Idle", "BEAM SWEEP L2", "Beam Sweep R 2");
        _control.ChangeTransition("Idle", "BEAM SWEEP R2", "Beam Sweep L 2");
        _control.RemoveAction("Beam Sweep L", 0);
        _control.RemoveAction("Beam Sweep R", 0);
        _control.RemoveAction("Beam Sweep L 2", 0);
        _control.RemoveAction("Beam Sweep R 2", 2);
    }

    private static void Log(object obj)
    {
        Modding.Logger.Log("[AnyRadiance2.0] " + obj);
    }
}
