namespace Anyprime;
// Token: 0x02000005 RID: 5
internal class BeamSweeperClone : MonoBehaviour
{
    // Token: 0x06000012 RID: 18 RVA: 0x000042B3 File Offset: 0x000024B3
    private void Awake()
    {
         ManyRadiances.anyprime.Log("Added BeamSweeperClone MonoBehaviour");
        this._control = base.gameObject.LocateMyFSM("Control");
    }

    // Token: 0x06000013 RID: 19 RVA: 0x000042DC File Offset: 0x000024DC
    private void Start()
    {
        this._control.GetAction<GetOwner>( "Init", 0).storeGameObject = base.gameObject;
        this._control.ChangeTransition( "Idle", "BEAM SWEEP L", "Beam Sweep R");
        this._control.ChangeTransition("Idle", "BEAM SWEEP R", "Beam Sweep L");
        this._control.ChangeTransition("Idle", "BEAM SWEEP L2", "Beam Sweep R 2");
        this._control.ChangeTransition("Idle", "BEAM SWEEP R2", "Beam Sweep L 2");
        this._control.RemoveAction("Beam Sweep L", 0);
        this._control.RemoveAction("Beam Sweep R", 0);
        this._control.RemoveAction("Beam Sweep L 2", 0);
        this._control.RemoveAction("Beam Sweep R 2", 2);
    }

    // Token: 0x04000020 RID: 32
    private PlayMakerFSM _control;
}
