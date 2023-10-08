
namespace Atomic
{
    // Token: 0x02000005 RID: 5
    internal class BeamSweeperClone : MonoBehaviour
    {
        // Token: 0x06000011 RID: 17 RVA: 0x0000388D File Offset: 0x00001A8D
        private void Awake()
        {
            BeamSweeperClone.Log("Added BeamSweeperClone MonoBehaviour");
            this._control = base.gameObject.LocateMyFSM("Control");
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000038B4 File Offset: 0x00001AB4
        private void Start()
        {
            this._control.GetAction<GetOwner>("Init", 0).storeGameObject = base.gameObject;
            this._control.ChangeTransition("Idle", "BEAM SWEEP L", "Beam Sweep L");
            this._control.ChangeTransition("Idle", "BEAM SWEEP R", "Beam Sweep L");
            this._control.ChangeTransition("Idle", "BEAM SWEEP L2", "Beam Sweep L");
            this._control.ChangeTransition("Idle", "BEAM SWEEP R2", "Beam Sweep L");
            this._control.ChangeTransition("Beam Sweep L", "FINISHED", "Beam Sweep R");
            this._control.ChangeTransition("Beam Sweep L 2", "FINISHED", "Beam Sweep R 2");
            this._control.ChangeTransition("Beam Sweep R 2", "FINISHED", "Idle");
            this._control.ChangeTransition("Beam Sweep R", "FINISHED", "Beam Sweep L 2");
            this._control.AddAction("Beam Sweep R", new Wait
            {
                Active = true,
                Enabled = true,
                time = 0.3f,
                finishEvent = new FsmEvent("FINISHED")
            });
            this._control.RemoveAction("Beam Sweep L", 0);
            this._control.RemoveAction("Beam Sweep R", 0);
            this._control.RemoveAction("Beam Sweep L 2", 0);
            this._control.RemoveAction("Beam Sweep R 2", 2);
            BeamSweeperClone.Log("it's double beam time");
        }

        // Token: 0x06000013 RID: 19 RVA: 0x0000369F File Offset: 0x0000189F
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Atomic Radiance]" + ((obj != null) ? obj.ToString() : null));
        }

        // Token: 0x0400001F RID: 31
        private PlayMakerFSM _control;
    }
}
