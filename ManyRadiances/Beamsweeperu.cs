
namespace Ultimatum
{
    // Token: 0x02000002 RID: 2
    internal class BeamSweeperClone: MonoBehaviour
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            BeamSweeperClone.Log("Added BeamSweeperClone MonoBehaviour");
            this._control = base.gameObject.LocateMyFSM("Control");
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
        private void Start()
        {
            this._control.GetAction<GetOwner>( "Init", 0).storeGameObject = base.gameObject;
            this._control.ChangeTransition("Idle", "BEAM SWEEP L", "Beam Sweep R");
            this._control.ChangeTransition("Idle", "BEAM SWEEP R", "Beam Sweep L");
            this._control.ChangeTransition("Idle", "BEAM SWEEP L2", "Beam Sweep R 2");
            this._control.ChangeTransition("Idle", "BEAM SWEEP R2", "Beam Sweep L 2");
            this._control.RemoveAction("Beam Sweep L", 0);
            this._control.RemoveAction("Beam Sweep R", 0);
            this._control.RemoveAction("Beam Sweep L 2", 0);
            this._control.RemoveAction("Beam Sweep R 2", 2);
            BeamSweeperClone.Log("it's double beam time");
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002162 File Offset: 0x00000362
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Ultimatum Radiance] " + obj);
        }

        // Token: 0x04000001 RID: 1
        private PlayMakerFSM _control;
    }
}
