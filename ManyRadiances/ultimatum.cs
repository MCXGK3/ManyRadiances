using System;
using System.Collections;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using JetBrains.Annotations;
using Modding;
using UnityEngine;

namespace ManyRadiances
{
    // Token: 0x02000003 RID: 3
    internal class ultimatum : MonoBehaviour
    {
        // Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
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
            this._spikes = new GameObject[5];
            this._beamsweeper = GameObject.Find("Beam Sweeper");
            this._beamsweeper2 = UnityEngine.Object.Instantiate<GameObject>(this._beamsweeper);
            this._beamsweeper2.AddComponent<Ultimatum.BeamSweeperClone>();
            this._beamsweepercontrol = this._beamsweeper.LocateMyFSM("Control");
            this._beamsweeper2control = this._beamsweeper2.LocateMyFSM("Control");
            this._knight = GameObject.Find("Knight");
            this._spellControl = this._knight.LocateMyFSM("Spell Control");
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000022BC File Offset: 0x000004BC
        private void Start()
        {
            Log("Changing fight variables...");
            this._hm.hp += 500;
            this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 500;
            this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 250;
            this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 250;
            this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 250;
            this._control.GetAction<SetHP>("Scream", 7).hp = 1250;
            for (int i = 0; i < this._spikes.Length; i++)
            {
                Vector2 position = new Vector2(57f + (float)i / 2f, 153.8f);
                this._spikes[i] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikes[i].transform.SetPosition2D(position);
                this._spikes[i].LocateMyFSM("Control").SendEvent("DOWN");
            }
            this._attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.75f;
            this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 7;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 6;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 8;
            this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.4f;
            this._attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.01f;
            this._attackChoices.GetAction<Wait>("Orb Recover", 0).time = 1.25f;
            this._attackCommands.GetAction<Wait>("CW Repeat", 0).time = 0f;
            this._attackCommands.GetAction<Wait>("CCW Repeat", 0).time = 0f;
            this._attackCommands.GetAction<FloatAdd>("CW Restart", 2).add = -10f;
            this._attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add = 10f;
            this._attackCommands.RemoveAction("CW Restart", 1);
            this._attackCommands.RemoveAction("CCW Restart", 1);
            this._attackChoices.GetAction<Wait>("Beam Sweep L", 0).time = 4.05f;
            this._attackChoices.GetAction<Wait>("Beam Sweep R", 0).time = 4.05f;
            this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
            this._attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
            this._attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time = 5.05f;
            this._attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time = 5.05f;
            this._attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
            this._attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
            this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.525f;
            this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.55f;
            this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.5f;
            this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.525f;
            this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.5f;
            this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.525f;
            this._attackCommands.GetAction<SendEventByName>("EB 4", 4).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 4", 5).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 5", 5).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 5", 6).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 6", 5).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 6", 6).time = 0.6f;
            this._attackCommands.GetAction<SendEventByName>("EB 7", 8).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 7", 9).time = 0.625f;
            this._attackCommands.GetAction<SendEventByName>("EB 8", 8).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 8", 9).time = 0.625f;
            this._attackCommands.GetAction<SendEventByName>("EB 9", 8).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("EB 9", 9).time = 0.625f;
            this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.75f;
            this._attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time = 0.05f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay = 0.35f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay = 0.7f;
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay = 1.05f;
            this._attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time = 2.3f;
            this._control.GetAction<Wait>("Rage Comb", 0).time = 0.6f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.5f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 2.1f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 3.7f;
            this._attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 5f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.5f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 2.1f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 3.7f;
            this._attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 5f;
            this.AddNailWall("Nail L Sweep", "COMB R", 1.3f, 1);
            this.AddNailWall("Nail R Sweep", "COMB L", 1.3f, 1);
            this.AddNailWall("Nail L Sweep", "COMB R", 2.9f, 1);
            this.AddNailWall("Nail R Sweep", "COMB L", 2.9f, 1);
            this.AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
            this.AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
            Log("fin.");
        }

        // Token: 0x06000006 RID: 6 RVA: 0x00002B70 File Offset: 0x00000D70
        private void Update()
        {
            bool value = this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value;
            if (value)
            {
                int cwrepeats = this.CWRepeats;
                int num = cwrepeats;
                if (num != 0)
                {
                    if (num == 1)
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
                bool flag = this.CWRepeats == 2;
                if (flag)
                {
                    this.CWRepeats = 0;
                }
            }
            bool flag2 = this._beamsweepercontrol.ActiveStateName == this._beamsweeper2control.ActiveStateName;
            if (flag2)
            {
                string activeStateName = this._beamsweepercontrol.ActiveStateName;
                string a = activeStateName;
                if (!(a == "Beam Sweep L"))
                {
                    if (a == "Beam Sweep R")
                    {
                        this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP L"));
                    }
                }
                else
                {
                    this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP R"));
                }
            }
            bool flag3 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value - 250 && !this.fullSpikesSet;
            if (flag3)
            {
                this.fullSpikesSet = true;
                for (int i = 0; i < 5; i++)
                {
                    this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", i).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", i).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Wave L", i + 2).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Wave R", i + 2).sendEvent = "UP";
                }
                this._spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMin = 0.1f;
                this._spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMax = 0.1f;
                this._spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMin = 0.1f;
                this._spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMax = 0.1f;
                this._spikeMasterControl.SetState("Spike Waves");
                base.StartCoroutine(this.AddDivePunishment());
                this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 1.5f;
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
            bool flag4 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value + 30 && !this.disableBeamSet;
            if (flag4)
            {
                this.disableBeamSet = true;
                this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP L", "Orb Wait");
                this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Eye Beam Wait");
            }
            bool flag5 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !this.arena2Set;
            if (flag5)
            {
                Modding.Logger.Log("[Ultimatum Radiance] Starting Phase 2");
                this.arena2Set = true;
                this._spellControl.RemoveAction("Q2 Land", 0);
                this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 6;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 5;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 7;
                this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.6f;
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep L", 3).x = 89f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).time = 5f;
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep R", 4).x = 32.6f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).time = 5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep L", 2).x = 89f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).time = 5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep R", 3).x = 32.6f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).time = 5f;
            }
            bool flag6 = base.gameObject.transform.position.y >= 150f;
            if (flag6)
            {
                bool flag7 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 100;
                if (flag7)
                {
                    GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                    bool flag8 = !this.onePlatSet;
                    if (flag8)
                    {
                        this.onePlatSet = true;
                        Log("Removing upper right platform");
                        this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.8f;
                    }
                }
                bool flag9 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 100 - 150;
                if (flag9)
                {
                    foreach (GameObject go in this._spikes)
                    {
                        go.LocateMyFSM("Control").SendEvent("UP");
                    }
                    bool flag10 = !this.platSpikesSet;
                    if (flag10)
                    {
                        this.platSpikesSet = true;
                        GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                        base.StartCoroutine(this.AddDivePunishment());
                    }
                }
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00003587 File Offset: 0x00001787
        private IEnumerator AddDivePunishment()
        {
            yield return new WaitForSeconds(2f);
            this._spellControl.InsertAction("Q2 Land", new CallMethod
            {
                behaviour = this,
                methodName = "DivePunishment",
                parameters = new FsmVar[0],
                everyFrame = false
            }, 0);
            yield break;
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00003596 File Offset: 0x00001796
        [UsedImplicitly]
        public void DivePunishment()
        {
            Log("YOU WON'T CHEESE SPIKES IN THIS TOWN AGAIN");
            HeroController.instance.TakeDamage(base.gameObject, CollisionSide.bottom, 1, 0);
            EventRegister.SendEvent("HERO DAMAGED");
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000035C4 File Offset: 0x000017C4
        private void AddNailWall(string stateName, string eventName, float delay, int index)
        {
            this._attackChoices.InsertAction(stateName, new SendEventByName
            {
                eventTarget = this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 0).eventTarget,
                sendEvent = eventName,
                delay = delay,
                everyFrame = false
            }, index);
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00003624 File Offset: 0x00001824
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

        // Token: 0x0600000B RID: 11 RVA: 0x0000366A File Offset: 0x0000186A
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Ultimatum Radiance] " + ((obj != null) ? obj.ToString() : null));
        }

        // Token: 0x04000002 RID: 2
        private GameObject _spikeMaster;

        // Token: 0x04000003 RID: 3
        private GameObject _spikeTemplate;

        // Token: 0x04000004 RID: 4
        private GameObject[] _spikes;

        // Token: 0x04000005 RID: 5
        private GameObject _beamsweeper;

        // Token: 0x04000006 RID: 6
        private GameObject _beamsweeper2;

        // Token: 0x04000007 RID: 7
        private GameObject _knight;

        // Token: 0x04000008 RID: 8
        private HealthManager _hm;

        // Token: 0x04000009 RID: 9
        private PlayMakerFSM _attackChoices;

        // Token: 0x0400000A RID: 10
        private PlayMakerFSM _attackCommands;

        // Token: 0x0400000B RID: 11
        private PlayMakerFSM _control;

        // Token: 0x0400000C RID: 12
        private PlayMakerFSM _phaseControl;

        // Token: 0x0400000D RID: 13
        private PlayMakerFSM _spikeMasterControl;

        // Token: 0x0400000E RID: 14
        private PlayMakerFSM _beamsweepercontrol;

        // Token: 0x0400000F RID: 15
        private PlayMakerFSM _beamsweeper2control;

        // Token: 0x04000010 RID: 16
        private PlayMakerFSM _spellControl;

        // Token: 0x04000011 RID: 17
        private int CWRepeats = 0;

        // Token: 0x04000012 RID: 18
        private bool fullSpikesSet = false;

        // Token: 0x04000013 RID: 19
        private bool disableBeamSet = false;

        // Token: 0x04000014 RID: 20
        private bool arena2Set = false;

        // Token: 0x04000015 RID: 21
        private bool onePlatSet = false;

        // Token: 0x04000016 RID: 22
        private bool platSpikesSet = false;

        // Token: 0x04000017 RID: 23
        private const int fullSpikesHealth = 250;

        // Token: 0x04000018 RID: 24
        private const int onePlatHealth = 100;

        // Token: 0x04000019 RID: 25
        private const int platSpikesHealth = 150;

        // Token: 0x0400001A RID: 26
        private const float nailWallDelay = 0.8f;
    }
}
