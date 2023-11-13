using System.Collections.Generic;
public class supernova : MonoBehaviour
{
    // Token: 0x06000007 RID: 7 RVA: 0x0000219C File Offset: 0x0000039C
    private void Awake()
    {
        Log("Added Radiance MonoBehaviour");
        this._hm = base.gameObject.GetComponent<HealthManager>();
        this._attackChoices = base.gameObject.LocateMyFSM("Attack Choices");
        this._attackCommands = base.gameObject.LocateMyFSM("Attack Commands");
        this._control = base.gameObject.LocateMyFSM("Control");
        this._phaseControl = base.gameObject.LocateMyFSM("Phase Control");
        this._spikeTemplate = GameObject.Find("Radiant Spike");
        this._beamsweeper = GameObject.Find("Beam Sweeper");
        this._beamsweeper2 = UnityEngine.Object.Instantiate<GameObject>(this._beamsweeper);
        this._beamsweeper2.AddComponent<Supernova.BeamSweeperClone>();
        this._beamsweepercontrol = this._beamsweeper.LocateMyFSM("Control");
        this._beamsweeper2control = this._beamsweeper2.LocateMyFSM("Control");
        this._radiantBeam0 = GameObject.Find("Radiant Beam");
        this._radiantBeam1 = GameObject.Find("Radiant Beam (1)");
        this._radiantBeam2 = GameObject.Find("Radiant Beam (2)");
        this._radiantBeam3 = GameObject.Find("Radiant Beam (3)");
        this._radiantBeam4 = GameObject.Find("Radiant Beam (4)");
        this._radiantBeam5 = GameObject.Find("Radiant Beam (5)");
        this._radiantBeam6 = GameObject.Find("Radiant Beam (6)");
        this._radiantBeam7 = GameObject.Find("Radiant Beam (7)");
        this._radiantBeams.Add(this._radiantBeam0);
        this._radiantBeams.Add(this._radiantBeam1);
        this._radiantBeams.Add(this._radiantBeam2);
        this._radiantBeams.Add(this._radiantBeam3);
        this._radiantBeams.Add(this._radiantBeam4);
        this._radiantBeams.Add(this._radiantBeam5);
        this._radiantBeams.Add(this._radiantBeam6);
        this._radiantBeams.Add(this._radiantBeam7);
        this._knight = GameObject.Find("Knight");
    }

    // Token: 0x06000008 RID: 8 RVA: 0x000023A8 File Offset: 0x000005A8
    private void Start()
    {
        Log("Changing Health");
        this._hm.hp += 2200;
        this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 2200;
        this._phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 1800;
        this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 1200;
        this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 450;
        this._control.GetAction<SetHP>("Scream", 7).hp = 2500;
        Log("Managing Spikes");
        this._phaseControl.RemoveAction( "Set Phase 2", 0);
        this._phaseControl.RemoveAction("Set Phase 3", 0);
        float num = 58.5f;
        while ((double)num <= 62.5)
        {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num, 21.25f), Quaternion.identity);
            gameObject2.GetComponent<DamageHero>().damageDealt = 1;
            gameObject2.GetComponent<DamageHero>().hazardType = 0;
            gameObject2.LocateMyFSM("Control").SendEvent("DOWN");
            this._firstSpikeGroup.Add(gameObject2);
            num += 1f;
        }
        float num2 = 50.5f;
        while ((double)num2 <= 57.5)
        {
            GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num2, 21.25f), Quaternion.identity);
            gameObject3.GetComponent<DamageHero>().damageDealt = 1;
            gameObject3.GetComponent<DamageHero>().hazardType = 0;
            gameObject3.LocateMyFSM("Control").SendEvent("DOWN");
            this._swordSpikeGroup.Add(gameObject3);
            num2 += 1f;
        }
        float num3 = 63.5f;
        while ((double)num3 <= 70.5)
        {
            GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num3, 21.25f), Quaternion.identity);
            gameObject4.GetComponent<DamageHero>().damageDealt = 1;
            gameObject4.GetComponent<DamageHero>().hazardType = 0;
            gameObject4.LocateMyFSM("Control").SendEvent("DOWN");
            this._swordSpikeGroup.Add(gameObject4);
            num3 += 1f;
        }
        for (float num4 = 58.5f; num4 <= 62.5f; num4 += 1f)
        {
            GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num4, 34.7f), Quaternion.identity);
            gameObject5.GetComponent<DamageHero>().damageDealt = 1;
            gameObject5.GetComponent<DamageHero>().hazardType = 0;
            gameObject5.LocateMyFSM("Control").SendEvent("DOWN");
            this._platsSpikeGroup.Add(gameObject5);
        }
        for (float num5 = 46.4f; num5 <= 48.4f; num5 += 1f)
        {
            GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num5, 43.7f), Quaternion.identity);
            gameObject6.GetComponent<DamageHero>().damageDealt = 1;
            gameObject6.GetComponent<DamageHero>().hazardType = 0;
            gameObject6.LocateMyFSM("Control").SendEvent("DOWN");
            this._platsSpikeGroup.Add(gameObject6);
        }
        for (float num6 = 57.6f; num6 <= 61.6f; num6 += 1f)
        {
            GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num6, 45.9f), Quaternion.identity);
            gameObject7.GetComponent<DamageHero>().damageDealt = 1;
            gameObject7.GetComponent<DamageHero>().hazardType = 0;
            gameObject7.LocateMyFSM("Control").SendEvent("DOWN");
            this._platsSpikeGroup.Add(gameObject7);
        }
        for (float num7 = 66.8f; num7 <= 68.8f; num7 += 1f)
        {
            GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num7, 39.1f), Quaternion.identity);
            gameObject8.GetComponent<DamageHero>().damageDealt = 1;
            gameObject8.GetComponent<DamageHero>().hazardType = 0;
            gameObject8.LocateMyFSM("Control").SendEvent("DOWN");
            this._platsSpikeGroup.Add(gameObject8);
        }
        for (float num8 = 61.6f; num8 <= 65.6f; num8 += 1f)
        {
            GameObject gameObject9 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num8, 51.8f), Quaternion.identity);
            gameObject9.GetComponent<DamageHero>().damageDealt = 1;
            gameObject9.GetComponent<DamageHero>().hazardType = 0;
            gameObject9.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject9);
        }
        for (int i = 57; i <= 59; i++)
        {
            GameObject gameObject10 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2((float)i, 58.2f), Quaternion.identity);
            gameObject10.GetComponent<DamageHero>().damageDealt = 1;
            gameObject10.GetComponent<DamageHero>().hazardType = 0;
            gameObject10.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject10);
        }
        for (float num9 = 63.2f; num9 <= 65.2f; num9 += 1f)
        {
            GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num9, 64.1f), Quaternion.identity);
            gameObject11.GetComponent<DamageHero>().damageDealt = 1;
            gameObject11.GetComponent<DamageHero>().hazardType = 0;
            gameObject11.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject11);
        }
        for (float num10 = 64.7f; num10 <= 66.7f; num10 += 1f)
        {
            GameObject gameObject12 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num10, 70.8f), Quaternion.identity);
            gameObject12.GetComponent<DamageHero>().damageDealt = 1;
            gameObject12.GetComponent<DamageHero>().hazardType = 0;
            gameObject12.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject12);
        }
        for (float num11 = 57.2f; num11 <= 61.2f; num11 += 1f)
        {
            GameObject gameObject13 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num11, 77.2f), Quaternion.identity);
            gameObject13.GetComponent<DamageHero>().damageDealt = 1;
            gameObject13.GetComponent<DamageHero>().hazardType = 0;
            gameObject13.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject13);
        }
        for (float num12 = 55.4f; num12 <= 57.4f; num12 += 1f)
        {
            GameObject gameObject14 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num12, 84.7f), Quaternion.identity);
            gameObject14.GetComponent<DamageHero>().damageDealt = 1;
            gameObject14.GetComponent<DamageHero>().hazardType = 0;
            gameObject14.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject14);
        }
        for (float num13 = 58.9f; num13 <= 60.9f; num13 += 1f)
        {
            GameObject gameObject15 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num13, 89.2f), Quaternion.identity);
            gameObject15.GetComponent<DamageHero>().damageDealt = 1;
            gameObject15.GetComponent<DamageHero>().hazardType = 0;
            gameObject15.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject15);
        }
        for (float num14 = 61.1f; num14 <= 65.1f; num14 += 1f)
        {
            GameObject gameObject16 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num14, 94.8f), Quaternion.identity);
            gameObject16.GetComponent<DamageHero>().damageDealt = 1;
            gameObject16.GetComponent<DamageHero>().hazardType = 0;
            gameObject16.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject16);
        }
        for (float num15 = 57.2f; num15 <= 59.2f; num15 += 1f)
        {
            GameObject gameObject17 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num15, 101.3f), Quaternion.identity);
            gameObject17.GetComponent<DamageHero>().damageDealt = 1;
            gameObject17.GetComponent<DamageHero>().hazardType = 0;
            gameObject17.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject17);
        }
        for (float num16 = 63.3f; num16 <= 65.3f; num16 += 1f)
        {
            GameObject gameObject18 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num16, 107.4f), Quaternion.identity);
            gameObject18.GetComponent<DamageHero>().damageDealt = 1;
            gameObject18.GetComponent<DamageHero>().hazardType = 0;
            gameObject18.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject18);
        }
        for (float num17 = 64.6f; num17 <= 66.6f; num17 += 1f)
        {
            GameObject gameObject19 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num17, 114f), Quaternion.identity);
            gameObject19.GetComponent<DamageHero>().damageDealt = 1;
            gameObject19.GetComponent<DamageHero>().hazardType = 0;
            gameObject19.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject19);
        }
        for (float num18 = 57.2f; num18 <= 61.2f; num18 += 1f)
        {
            GameObject gameObject20 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num18, 120.5f), Quaternion.identity);
            gameObject20.GetComponent<DamageHero>().damageDealt = 1;
            gameObject20.GetComponent<DamageHero>().hazardType = 0;
            gameObject20.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject20);
        }
        for (float num19 = 55.3f; num19 <= 57.3f; num19 += 1f)
        {
            GameObject gameObject21 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num19, 128.4f), Quaternion.identity);
            gameObject21.GetComponent<DamageHero>().damageDealt = 1;
            gameObject21.GetComponent<DamageHero>().hazardType = 0;
            gameObject21.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject21);
        }
        for (int j = 59; j <= 61; j++)
        {
            GameObject gameObject22 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2((float)j, 133.4f), Quaternion.identity);
            gameObject22.GetComponent<DamageHero>().damageDealt = 1;
            gameObject22.GetComponent<DamageHero>().hazardType = 0;
            gameObject22.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject22);
        }
        for (float num20 = 61.3f; num20 <= 65.3f; num20 += 1f)
        {
            GameObject gameObject23 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num20, 139.2f), Quaternion.identity);
            gameObject23.GetComponent<DamageHero>().damageDealt = 1;
            gameObject23.GetComponent<DamageHero>().hazardType = 0;
            gameObject23.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject23);
        }
        for (float num21 = 61.8f; num21 <= 63.8f; num21 += 1f)
        {
            GameObject gameObject24 = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num21, 146.5f), Quaternion.identity);
            gameObject24.GetComponent<DamageHero>().damageDealt = 1;
            gameObject24.GetComponent<DamageHero>().hazardType = 0;
            gameObject24.LocateMyFSM("Control").SendEvent("DOWN");
            this._climbSpikeGroup.Add(gameObject24);
        }
        for (float num22 = 57f; num22 <= 59f; num22 += 1f)
        {
            GameObject gameObjectA = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num22, 153.8f), Quaternion.identity);
            gameObjectA.GetComponent<DamageHero>().damageDealt = 2;
            gameObjectA.GetComponent<DamageHero>().hazardType = 0;
            gameObjectA.LocateMyFSM("Control").SendEvent("DOWN");
            this._finalSpikeGroup.Add(gameObjectA);
        }
        for (float num23 = 66.75f; num23 <= 68.75f; num23 += 1f)
        {
            GameObject gameObjectB = UnityEngine.Object.Instantiate<GameObject>(this._spikeTemplate, new Vector2(num23, 153.8f), Quaternion.identity);
            gameObjectB.GetComponent<DamageHero>().damageDealt = 2;
            gameObjectB.GetComponent<DamageHero>().hazardType = 0;
            gameObjectB.LocateMyFSM("Control").SendEvent("DOWN");
            this._finalSpikeGroup.Add(gameObjectB);
        }
        Log("Modifying Orbs");
        this._attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 10;
        this._attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.2f;
        this._attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.075f;
        this._attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.15f;
        this._attackCommands.GetAction<Wait>("Orb Antic", 0).time.Value = 0.2f;
        this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min.Value = 9;
        this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max.Value = 13;
        Log("Modifying Sword Burst");
        this._attackCommands.GetAction<SetIntValue>("Nail Fan", 4).intValue.Value = 18;
        this._attackCommands.GetAction<Wait>("Nail Fan", 2).time.Value = 0.5f;
        this._attackCommands.GetAction<SetIntValue>("CW Restart", 0).intValue.Value = 18;
        this._attackCommands.GetAction<SetIntValue>("CCW Restart", 0).intValue.Value = 18;
        this._attackCommands.GetAction<Wait>("CW Repeat", 0).time = 0.001f;
        this._attackCommands.GetAction<Wait>("CCW Repeat", 0).time = 0.001f;
        this._attackCommands.GetAction<FloatAdd>("CW Restart", 2).add.Value = -7.5f;
        this._attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add.Value = 7.5f;
        this._attackCommands.RemoveAction("CW Restart", 1);
        this._attackCommands.RemoveAction("CCW Restart", 1);
        this._attackCommands.RemoveAction("CW Repeat", 0);
        this._attackCommands.RemoveAction("CCW Repeat", 0);
        this._attackCommands.GetAction<FloatAdd>("CW Spawn", 2).add.Value = -20f;
        this._attackCommands.GetAction<FloatAdd>("CCW Spawn", 2).add.Value = 20f;
        Log("Modifying Beam Wall");
        this._attackChoices.GetAction<Wait>("Beam Sweep L", 0).time.Value = 4.05f;
        this._attackChoices.GetAction<Wait>("Beam Sweep R", 0).time.Value = 4.05f;
        this._attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
        this._attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
        this._attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time.Value = 5.05f;
        this._attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time.Value = 5.05f;
        this._attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent.Value = "BEAM SWEEP L";
        this._attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent.Value = "BEAM SWEEP R";
        Log("Modifying Beam Burst");
        this._attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.5f;
        this._attackCommands.GetAction<Wait>("EB 1", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.5f;
        this._attackCommands.GetAction<Wait>("EB 2", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.5f;
        this._attackCommands.GetAction<Wait>("EB 3", 10).time = 0.5f;
        this._attackCommands.GetAction<SendEventByName>("EB 4", 4).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 4", 5).time = 0.6f;
        this._attackCommands.GetAction<SendEventByName>("EB 5", 5).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 5", 6).time = 0.6f;
        this._attackCommands.GetAction<SendEventByName>("EB 6", 5).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 6", 6).time = 0.6f;
        this._attackCommands.GetAction<SendEventByName>("EB 7", 8).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 7", 9).time = 0.6f;
        this._attackCommands.GetAction<SendEventByName>("EB 8", 8).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 8", 9).time = 0.6f;
        this._attackCommands.GetAction<SendEventByName>("EB 9", 8).delay = 0.6f;
        this._attackCommands.GetAction<Wait>("EB 9", 9).time = 0.8f;
        Log("Modifying Climb Beam");
        this._attackCommands.GetAction<SendEventByName>("Aim", 8).delay = 0.3f;
        this._attackCommands.GetAction<SendEventByName>("Aim", 9).delay = 0.3f;
        this._attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 0.55f;
        this._attackCommands.GetAction<Wait>("Aim", 11).time = 0.6f;
        Log("Modifying Sword Rain");
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay.Value = 0.33f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay.Value = 0.66f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay.Value = 0.99f;
        this._attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time.Value = 1f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 1).delay.Value = 1.33f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 2).delay.Value = 1.66f;
        this._attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 3).delay.Value = 1.99f;
        this._attackChoices.GetAction<Wait>("Nail Top Sweep 2", 4).time.Value = 2.5f;
        this._control.GetAction<Wait>("Rage Comb", 0).time.Value = 0.35f;
        Log("Modifying Sword Wall");
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.25f;
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.85f;
        this._attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 3.45f;
        this._attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 5f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.25f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.85f;
        this._attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 3.45f;
        this._attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 5f;
        this.AddNailWall("Nail L Sweep", "COMB R", 1.3f, 1);
        this.AddNailWall("Nail R Sweep", "COMB L", 1.3f, 1);
        this.AddNailWall("Nail L Sweep", "COMB R", 2.9f, 1);
        this.AddNailWall("Nail R Sweep", "COMB L", 2.9f, 1);
        this.AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
        this.AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
        Log("Finished.");
    }

    // Token: 0x06000009 RID: 9 RVA: 0x00003B4C File Offset: 0x00001D4C
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
                    this._attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    this.CWRepeats = 3;
                    break;
                case 3:
                    this.CWRepeats = 4;
                    break;
            }
        }
        else
        {
            bool flag = this.CWRepeats == 4;
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
        bool flag3 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 1;
        if (flag3)
        {
            bool flag4 = !this.arena1Set;
            if (flag4)
            {
                Log("Starting Arena 1");
                this.arena1Set = true;
                Log("Modifying Beam Sweeper Area");
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep L", 3).x = 89f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).time = 3.5f;
                this._beamsweepercontrol.GetAction<SetPosition>("Beam Sweep R", 4).x = 32.6f;
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(50f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).time = 3.5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep L", 2).x = 89f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).time = 3.5f;
                this._beamsweeper2control.GetAction<SetPosition>("Beam Sweep R", 3).x = 32.6f;
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(50f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).time = 3.5f;
            }
            bool flag5 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value && this.AreSpikesDowned();
            if (flag5)
            {
                foreach (GameObject item in this._firstSpikeGroup)
                {
                    item.LocateMyFSM("Control").SendEvent("UP");
                }
                foreach (GameObject item2 in this._swordSpikeGroup)
                {
                    item2.LocateMyFSM("Control").SendEvent("UP");
                }
            }
        }
        bool flag6 = this._attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2;
        if (flag6)
        {
            bool flag7 = this.AreSpikesDowned();
            if (flag7)
            {
                foreach (GameObject item3 in this._swordSpikeGroup)
                {
                    item3.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            bool flag8 = !this.arena2Set;
            if (flag8)
            {
                Log("Starting Arena 2");
                this.arena2Set = true;
                Log("Modifying Beam Sweeper Area");
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-55f, 0f, 0f);
                this._beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(55f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-55f, 0f, 0f);
                this._beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(55f, 0f, 0f);
                Log("Increasing Number of Orbs for Plats");
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min.Value = 12;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max.Value = 15;
                Log("Removing First Phase Spikes");
                foreach (GameObject item4 in this._firstSpikeGroup)
                {
                    item4.LocateMyFSM("Control").SendEvent("DOWN");
                }
                Log("Setting Sword Rain Spikes to 2 damage");
                foreach (GameObject item5 in this._swordSpikeGroup)
                {
                    item5.GetComponent<DamageHero>().damageDealt = 2;
                }
            }
            bool flag9 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value - 100 && !this.arena3Set;
            if (flag9)
            {
                Log("Starting Arena 3");
                this.arena3Set = true;
                Log("Removing Platforms");
                GameObject.Find("Radiant Plat Thick (2)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                GameObject.Find("P2 SetA/Radiant Plat Wide (2)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                GameObject.Find("Radiant Plat Small (2)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                Log("Decreasing Number of Orbs for Plats");
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).min.Value = 6;
                this._attackCommands.GetAction<RandomInt>("Orb Antic", 2).max.Value = 8;
            }
            bool flag10 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value - 500 && this.AreSpikesDowned();
            if (flag10)
            {
                foreach (GameObject item6 in this._platsSpikeGroup)
                {
                    item6.LocateMyFSM("Control").SendEvent("UP");
                }
            }
        }
        bool flag11 = base.gameObject.transform.position.y < 150f;
        if (!flag11)
        {
            bool flag12 = this.AreSpikesDowned();
            if (flag12)
            {
                foreach (GameObject item7 in this._finalSpikeGroup)
                {
                    item7.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            bool flag13 = !this.arena4Set1;
            if (flag13)
            {
                this.arena4Set1 = true;
                this._attackCommands.GetAction<Wait>("Orb Antic", 0).time.Value = 0.01f;
                this._attackCommands.GetAction<Wait>("FinalOrb Pause", 0).time.Value = 0.5f;
                this._attackChoices.GetAction<Wait>("Orb Recover", 0).time.Value = 0.01f;
            }
            bool flag14 = this._knight.gameObject.transform.position.y < 153.5f;
            if (flag14)
            {
                bool flag15 = this._hm.hp < this._phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value && this.AreSpikesDowned();
                if (flag15)
                {
                    foreach (GameObject item8 in this._climbSpikeGroup)
                    {
                        item8.LocateMyFSM("Control").SendEvent("UP");
                    }
                }
            }
            else
            {
                bool flag16 = !this.arena4Set2 && this._hm.hp <= 1050;
                if (flag16)
                {
                    this.arena4Set2 = true;
                    Log("Knight pos y: " + this._knight.gameObject.transform.position.y.ToString());
                    this._attackCommands.GetAction<Wait>("FinalOrb Pause", 0).time.Value = 0.25f;
                }
            }
        }
    }

    // Token: 0x0600000A RID: 10 RVA: 0x00004624 File Offset: 0x00002824
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

    // Token: 0x0600000B RID: 11 RVA: 0x00004684 File Offset: 0x00002884
    private bool AreSpikesDowned()
    {
        return this._spikeTemplate.LocateMyFSM("Control").ActiveStateName == "Downed";
    }

    // Token: 0x0600000C RID: 12 RVA: 0x000046C0 File Offset: 0x000028C0
    private static FsmEvent GetFsmEventByName(PlayMakerFSM fsm, string eventName)
    {
        FsmEvent[] fsmEvents = fsm.FsmEvents;
        foreach (FsmEvent fsmEvent in fsmEvents)
        {
            bool flag = fsmEvent.Name == eventName;
            if (flag)
            {
                return fsmEvent;
            }
        }
        return null;
    }

    // Token: 0x0600000D RID: 13 RVA: 0x0000470B File Offset: 0x0000290B
    private static void Log(object obj)
    {
        Modding.Logger.Log("[Supernova Radiance]: " + ((obj != null) ? obj.ToString() : null));
    }

    // Token: 0x04000003 RID: 3
    private GameObject _spikeTemplate;

    // Token: 0x04000004 RID: 4
    private List<GameObject> _firstSpikeGroup = new List<GameObject>();

    // Token: 0x04000005 RID: 5
    private List<GameObject> _swordSpikeGroup = new List<GameObject>();

    // Token: 0x04000006 RID: 6
    private List<GameObject> _platsSpikeGroup = new List<GameObject>();

    // Token: 0x04000007 RID: 7
    private List<GameObject> _climbSpikeGroup = new List<GameObject>();

    // Token: 0x04000008 RID: 8
    private List<GameObject> _finalSpikeGroup = new List<GameObject>();

    // Token: 0x04000009 RID: 9
    private GameObject _radiantBeam0;

    // Token: 0x0400000A RID: 10
    private GameObject _radiantBeam1;

    // Token: 0x0400000B RID: 11
    private GameObject _radiantBeam2;

    // Token: 0x0400000C RID: 12
    private GameObject _radiantBeam3;

    // Token: 0x0400000D RID: 13
    private GameObject _radiantBeam4;

    // Token: 0x0400000E RID: 14
    private GameObject _radiantBeam5;

    // Token: 0x0400000F RID: 15
    private GameObject _radiantBeam6;

    // Token: 0x04000010 RID: 16
    private GameObject _radiantBeam7;

    // Token: 0x04000011 RID: 17
    private List<GameObject> _radiantBeams = new List<GameObject>();

    // Token: 0x04000012 RID: 18
    private GameObject _beamsweeper;

    // Token: 0x04000013 RID: 19
    private GameObject _beamsweeper2;

    // Token: 0x04000014 RID: 20
    private HealthManager _hm;

    // Token: 0x04000015 RID: 21
    private PlayMakerFSM _attackChoices;

    // Token: 0x04000016 RID: 22
    private PlayMakerFSM _attackCommands;

    // Token: 0x04000017 RID: 23
    private PlayMakerFSM _control;

    // Token: 0x04000018 RID: 24
    private PlayMakerFSM _phaseControl;

    // Token: 0x04000019 RID: 25
    private PlayMakerFSM _beamsweepercontrol;

    // Token: 0x0400001A RID: 26
    private PlayMakerFSM _beamsweeper2control;

    // Token: 0x0400001B RID: 27
    private GameObject _knight;

    // Token: 0x0400001C RID: 28
    private int CWRepeats = 0;

    // Token: 0x0400001D RID: 29
    private bool arena1Set = false;

    // Token: 0x0400001E RID: 30
    private bool arena2Set = false;

    // Token: 0x0400001F RID: 31
    private bool arena3Set = false;

    // Token: 0x04000020 RID: 32
    private bool arena4Set1 = false;

    // Token: 0x04000021 RID: 33
    private bool arena4Set2 = false;

    // Token: 0x04000022 RID: 34
    private const int reducedPlatsHealth = 100;

    // Token: 0x04000023 RID: 35
    private const int platSpikesHealth = 500;

    // Token: 0x04000024 RID: 36
    private const int finalOrbSpeed = 1050;
}
