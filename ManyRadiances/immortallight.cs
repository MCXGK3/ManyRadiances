using Modding;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using HutongGames.PlayMaker.Actions;
using WavLib;
using Satchel;
using Satchel.Futils;
using HutongGames.PlayMaker;

namespace ManyRadiances
{
    internal class immortallight : MonoBehaviour
    {
        PlayMakerFSM _con;
        PlayMakerFSM _cho;
        PlayMakerFSM _com;
        PlayMakerFSM _tele;
        PlayMakerFSM _phcon;
        HealthManager _hp;
        GameObject oribeam;
        GameObject fakebb;
        GameObject glow;
        GameObject bb = null;
        List<GameObject> orbList = new List<GameObject>();
        List<GameObject> useorbs = new List<GameObject>();
        List<GameObject> a2beams = new List<GameObject>();
        GameObject knight;
        bool foundbeam = false;
        bool final = false;
        bool finalSet1 = false;
        bool finalSet2 = false;
        private AudioClip BeamAnticClip;
        private AudioClip BeamFireClip;
        private AudioClip tip;
        private string convoTitle;


        int initHp = 5000;
        int spikeWaves = 4000;
        int a1Rage = 3000;
        int stun = 2500;
        int p5Acend = 1000;
        int Scream = 2000;


        float gap = 0.7f;
        float gapnow = 0f;
        float height = 80;
        float bbgap = 0.2f;
        float bbgapnow = 0f;
        float finalgap = 5f;
        float finalgapnow = 0f;


        float hour = (90f - System.DateTime.Now.Hour * 30f) % 360f;
        float minute = (90f - System.DateTime.Now.Minute * 6f) % 360f;
        float second = (90f - System.DateTime.Now.Second * 6f) % 360f;
        GameObject timeClock = new GameObject();
        GameObject Clock;
        GameObject hourHand;
        GameObject minuteHand;
        GameObject secondHand;
        AudioSource clock;
        float zoomscale;
        bool zoom = false;

        private void Awake()
        {
            _cho = gameObject.LocateMyFSM("Attack Choices");
            _com = gameObject.LocateMyFSM("Attack Commands");
            _con = gameObject.LocateMyFSM("Control");
            _tele = gameObject.LocateMyFSM("Teleport");
            _hp = gameObject.GetComponent<HealthManager>();
            _phcon = gameObject.LocateMyFSM("Phase Control");

            tip = LoadAudioClip("ManyRadiances.Resources.clock.wav");
            ModifyRadiance();
        }


        private IEnumerator beamFire(GameObject beam, float antic, float fire, float end)
        {
            AudioSource aus;
            aus = beam.GetAddComponent<AudioSource>();
            beam.SetActive(true);
            PlayMakerFSM beamFSM = beam.LocateMyFSM("Control");
            aus.PlayOneShot(BeamAnticClip, 0.5f * GameManager.instance.gameSettings.soundVolume / 10f);
            beamFSM.SendEvent("ANTIC");
            yield return new WaitForSeconds(antic);
            aus.PlayOneShot(BeamFireClip, 0.5f * GameManager.instance.gameSettings.soundVolume / 10f);
            beamFSM.SendEvent("FIRE");
            yield return new WaitForSeconds(fire);
            beamFSM.SendEvent("END");
            yield return new WaitForSeconds(end);
            yield return new WaitForSeconds(1f);
            Destroy(beam);
            yield break;
        }

        private IEnumerator beginbeams(GameObject orb)
        {
            GameObject beam = Instantiate(oribeam);
            GameObject beam2 = Instantiate(oribeam);
            beam.transform.SetRotation2D(0);
            beam2.transform.SetRotation2D(0);
            beam.transform.position = orb.transform.position;
            beam2.transform.position = orb.transform.position;
            //beam.transform.position += new Vector3(0, -20f, 0);
            float f = UnityEngine.Random.Range(0f, 180f);
            if (_cho.GetVariable<FsmInt>("Arena").Value != 1)
            {
                float num = knight.transform.position.y - orb.transform.position.y;
                float num2 = knight.transform.position.x - orb.transform.position.x;
                float num3;
                for (num3 = Mathf.Atan2(num, num2) * (180f / (float)Math.PI); num3 < 0f; num3 += 360f)
                {
                }
                f = num3;
            }
            beam.transform.SetRotation2D(f);
            beam2.transform.SetRotation2D(180f + f);
            //beams[i].transform.Translate((float)(-20f * Math.Cos(f)), (float)(-20f * Math.Sin(f)), 0,Space.Self);
            beam.SetActive(true);
            beam2.SetActive(true);
            if (_cho.GetVariable<FsmInt>("Arena").Value != 1)
            {
                a2beams.Add(beam);
                a2beams.Add(beam2);
            }
            else
            {
                StartCoroutine(beamFire(beam, 1.0f, 0.3f, 0f));
                StartCoroutine(beamFire(beam2, 1.0f, 0.3f, 0f));
            }

            yield break;
            //StartCoroutine(beginbeam(beams[i],knight.transform.position,transform));

        }

        private IEnumerator beginsweeper(bool left, float floor)
        {

            float ll, lr, rl, rr;
            float le, ri;
            if (floor < 30f)
            {
                ll = 43f; lr = 45f;
                rl = 78f; rr = 80f;
            }
            else
            {
                ll = 32f; lr = 34f;
                rl = 83f; rr = 85f;
            }
            le = UnityEngine.Random.Range(ll, lr);
            ri = UnityEngine.Random.Range(rl, rr);
            if (left)
            {
                for (float i = le; i <= ri; i += 2.5f)
                {
                    GameObject beam = Instantiate(oribeam);
                    beam.transform.position = new Vector3(i, floor);
                    beam.transform.SetRotation2D(90f);
                    StartCoroutine(beamFire(beam, 1.0f, 0.5f, 0f));
                    yield return new WaitForSeconds(0.05f);
                }
            }
            else
            {
                for (float i = ri; i >= le; i -= 2.5f)
                {
                    GameObject beam = Instantiate(oribeam);
                    beam.transform.position = new Vector3(i, floor - 5f);
                    beam.transform.SetRotation2D(90f);
                    StartCoroutine(beamFire(beam, 1.0f, 0.5f, 0f));
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield break;

        }

        private IEnumerator SendEventToNail(GameObject nail, float delay, string s)
        {
            yield return new WaitForSeconds(delay);
            nail.LocateMyFSM("Control").SendEvent(s);
            yield break;
        }
        private void SendEventToBB(GameObject go, string eve)
        {
            if (go != null)
            {
                List<GameObject> list = new List<GameObject>();
                go.FindAllChildren(list);
                foreach (GameObject go2 in list)
                {
                    PlayMakerFSM bbcon;
                    bbcon = go2.LocateMyFSM("Control");

                    bbcon.SendEvent(eve);
                }
                list.Clear();
            }
        }


        private IEnumerator BBrorate(GameObject eb, float v, float time, bool random)
        {
            float timer = 0f;
            if (UnityEngine.Random.Range(0, 2) == 0 || random)
            {
                while (timer <= time)
                {
                    timer += Time.deltaTime;
                    eb.transform.Rotate(Vector3.back, -v * Time.deltaTime, Space.Self);
                    yield return null;
                }
            }
            else
            {
                while (timer <= time)
                {
                    timer += Time.deltaTime;
                    eb.transform.Rotate(Vector3.back, v * Time.deltaTime, Space.Self);
                    yield return null;
                }
            }
            yield break;
        }


        private void Start()
        {
            knight = GameObject.Find("Knight");

            //获得激光的两个声音
            BeamAnticClip = _com.GetAction<AudioPlayerOneShotSingle>("Aim", 1).audioClip.Value as AudioClip;
            BeamFireClip = _com.GetAction<AudioPlayerOneShotSingle>("Aim", 3).audioClip.Value as AudioClip;



            ModifyOrbs();
            ModifyRage();
            ModifyBeamSweeper();
            ModifyEyeBeams();
            ModifyNailFan();
            ModifyNailWall(true);
            ModifySpike();
            ModifyAcend();
            ModifyFinal();
            ModifyScene();

            _con.InsertCustomAction("Tendrils1", () => { ModifyPlatsPhase(); }, 0);



            for (int i = 0; i < 73; i++)
            {
                useorbs.Add(Instantiate(_com.GetAction<SpawnObjectFromGlobalPool>("Spawn Fireball", 1).gameObject.Value));
                useorbs[i].LocateMyFSM("Orb Control").RemoveTransition("Stop Particles", "FINISHED");
                useorbs[i].LocateMyFSM("Orb Control").GetAction<AudioPlaySimple>("Init", 0).volume = 0.1f;
            }
            useorbs[0].LocateMyFSM("Orb Control").GetAction<SetDamageHeroAmount>("Init", 5).damageDealt = 0;
            useorbs[0].LocateMyFSM("Orb Control").GetAction<SetDamageHeroAmount>("Dissipate", 3).damageDealt = 0;


            foreach (var orb in useorbs)
            {
                orb.LocateMyFSM("Orb Control").InsertCustomAction("Stop Particles", () =>
                {
                    orb.SetActive(false);
                }, 3);
                orb.SetActive(false);
            }

        }

        private void ModifyScene()
        {
            Scene scene = SceneUtils.getCurrentScene();
            string[] strings = { "GG_scenery_0004_17 (80)", "GG_scenery_0004_17 (86)", "GG_scenery_0004_17 (87)", "GG_scenery_0004_17 (89)", "GG_scenery_0004_17 (90)" };
            foreach (var go in scene.GetAllGameObjects())
            {
                if (go.name.IsAny(strings))
                {
                    go.SetActive(false);
                }
            }
        }


        private void ModifyBeamSweeper()
        {
            _cho.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[6] = 0.5f;
            _cho.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[7] = 0.5f;
            _cho.RemoveAction("Beam Sweep L", 1);
            _cho.InsertCustomAction("Beam Sweep L", () =>
            {
                StartCoroutine(beginsweeper(true, 20f));
            }, 1);
            _cho.RemoveAction("Beam Sweep R", 1);
            _cho.InsertCustomAction("Beam Sweep R", () =>
            {
                StartCoroutine(beginsweeper(false, 20f));
            }, 1);
            _cho.RemoveAction("Beam Sweep L 2", 1);
            _cho.InsertCustomAction("Beam Sweep L 2", () =>
            {
                StartCoroutine(beginsweeper(true, 30f));
            }, 1);
            _cho.RemoveAction("Beam Sweep R 2", 1);
            _cho.InsertCustomAction("Beam Sweep R 2", () =>
            {
                StartCoroutine(beginsweeper(false, 30f));
            }, 1);

        }

        private void ModifyRage()
        {

            _con.ChangeTransition("Rage1 Start", "FINISHED", "Rage Eyes");
            _com.GetAction<SendEventByName>("EB 4", 3).delay = 0.7f;
            _com.GetAction<SendEventByName>("EB 4", 4).delay = 1.0f;
            _com.GetAction<SendEventByName>("EB 5", 4).delay = 0.7f;
            _com.GetAction<SendEventByName>("EB 5", 5).delay = 1.0f;
            _com.GetAction<SendEventByName>("EB 6", 4).delay = 0.7f;
            _com.GetAction<SendEventByName>("EB 6", 5).delay = 1.0f;
            _con.InsertCustomAction("Climb Plats1", () =>
            {
                _com.ChangeTransition("EB 4", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB 5", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB 6", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB Glow End 2", "FINISHED", "Comb Top");
                _com.GetVariable<FsmGameObject>("Eye Beam Glow").Value.SetActive(false);
            }, 0);
            _con.RemoveAction("Stun1 Start", 12);
            _con.RemoveAction("Stun1 Out", 8);
        }

        private void ModifyOrbs()
        {
            //改变位置
            _com.GetAction<DistanceBetweenPoints>("Orb Pos", 5).point2 = _com.GetVariable<FsmVector3>("Hero Pos");
            //加速
            _com.GetAction<Wait>("Orb Pause", 0).time = 0.10f;
            _com.GetAction<Wait>("Orb Antic", 0).time = 0.05f;

            //增加数量
            _com.GetAction<RandomInt>("Orb Antic", 2).min = 6;
            _com.GetAction<RandomInt>("Orb Antic", 2).max = 8;

            //去除send FIRE
            _com.RemoveAction("Spawn Fireball", 3);

            //给orblist添加本次光球并开启光球激光
            _com.GetAction<Wait>("Orb Summon", 2).time = 0.03f;
            _com.InsertCustomAction("Spawn Fireball", () =>
            {
                GameObject orb = _com.FsmVariables.FindFsmGameObject("Projectile").Value;
                orbList.Add(orb);
                StartCoroutine(beginbeams(orb));
            }, 3);

            //激光结束时释放光球（p4光球将不会释放）
            _com.InsertCustomAction("Orb End", () => {
                foreach (var orb in orbList)
                {
                    if (orb != null)
                    {
                        PlayMakerFSM orbFSM = orb.LocateMyFSM("Orb Control");
                        orbFSM.SendEvent("FIRE");
                    }
                }
                orbList.Clear();
            }, 0);


        }

        private void ModifyEyeBeams()
        {
            FsmState EB1 = _com.GetState("EB 1");
            FsmState EB2 = _com.GetState("EB 2");
            FsmState EB3 = _com.GetState("EB 3");
            FsmState EB7 = _com.GetState("EB 7");
            FsmState EB8 = _com.GetState("EB 8");
            FsmState EB9 = _com.GetState("EB 9");
            EB1.GetAction<SendEventByName>(8).delay = 0.5f;
            EB1.GetAction<SendEventByName>(9).delay = 0.8f;
            EB1.InsertCustomAction(() =>
            {
                float roration = _com.FsmVariables.FindFsmGameObject("Eye Beam Burst1").Value.transform.rotation.eulerAngles.z;
                roration += UnityEngine.Random.Range(10f, 30f);
                _com.FsmVariables.FindFsmGameObject("Eye Beam Burst2").Value.transform.SetRotation2D(roration);
                roration += UnityEngine.Random.Range(10f, 30f);
                _com.FsmVariables.FindFsmGameObject("Eye Beam Burst3").Value.transform.SetRotation2D(roration);
                SendEventToBB(_com.FsmVariables.FindFsmGameObject("Eye Beam Burst2").Value, "ANTIC");
                SendEventToBB(_com.FsmVariables.FindFsmGameObject("Eye Beam Burst3").Value, "ANTIC");
                StartCoroutine(BBrorate(_com.FsmVariables.FindFsmGameObject("Eye Beam Burst1").Value, 40f, 0.5f, false));
            }, 8);

            EB2.GetAction<SendEventByName>(8).delay = 0.5f;
            EB2.GetAction<SendEventByName>(9).delay = 0.8f;
            EB2.InsertCustomAction(() =>
            {
                StartCoroutine(BBrorate(_com.FsmVariables.FindFsmGameObject("Eye Beam Burst2").Value, 40f, 0.5f, false));
            }, 8);
            EB2.RemoveAction(6);

            EB3.GetAction<SendEventByName>(8).delay = 0.5f;
            EB3.GetAction<SendEventByName>(9).delay = 0.8f;
            EB3.InsertCustomAction(() =>
            {
                StartCoroutine(BBrorate(_com.FsmVariables.FindFsmGameObject("Eye Beam Burst3").Value, 40f, 0.5f, false));
            }, 8);
            EB3.RemoveAction(6);
        }

        private void ModifyNailWall(bool rel)
        {
            _com.GetAction<SetFsmInt>("Comb L 2", 1).setValue = 1;
            _com.GetAction<SetFsmInt>("Comb R 2", 1).setValue = 2;
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 0).sendEvent = "COMB L2";
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 1).sendEvent = "COMB R2";
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 2).sendEvent = "COMB L2";
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 3).sendEvent = "COMB R2";

            _cho.GetAction<SendEventByName>("Nail L Sweep", 1).sendEvent = "COMB R2";
            _cho.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.2f;
            _cho.GetAction<SendEventByName>("Nail L Sweep", 2).sendEvent = "COMB TOP";



            _cho.GetAction<SendEventByName>("Nail R Sweep", 1).sendEvent = "COMB L2";
            _cho.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.2f;
            _cho.GetAction<SendEventByName>("Nail R Sweep", 2).sendEvent = "COMB TOP";



        }

        private void ModifyNailWall()
        {
            _com.InsertCustomAction("Comb Top", () =>
            {
                _com.FsmVariables.FindFsmGameObject("Attack Obj").Value.LocateMyFSM("Control").FsmVariables.GetFsmInt("Type").Value = UnityEngine.Random.Range(1, 6);
            }, 3);
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 0).delay = 0f;
            _cho.GetAction<SendEventByName>("Nail Top Sweep", 1).delay = 0.5f;
            _cho.GetAction<Wait>("Nail Top Sweep", 4).time = 3.0f;
            _cho.RemoveAction("Nail Top Sweep", 2);
            _cho.RemoveAction("Nail Top Sweep", 2);
            _cho.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[2] = 0.35f;
            _com.InsertCustomAction("Comb L", () =>
            {
                _com.FsmVariables.FindFsmGameObject("Attack Obj").Value.LocateMyFSM("Control").FsmVariables.GetFsmInt("Type").Value = UnityEngine.Random.Range(1, 6);
            }, 3);
            _cho.GetAction<SendEventByName>("Nail L Sweep", 0).delay = 0f;
            _cho.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.5f;
            _cho.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 3.0f;
            _cho.GetAction<Wait>("Nail L Sweep", 3).time = 3.0f;
            _cho.RemoveAction("Nail L Sweep", 2);
            _cho.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[1] = 0.25f;

            _com.InsertCustomAction("Comb R", () =>
            {
                _com.FsmVariables.FindFsmGameObject("Attack Obj").Value.LocateMyFSM("Control").FsmVariables.GetFsmInt("Type").Value = UnityEngine.Random.Range(1, 6);
            }, 3);
            _cho.GetAction<SendEventByName>("Nail R Sweep", 0).delay = 0f;
            _cho.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.5f;
            _cho.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 3.0f;
            _cho.GetAction<Wait>("Nail R Sweep", 3).time = 3.0f;
            _cho.RemoveAction("Nail L Sweep", 2);
            _cho.GetAction<SendRandomEventV3>("A1 Choice", 1).weights[0] = 0.25f;

        }

        private void ModifyNailFan()
        {
            //剑加密
            _com.GetAction<Wait>("Nail Fan", 2).time = 0.2f;
            _com.GetAction<SetIntValue>("Nail Fan", 4).intValue = 24;
            _com.GetAction<SetBoolValue>("Nail Fan", 5).boolValue = true;
            _com.GetAction<FloatAdd>("CW Spawn", 2).add = 15f;
            _com.GetAction<FloatAdd>("CCW Spawn", 2).add = 15f;

            //剑分离
            _com.RemoveAction("CW Spawn", 4);
            _com.InsertAction("CW Spawn", new Wait { time = 0.07f, finishEvent = new FsmEvent("FINISHED") }, 4);
            _com.InsertCustomAction("CW Msg", () =>
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 0.4f, "FLY"));
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 2.4f, "NAIL END"));
                }
                else
                {
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 0.4f, "FAN ATTACK CW"));
                }
            }, 1);
            _com.RemoveAction("CCW Spawn", 4);
            _com.InsertAction("CCW Spawn", new Wait { time = 0.07f, finishEvent = new FsmEvent("FINISHED") }, 4);
            _com.InsertCustomAction("CCW Msg", () =>
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 0.4f, "FLY"));
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 2.4f, "NAIL END"));
                }
                else
                {
                    StartCoroutine(SendEventToNail(_com.FsmVariables.FindFsmGameObject("Attack Obj").Value, 0.4f, "FAN ATTACK CCW"));
                }
            }, 1);


            //剑间隔
            _com.GetAction<Wait>("CW Restart", 1).time = 0f;
            _com.GetAction<Wait>("CCW Restart", 1).time = 0f;

        }

        private void ModifySpike()
        {
            PlayMakerFSM _spikecon = GameObject.Find("Spike Control").LocateMyFSM("Control");
            List<GameObject> spikes = new List<GameObject>();
            spikes.Add(GameObject.Find("Radiant Spike"));
            for (int i = 1; i <= 24; i++)
            {
                string name = "Radiant Spike " + "(" + i + ")";
                spikes.Add(GameObject.Find(name));
            }
            spikes.Sort((a, b) => { return a.transform.GetPositionX() < b.transform.GetPositionX() ? 1 : -1; });
            foreach (GameObject spike in spikes)
            {

                spike.LocateMyFSM("Control").GetAction<Wait>("Floor Antic", 2).time = 1f;
                spike.LocateMyFSM("Hero Saver").RemoveAction("Send", 0);
                spike.LocateMyFSM("Hero Saver").InsertCustomAction("Send", () =>
                {
                    int j = spikes.IndexOf(spike) + 1;
                    int i = spikes.IndexOf(spike) - 1;
                    while (j < spikes.Count)
                    {
                        string activename = spikes[j].LocateMyFSM("Control").ActiveStateName;
                        if (activename == "Down" || activename == "Downed") break;
                        else
                        {
                            spikes[j].LocateMyFSM("Control").SendEvent("DOWN");
                        }
                        j++;
                    }
                    while (i >= 0)
                    {
                        string activename = spikes[i].LocateMyFSM("Control").ActiveStateName;
                        if (activename == "Down" || activename == "Downed") break;
                        else
                        {
                            spikes[i].LocateMyFSM("Control").SendEvent("DOWN");
                        }
                        i--;
                    }
                    spike.LocateMyFSM("Control").SendEvent("DOWN");
                }, 0);
            }
            if (_spikecon != null)
            {
                for (int i = 2; i <= 6; i++)
                {
                    _spikecon.RemoveAction("Wave L", 2);
                    _spikecon.RemoveAction("Wave R", 2);
                }
                _spikecon.InsertCustomAction("Wave L", () =>
                {

                    foreach (var spike in spikes)
                    {
                        if (UnityEngine.Random.Range(0, 3) > 0)
                        {
                            spike.LocateMyFSM("Control").SendEvent("UP");
                        }
                        else
                        {
                        }
                    }
                }, 2);
                _spikecon.InsertCustomAction("Wave R", () =>
                {

                    foreach (var spike in spikes)
                    {
                        if (UnityEngine.Random.Range(0, 3) > 0)
                        {
                            spike.LocateMyFSM("Control").SendEvent("UP");
                        }
                        else
                        {
                        }
                    }
                }, 2);

            }
        }

        private void ModifyRadiance()
        {
            _hp.hp = initHp;
            _phcon.FsmVariables.FindFsmInt("P2 Spike Waves").Value = spikeWaves;
            _phcon.FsmVariables.FindFsmInt("P3 A1 Rage").Value = a1Rage;
            _phcon.FsmVariables.FindFsmInt("P4 Stun1").Value = stun;
            _phcon.FsmVariables.FindFsmInt("P5 Acend").Value = p5Acend;
            _con.GetAction<SetHP>("Scream", 7).hp = Scream;


        }

        private void ModifyPlatsPhase()
        {

            //常态
            _com.RemoveTransition("Aim Back", "FINISHED");
            _com.GetAction<Wait>("AB Start", 1).time = 0.02f;

            _con.InsertCustomAction("Tele Cast?", () =>
            {

                if (_con.FsmVariables.FindFsmInt("Last Tele Pos").Value != 6)
                {
                    float num = knight.transform.position.y - 1.5f - gameObject.transform.position.y;
                    float num2 = knight.transform.position.x - gameObject.transform.position.x;
                    float num3;
                    for (num3 = Mathf.Atan2(num, num2) * (180f / (float)Math.PI); num3 < 0f; num3 += 360f)
                    {
                    }
                    num3 += UnityEngine.Random.Range(-5f, 5f);
                    GameObject beam = Instantiate(oribeam);
                    beam.transform.position = gameObject.transform.position;
                    beam.transform.SetPositionY(gameObject.transform.position.y + 1.5f);
                    beam.transform.SetRotation2D(num3);
                    StartCoroutine(beamFire(beam, 1.0f, 0.5f, 0f));
                }
                else
                {
                    GameObject beam = Instantiate(oribeam);
                    beam.transform.position = gameObject.transform.position;
                    beam.transform.SetPositionY(gameObject.transform.position.y + 1.5f);
                    beam.transform.SetRotation2D(180f);
                    StartCoroutine(beamFire(beam, 0f, 2f, 0f));
                    StartCoroutine(BBrorate(beam, 90f, 2f, true));
                }

            }, 0);


            //脸激光变光球圈
            _com.ChangeTransition("Eb Extra Wait", "FINISHED", "EB End");
            _com.GetAction<Wait>("Eb Extra Wait", 0).time = 3.5f;
            _com.InsertCustomAction("Eb Extra Wait", () => {

                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    StartCoroutine(OrbCircle(gameObject, true));
                }
                else
                {
                    StartCoroutine(OrbCircle(knight, false));
                }



            }, 0);


            //光球激光速度减慢
            _cho.GetAction<SendRandomEventV3>("A2 Choice", 1).weights[2] = 0.5f;
            _com.GetAction<Wait>("Orb Pause", 0).time = 0.75f;
            _com.InsertCustomAction("Orb Antic", () =>
            {
                foreach (var beam in a2beams)
                {
                    Destroy(beam);
                }
                a2beams.Clear();
            }, 3);
            _com.RemoveAction("Orb Pos", 7);
            _com.GetAction<Wait>("Orb Summon", 2).time = 0.5f;
            _com.InsertCustomAction("Final Event", () =>
            {
                AudioSource aus;
                aus = a2beams[0].GetAddComponent<AudioSource>();
                aus.PlayOneShot(BeamAnticClip, 0.5f * GameManager.instance.gameSettings.soundVolume / 10f);
                foreach (var beam in a2beams)
                {
                    beam.LocateMyFSM("Control").SendEvent("ANTIC");
                }
            }, 0);
            _com.InsertCustomAction("Orb Check", () =>
            {
                AudioSource aus;
                aus = a2beams[0].GetAddComponent<AudioSource>();
                aus.PlayOneShot(BeamFireClip, 0.5f * GameManager.instance.gameSettings.soundVolume / 10f);
                foreach (var beam in a2beams)
                {
                    beam.LocateMyFSM("Control").SendEvent("FIRE");
                }
                StartCoroutine(BeamEndAndAntic());
            }, 0);

            _com.InsertCustomAction("Orb End", () =>
            {
                foreach (var beam in a2beams)
                {
                    beam.SetActive(false);
                }
            }, 0);

            //横刺乱向

            _com.InsertCustomAction("Comb L 2", () =>
            {
                int r = UnityEngine.Random.Range(6, 9);
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<SetPosition>("Top 2", 0).y = 58f;
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<SetFloatValue>("Top 2", 2).floatValue = 12f;
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<iTweenMoveBy>("Tween", 0).vector = new Vector3(0, 50, 0);

                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetVariable<FsmInt>("Type").Value = r;// > 2 ? r + 3 : r;
            }, 3);

            _com.InsertCustomAction("Comb R 2", () =>
            {
                int r = UnityEngine.Random.Range(6, 9);
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<SetPosition>("Top 2", 0).y = 58f;
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<SetFloatValue>("Top 2", 2).floatValue = 12f;
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetAction<iTweenMoveBy>("Tween", 0).vector = new Vector3(0, 50, 0);
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").GetVariable<FsmInt>("Type").Value = r; //> 2 ? r + 3 : r;
            }, 3);

        }

        private IEnumerator OrbCircle(GameObject gameObject, bool v)
        {
            float a;
            Color color = GameObject.Find("Halo").GetComponent<SpriteRenderer>().color;
            a = color.a;
            color.a = 1f;
            GameObject.Find("Halo").GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.5f);
            int num = 12;
            float degree = 360f / num;
            float distance = v ? 3f : 12f;
            int turn = 0;
            for (int i = 0; i < 9; i++)
            {
                Vector3 pos;
                for (int j = 1 + turn * num; j < num + turn * num; j++)
                {
                    pos = gameObject.transform.position + new Vector3(Mathf.Cos(j * degree) * distance, Mathf.Sin(j * degree) * distance, 0);

                    useorbs[j].transform.position = pos;

                    useorbs[j].LocateMyFSM("Orb Control").SetState("Init");
                    useorbs[j].SetActive(true);


                }
                distance += 1f * (v ? 2f : -1);
                yield return new WaitForSeconds(0.3f);
                for (int j = 1 + turn * num; j < num + turn * num; j++)
                {
                    useorbs[j].LocateMyFSM("Orb Control").SendEvent("DESTROY");
                }
                turn = turn + 1 > 4 ? 0 : turn + 1;
            }
            useorbs[0].LocateMyFSM("Orb Control").SendEvent("DESTROY");
            Color color1 = GameObject.Find("Halo").GetComponent<SpriteRenderer>().color;
            color1.a = a;
            GameObject.Find("Halo").GetComponent<SpriteRenderer>().color = color1;
            yield break;
        }

        private IEnumerator BeamEndAndAntic()
        {
            yield return new WaitForSeconds(0.05f);
            foreach (var beam in a2beams)
            {
                beam.LocateMyFSM("Control").SendEvent("END");
            }
            yield return null;
            foreach (var beam in a2beams)
            {
                beam.LocateMyFSM("Control").SendEvent("ANTIC");
            }
        }

        private IEnumerator Shown()
        {
            while (knight.transform.position.y < 50f)
                yield return null;
            ShowConvo("黄昏会消去耀阳");
            while (knight.transform.position.y < 75f)
                yield return null;
            ShowConvo("新月会取代满月");
            while (knight.transform.position.y < 100f)
                yield return null;
            ShowConvo("疾风会卷去落木");
            while (knight.transform.position.y < 125f)
                yield return null;
            ShowConvo("泪水会消散时间");
            while (knight.transform.position.y < 140f)
                yield return null;
            ShowConvo("万物皆有其起落");
            yield break;



        }

        private IEnumerator Zoomout()
        {
            float vel = 0.4f;
            GameObject camera = GameCameras.instance.gameObject.FindGameObjectInChildren("CameraParent").FindGameObjectInChildren("tk2dCamera");
            yield return new WaitForSeconds(1.5f);
            if (camera != null)
            {
                zoomscale = camera.GetComponent<tk2dCamera>().ZoomFactor;
                zoom = true;

                while (camera.GetComponent<tk2dCamera>().ZoomFactor > 0.8)
                {
                    camera.GetComponent<tk2dCamera>().ZoomFactor -= vel * Time.deltaTime;
                    yield return null;
                }
            }
            yield break;
        }
        private IEnumerator Zoomin()
        {
            float vel = 0.4f;
            GameObject camera = GameCameras.instance.gameObject.FindGameObjectInChildren("CameraParent").FindGameObjectInChildren("tk2dCamera");
            if (camera != null)
            {


                while (camera.GetComponent<tk2dCamera>().ZoomFactor < 0.9)
                {
                    camera.GetComponent<tk2dCamera>().ZoomFactor += vel * Time.deltaTime;
                    yield return null;
                }
            }
            yield break;
        }
        private void ModifyAcend()
        {
            gap = 0.5f;
            height = 70f;
            _com.InsertCustomAction("Comb Top 2", () =>
            {
                GameObject nails = _com.FsmVariables.FindFsmGameObject("Attack Obj").Value;
                if (height < 160f) { height += 4.5f; }
                else { gap = 1.2f; }
                nails.LocateMyFSM("Control").GetAction<SetPosition>("Top 2", 0).y = height;
                nails.LocateMyFSM("Control").GetAction<SetFloatValue>("Top 2", 2).floatValue = 8f;
                nails.LocateMyFSM("Control").GetAction<iTweenMoveBy>("Tween", 0).vector = new Vector3(0, 150, 0);
                nails.LocateMyFSM("Control").InsertCustomAction("Antic", () =>
                {
                    List<GameObject> nailist = new List<GameObject>();
                    nails.LocateMyFSM("Control").FsmVariables.FindFsmGameObject("Nails").Value.FindAllChildren(nailist);
                    foreach (var nail in nailist)
                    {

                        if (nail.name.Contains("Radiant Nail"))
                        {
                            nail.transform.SetPositionY(nail.transform.GetPositionY() + UnityEngine.Random.Range(-3f, 3f));
                            nail.transform.SetPositionX(nail.transform.GetPositionX() + UnityEngine.Random.Range(-0.75f, 0.75f));
                        }
                    }
                    nails.LocateMyFSM("Control").RemoveAction("Antic", 2);
                }, 2);
            }, 3);

            _con.RemoveAction("Plat Setup", 4);
            _con.RemoveAction("Plat Setup", 2);
            _con.InsertCustomAction("Plat Setup", () => {
                GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
                PlayerData.instance.SetHazardRespawn(new Vector3(59f, 46f, 0), true);

                Clock = Instantiate(_com.GetAction<SpawnObjectFromGlobalPool>("Spawn Fireball", 1).gameObject.Value);
                clock = Clock.GetComponent<AudioSource>();
                clock.Stop();
                Clock.transform.SetPosition2D(0f, 0f);
                hourHand = Instantiate(oribeam);
                clock = hourHand.GetComponent<AudioSource>();
                if (clock != null)
                    clock.Stop();
                hourHand.transform.SetRotation2D(0);
                hourHand.transform.SetPosition2D(0f, 0f);
                minuteHand = Instantiate(oribeam);
                clock = minuteHand.GetComponent<AudioSource>();
                if (clock != null)
                    clock.Stop();
                minuteHand.transform.SetRotation2D(0);
                minuteHand.transform.SetPosition2D(0f, 0f);
                secondHand = Instantiate(oribeam);
                clock = secondHand.GetComponent<AudioSource>();
                if (clock != null)
                    clock.Stop();
                clock = secondHand.GetAddComponent<AudioSource>();
                secondHand.transform.SetRotation2D(0);
                secondHand.transform.SetPosition2D(0f, 0f);


                Clock.transform.SetParent(timeClock.transform);
                hourHand.transform.SetParent(timeClock.transform);
                minuteHand.transform.SetParent(timeClock.transform);
                secondHand.transform.SetParent(timeClock.transform);
                hourHand.transform.localScale = new Vector3(5f, 3f, 1f);
                minuteHand.transform.localScale = new Vector3(10f, 2f, 1f);
                secondHand.transform.SetScaleY(1f);
                timeClock.transform.SetPosition2D(63f, 97.5f);
                hourHand.transform.SetRotation2D(hour);
                minuteHand.transform.SetRotation2D(minute);
                secondHand.transform.SetRotation2D(second);
                timeClock.SetActiveChildren(true);
                StartCoroutine(beamFire(hourHand, 5f, 9999f, 0));
                StartCoroutine(beamFire(minuteHand, 5f, 9999f, 0));
                StartCoroutine(beamFire(secondHand, 5f, 9999f, 0));
                StartCoroutine(Zoomout());
                StartCoroutine("Shown");
                



            }, 2);
            _con.GetAction<Wait>("Plat Setup", 5).time = 0.5f;
            _con.GetAction<SendEventByName>("Ascend Cast", 1).sendEvent = "COMB TOP2";


            _con.InsertCustomAction("Scream", () => {
                _com.GetVariable<FsmGameObject>("Attack Obj").Value.LocateMyFSM("Control").SetState("Reset");
                GameObject.Find("Abyss Pit").LocateMyFSM("Ascend").FsmVariables.FindFsmFloat("Hero Y").Value = knight.transform.position.y;
                GameObject.Find("Abyss Pit").LocateMyFSM("Ascend").SendEvent("ASCEND");
                global::PlayerData.instance.SetHazardRespawn(new Vector3(58, 153, 0), true);
                timeClock.SetActive(false);
                final = true;
            }, 8);



        }

        private void ModifyFinal()
        {

            _com.RemoveTransition("Set Final Orbs", "FINISHED");
            int num = 3;
            float distance = 3f;
            _con.InsertAction("Scream", new Wait() { time = 5f, realTime = false }, 8);

            _con.InsertCustomAction("Scream", () =>
            {
                    StartCoroutine(Zoomin());
                    StopCoroutine("Shown");
                    ShowConvo("唯！永！恒！不！灭！");
                
                for (int i = 1; i < num + 1; i++)
                {
                    useorbs[i].LocateMyFSM("Orb Control").GetAction<ChaseObjectV2>("Chase Hero", 3).target = gameObject;
                    useorbs[i].LocateMyFSM("Orb Control").GetAction<ChaseObjectV2>("Chase Hero 2", 4).target = gameObject;
                    useorbs[i].LocateMyFSM("Orb Control").RemoveAction("Chase Hero", 2);
                    useorbs[i].LocateMyFSM("Orb Control").RemoveAction("Chase Hero", 0);
                    useorbs[i].LocateMyFSM("Orb Control").RemoveAction("Chase Hero 2", 3);
                    useorbs[i].LocateMyFSM("Orb Control").RemoveAction("Chase Hero 2", 2);
                    useorbs[i].LocateMyFSM("Orb Control").RemoveAction("Chase Hero 2", 0);
                    useorbs[i].transform.position = gameObject.transform.position + distance * new Vector3(Mathf.Cos(i * (360f / num)), Mathf.Sin(i * (360f / num)), 0);
                    useorbs[i].LocateMyFSM("Orb Control").SetState("Init");
                }
                StartCoroutine(finalshow());
            }, 2);

            _com.InsertCustomAction("Final Hit", () =>
            {
                for (int i = 1; i < num + 1; i++)
                {
                    useorbs[i].LocateMyFSM("Orb Control").SendEvent("DESTROY");
                    useorbs[i].SetActive(false);
                }
            }, 0);

        }

        IEnumerator finalshow()
        {
            float distance = 5f;
            float delay = 1.5f;
            float v = 80f;
            float now = 0f;
            int num = 1;
            List<GameObject> orbs = new List<GameObject>();
            yield return new WaitForSeconds(0.2f);
            useorbs[1].transform.position = gameObject.transform.position + new Vector3(0, distance, 0);
            useorbs[1].LocateMyFSM("Orb Control").GetAction<AudioPlaySimple>("Init", 0).volume = 0.5f;
            useorbs[1].SetActive(true);
            orbs.Add(useorbs[1]);
            num = 2;
            while (num < 5)
            {
                if (now > delay)
                {
                    now = 0f;
                    if (num < 4)
                    {
                        useorbs[num].transform.position = gameObject.transform.position + new Vector3(0, distance, 0);
                        useorbs[num].LocateMyFSM("Orb Control").GetAction<AudioPlaySimple>("Init", 0).volume = 0.5f;
                        useorbs[num].SetActive(true);
                        orbs.Add(useorbs[num]);
                    }
                    num++;
                }
                foreach (var orb in orbs)
                {
                    orb.transform.RotateAround(gameObject.transform.position, Vector3.back, v * Time.deltaTime);
                }
                now += Time.deltaTime;
                yield return null;
            }
            foreach (var orb in orbs) { orb.LocateMyFSM("Orb Control").SendEvent("FIRE"); }
            yield break;
        }


        private void Update()
        {
            if (!foundbeam)
            {
                string actstr = _com.ActiveStateName;
                if (actstr == "Idle")
                {
                    oribeam = _com.FsmVariables.FindFsmGameObject("Ascend Beam").Value;

                    foundbeam = true;
                }
            }

            //Log(_com.GetVariable<FsmVector3>("Hero Pos"));

            if (_con.ActiveStateName == "Ascend Cast")
            {
                if (gapnow >= gap)
                {
                    _com.SendEvent("COMB TOP2");
                    gapnow = 0f;
                }
                gapnow += Time.deltaTime;

                if (bbgapnow >= bbgap)
                {
                    second -= 6f;
                    clock.PlayOneShot(tip, 1.5f * GameManager.instance.gameSettings.soundVolume / 10f);

                    second %= 360f;
                    minute %= 360f;
                    hour %= 360f;
                    secondHand.transform.SetRotation2D(second);
                    minuteHand.transform.SetRotation2D(minute);
                    hourHand.transform.SetRotation2D(hour);
                    bbgapnow = 0f;
                }
                bbgapnow += Time.deltaTime;

            }

            if (gameObject.transform.position.y > 150f && _hp.hp <= 1500 && !finalSet1 && final)
            {
                finalSet1 = true;
                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeTransition("Idle", "SLOW VANISH", "Vanish Antic");
                GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").ChangeTransition("Idle", "SLOW VANISH", "Vanish Antic");
                GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").GetAction<Wait>("Vanish Antic", 1).time = 1f;
                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").GetAction<Wait>("Vanish Antic", 1).time = 1f;

                GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeTransition("Appear 2", "SLOW VANISH", "Vanish Antic");
                GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").ChangeTransition("Appear 2", "SLOW VANISH", "Vanish Antic");
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                    finalSet2 = true;
                    PlayerData.instance.SetHazardRespawn(new Vector3(58, 153, 0), true);
                }
                else
                {
                    GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                    finalSet2 = false;
                    PlayerData.instance.SetHazardRespawn(new Vector3(68, 153, 0), false);
                }
            }
            if (finalSet1)
            {
                if (finalgapnow >= finalgap)
                {
                    finalgapnow = 0f;
                    if (!finalSet2)
                    {
                        finalSet2 = true;
                        GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                        GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
                        PlayerData.instance.SetHazardRespawn(new Vector3(58, 153, 0), true);


                    }
                    else
                    {
                        finalSet2 = false;
                        GameObject.Find("Radiant Plat Small (11)").LocateMyFSM("radiant_plat").SendEvent("SLOW VANISH");
                        GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").SendEvent("APPEAR");
                        PlayerData.instance.SetHazardRespawn(new Vector3(68, 153, 0), false);
                    }
                }
                finalgapnow += Time.deltaTime;
            }


        }

        private void OnDestroy()
        {
            foundbeam = false;
            PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(HutongGames.PlayMaker.FsmVariables.GlobalVariables.GetFsmGameObject("Enemy Dream Msg").Value, "Display");
            if (playMakerFSM.GetState("Set Convo").Actions.Length == 6)
            {
                playMakerFSM.RemoveAction("Set Convo", 4);
            }
            foreach (var orb in orbList) { if (orb != null) { Destroy(orb); } }
            foreach (var orb in useorbs) { if (orb != null) { Destroy(orb); } }
            foreach (var beam in a2beams) { if (beam != null) { Destroy(beam); } }
            if (zoom) { GameCameras.instance.gameObject.FindGameObjectInChildren("CameraParent").FindGameObjectInChildren("tk2dCamera").GetComponent<tk2dCamera>().ZoomFactor = zoomscale; }
        }

        public static AudioClip LoadAudioClip(string path)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            var wavData = new WavData();
            wavData.Parse(stream);
            stream.Close();
            var samples = wavData.GetSamples();
            var clip = AudioClip.Create("Final Battle", samples.Length / wavData.FormatChunk.NumChannels, wavData.FormatChunk.NumChannels, (int)wavData.FormatChunk.SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private void ShowConvo(string msg)
        {
            PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(HutongGames.PlayMaker.FsmVariables.GlobalVariables.GetFsmGameObject("Enemy Dream Msg").Value, "Display");
            if (playMakerFSM.GetState("Set Convo").Actions.Length == 5)
            {
                playMakerFSM.InsertCustomAction("Set Convo", () =>
                {
                    playMakerFSM.FsmVariables.GetFsmString("Convo Text").Value = convoTitle;
                }, 4);
            }
            playMakerFSM.FsmVariables.GetFsmString("Convo Title").Value = "Radiance";
            playMakerFSM.FsmVariables.GetFsmInt("Convo Amount").Value = 1;
            convoTitle = msg;
            playMakerFSM.SendEvent("DISPLAY ENEMY DREAM");
        }

        private void Log(object obj)
        {
            if (obj == null)
            {
                Modding.Logger.Log("[immortallight]:" + null);
            }
            else Modding.Logger.Log("[immortallight]:" + obj);
        }
    }
}