

namespace Dumb
{
    // Token: 0x02000004 RID: 4
    internal class BeamSweeperClone : MonoBehaviour
    {
        // Token: 0x0600000E RID: 14 RVA: 0x00004807 File Offset: 0x00002A07
        private void Awake()
        {
            Modding.Logger.Log("[Dumb Radiance]:Added BeamSweeperClone MonoBehaviour");
            this._control = base.gameObject.LocateMyFSM("Control");
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00004830 File Offset: 0x00002A30
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
            Modding.Logger.Log("[Dumb Radiance]:it's double beam time");
        }

        // Token: 0x0400002F RID: 47
        private PlayMakerFSM _control;
    }
}
