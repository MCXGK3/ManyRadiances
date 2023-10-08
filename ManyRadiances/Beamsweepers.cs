namespace Supernova;

// Token: 0x02000004 RID: 4
internal class BeamSweeperClone : MonoBehaviour
{
    // Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
    private void Awake()
    {
        BeamSweeperClone.Log("Added BeamSweeperClone MonoBehaviour");
        this._control = base.gameObject.LocateMyFSM("Control");
    }

    // Token: 0x06000004 RID: 4 RVA: 0x00002090 File Offset: 0x00000290
    private void Start()
    {
        this._control.GetAction<GetOwner>("Init", 0).storeGameObject = base.gameObject;
        this._control.ChangeTransition("Idle", "BEAM SWEEP L", "Beam Sweep R");
        this._control.ChangeTransition("Idle", "BEAM SWEEP R", "Beam Sweep L");
        this._control.ChangeTransition("Idle", "BEAM SWEEP L2", "Beam Sweep R 2");
        this._control.ChangeTransition("Idle", "BEAM SWEEP R2", "Beam Sweep L 2");
        this._control.RemoveAction("Beam Sweep L", 0);
        this._control.RemoveAction("Beam Sweep R", 0);
        this._control.RemoveAction("Beam Sweep L 2", 0);
        this._control.RemoveAction("Beam Sweep R 2", 2);
    }

    // Token: 0x06000005 RID: 5 RVA: 0x00002173 File Offset: 0x00000373
    private static void Log(object obj)
    {
        Modding.Logger.Log("[Supernova BeamSweeperClone] " + ((obj != null) ? obj.ToString() : null));
    }

    // Token: 0x04000002 RID: 2
    private PlayMakerFSM _control;
}
