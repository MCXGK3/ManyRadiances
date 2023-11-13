using Object = UnityEngine.Object;

public class any1 : MonoBehaviour
{
    private int HP = 2000;

    private GameObject _spikeMaster;

    private GameObject _spikeTemplate;

    private GameObject _spikeClone;

    private GameObject _spikeClone2;

    private GameObject _spikeClone3;

    private GameObject _spikeClone4;

    private GameObject _spikeClone5;

    private GameObject _beamsweeper;

    private GameObject _beamsweeper2;

    private GameObject _knight;

    private HealthManager _hm;

    private PlayMakerFSM _attackChoices;

    private PlayMakerFSM _attackCommands;

    private PlayMakerFSM _control;

    private PlayMakerFSM _phaseControl;

    private PlayMakerFSM _spikeMasterControl;

    private PlayMakerFSM _beamsweepercontrol;

    private PlayMakerFSM _beamsweeper2control;

    private PlayMakerFSM _spellControl;

    private int CWRepeats = 0;

    private bool fullSpikesSet = false;

    private bool disableBeamSet = false;

    private bool arena2Set = false;

    private bool onePlatSet = false;

    private bool platSpikesSet = false;

    private const int fullSpikesHealth = 750;

    private const int onePlatHealth = 500;

    private const int platSpikesHealth = 500;

    private const float nailWallDelay = 0.8f;

    private bool isAttacking = false;

    private string now;

    private void Awake()
    {
        Log("Added AbsRad MonoBehaviour");
        _hm = base.gameObject.GetComponent<HealthManager>();
        _attackChoices = base.gameObject.LocateMyFSM("Attack Choices");
        _attackCommands = base.gameObject.LocateMyFSM("Attack Commands");
        _control = base.gameObject.LocateMyFSM("Control");
        _phaseControl = base.gameObject.LocateMyFSM("Phase Control");
        _spikeMaster = GameObject.Find("Spike Control");
        _spikeMasterControl = _spikeMaster.LocateMyFSM("Control");
        _spikeTemplate = GameObject.Find("Radiant Spike");
        _beamsweeper = GameObject.Find("Beam Sweeper");
        _beamsweeper2 = Object.Instantiate(_beamsweeper);
        _beamsweeper2.AddComponent<Any1.BeamSweeperClone>();
        _beamsweepercontrol = _beamsweeper.LocateMyFSM("Control");
        _beamsweeper2control = _beamsweeper2.LocateMyFSM("Control");
        _knight = GameObject.Find("Knight");
        _spellControl = _knight.LocateMyFSM("Spell Control");
    }

    private void Start()
    {
        Log("Changing fight variables...");
        _hm.hp += HP;
        _phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 1750;
        _phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 1000;
        _phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 1000;
        _phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value += 1000;
        _control.GetAction<SetHP>( "Scream", 7).hp = 2000;
        _spikeClone = Object.Instantiate(_spikeTemplate);
        _spikeClone.transform.SetPositionX(58f);
        _spikeClone.transform.SetPositionY(153.8f);
        _spikeClone2 = Object.Instantiate(_spikeTemplate);
        _spikeClone2.transform.SetPositionX(57.5f);
        _spikeClone2.transform.SetPositionY(153.8f);
        _spikeClone3 = Object.Instantiate(_spikeTemplate);
        _spikeClone3.transform.SetPositionX(57f);
        _spikeClone3.transform.SetPositionY(153.8f);
        _spikeClone4 = Object.Instantiate(_spikeTemplate);
        _spikeClone4.transform.SetPositionX(58.5f);
        _spikeClone4.transform.SetPositionY(153.8f);
        _spikeClone5 = Object.Instantiate(_spikeTemplate);
        _spikeClone5.transform.SetPositionX(59f);
        _spikeClone5.transform.SetPositionY(153.8f);
        _spikeClone.LocateMyFSM("Control").SendEvent("DOWN");
        _spikeClone2.LocateMyFSM("Control").SendEvent("DOWN");
        _spikeClone3.LocateMyFSM("Control").SendEvent("DOWN");
        _spikeClone4.LocateMyFSM("Control").SendEvent("DOWN");
        _spikeClone5.LocateMyFSM("Control").SendEvent("DOWN");
        _attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.1f;
        _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 12;
        _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 10;
        _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 14;
        _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.1f;
        _attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.1f;
        _attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.5f;
        _attackCommands.GetAction<Wait>("CW Repeat", 0).time = -0.5f;
        _attackCommands.GetAction<Wait>("CCW Repeat", 0).time = -0.5f;
        _attackCommands.GetAction<FloatAdd>("CW Restart", 2).add = -10f;
        _attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add = 10f;
        _attackCommands.RemoveAction("CW Restart", 1);
        _attackCommands.RemoveAction("CCW Restart", 1);
        _attackChoices.GetAction<Wait>("Beam Sweep L", 0).time = 0.5f;
        _attackChoices.GetAction<Wait>("Beam Sweep R", 0).time = 0.5f;
        _attackChoices.ChangeTransition( "A1 Choice", "BEAM SWEEP R", "Beam Sweep L");
        _attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");
        _attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time = 5.05f;
        _attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time = 5.05f;
        _attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent = "BEAM SWEEP L";
        _attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent = "BEAM SWEEP R";
        _attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 1", 10).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 2", 10).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 3", 10).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 4", 4).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 4", 5).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 5", 5).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 5", 6).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 6", 5).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 6", 6).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 7", 8).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 7", 9).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 8", 8).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 8", 9).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 9", 8).delay = 0.3f;
        _attackCommands.GetAction<Wait>("EB 9", 9).time = 0.5f;
        _attackCommands.GetAction<SendEventByName>("Aim", 10).delay = 1f;
        _attackCommands.GetAction<Wait>("Aim", 11).time = 0.5f;
        _attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time = 0.05f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay = 0.35f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay = 0.7f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay = 1.05f;
        _attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time = 2.3f;
        _control.GetAction<Wait>("Rage Comb", 0).time = 0.35f;
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.25f;
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.85f;
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 3.45f;
        _attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 5f;
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.25f;
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.85f;
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 3.45f;
        _attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 5f;
        AddNailWall("Nail L Sweep", "COMB R", 1.3f, 1);
        AddNailWall("Nail R Sweep", "COMB L", 1.3f, 1);
        AddNailWall("Nail L Sweep", "COMB R", 2.9f, 1);
        AddNailWall("Nail R Sweep", "COMB L", 2.9f, 1);
        AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
        AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);
        Log("fin.");
    }

    private void Update()
    {
        if (_attackCommands.FsmVariables.GetFsmBool("Repeated").Value)
        {
            switch (CWRepeats)
            {
                case 1:
                    CWRepeats = 2;
                    break;
                case 0:
                    CWRepeats = 1;
                    _attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    break;
            }
        }
        else if (CWRepeats == 2)
        {
            CWRepeats = 0;
        }
        if (_beamsweepercontrol.ActiveStateName == _beamsweeper2control.ActiveStateName)
        {
            string activeStateName = _beamsweepercontrol.ActiveStateName;
            string text = activeStateName;
            if (text != null)
            {
                if (!(text == "Beam Sweep L"))
                {
                    if (text == "Beam Sweep R")
                    {
                        _beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP L"));
                    }
                }
                else
                {
                    _beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP R"));
                }
            }
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value - 750 && !fullSpikesSet)
        {
            fullSpikesSet = true;
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 0).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 1).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 2).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 3).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 4).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 0).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 1).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 2).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 3).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Right", 4).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave L", 2).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave L", 3).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave L", 4).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave L", 5).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave L", 6).sendEvent = "UP";
            _spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMin = 0.1f;
            _spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMax = 0.1f;
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 2).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 3).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 4).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 5).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 6).sendEvent = "UP";
            _spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMin = 0.1f;
            _spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMax = 0.1f;
            _spikeMasterControl.SetState("Spike Waves");
            _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.1f;
            _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 12;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 10;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 14;
            /*_attackCommands.GetAction<SendEventByName>("EB 1", 2).delay = 0.3f;
            _attackCommands.GetAction<SendEventByName>("EB 1", 3).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 1", 8).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 1", 9).delay = 0.5f;
            _attackCommands.GetAction<Wait>("EB 1", 10).time = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 2", 3).delay = 0.3f;
            _attackCommands.GetAction<SendEventByName>("EB 2", 4).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 2", 8).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 2", 9).delay = 0.5f;
            _attackCommands.GetAction<Wait>("EB 2", 10).time = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 3", 3).delay = 0.3f;
            _attackCommands.GetAction<SendEventByName>("EB 3", 4).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 3", 8).delay = 0.5f;
            _attackCommands.GetAction<SendEventByName>("EB 3", 9).delay = 0.5f;
            _attackCommands.GetAction<Wait>("EB 3", 10).time = 0.5f;*/
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value + 30 && !disableBeamSet)
        {
            disableBeamSet = true;
            //Log("BEGIN");
            _attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP L", "Orb Wait");
			_attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Eye Beam Wait");
            /*_attackChoices.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[4] = 0f;
            _attackChoices.GetAction<SendRandomEventV3>("A1 Choice", 1).missedMax[4] = 9999;
            _attackChoices.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[5] = 0f;
            _attackChoices.GetAction<SendRandomEventV3>("A1 Choice", 1).missedMax[5] = 9999;*/
            //Log("BEAMSET DISABLED IN "+_hm.hp);
        }
        /*if(true)
		{
            string activeStateName = _attackChoices.ActiveStateName;
            if (now ==activeStateName) ;
			else { Log(activeStateName+" IN "+_hm.hp); }
			now= activeStateName;
		}
        if (_attackChoices.ActiveStateName == "Eye Beam Recover")
		{
			isAttacking = false;
		}
        if (_attackChoices.ActiveStateName == "Orb Recover")
        {
            isAttacking = false;
        }
        if (_attackChoices.ActiveStateName=="Beam Sweep L" && !isAttacking )
		{
			//Log("BEAM FIRED LEFT IN " + _hm.hp);
		}
        if (_attackChoices.ActiveStateName == "Orb Wait"&&!isAttacking)
        {
			isAttacking = true;
            //Log("ORB FIRED IN " + _hm.hp);
        }
        if (_attackChoices.ActiveStateName == "Beam Sweep R" && !isAttacking)
        {
          //  Log("BEAM FIRED RIGHT IN " + _hm.hp);
        }
        if (_attackChoices.ActiveStateName == "Eye Beam Wait" && !isAttacking)
        {
            isAttacking = true;
           // Log("EYE BEAM FIRED IN " + _hm.hp);
        }*/
        if (_attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2 && !arena2Set)
        {
            Modding.Logger.Log("[AnyRadiance1.0] Starting Phase 2");
            arena2Set = true;
            _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 12;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 10;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 14;
            _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.1f;
            _beamsweepercontrol.GetAction<SetPosition>("Beam Sweep L", 3).x = 89f;
            _beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).vector = new Vector3(-75f, 0f, 0f);
            _beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep L", 5).time = 3f;
            _beamsweepercontrol.GetAction<SetPosition>("Beam Sweep R", 4).x = 32.6f;
            _beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).vector = new Vector3(75f, 0f, 0f);
            _beamsweepercontrol.GetAction<iTweenMoveBy>("Beam Sweep R", 6).time = 3f;
            _beamsweeper2control.GetAction<SetPosition>("Beam Sweep L", 2).x = 89f;
            _beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).vector = new Vector3(-75f, 0f, 0f);
            _beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep L", 4).time = 3f;
            _beamsweeper2control.GetAction<SetPosition>("Beam Sweep R", 3).x = 32.6f;
            _beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).vector = new Vector3(75f, 0f, 0f);
            _beamsweeper2control.GetAction<iTweenMoveBy>("Beam Sweep R", 5).time = 3f;
        }
        if (!(base.gameObject.transform.position.y >= 150f))
        {
            return;
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 500)
        {
            GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
            if (!onePlatSet)
            {
                onePlatSet = true;
                Log("Removing upper right platform");
                _attackCommands.GetAction<Wait>("Orb Antic", 0).time = 0.01f;
                _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 5;
                _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 4;
                _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 6;
                _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.01f;
                _attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.01f;
                _attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.1f;
            }
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value - 500 - 500)
        {
            _spikeClone.LocateMyFSM("Control").SendEvent("UP");
            _spikeClone2.LocateMyFSM("Control").SendEvent("UP");
            _spikeClone3.LocateMyFSM("Control").SendEvent("UP");
            _spikeClone4.LocateMyFSM("Control").SendEvent("UP");
            _spikeClone5.LocateMyFSM("Control").SendEvent("UP");
            if (!platSpikesSet)
            {
                platSpikesSet = true;
                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
            }
        }
    }

    private void AddNailWall(string stateName, string eventName, float delay, int index)
    {
        _attackChoices.InsertAction(stateName, (FsmStateAction)new SendEventByName
        {
            eventTarget = _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 0).eventTarget,
            sendEvent = eventName,
            delay = delay,
            everyFrame = false
        }, index);
    }

    private static FsmEvent GetFsmEventByName(PlayMakerFSM fsm, string eventName)
    {
        FsmEvent[] fsmEvents = fsm.FsmEvents;
        foreach (FsmEvent fsmEvent in fsmEvents)
        {
            if (fsmEvent.Name == eventName)
            {
                return fsmEvent;
            }
        }
        return null;
    }

    private static void Log(object obj)
    {
        Modding.Logger.Log("[AnyRadiance1.0] " + obj);
    }
}
