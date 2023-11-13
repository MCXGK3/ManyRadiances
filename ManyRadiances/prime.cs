using System;

namespace ManyRadiances;
// Token: 0x02000002 RID: 2
public class anyprime : MonoBehaviour
{
    // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
    private void Awake()
    {
        ModHooks.TakeDamageHook += CreateRespawn;
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
        this._beamsweeper2.AddComponent<Anyprime.BeamSweeperClone>();
        this._beamsweepercontrol = this._beamsweeper.LocateMyFSM("Control");
        this._beamsweeper2control = this._beamsweeper2.LocateMyFSM("Control");
        this._knight = GameObject.Find("Knight");
        this._spellControl = this._knight.LocateMyFSM("Spell Control");
        this._teleport = base.gameObject.LocateMyFSM("Teleport");
        this._spikeset1 = new GameObject[5];
        this._spikeset2 = new GameObject[9];
        this._spikeset3 = new GameObject[9];
    }

    private int CreateRespawn(ref int hazardType, int damage)
    {
        bool flag = hazardType == 2 && gameObject != null;
        if (flag)
        {
            bool flag2 = gameObject.transform.GetPositionY() > 150f && GameObject.Find("Knight").transform.GetPositionY() > 60f;
            if (flag2)
            {
                GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
            }
            else
            {
                bool flag3 = gameObject.LocateMyFSM("Attack Choices").FsmVariables.GetFsmInt("Arena").Value == 2;
                if (flag3)
                {
                    GameObject.Find("Hazard Plat/Radiant Plat Wide (4)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
                }
            }
        }
        return damage;
    }

    // Token: 0x06000002 RID: 2 RVA: 0x000021C4 File Offset: 0x000003C4
    private void Start()
    {
        Log("Changing fight variables...");
        this._control.GetAction<SendEventByName>("First Tele", 3).sendEvent = "HugeShake";
        this._hm.hp += this.HP;
        this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 1750;
        this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 1700;
        this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 1300;
        this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 1000;
        this._control.GetAction<SetHP>("Scream", 7).hp = 2000;
        this._attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.1f;
        this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 12;
        this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 10;
        this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 14;
        this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.1f;
        this._attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.1f;
        this._attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.5f;
        this._attackCommands.GetAction<SetIntValue>("Nail Fan", 4).intValue.Value = 18;
        this._attackCommands.GetAction<Wait>("Nail Fan", 2).time.Value = 0.01f;
        this._attackCommands.GetAction<SetIntValue>("CW Restart", 0).intValue.Value = 18;
        this._attackCommands.GetAction<SetIntValue>("CCW Restart", 0).intValue.Value = 18;
        this._attackCommands.GetAction<FloatAdd>("CW Restart", 2).add.Value = -10f;
        this._attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add.Value = 10f;
        this._attackCommands.RemoveAction("CW Restart", 1);
        this._attackCommands.RemoveAction("CCW Restart", 1);
        this._attackCommands.RemoveAction("CW Repeat", 0);
        this._attackCommands.RemoveAction("CCW Repeat", 0);
        this._attackCommands.GetAction<FloatAdd>("CW Spawn", 2).add.Value = -20f;
        this._attackCommands.GetAction<FloatAdd>("CCW Spawn", 2).add.Value = 20f;
        this._attackChoices.GetAction<Wait>("Beam Sweep L", 0).time = 0.5f;
        this._attackChoices.GetAction<Wait>("Beam Sweep R", 0).time = 0.5f;
        this._attackChoices.ChangeTransition( "A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
        this._attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
        this._attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
        this._attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
        this._attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time = 3.5f;
        this._attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time = 3.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 4", 4).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 4", 5).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 5", 5).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 5", 6).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 6", 5).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 6", 6).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 7", 8).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 7", 9).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 8", 8).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 8", 9).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 9", 8).delay = 0.3f;
        this._attackCommands.GetAction<Wait>("EB 9", 9).time = 0.5f;
        this._attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time = 0.05f;
        this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 1f;
        this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.4f;
        this._attackCommands.GetAction<SendEventByName>("Aim", 8).delay = 0.4f;
        this._attackCommands.GetAction<SendEventByName>("Aim", 9).delay = 0.4f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay = 0.35f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay = 0.7f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay = 1.05f;
        this._attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time = 3f;
        this._attackChoices.InsertAction( "Nail Top Sweep", new SendEventByName
        {
            eventTarget = this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).eventTarget,
            sendEvent = "COMB TOP",
            delay = 1.4f,
            everyFrame = false
        }, 4);
        this._attackChoices.InsertAction( "Nail Top Sweep", new SendEventByName
        {
            eventTarget = this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).eventTarget,
            sendEvent = "COMB TOP",
            delay = 1.75f,
            everyFrame = false
        }, 4);
        this._control.GetAction<Wait>("Rage Comb", 0).time = 0.35f;
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.25f;
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.85f;
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 3.45f;
        this._attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 4.5f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.25f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.85f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 3.45f;
        this._attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 4.5f;
        this.AddNailWall("Nail L Sweep", "COMB R", 1.3f, 1);
        this.AddNailWall("Nail R Sweep", "COMB L", 1.3f, 1);
        this.AddNailWall("Nail L Sweep", "COMB R", 2.9f, 1);
        this.AddNailWall("Nail R Sweep", "COMB L", 2.9f, 1);
        this.AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
        this.AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
        this._teleport.GetAction<SendEventByName>("Arrive", 5).eventTarget = this._control.GetAction<SendEventByName>("Stun1 Out", 9).eventTarget;
        this._teleport.GetAction<SendEventByName>("Arrive", 5).sendEvent = "SmallShake";
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
        foreach (object obj in this._spikeMaster.transform)
        {
            Transform transform = (Transform)obj;
            foreach (object obj2 in transform.gameObject.transform)
            {
                Transform transform2 = (Transform)obj2;
                GameObject gameObject = transform2.gameObject;
                gameObject.GetComponent<DamageHero>().damageDealt = 2;
            }
        }
        this._control.FsmVariables.FindFsmFloat("A1 X Min").Value -= 5f;
        this._control.FsmVariables.FindFsmFloat("A1 X Max").Value += 5f;
        this._control.GetAction<RandomFloat>("Set Dest", 4).min.Value -= 2f;
        this._control.GetAction<RandomFloat>("Set Dest", 4).max.Value += 2f;
        this._phaseControl.RemoveAction("Set Phase 3", 0);
        this._control.GetAction<Wait>("Stun1 Start", 9).time = 0.5f;
        this._control.GetAction<Wait>("Stun1 Roar", 3).time = 1f;
        this._control.GetAction<Wait>("Plat Setup", 6).time = 1f;
        for (int i = 0; i < this._spikeset1.Length; i++)
        {
            Vector2 position = new Vector2(66.5f + (float)i / 2f, 39.1f);
            this._spikeset1[i] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeset1[i].SetActive(true);
            this._spikeset1[i].transform.SetPosition2D(position);
            this._spikeset1[i].GetComponent<DamageHero>().damageDealt = 2;
            this._spikeset1[i].LocateMyFSM("Control").SendEvent("DOWN");
        }
        for (int j = 0; j < this._spikeset2.Length; j++)
        {
            Vector2 position2 = new Vector2(57.7f + (float)j / 2f, 45.9f);
            this._spikeset2[j] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeset2[j].SetActive(true);
            this._spikeset2[j].transform.SetPosition2D(position2);
            this._spikeset2[j].GetComponent<DamageHero>().damageDealt = 2;
            this._spikeset2[j].LocateMyFSM("Control").SendEvent("DOWN");
        }
        for (int k = 0; k < this._spikeset3.Length; k++)
        {
            Vector2 position3 = new Vector2(49.6f + (float)k / 2f, 37.6f);
            this._spikeset3[k] = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate);
            this._spikeset3[k].SetActive(true);
            this._spikeset3[k].transform.SetPosition2D(position3);
            this._spikeset3[k].GetComponent<DamageHero>().damageDealt = 2;
            this._spikeset3[k].LocateMyFSM("Control").SendEvent("DOWN");
        }
        FsmEventTarget fsmEventTarget = new FsmEventTarget();
        fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
        fsmEventTarget.excludeSelf = false;
        fsmEventTarget.gameObject = new FsmOwnerDefault();
        fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
        fsmEventTarget.gameObject.GameObject.Value = GameObject.Find("P2 SetA/Radiant Plat Wide (2)");
        fsmEventTarget.sendToChildren = true;
        this._control.GetAction<SendEventByName>("Climb Plats1", 3).eventTarget = fsmEventTarget;
        GameObject gameObject2 = GameObject.Find("Hazard Plat/Radiant Plat Wide (4)");
        FsmEventTarget fsmEventTarget2 = new FsmEventTarget();
        fsmEventTarget2.target = FsmEventTarget.EventTarget.GameObject;
        fsmEventTarget2.excludeSelf = false;
        fsmEventTarget2.gameObject = new FsmOwnerDefault();
        fsmEventTarget2.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
        fsmEventTarget2.gameObject.GameObject.Value = gameObject2;
        fsmEventTarget2.sendToChildren = true;
        this._control.AddAction("Climb Plats1", new SendEventByName
        {
            eventTarget = fsmEventTarget2,
            sendEvent = "SLOW VANISH",
            delay = 1.25f,
            everyFrame = false
        });
        gameObject2.LocateMyFSM("radiant_plat").GetAction<Wait>("Vanish Antic", 1).time = 3.5f;
        this._attackCommands.RemoveAction("Set Final Orbs", 0);
        GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").GetAction<Wait>( "Vanish Antic", 1).time = 3.5f;
        this._control.GetAction<SetVector3Value>("Tele 11", 1).vector3Value = new Vector3(62.94f, 157.65f, 0.006f);
        this._control.GetAction<SetVector3Value>("Tele 12", 1).vector3Value = new Vector3(53.88f, 157.65f, 0.006f);
        this._control.GetAction<SetVector3Value>("Tele 13", 1).vector3Value = new Vector3(72.4f, 157.65f, 0.006f);
        Log("fin.");
    }

    // Token: 0x06000003 RID: 3 RVA: 0x000034E8 File Offset: 0x000016E8
    private void Update()
    {
        bool value = this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value;
        if (value)
        {
            switch (this.CWRepeats)
            {
                case 0:
                    this.CWRepeats = 1;
                    this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    break;
                case 1:
                    this.CWRepeats = 2;
                    this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    break;
                case 2:
                    this.CWRepeats = 3;
                    break;
            }
        }
        else
        {
            bool flag = this.CWRepeats == 3;
            if (flag)
            {
                this.CWRepeats = 0;
            }
        }
        bool flag2 = this._beamsweepercontrol.ActiveStateName == this._beamsweeper2control.ActiveStateName;
        if (flag2)
        {
            string activeStateName = this._beamsweepercontrol.ActiveStateName;
            string text = activeStateName;
            bool flag3 = text != null;
            if (flag3)
            {
                bool flag4 = !(text == "Beam Sweep L");
                if (flag4)
                {
                    bool flag5 = text == "Beam Sweep R";
                    if (flag5)
                    {
                        this._beamsweeper2control.SendEvent("BEAM SWEEP L");
                    }
                }
                else
                {
                    this._beamsweeper2control.SendEvent("BEAM SWEEP R");
                }
            }
        }
        bool flag6 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value + 30 && !this.disableBeamSet;
        if (flag6)
        {
            this.disableBeamSet = true;
            this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP L", "Orb Wait");
            this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Eye Beam Wait");
        }
        bool flag7 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value && !this.swordRainSpikesSet;
        if (flag7)
        {
            this.swordRainSpikesSet = true;
            foreach (object obj in this._spikeMaster.transform)
            {
                Transform transform = (Transform)obj;
                foreach (object obj2 in transform.gameObject.transform)
                {
                    Transform transform2 = (Transform)obj2;
                    transform2.gameObject.GetComponent<DamageHero>().damageDealt = 1;
                }
            }
        }
        bool flag8 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !this.arena2Set;
        if (flag8)
        {
            this.arena2Set = true;
            this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep L", 3).x = 89f;
            this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-75f, 0f, 0f);
            this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).time = 3f;
            this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep R", 4).x = 32.6f;
            this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(75f, 0f, 0f);
            this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).time = 3f;
            this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep L", 2).x = 89f;
            this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-75f, 0f, 0f);
            this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).time = 3f;
            this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep R", 3).x = 32.6f;
            this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(75f, 0f, 0f);
            this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).time = 3f;
        }
        bool flag9 = this.arena2Set && this._spikeset1[0].LocateMyFSM("Control").ActiveStateName == "Downed";
        if (flag9)
        {
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
        }
        bool flag10 = this.arena2Set && GameObject.Find("Hazard Plat/Radiant Plat Wide (4)").LocateMyFSM("radiant_plat").ActiveStateName == "Appear 2";
        if (flag10)
        {
            GameObject.Find("Hazard Plat/Radiant Plat Wide (4)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
        }
        bool flag11 = base.gameObject.transform.position.y < 150f;
        if (flag11)
        {
            bool flag12 = this.ascendBeam == null;
            if (flag12)
            {
                this.ascendBeam = GameObject.Find("Ascend Beam");
            }
        }
        bool flag13 = this._knight.transform.GetPositionY() > 152f && !this.finalDanceExtraAttackConfigured;
        if (flag13)
        {
            this.finalDanceExtraAttackConfigured = true;
            this.ascendBeam.LocateMyFSM("Control").SendEvent("END");
            this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.65f;
            this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.95f;
            this._attackCommands.GetAction<SendEventByName>("Aim", 8).delay = 0.5f;
            this._attackCommands.GetAction<SendEventByName>("Aim", 9).delay = 0.5f;
            this._control.AddAction("Final Idle", this._attackCommands.GetAction<ActivateGameObject>( "AB Start", 0));
            for (int l = 0; l <= 10; l++)
            {
                this._control.AddAction("Final Idle", this._attackCommands.GetAction("Aim", l));
            }
            this._control.InsertAction("A2 Tele Choice 2", new ActivateGameObject
            {
                gameObject = this._attackCommands.GetAction<ActivateGameObject>("AB Start", 0).gameObject,
                activate = false,
                recursive = false,
                resetOnExit = false,
                everyFrame = false
            }, 0);
        }
        bool flag14 = this._hm.hp <= this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value && !this.finalDanceOrbsConfigured;
        if (flag14)
        {
            this.finalDanceOrbsConfigured = true;
            this._attackCommands.GetAction<Wait>("Orb Antic", 0).time.Value = 0.01f;
            this._attackCommands.GetAction<Wait>("FinalOrb Pause", 0).time.Value = 0.75f;
            this._attackChoices.GetAction<Wait>("Orb Recover", 0).time.Value = 0.01f;
        }
        bool flag15 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 500 && !this.onePlatSet;
        if (flag15)
        {
            this.onePlatSet = true;
            GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
        }
        bool flag16 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 500 - 500 && !this.noPlatsSet;
        if (flag16)
        {
            this.noPlatsSet = true;
            GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
        }
        bool flag17 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 500 - 500 && GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").ActiveStateName == "Appear 2";
        if (flag17)
        {
            GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
        }
    }

    // Token: 0x06000004 RID: 4 RVA: 0x00003EB0 File Offset: 0x000020B0
    private void AddNailWall(string stateName, string eventName, float delay, int index)
    {
        this._attackChoices.InsertAction( stateName, new SendEventByName
        {
            eventTarget = this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 0).eventTarget,
            sendEvent = eventName,
            delay = delay,
            everyFrame = false
        }, index);
    }
    private void Ondestroyed()
    {
        ModHooks.TakeDamageHook -= CreateRespawn;
    }
    // Token: 0x06000005 RID: 5 RVA: 0x00003F0D File Offset: 0x0000210D
    public static void Log(object obj)
    {
        Modding.Logger.Log("[Any Radiance Prime] - " + ((obj != null) ? obj.ToString() : null));
    }

    // Token: 0x04000001 RID: 1
    private int HP = 2000;

    // Token: 0x04000002 RID: 2
    private GameObject _spikeMaster;

    // Token: 0x04000003 RID: 3
    private GameObject _spikeTemplate;

    // Token: 0x04000004 RID: 4
    private GameObject _beamsweeper;

    // Token: 0x04000005 RID: 5
    private GameObject _beamsweeper2;

    // Token: 0x04000006 RID: 6
    private GameObject _knight;

    // Token: 0x04000007 RID: 7
    private HealthManager _hm;

    // Token: 0x04000008 RID: 8
    private PlayMakerFSM _attackChoices;

    // Token: 0x04000009 RID: 9
    private PlayMakerFSM _attackCommands;

    // Token: 0x0400000A RID: 10
    private PlayMakerFSM _control;

    // Token: 0x0400000B RID: 11
    private PlayMakerFSM _phaseControl;

    // Token: 0x0400000C RID: 12
    private PlayMakerFSM _spikeMasterControl;

    // Token: 0x0400000D RID: 13
    private PlayMakerFSM _beamsweepercontrol;

    // Token: 0x0400000E RID: 14
    private PlayMakerFSM _beamsweeper2control;

    // Token: 0x0400000F RID: 15
    private PlayMakerFSM _spellControl;

    // Token: 0x04000010 RID: 16
    private PlayMakerFSM _teleport;

    // Token: 0x04000011 RID: 17
    private GameObject[] _spikeset1;

    // Token: 0x04000012 RID: 18
    private GameObject[] _spikeset2;

    // Token: 0x04000013 RID: 19
    private GameObject[] _spikeset3;

    // Token: 0x04000014 RID: 20
    private GameObject ascendBeam = null;

    // Token: 0x04000015 RID: 21
    private int CWRepeats = 0;

    // Token: 0x04000016 RID: 22
    private bool disableBeamSet = false;

    // Token: 0x04000017 RID: 23
    private bool swordRainSpikesSet = false;

    // Token: 0x04000018 RID: 24
    private bool arena2Set = false;

    // Token: 0x04000019 RID: 25
    private bool onePlatSet = false;

    // Token: 0x0400001A RID: 26
    private bool noPlatsSet = false;

    // Token: 0x0400001B RID: 27
    private bool finalDanceOrbsConfigured = false;

    // Token: 0x0400001C RID: 28
    private bool finalDanceExtraAttackConfigured = false;
}
