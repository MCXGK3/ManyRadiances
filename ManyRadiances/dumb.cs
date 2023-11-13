using System.Collections;
using GlobalEnums;
using JetBrains.Annotations;


namespace ManyRadiances
{
    // Token: 0x02000002 RID: 2
    public class dumb : MonoBehaviour
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            Log("Added AbsRad MonoBehaviour");
            this._hm = base.gameObject.GetComponent<HealthManager>();
            this._fireballTemplate = base.gameObject.LocateMyFSM("Attack Commands").GetAction<SpawnObjectFromGlobalPool>("Spawn Fireball", 1).gameObject.Value;
            this._attackChoices = base.gameObject.LocateMyFSM("Attack Choices");
            this._attackCommands = base.gameObject.LocateMyFSM("Attack Commands");
            this._control = base.gameObject.LocateMyFSM("Control");
            this._phaseControl = base.gameObject.LocateMyFSM("Phase Control");
            this._spikeMaster = GameObject.Find("Spike Control");
            this._spikeMasterControl = this._spikeMaster.LocateMyFSM("Control");
            this._spikeTemplate = GameObject.Find("Radiant Spike");
            this._spikes = new GameObject[5];
            this._spikeset1 = new GameObject[5];
            this._spikeset2 = new GameObject[9];
            this._spikeset3 = new GameObject[9];
            this._spikeset4 = new GameObject[9];
            this._spikeset5 = new GameObject[5];
            this._spikeset6 = new GameObject[5];
            this._spikeset7 = new GameObject[5];
            this._beamsweeper = GameObject.Find("Beam Sweeper");
            this._beamsweeper2 = UnityEngine.Object.Instantiate<GameObject>(this._beamsweeper);
            this._beamsweeper2.AddComponent<Dumb.BeamSweeperClone>();
            this._beamsweepercontrol = this._beamsweeper.LocateMyFSM("Control");
            this._beamsweeper2control = this._beamsweeper2.LocateMyFSM("Control");
            this._knight = GameObject.Find("Knight");
            this._spellControl = this._knight.LocateMyFSM("Spell Control");
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002218 File Offset: 0x00000418
        private void Start()
        {
            Log("Changing fight variables...");
            for (int i = 0; i < 3; i++)
            {
                this._attackCommands.AddAction("Nail Fan", this._attackCommands.GetAction("Comb Top", i));
            }
            for (int j = 0; j < 3; j++)
            {
                this._attackCommands.AddAction("Orb Antic", this._attackCommands.GetAction("Comb L", j));
            }
            for (int k = 0; k < 3; k++)
            {
                this._attackCommands.AddAction("Comb Top", this._attackCommands.GetAction("Comb R", k));
            }
            this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.6f;
            this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.625f;
            this._attackCommands.GetAction<RandomFloat>("Aim", 4).min = -1f;
            this._attackCommands.GetAction<RandomFloat>("Aim", 4).max = 1f;
            this._hm.hp += 750;
            this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 750;
            this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 550;
            this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 550;
            this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 200;
            this._control.GetAction<SetHP>("Scream", 7).hp = 1200;
            this._attackChoices.ChangeTransition("Beam Sweep L", "FINISHED", "Orb Wait");
            this._attackChoices.ChangeTransition("Beam Sweep L 2", "FINISHED", "Orb Wait");
            for (int l = 0; l < this._spikes.Length; l++)
            {
                Vector2 position = new Vector2(57f + (float)l / 2f, 153.8f);
                this._spikes[l] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikes[l].SetActive(true);
                this._spikes[l].transform.SetPosition2D(position);
                this._spikes[l].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int m = 0; m < this._spikeset2.Length; m++)
            {
                Vector2 position2 = new Vector2(57.7f + (float)m / 2f, 45.9f);
                this._spikeset2[m] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset2[m].SetActive(true);
                this._spikeset2[m].transform.SetPosition2D(position2);
                this._spikeset2[m].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int n = 0; n < this._spikeset3.Length; n++)
            {
                Vector2 position3 = new Vector2(49.6f + (float)n / 2f, 37.6f);
                this._spikeset3[n] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset3[n].SetActive(true);
                this._spikeset3[n].transform.SetPosition2D(position3);
                this._spikeset3[n].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int num = 0; num < this._spikeset4.Length; num++)
            {
                Vector2 position4 = new Vector2(58.5f + (float)num / 2f, 34.7f);
                this._spikeset4[num] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset4[num].SetActive(true);
                this._spikeset4[num].transform.SetPosition2D(position4);
                this._spikeset4[num].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int num2 = 0; num2 < this._spikeset1.Length; num2++)
            {
                Vector2 position5 = new Vector2(66.5f + (float)num2 / 2f, 39.1f);
                this._spikeset1[num2] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset1[num2].SetActive(true);
                this._spikeset1[num2].transform.SetPosition2D(position5);
                this._spikeset1[num2].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int num3 = 0; num3 < this._spikeset5.Length; num3++)
            {
                Vector2 position6 = new Vector2(46.2f + (float)num3 / 2f, 43.7f);
                this._spikeset5[num3] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset5[num3].SetActive(true);
                this._spikeset5[num3].transform.SetPosition2D(position6);
                this._spikeset5[num3].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int num4 = 0; num4 < this._spikeset6.Length; num4++)
            {
                Vector2 position7 = new Vector2(41.2f + (float)num4 / 2f, 36.7f);
                this._spikeset6[num4] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset6[num4].SetActive(true);
                this._spikeset6[num4].transform.SetPosition2D(position7);
                this._spikeset6[num4].LocateMyFSM("Control").SendEvent("DOWN");
            }
            for (int num5 = 0; num5 < this._spikeset7.Length; num5++)
            {
                Vector2 position8 = new Vector2(72f + (float)num5 / 2f, 45.1f);
                this._spikeset7[num5] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
                this._spikeset7[num5].SetActive(true);
                this._spikeset7[num5].transform.SetPosition2D(position8);
                this._spikeset7[num5].LocateMyFSM("Control").SendEvent("DOWN");
            }
            this._attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.75f;
            this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 7;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 2;
            this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 4;
            this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.4f;
            this._attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.01f;
            this._attackChoices.GetAction<Wait>("Orb Recover", 0).time = 1.15f;
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
            this._attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time = 0.05f;
            this._attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time = 3.3f;
            this._attackChoices.RemoveAction("Nail Top Sweep", 3);
            this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay = 1.7f;
            this._attackChoices.RemoveAction("Nail Top Sweep", 1);
            this._control.GetAction<Wait>("Rage Comb", 0).time = 0.6f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 0).delay = 0.5f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.7f;
            this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 2.9f;
            this._attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 4f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 0).delay = 0.5f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.7f;
            this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 2.9f;
            this._attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 4f;
            this.AddNailWall("Nail L Sweep", "COMB R", 1.1f, 1);
            this.AddNailWall("Nail R Sweep", "COMB L", 1.1f, 1);
            this.AddNailWall("Nail L Sweep", "COMB R", 2.3000002f, 1);
            this.AddNailWall("Nail R Sweep", "COMB L", 2.3000002f, 1);
            this.AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
            this.AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
            this.health_lock = this._hm.hp - 50;
            Log("fin.");
        }

        // Token: 0x06000003 RID: 3 RVA: 0x0000300C File Offset: 0x0000120C
        private void Update()
        {
            this.cycle++;
            this.cycle %= 900;
            bool flag = !this.fullSpikesSet || this.arena2Set;
            bool flag2 = flag;
            if (flag2)
            {
                bool flag3 = this.cycle == 20;
                bool flag4 = flag3;
                if (flag4)
                {
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 1", 2).delay = 0.85f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 3).delay = 0.85f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 8).delay = 0.85f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.95f;
                    this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.82f;
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 2", 3).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 4).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 8).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.15f;
                    this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.25f;
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 3", 3).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 4).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 8).delay = 0.05f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.15f;
                    this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.25f;
                }
                bool flag5 = this.cycle == 600;
                bool flag6 = flag5;
                if (flag6)
                {
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 1", 2).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 3).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 8).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.65f;
                    this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.66f;
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 2", 3).delay = 0.35f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 4).delay = 0.35f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 8).delay = 0.35f;
                    this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.25f;
                    this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.66f;
                    this._attackCommands.GetAction<AudioPlayerOneShotSingle>("EB 3", 3).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 4).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 8).delay = 0.55f;
                    this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.65f;
                    this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.26f;
                }
            }
            bool value = this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value;
            bool flag7 = value;
            if (flag7)
            {
                switch (this.CWRepeats)
                {
                    case 0:
                        {
                            this.CWRepeats = 1;
                            bool flag8 = this.cycle < 600;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                            }
                            break;
                        }
                    case 1:
                        {
                            this.CWRepeats = 2;
                            bool flag10 = this.cycle < 300;
                            bool flag11 = flag10;
                            if (flag11)
                            {
                                this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                            }
                            break;
                        }
                    case 2:
                        this.CWRepeats = 3;
                        break;
                }
            }
            else
            {
                bool flag12 = this.CWRepeats == 3;
                bool flag13 = flag12;
                if (flag13)
                {
                    this.CWRepeats = 0;
                }
            }
            bool flag14 = this._beamsweepercontrol.ActiveStateName == this._beamsweeper2control.ActiveStateName;
            bool flag15 = flag14;
            if (flag15)
            {
                string activeStateName = this._beamsweepercontrol.ActiveStateName;
                bool flag16 = !(activeStateName == "Beam Sweep L");
                if (flag16)
                {
                    bool flag17 = activeStateName == "Beam Sweep R";
                    if (flag17)
                    {
                        this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP L"));
                    }
                }
                else
                {
                    this._beamsweeper2control.ChangeState(GetFsmEventByName(this._beamsweeper2control, "BEAM SWEEP R"));
                }
            }
            bool flag18 = this.cycle % 250 == 0;
            bool flag19 = flag18;
            if (flag19)
            {
                this.health_lock = this._hm.hp - 20;
            }
            bool flag20 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value - 200 + 80 && !this.disableSwordSweep;
            bool flag21 = flag20;
            if (flag21)
            {
                this.disableSwordSweep = true;
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL L SWEEP", "Beam Sweep L");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL R SWEEP", "Beam Sweep L");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL FAN", "Eye Beam Wait");
                this._attackChoices.ChangeTransition("A1 Choice", "NAIL TOP SWEEP", "Orb Wait");
            }
            bool flag22 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value - 200 && !this.fullSpikesSet;
            bool flag23 = flag22;
            if (flag23)
            {
                this.fullSpikesSet = true;
                for (int i = 5; i >= 3; i--)
                {
                    this._attackCommands.RemoveAction("Orb Antic", i);
                }
                for (int j = 8; j >= 6; j--)
                {
                    this._attackCommands.RemoveAction("Nail Fan", j);
                }
                for (int k = 0; k < 5; k++)
                {
                    this._spikeMasterControl.GetAction<SendEventByName>("Spikes Left", k).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Spikes Right", k).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Wave L", k + 2).sendEvent = "UP";
                    this._spikeMasterControl.GetAction<SendEventByName>("Wave R", k + 2).sendEvent = "UP";
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
            }
            bool flag24 = this.arena2Timer > 1705;
            bool flag25 = flag24;
            if (flag25)
            {
                this.arena2Timer = 1500;
            }
            bool flag26 = this.arena2Timer == 1700;
            bool flag27 = flag26;
            if (flag27)
            {
                Log("arena2 spikes set!");
                bool flag28 = this.cycle1 == 0;
                bool flag29 = flag28;
                if (flag29)
                {
                    base.StartCoroutine(this.AddDivePunishment());
                }
                foreach (GameObject go in this._spikeset1)
                {
                    go.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go2 in this._spikeset2)
                {
                    go2.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go3 in this._spikeset3)
                {
                    go3.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go4 in this._spikeset4)
                {
                    go4.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go5 in this._spikeset5)
                {
                    go5.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go6 in this._spikeset6)
                {
                    go6.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject go7 in this._spikeset7)
                {
                    go7.LocateMyFSM("Control").SendEvent("UP");
                }
                this.arena2SpikesSet = true;
            }
            bool flag30 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value + 30 && !this.disableBeamSet;
            bool flag31 = flag30;
            if (flag31)
            {
                this.disableBeamSet = true;
                this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP L", "Orb Wait");
                this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Eye Beam Wait");
            }
            bool flag32 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value + 120 && !this.nailfanToNormal;
            bool flag33 = flag32;
            if (flag33)
            {
                this._attackCommands.ChangeTransition("Nail Fan", "FINISHED", "Eb Extra Wait 2");
                this.nailfanToNormal = true;
            }
           
            bool flag43 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value + 60 && !this.ascension_ready;
            bool flag44 = flag43;
            if (flag44)
            {
                Log("radiance ascended");
                this.ascension_ready = true;
                this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.6f;
                this._attackCommands.GetAction<Wait>("Aim", 11).time = 1.325f;
                this._attackCommands.GetAction<RandomFloat>("Aim", 4).min = -7f;
                this._attackCommands.GetAction<RandomFloat>("Aim", 4).max = 7f;
                this._attackCommands.ChangeTransition("Aim", "FINISHED", "Aim Back");
            }
            bool flag45 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2;
            bool flag46 = flag45;
            if (flag46)
            {
                this.arena2Timer++;
            }
            bool flag47 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !this.arena2Set;
            bool flag48 = flag47;
            if (flag48)
            {
                Modding.Logger.Log("[Dumb Radiance] Starting Phase 2");
                this.arena2Set = true;
                this._attackCommands.GetAction<Wait>("CW Repeat", 0).time = 0.025f;
                this._attackCommands.GetAction<Wait>("CCW Repeat", 0).time = 0.025f;
                this._attackCommands.ChangeTransition("Nail Fan", "FINISHED", "AB Start");
                this._attackCommands.ChangeTransition("Aim", "FINISHED", "Eb Extra Wait 2");
                this._spellControl.RemoveAction("Q2 Land", 0);
                this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 6;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 1;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 4;
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
            bool flag49 = base.gameObject.transform.position.y >= 150f;
            bool flag50 = flag49;
            if (flag50)
            {
                bool flag51 = !this.last_dance_set;
                bool flag52 = flag51;
                if (flag52)
                {
                    this.last_dance_set = true;
                }
                bool flag53 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 100;
                bool flag54 = flag53;
                if (flag54)
                {
                    GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                    bool flag55 = !this.onePlatSet;
                    bool flag56 = flag55;
                    if (flag56)
                    {
                        this.onePlatSet = true;
                        Log("Removing upper right platform");
                        this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.8f;
                    }
                }
                bool flag57 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 100 - 100;
                bool flag58 = flag57;
                if (flag58)
                {
                    foreach (GameObject go8 in this._spikes)
                    {
                        go8.LocateMyFSM("Control").SendEvent("UP");
                    }
                    bool flag59 = !this.platSpikesSet;
                    bool flag60 = flag59;
                    if (flag60)
                    {
                        this.platSpikesSet = true;
                        GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
                        base.StartCoroutine(this.AddDivePunishment());
                    }
                }
            }
            bool flag61 = this._hm.hp < this.health_lock && this._hm.hp > this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value;
            bool flag62 = flag61;
            if (flag62)
            {
                this._hm.hp = this.health_lock;
            }
        }
        private void FixedUpdate()
        {
            bool flag34 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value;
            bool flag35 = flag34;
            if (flag35)
            {
                bool flag36 = this.cycle1 % 200 == 0;
                bool flag37 = flag36;
                if (flag37)
                {
                    //this._spellControl.RemoveAction("Q2 Land", 0);
                }
                bool flag38 = this._knight.transform.position.y >= 70.8f;
                if (flag38)
                {
                    bool flag39 = this.cycle1 < 600;
                    bool flag40 = flag39;
                    if (flag40)
                    {
                        bool flag41 = this.cycle1 % 100 == 0;
                        bool flag42 = flag41;
                        if (flag42)
                        {
                            this.fireball = UnityEngine.Object.Instantiate<GameObject>(this._fireballTemplate);
                            this.fireball.SetActive(true);
                            Log("throwing orb");
                            this.fireball.AddComponent<Dumb.OrbThrow>();
                            this.fireball.transform.SetPosition2D(this._knight.transform.position.x + UnityEngine.Random.Range(-3f, 3f), this._knight.transform.position.y /*+*/- UnityEngine.Random.Range(9f, 14f));
                        }
                        this.cycle1++;
                    }
                }
            }
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000045D7 File Offset: 0x000027D7
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

        // Token: 0x06000005 RID: 5 RVA: 0x000045E6 File Offset: 0x000027E6
        [UsedImplicitly]
        public void DivePunishment()
        {
            Log("YOU WON'T CHEESE SPIKES IN THIS TOWN AGAIN");
            HeroController.instance.TakeDamage(base.gameObject, CollisionSide.bottom, 1, 0);
            EventRegister.SendEvent("HERO DAMAGED");
        }

        // Token: 0x06000006 RID: 6 RVA: 0x00004614 File Offset: 0x00002814
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

        // Token: 0x06000007 RID: 7 RVA: 0x00004674 File Offset: 0x00002874
        private static FsmEvent GetFsmEventByName(PlayMakerFSM fsm, string eventName)
        {
            foreach (FsmEvent fsmEvent in fsm.FsmEvents)
            {
                bool flag = fsmEvent.Name == eventName;
                bool flag2 = flag;
                if (flag2)
                {
                    return fsmEvent;
                }
            }
            return null;
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000046BF File Offset: 0x000028BF
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Dumb Radiance] " + ((obj != null) ? obj.ToString() : null));
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000046DE File Offset: 0x000028DE
        private void OnDestroy()
        {
            //this._spellControl.RemoveFirstAction("Q2 Land");
            HeroController.instance.gameObject.LocateMyFSM("Spell Control").GetState("Q2 Land").Actions = ManyRadiances.origQactions;
        }

        // Token: 0x04000001 RID: 1
        private GameObject _spikeMaster;

        // Token: 0x04000002 RID: 2
        private GameObject _spikeTemplate;

        // Token: 0x04000003 RID: 3
        private GameObject fireball;

        // Token: 0x04000004 RID: 4
        private GameObject[] _spikes;

        // Token: 0x04000005 RID: 5
        private GameObject[] _spikeset1;

        // Token: 0x04000006 RID: 6
        private GameObject[] _spikeset2;

        // Token: 0x04000007 RID: 7
        private GameObject[] _spikeset3;

        // Token: 0x04000008 RID: 8
        private GameObject[] _spikeset4;

        // Token: 0x04000009 RID: 9
        private GameObject[] _spikeset5;

        // Token: 0x0400000A RID: 10
        private GameObject[] _spikeset6;

        // Token: 0x0400000B RID: 11
        private GameObject[] _spikeset7;

        // Token: 0x0400000C RID: 12
        private GameObject _fireballTemplate;

        // Token: 0x0400000D RID: 13
        private GameObject _beamsweeper;

        // Token: 0x0400000E RID: 14
        private GameObject _beamsweeper2;

        // Token: 0x0400000F RID: 15
        private GameObject _knight;

        // Token: 0x04000010 RID: 16
        private HealthManager _hm;

        // Token: 0x04000011 RID: 17
        private bool disableSwordSweep = false;

        // Token: 0x04000012 RID: 18
        private PlayMakerFSM _attackChoices;

        // Token: 0x04000013 RID: 19
        private PlayMakerFSM _attackCommands;

        // Token: 0x04000014 RID: 20
        private PlayMakerFSM _control;

        // Token: 0x04000015 RID: 21
        private PlayMakerFSM _phaseControl;

        // Token: 0x04000016 RID: 22
        private PlayMakerFSM _spikeMasterControl;

        // Token: 0x04000017 RID: 23
        private PlayMakerFSM _beamsweepercontrol;

        // Token: 0x04000018 RID: 24
        private PlayMakerFSM _beamsweeper2control;

        // Token: 0x04000019 RID: 25
        private PlayMakerFSM _spellControl;

        // Token: 0x0400001A RID: 26
        private bool nailfanToNormal = false;

        // Token: 0x0400001B RID: 27
        private int health_lock;

        // Token: 0x0400001C RID: 28
        private int cycle = 0;

        // Token: 0x0400001D RID: 29
        private int arena2Timer = 0;

        // Token: 0x0400001E RID: 30
        private int CWRepeats = 0;

        // Token: 0x0400001F RID: 31
        private int cycle1 = 0;

        // Token: 0x04000020 RID: 32
        private bool ascension_ready = false;

        // Token: 0x04000021 RID: 33
        private bool last_dance_set = false;

        // Token: 0x04000022 RID: 34
        private bool fullSpikesSet = false;

        // Token: 0x04000023 RID: 35
        private bool disableBeamSet = false;

        // Token: 0x04000024 RID: 36
        private bool arena2Set = false;

        // Token: 0x04000025 RID: 37
        private bool arena2SpikesSet = false;

        // Token: 0x04000026 RID: 38
        private bool onePlatSet = false;

        // Token: 0x04000027 RID: 39
        private bool platSpikesSet = false;

        // Token: 0x04000028 RID: 40
        private const int fullSpikesHealth = 200;

        // Token: 0x04000029 RID: 41
        private const int onePlatHealth = 100;

        // Token: 0x0400002A RID: 42
        private const int platSpikesHealth = 100;

        // Token: 0x0400002B RID: 43
        private const int arena2fullSpikesHealth = 350;

        // Token: 0x0400002C RID: 44
        private const float nailWallDelay = 0.6f;
    }
}
