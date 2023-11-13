using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Satchel;
using HutongGames.PlayMaker.Actions;

namespace ManyRadiances
{
    internal class radiancetest : MonoBehaviour
    {
        PlayMakerFSM att_cho;
        PlayMakerFSM att_com;
        PlayMakerFSM con;
        PlayMakerFSM ph_con;
        PlayMakerFSM tele;
        string activestate;
        GameObject orb;
        string choact;
        float chotime = 0f;
        string comact;
        float comtime = 0f;
        string conact;
        float contime = 0f;
        string phconact;
        float phcontime = 0f;
        string teleact;
        float teletime = 0f;
        float time = 0f;

        private void Awake()
        {
            att_cho = gameObject.LocateMyFSM("Attack Choices");
            att_com = gameObject.LocateMyFSM("Attack Commands");
            con = gameObject.LocateMyFSM("Control");
            ph_con = gameObject.LocateMyFSM("Phase Control");
            tele = gameObject.LocateMyFSM("Teleport");
            orb = att_com.GetAction<SpawnObjectFromGlobalPool>("Spawn Fireball", 1).gameObject.Value;
        }

        private void Start()
        {
            att_cho.InsertCustomAction("A2 Choice", () =>
            {
                Log(att_cho.FsmVariables.GetFsmInt("Ct Nail R Sweep").Value);
            }, 0);
            att_cho.InsertCustomAction("A2 Choice", () =>
            {
                Log(att_cho.FsmVariables.GetFsmInt("Ms Nail R Sweep").Value);
            }, 0);
            // Material[] materials = orb.GetComponent<tk2dSprite>().Collection.materials;
            /* Texture[] texorb = orb.GetComponent<tk2dSprite>().Collection.textures;
             Material[] materials = orb.GetComponent<MeshRenderer>().materials;
             Log(orb);
             Log(materials.Length);
             foreach (Material mat in materials)
             //foreach(Texture texture in texorb)
             {
                 TextureUtils.WriteTextureToFile(mat.mainTexture, "C:\\Users\\shownyoung\\Desktop\\temp\\" + mat.mainTexture.name + ".png");
                 Log(mat.mainTexture.name);
             }*/
            SendRandomEventV3 x;
            SREV3 sREV3 = new SREV3();
            x = att_cho.GetAction<SendRandomEventV3>("A2 Choice", 3);
            sREV3.events=x.events;
            sREV3.eventMax = x.eventMax;
            sREV3.missedMax = x.missedMax;
            sREV3.trackingIntsMissed = x.trackingIntsMissed;
            sREV3.trackingInts = x.trackingInts;
            sREV3.weights = x.weights;
            att_cho.RemoveAction("A2 Choice", 3);
            att_cho.InsertAction("A2 Choice", sREV3, 2);
            Log("OK");
        }

        private void Update()
        {
            chotime += Time.deltaTime;
            contime += Time.deltaTime;
            teletime += Time.deltaTime;
            phcontime += Time.deltaTime;
            comtime += Time.deltaTime;
            time += Time.deltaTime;
            string temp;
            temp = att_cho.ActiveStateName;
            if (temp != choact)
            {
                bool flag=false;
                flag = temp.IsAny("Nail L Sweep 2", "Nail R Sweep 2") && choact.IsAny("Nail L Sweep 2", "Nail R Sweep 2");
                if (chotime >= 7f)
                {
                    Log("#######################");
                    Log(time);
                }
                choact = temp;
                Log("Attack Choices:" + choact);
                Log("Attack Choices:" + chotime);
                
                if(!flag)   chotime = 0f;
                
            }
            /*temp = att_com.ActiveStateName;
            if (temp != comact)
            {
                comact = temp;
                Log("Attack Commands:" + comact);
                Log("Attack Commands:" + comtime);
                comtime = 0f;
            }
            
            temp = con.ActiveStateName;
            if (temp != conact)
            {
                conact = temp;
                Log("Control:" + conact);
                Log("Control:" + contime);
                contime = 0f;
            }
            
            temp = ph_con.ActiveStateName;
            if (temp != phconact)
            {
                phconact = temp;
                Log("Phase Control:" + phconact);
                Log("Phase Control:" + phcontime);
                phcontime = 0f;
            }
            
            temp = tele.ActiveStateName;
            if (temp != teleact)
            {
                teleact = temp;
                Log("Teleport:" + teleact);
                Log("Teleport:" + teletime);
                teletime = 0f;
            }*/

        }

        private void Log(object obj)
        {
            Modding.Logger.Log(obj);
        }
    }
    public class SREV3 : FsmStateAction
    {
        [CompoundArray("Events", "Event", "Weight")]
        public FsmEvent[] events;

        [HasFloatSlider(0f, 1f)]
        public FsmFloat[] weights;

        [UIHint(UIHint.Variable)]
        public FsmInt[] trackingInts;

        public FsmInt[] eventMax;

        [UIHint(UIHint.Variable)]
        public FsmInt[] trackingIntsMissed;

        public FsmInt[] missedMax;

        private int loops;

        private DelayedEvent delayedEvent;

        public override void Reset()
        {
            events = new FsmEvent[3];
            weights = new FsmFloat[3] { 1f, 1f, 1f };
        }

        public override void OnEnter()
        {
            bool flag = false;
            bool flag2 = false;
            int num = 0;
            while (!flag)
            {
                int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(weights);
                if (randomWeightedIndex != -1)
                {
                    for (int i = 0; i < trackingIntsMissed.Length; i++)
                    {
                        if (trackingIntsMissed[i].Value >= missedMax[i].Value)
                        {
                            flag2 = true;
                            num = i;
                        }
                    }

                    if (flag2)
                    {
                        flag = true;
                        for (int j = 0; j < trackingInts.Length; j++)
                        {
                            trackingInts[j].Value = 0;
                            trackingIntsMissed[j].Value++;
                        }

                        trackingIntsMissed[num].Value = 0;
                        trackingInts[num].Value = 1;
                        base.Fsm.Event(events[num]);
                    }
                    else if (trackingInts[randomWeightedIndex].Value < eventMax[randomWeightedIndex].Value)
                    {
                        int value = ++trackingInts[randomWeightedIndex].Value;
                        for (int k = 0; k < trackingInts.Length; k++)
                        {
                            trackingInts[k].Value = 0;
                            trackingIntsMissed[k].Value++;
                        }

                        trackingInts[randomWeightedIndex].Value = value;
                        trackingIntsMissed[randomWeightedIndex].Value = 0;
                        flag = true;
                        base.Fsm.Event(events[randomWeightedIndex]);
                    }
                }
                ManyRadiances.Instance.Log("loops is "+loops);
                loops++;
                if (loops > 100)
                {
                    base.Fsm.Event(events[0]);
                    flag = true;
                    Finish();
                }
            }

            Finish();
        }
    }


}

