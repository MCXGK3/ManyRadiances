using Satchel.Futils;
using UnityEngine;
using UnityEngine.UIElements;

namespace ManyRadiances
{
    public class immortallight:MonoBehaviour
    {
        PlayMakerFSM _con;
        PlayMakerFSM _cho;
        PlayMakerFSM _com;
        PlayMakerFSM _tele;
        HealthManager _hp;
        GameObject[] beams=new GameObject[50];
        GameObject oribeam;
        GameObject beamburst;
        GameObject fakebb;
        List<GameObject> orbList=new List<GameObject>();
        GameObject knight;
        bool foundbeam=false;
        private AudioClip BeamAnticClip;
        private AudioClip BeamFireClip;

        private void Awake()
        {
            _cho = gameObject.LocateMyFSM("Attack Choices");
            _com = gameObject.LocateMyFSM("Attack Commands");
            _con = gameObject.LocateMyFSM("Control");
            _tele = gameObject.LocateMyFSM("Teleport");
            _hp=gameObject.GetComponent<HealthManager>();

        }

        private IEnumerator beamFire(GameObject beam,float antic,float fire,float end)
        {
            AudioSource aus;
            aus=beam.GetAddComponent<AudioSource>();
            beam.SetActive(true);
            PlayMakerFSM beamFSM = beam.LocateMyFSM("Control");
            aus.PlayOneShot(BeamAnticClip);
            beamFSM.SendEvent("ANTIC");
            yield return new WaitForSeconds(antic);
            aus.PlayOneShot(BeamFireClip);
            beamFSM.SendEvent("FIRE");
            yield return new WaitForSeconds(fire);
            beamFSM.SendEvent("END");
            yield return new WaitForSeconds(end);
            Destroy(beam);
            yield break;
        }

        private IEnumerator beginbeams(GameObject orb)
        {
                GameObject beam=Instantiate(oribeam);
                beam.transform.SetRotation2D(90);
                beam.transform.position=orb.transform.position;
                beam.transform.position += new Vector3(0, -20f, 0);
                float f= UnityEngine.Random.Range(0f, 180f);
                beam.transform.RotateAround(orb.transform.position,Vector3.back,f);
                //beams[i].transform.Translate((float)(-20f * Math.Cos(f)), (float)(-20f * Math.Sin(f)), 0,Space.Self);
                beam.SetActive(true);
                PlayMakerFSM beamFSM = beam.LocateMyFSM("Control");
            StartCoroutine(beamFire(beam, 0.5f, 0.5f, 0f));
                    
            yield break;
            //StartCoroutine(beginbeam(beams[i],knight.transform.position,transform));

        }

        private IEnumerator beginsweeper(bool left, float floor)
        {
            
            float ll,lr, rl,rr;
            float le, ri;
            if (floor <= 30f)
            {
                ll = 43f; lr = 45f;
                rl = 78f; rr = 80f;
            }
            else
            {
                ll = 34f;lr = 36f;
                rl = 83f;rr = 85f;
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
                    StartCoroutine(beamFire(beam, 1.5f, 0.5f, 0f));
                    yield return new WaitForSeconds(0.05f);
                }
            }
            else
            {
                for (float i = ri; i >= le; i-=2.5f)
                {
                    GameObject beam = Instantiate(oribeam);
                    beam.transform.position = new Vector3(i, floor-5f);
                    beam.transform.SetRotation2D(90f);
                    StartCoroutine(beamFire(beam, 1.5f, 0.5f, 0f));
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield break;
            
        }

        private void SendEventToBB(GameObject go ,string eve){
            if(go != null)
            {
                List<GameObject> list = new List<GameObject>();
                go.FindAllChildren(list);
                foreach(GameObject go2 in list)
                {
                    PlayMakerFSM bbcon;
                    bbcon= go2.LocateMyFSM("Control");
                    bbcon.SendEvent(eve);
                }
                list.Clear();
            }
        }

        private IEnumerator Arrive()
        {
            fakebb.transform.position = gameObject.transform.position;
            fakebb.transform.SetRotation2D(UnityEngine.Random.Range(0f, 360f));
            SendEventToBB(fakebb, "ANTIC");
            yield return new WaitForSeconds(1f);
            SendEventToBB(fakebb, "FIRE");
            yield break;
        }

        private IEnumerator BBrorate()
        {
            float time = 0f;
            while(time <=5f) {
                time += Time.deltaTime;
                fakebb.transform.Rotate(Vector3.back, 24f * Time.deltaTime, Space.Self);
                yield return null;
            }
            yield break;
        }

        private void Start()
        {
            knight = GameObject.Find("Knight");
            beamburst = _com.FsmVariables.FindFsmGameObject("Eye Beam Burst1").Value;
            ModifyOrbs();
            ModifyRage();
            ModifyBeamSweeper();
            ModifyRadiance();
            ModifyEyeBeams();
            
            //获得激光的两个声音
            BeamAnticClip = _com.GetAction<AudioPlayerOneShotSingle>("Aim", 1).audioClip.Value as AudioClip;
            Log(BeamAnticClip);
            BeamFireClip = _com.GetAction<AudioPlayerOneShotSingle>("Aim", 3).audioClip.Value as AudioClip;
            Log(BeamFireClip);

        }

        private void ModifyBeamSweeper()
        {
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
            _com.GetAction<SendEventByName>("EB 4",3).delay = 0.3f;
            _com.GetAction<SendEventByName>("EB 4", 4).delay = 0.5f;
            //_com.GetAction<Wait>("EB 4", 5).time = 0.5f;
            _com.GetAction<SendEventByName>("EB 5", 4).delay = 0.3f;
            _com.GetAction<SendEventByName>("EB 5", 5).delay = 0.5f;
            //_com.GetAction<Wait>("EB 5", 6).time = 0.5f;
            _com.GetAction<SendEventByName>("EB 6", 4).delay = 0.3f;
            _com.GetAction<SendEventByName>("EB 6", 5).delay = 0.5f;
            //_com.GetAction<Wait>("EB 6", 6).time = 0.5f;
            _con.InsertCustomAction("Climb Plats1", () =>
            {
                _com.ChangeTransition("EB 4", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB 5", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB 6", "FINISHED", "EB Glow End 2");
                _com.ChangeTransition("EB Glow End 2", "FINISHED", "Comb Top");
            }, 0);
                }

        private void ModifyOrbs()
        {
            //去除send FIRE
            _com.RemoveAction("Spawn Fireball", 3);

            //给orblist添加本次光球并开启光球激光
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
            _com.ChangeTransition("NF Glow", "FINISHED","EB Glow End");
            _com.GetAction<Wait>("NF Glow", 1).time=5f;
            _com.InsertCustomAction("NF Glow", () =>{
                StartCoroutine(BBrorate());
            },0);
        }

        private void ModifyNailFan()
        {
            _com.RemoveAction("CW Spawn", 4);
            _com.RemoveAction("CCW Spawn", 4);
        }

        private void ModifyRadiance()
        {
            /*fakebb = GameObject.Instantiate(beamburst);
            fakebb.transform.position = gameObject.transform.position;*/
            //_con.ChangeTransition("First Tele", "TELEPORTED", "Intro Antic");
           /* _con.InsertCustomAction("Arena 1 Start", () =>
            {
                fakebb.transform.SetRotation2D(UnityEngine.Random.Range(0f, 360f));
                fakebb.SetActive(true);
                SendEventToBB(fakebb, "ANTIC");
            }, 0);
            _tele.InsertCustomAction("Reset", () =>{
                //StartCoroutine(Arrive());
            },0);
            _tele.InsertCustomAction("Antic", () =>
            {
                SendEventToBB(fakebb, "END");
            }, 0);*/
            _com.GetAction<SetFsmInt>("Comb L 2", 1).setValue = 6;
            _com.GetAction<SetFsmInt>("Comb R 2", 1).setValue = 6;
        }

        private void Update()
        {
            if (!foundbeam)
            {
                string actstr = _com.ActiveStateName;
                if (actstr=="Idle")
                {
                    oribeam = _com.FsmVariables.FindFsmGameObject("Ascend Beam").Value;

                    foundbeam = true;
                }
            }
                
          
        }

        private void OnDestroy()
        {
            foundbeam=false;
        }


        private void Log(object obj)
        {
            if (obj == null)
            {
                Modding.Logger.Log("[immortallight]:"+null);
            }
            else Modding.Logger.Log("[immortallight]:" + obj);
        }
    }
}