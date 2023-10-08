using System;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using JetBrains.Annotations;
using Modding;
using UnityEngine;

namespace ManyRadiances
{
    // Token: 0x02000002 RID: 2
    internal class atomic : MonoBehaviour
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            Log("Added AbsRad MonoBehaviour");
            this._hm = base.gameObject.GetComponent<HealthManager>();
            this._attackChoices = base.gameObject.LocateMyFSM("Attack Choices");
            this._attackCommands = base.gameObject.LocateMyFSM("Attack Commands");
            this._control = base.gameObject.LocateMyFSM("Control");
            this._phaseControl = base.gameObject.LocateMyFSM("Phase Control");
            this._spikeMaster = GameObject.Find("Spike Control");
            this._spikeMasterControl = this._spikeMaster.LocateMyFSM("Control");
            this._spikeTemplate = GameObject.Find("Radiant Spike");
            this._beamsweeper = GameObject.Find("Beam Sweeper");
            this._beamsweeper2 = UnityEngine.Object.Instantiate<GameObject>(this._beamsweeper);
            this._beamsweeper2.AddComponent<Atomic.BeamSweeperClone>();
            this._beamsweepercontrol = this._beamsweeper.LocateMyFSM("Control");
            this._beamsweeper2control = this._beamsweeper2.LocateMyFSM("Control");
            this._knight = GameObject.Find("Knight");
            this._spellControl = this._knight.LocateMyFSM("Spell Control");
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002188 File Offset: 0x00000388
        private void Start()
        {
            Log("Changing fight variables...");
            this._hm.hp += 2500;
            this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 2500;
            this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 2000;
            this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 2000;
            this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 2000;
            this._control.GetAction<SetHP>("Scream", 7).hp = 3000;
            this._spikeClone = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeClone.transform.SetPositionX(58f);
            this._spikeClone.transform.SetPositionY(153.8f);
            this._spikeClone2 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeClone2.transform.SetPositionX(57.5f);
            this._spikeClone2.transform.SetPositionY(153.8f);
            this._spikeClone3 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeClone3.transform.SetPositionX(57f);
            this._spikeClone3.transform.SetPositionY(153.8f);
            this._spikeClone4 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeClone4.transform.SetPositionX(58.5f);
            this._spikeClone4.transform.SetPositionY(153.8f);
            this._spikeClone5 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeClone5.transform.SetPositionX(59f);
            this._spikeClone5.transform.SetPositionY(153.8f);
            this._spikeClone.LocateMyFSM("Control").SendEvent("DOWN");
            this._spikeClone2.LocateMyFSM("Control").SendEvent("DOWN");
            this._spikeClone3.LocateMyFSM("Control").SendEvent("DOWN");
            this._spikeClone4.LocateMyFSM("Control").SendEvent("DOWN");
            this._spikeClone5.LocateMyFSM("Control").SendEvent("DOWN");
            this._attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.2f;
            this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 12;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 6;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 8;
            this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.4f;
            this._attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.01f;
            this._attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.45f;
            this._attackCommands.GetAction<Wait>("CW Repeat", 0).time = 0f;
            this._attackCommands.GetAction<Wait>("CCW Repeat", 0).time = 0f;
            this._attackCommands.GetAction<FloatAdd>("CW Restart", 2).add = -10f;
            this._attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add = 10f;
            this._attackCommands.RemoveAction("CW Restart", 1);
            this._attackCommands.RemoveAction("CCW Restart", 1);
            this._attackChoices.GetAction<Wait>("Beam Sweep L", 0).time = 2.05f;
            this._attackChoices.GetAction<Wait>("Beam Sweep R", 0).time = 2.05f;
            this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
            this._attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
            this._attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time = 1.05f;
            this._attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time = 1.05f;
            this._attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
            this._attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
            this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.4f;
            this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 4", 4).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 4", 5).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 5", 5).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 5", 6).time = 0.4f;
            this._attackCommands.GetAction<SendEventByName>("EB 6", 5).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 6", 6).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 7", 8).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 7", 9).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 8", 8).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 8", 9).time = 0.4f;
            this._attackCommands.GetAction<SendEventByName>("EB 9", 8).delay = 0.1f;
            this._attackCommands.GetAction<Wait>("EB 9", 9).time = 0.6f;

            //修改楼梯激光代码
            //this._attackCommands.GetAction<SendEventByName>("Aim", 8).delay = 0f;
            //this._attackCommands.GetAction<SendEventByName>("Aim",9).delay = 0f;



            this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.05f;
            this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.03f;
            this._attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time = 0.19f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay = 0.1f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay = 0.3f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay = 0.3f;
            this._attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time = 0.3f;
            this._control.GetAction<Wait>("Rage Comb", 0).time = 0.6f;
            Log("fin.");
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000029F4 File Offset: 0x00000BF4
        private void Update()
        {
            bool value = this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value;
            if (value)
            {
                int cwrepeats = this.CWRepeats;
                bool flag = cwrepeats != 0;
                if (flag)
                {
                    bool flag2 = cwrepeats == 1;
                    if (flag2)
                    {
                        this.CWRepeats = 2;
                    }
                }
                else
                {
                    this.CWRepeats = 1;
                    this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                }
            }
            else
            {
                bool flag3 = this.CWRepeats == 2;
                if (flag3)
                {
                    this.CWRepeats = 0;
                }
            }
            bool flag4 = this._beamsweepercontrol.ActiveStateName == this._beamsweeper2control.ActiveStateName;
            if (flag4)
            {
                string activeStateName = this._beamsweepercontrol.ActiveStateName;
                bool flag5 = !(activeStateName == "Beam Sweep L");
                if (flag5)
                {
                    bool flag6 = activeStateName == "Beam Sweep R";
                    if (flag6)
                    {
                        this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP L"));
                    }
                }
                else
                {
                    this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP R"));
                }
            }
            bool flag7 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value - 500 && !this.fullSpikesSet;
            if (flag7)
            {
                this.fullSpikesSet = true;
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 0).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 1).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 2).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 3).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 4).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 0).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 1).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 2).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 3).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 4).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave L", 2).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave L", 3).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave L", 4).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave L", 5).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave L", 6).sendEvent = "UP";
                this._spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMin = 0.1f;
                this._spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMax = 0.1f;
                this._spikeMasterControl.GetAction<SendEventByName>("Wave R", 2).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave R", 3).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave R", 4).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave R", 5).sendEvent = "UP";
                this._spikeMasterControl.GetAction<SendEventByName>("Wave R", 6).sendEvent = "UP";
                this._spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMin = 0.1f;
                this._spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMax = 0.1f;
                this._spikeMasterControl.SetState("Spike Waves");
                this._spellControl.InsertAction("Q2 Land", new CallMethod
                {
                    behaviour = this,
                    methodName = "DivePunishment",
                    parameters = new FsmVar[0],
                    everyFrame = false
                }, 0);
                this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.75f;
                this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 2;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 1;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 3;
                this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 1", 2).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 1", 3).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 1", 8).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.85f;
                this._attackCommands.GetAction<Wait>("EB 1", 10).time = 1.92f;
                this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 2", 3).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 2", 4).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 2", 8).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.85f;
                this._attackCommands.GetAction<Wait>("EB 2", 10).time = 1.2f;
                this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 3", 3).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 3", 4).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 3", 8).delay = 0.75f;
                this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.85f;
                this._attackCommands.GetAction<Wait>("EB 3", 10).time = 1.2f;
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL L SWEEP", "Beam Sweep L");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL R SWEEP", "Beam Sweep L");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL FAN", "Eye Beam Wait");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL TOP SWEEP", "Orb Wait");
            }
            bool flag8 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !this.arena2Set;
            if (flag8)
            {
                Modding.Logger.Log("[Atomic Radiance] Starting Phase 2");
                this.arena2Set = true;
                this._spellControl.RemoveAction("Q2 Land", 0);
                this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 7;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 6;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 8;
                this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.5f;
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep L", 3).x = 190f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).time = 5f;
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep R", 4).x = 120.6f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).time = 5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep L", 2).x = 190f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).time = 5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep R", 3).x = 132.6f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).time = 5f;
            }
            bool flag9 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 1000;
            if (flag9)
            {
                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                bool flag10 = !this.onePlatSet;
                if (flag10)
                {
                    this.onePlatSet = true;
                    Log("Removing upper right platform");
                    this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.4f;
                }
            }
            bool flag11 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 1000 - 1000;
            if (flag11)
            {
                this._spikeClone.LocateMyFSM("Control").SendEvent("UP");
                this._spikeClone2.LocateMyFSM("Control").SendEvent("UP");
                this._spikeClone3.LocateMyFSM("Control").SendEvent("UP");
                this._spikeClone4.LocateMyFSM("Control").SendEvent("UP");
                this._spikeClone5.LocateMyFSM("Control").SendEvent("UP");
                bool flag12 = !this.platSpikesSet;
                if (flag12)
                {
                    this.platSpikesSet = true;
                    GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                    this._spellControl.InsertAction("Q2 Land", new CallMethod
                    {
                        behaviour = this,
                        methodName = "DivePunishment",
                        parameters = new FsmVar[0],
                        everyFrame = false
                    }, 0);
                }
            }
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000362A File Offset: 0x0000182A
        [UsedImplicitly]
        public void DivePunishment()
        {
            Log("YOU WON'T CHEESE SPIKES IN THIS TOWN AGAIN");
            HeroController.instance.TakeDamage(base.gameObject, CollisionSide.bottom, 1, 0);
            EventRegister.SendEvent("HERO DAMAGED");
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00003658 File Offset: 0x00001858
        private static FsmEvent GetFsmEventByName(PlayMakerFSM fsm, string eventName)
        {
            foreach (FsmEvent fsmEvent in fsm.FsmEvents)
            {
                bool flag = fsmEvent.Name == eventName;
                if (flag)
                {
                    return fsmEvent;
                }
            }
            return null;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000369F File Offset: 0x0000189F
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Atomic Radiance] " + ((obj != null) ? obj.ToString() : null));
        }

        // Token: 0x04000001 RID: 1
        private GameObject _spikeMaster;

        // Token: 0x04000002 RID: 2
        private GameObject _spikeTemplate;

        // Token: 0x04000003 RID: 3
        private GameObject _spikeClone;

        // Token: 0x04000004 RID: 4
        private GameObject _spikeClone2;

        // Token: 0x04000005 RID: 5
        private GameObject _spikeClone3;

        // Token: 0x04000006 RID: 6
        private GameObject _spikeClone4;

        // Token: 0x04000007 RID: 7
        private GameObject _spikeClone5;

        // Token: 0x04000008 RID: 8
        private GameObject _beamsweeper;

        // Token: 0x04000009 RID: 9
        private GameObject _beamsweeper2;

        // Token: 0x0400000A RID: 10
        private GameObject _knight;

        // Token: 0x0400000B RID: 11
        private HealthManager _hm;

        // Token: 0x0400000C RID: 12
        private PlayMakerFSM _attackChoices;

        // Token: 0x0400000D RID: 13
        private PlayMakerFSM _attackCommands;

        // Token: 0x0400000E RID: 14
        private PlayMakerFSM _control;

        // Token: 0x0400000F RID: 15
        private PlayMakerFSM _phaseControl;

        // Token: 0x04000010 RID: 16
        private PlayMakerFSM _spikeMasterControl;

        // Token: 0x04000011 RID: 17
        private PlayMakerFSM _beamsweepercontrol;

        // Token: 0x04000012 RID: 18
        private PlayMakerFSM _beamsweeper2control;

        // Token: 0x04000013 RID: 19
        private PlayMakerFSM _spellControl;

        // Token: 0x04000014 RID: 20
        private int CWRepeats;

        // Token: 0x04000015 RID: 21
        private bool fullSpikesSet;

        // Token: 0x04000016 RID: 22
        private bool arena2Set;

        // Token: 0x04000017 RID: 23
        private bool onePlatSet;

        // Token: 0x04000018 RID: 24
        private bool platSpikesSet;

        // Token: 0x04000019 RID: 25
        private const int fullSpikesHealth = 500;

        // Token: 0x0400001A RID: 26
        private const int onePlatHealth = 1000;

        // Token: 0x0400001B RID: 27
        private const int platSpikesHealth = 1000;
    }
}
