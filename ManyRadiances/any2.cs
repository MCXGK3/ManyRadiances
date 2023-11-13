using System;
using System.Collections.Generic;
using Any2;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;

public class any2 : MonoBehaviour
{
    private int HP = 2000;//增加血量
    private GameObject _spikeMaster;//spike  尖刺
    private GameObject _spikeTemplate;//template 模板
    private GameObject _spikeClone;//尖刺复制品
    private GameObject _spikeClone2;
    private GameObject _spikeClone3;
    private GameObject _spikeClone4;
    private GameObject _spikeClone5;
    private List<GameObject> _spikeGroup0 = new List<GameObject>();  //共计24个尖刺列表（初步怀疑为台阶刺）
    private List<GameObject> _spikeGroup1 = new List<GameObject>();
    private List<GameObject> _spikeGroup2 = new List<GameObject>();
    private List<GameObject> _spikeGroup3 = new List<GameObject>();
    private List<GameObject> _spikeGroup4 = new List<GameObject>();
    private List<GameObject> _spikeGroup5 = new List<GameObject>();
    private List<GameObject> _spikeGroup6 = new List<GameObject>();
    private List<GameObject> _spikeGroup7 = new List<GameObject>();
    private List<GameObject> _spikeGroup8 = new List<GameObject>();
    private List<GameObject> _spikeGroup9 = new List<GameObject>();
    private List<GameObject> _spikeGroup10 = new List<GameObject>();
    private List<GameObject> _spikeGroup11 = new List<GameObject>();
    private List<GameObject> _spikeGroup12 = new List<GameObject>();
    private List<GameObject> _spikeGroup13 = new List<GameObject>();
    private List<GameObject> _spikeGroup14 = new List<GameObject>();
    private List<GameObject> _spikeGroup15 = new List<GameObject>();
    private List<GameObject> _spikeGroup16 = new List<GameObject>();
    private List<GameObject> _spikeGroup17 = new List<GameObject>();
    private List<GameObject> _spikeGroup18 = new List<GameObject>();
    private List<GameObject> _spikeGroup19 = new List<GameObject>();
    private List<GameObject> _spikeGroup20 = new List<GameObject>();
    private List<GameObject> _spikeGroup21 = new List<GameObject>();
    private List<GameObject> _spikeGroup22 = new List<GameObject>();
    private List<GameObject> _spikeGroup23 = new List<GameObject>();
    private GameObject _radiantBeam0; //共计八道激光
    private GameObject _radiantBeam1;
    private GameObject _radiantBeam2;
    private GameObject _radiantBeam3;
    private GameObject _radiantBeam4;
    private GameObject _radiantBeam5;
    private GameObject _radiantBeam6;
    private GameObject _radiantBeam7;
    private List<GameObject> _radiantBeams = new List<GameObject>();//激光组
    private GameObject _beamsweeper;//两个横激光
    private GameObject _beamsweeper2;
    private HealthManager _hm;//（WHO？）血量控制
    private PlayMakerFSM _attackChoices;//攻击选择
    private PlayMakerFSM _attackCommands;//攻击控制
    private PlayMakerFSM _control;//辐光行为控制（位移？发怒？脸攻击？）
    private PlayMakerFSM _phaseControl;//阶段控制
    private PlayMakerFSM _spikeMasterControl;//尖刺控制
    private PlayMakerFSM _beamsweepercontrol;//横激光控制
    private PlayMakerFSM _beamsweeper2control;//横激光2控制
    private PlayMakerFSM _hs;//herosaver;
    private int CWRepeats = 0;//？？？？？？？？？
    private bool fullSpikesSet = false;//全地刺设置
    private bool arena2Set = false;//二阶段设置
    private bool onePlatSet = false;//一平台设置
    private const int fullSpikesHealth = 0;//全地刺血量
    private const int onePlatHealth = 0;//一平台血量
    private const int platSpikesHealth = 0;//平台刺血量
    private const float nailWallDelay = 0.5f;//横刺延迟————0.5s
    private void Awake()
    {
        Log("Added Radiance MonoBehaviour");
        _hm = base.gameObject.GetComponent<HealthManager>();//辐光血量控制组件获得
        _attackChoices = base.gameObject.LocateMyFSM("Attack Choices");//辐光攻击选择状态机获得
        _attackCommands = base.gameObject.LocateMyFSM("Attack Commands");//辐光攻击控制状态机获得
        _control = base.gameObject.LocateMyFSM("Control");//辐光控制状态机获得
        _phaseControl = base.gameObject.LocateMyFSM("Phase Control");//辐光阶段控制状态机获得
        _spikeMaster = GameObject.Find("Spike Control");//尖刺控制游戏物体获得
        _spikeMasterControl = _spikeMaster.LocateMyFSM("Control");//尖刺控制物体控制状态机获得
        _spikeTemplate = GameObject.Find("Radiant Spike");//辐光尖刺物体获得
        _beamsweeper = GameObject.Find("Beam Sweeper");//横激光物体获得
        _beamsweeper2 = UnityEngine.Object.Instantiate(_beamsweeper);//复制横激光物体
        _beamsweeper2.AddComponent<Any2.BeamSweeperClone>();//给横激光2添加组件<BeamSweeperClone>
        _beamsweepercontrol = _beamsweeper.LocateMyFSM("Control");//横激光控制状态机获得
        _beamsweeper2control = _beamsweeper2.LocateMyFSM("Control");//横激光2控制状态机获得
        _radiantBeam0 = GameObject.Find("Radiant Beam");//获得0-8的脸激光
        _radiantBeam1 = GameObject.Find("Radiant Beam (1)");
        _radiantBeam2 = GameObject.Find("Radiant Beam (2)");
        _radiantBeam3 = GameObject.Find("Radiant Beam (3)");
        _radiantBeam4 = GameObject.Find("Radiant Beam (4)");
        _radiantBeam5 = GameObject.Find("Radiant Beam (5)");
        _radiantBeam6 = GameObject.Find("Radiant Beam (6)");
        _radiantBeam7 = GameObject.Find("Radiant Beam (7)");
        _radiantBeams.Add(_radiantBeam0);  //将0到8的脸激光添加到脸激光列表中
        _radiantBeams.Add(_radiantBeam1);
        _radiantBeams.Add(_radiantBeam2);
        _radiantBeams.Add(_radiantBeam3);
        _radiantBeams.Add(_radiantBeam4);
        _radiantBeams.Add(_radiantBeam5);
        _radiantBeams.Add(_radiantBeam6);
        _radiantBeams.Add(_radiantBeam7);



        _hs = _spikeTemplate.LocateMyFSM("Hero Saver");
    }

    private void Start()
    {
        Log("Changing fight variables...");
        /*Material[] materials = base.gameObject.GetComponent<tk2dSprite>().Collection.materials;//获得材料（猜测为贴图）
		int num = 0;
		Material[] array = materials;
		foreach (Material material in array)
		{
			try
			{
				material.mainTexture = global::AnyRadiance.AnyRadiance.Sprites[num].texture;//改变贴图
				num++;
			}
			catch (Exception)
			{
			}
		}*/
        _hm.hp += HP;//整体血量加HP（2000血）
        _phaseControl.FsmVariables.GetFsmInt("P2 Spike Waves").Value += 2000;//细分二阶段全地刺血量阈值+2000=4600
        _phaseControl.FsmVariables.GetFsmInt("P3 A1 Rage").Value += 1500;//无限落刺阶段血量阈值+1500=3650
        _phaseControl.FsmVariables.GetFsmInt("P4 Stun1").Value += 1000;//P1结束阶段血量阈值+1000=2850
        _control.GetAction<SetHP>("Scream", 7).hp = 2000;//P4阶段血量为2000（猜测为P4）
                                                         //_hs.GetAction<SendEventByName>("Send", 0).delay = 0f;
        _spikeClone = UnityEngine.Object.Instantiate(_spikeTemplate);//对尖刺模板进行复制
        _spikeClone.transform.SetPositionX(58f);//尖刺位置横坐标设置在58f
        _spikeClone.transform.SetPositionY(153.8f);//尖刺位置纵坐标设置在153.8f
        _spikeClone.GetComponent<DamageHero>().damageDealt = 2;//尖刺对骑士造成伤害
        _spikeClone.GetComponent<DamageHero>().hazardType = 0;//尖刺伤害类型
        _spikeClone2 = UnityEngine.Object.Instantiate(_spikeTemplate);//尖刺模板复制2
        _spikeClone2.transform.SetPositionX(57.5f);
        _spikeClone2.transform.SetPositionY(153.8f);
        _spikeClone.GetComponent<DamageHero>().damageDealt = 2;
        _spikeClone.GetComponent<DamageHero>().hazardType = 0;
        _spikeClone3 = UnityEngine.Object.Instantiate(_spikeTemplate);//尖刺模板复制3
        _spikeClone3.transform.SetPositionX(57f);
        _spikeClone3.transform.SetPositionY(153.8f);
        _spikeClone.GetComponent<DamageHero>().damageDealt = 2;
        _spikeClone.GetComponent<DamageHero>().hazardType = 0;
        _spikeClone4 = UnityEngine.Object.Instantiate(_spikeTemplate);//尖刺模板复制4
        _spikeClone4.transform.SetPositionX(58.5f);
        _spikeClone4.transform.SetPositionY(153.8f);
        _spikeClone.GetComponent<DamageHero>().damageDealt = 2;
        _spikeClone.GetComponent<DamageHero>().hazardType = 0;
        _spikeClone5 = UnityEngine.Object.Instantiate(_spikeTemplate);//尖刺模板复制5
        _spikeClone5.transform.SetPositionX(59f);
        _spikeClone5.transform.SetPositionY(153.8f);
        _spikeClone.GetComponent<DamageHero>().damageDealt = 2;
        _spikeClone.GetComponent<DamageHero>().hazardType = 0;
        //////////////////////////////复制了共计5个尖刺并将它们放在从57f到59f的位置////////////////////////////////////////
        foreach (Transform item in _spikeMaster.transform)//对每个尖刺控制物体的transform组件进行操作
        {
            GameObject gameObject = item.gameObject;
            //Log("Child Name: " + gameObject.name);
            try
            {
                foreach (Transform item2 in gameObject.transform)
                {
                    GameObject gameObject2 = item2.gameObject;
                    //Log("Grandchild name: " + gameObject2.name);
                    gameObject2.GetComponent<DamageHero>().damageDealt = 2;//对共计25个地刺设置伤害数值和类型
                    gameObject2.GetComponent<DamageHero>().hazardType = 0;
                }
            }
            catch (Exception)
            {
            }
        }
        for (float num2 = 58.5f; num2 <= 62.5f; num2 += 1f)//在34.7f高度从58.5f到62.5f设置地刺，放到spikeGroup0中
        {
            GameObject gameObject3 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num2, 34.7f), Quaternion.identity);
            gameObject3.GetComponent<DamageHero>().damageDealt = 2;
            gameObject3.GetComponent<DamageHero>().hazardType = 0;
            gameObject3.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup0.Add(gameObject3);
        }
        for (float num3 = 49.7f; num3 <= 53.7f; num3 += 1f)//在37.6f高度从49.7f到53.7f设置地刺，放到spikeGroup1中
        {
            GameObject gameObject4 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num3, 37.6f), Quaternion.identity);
            gameObject4.GetComponent<DamageHero>().damageDealt = 2;
            gameObject4.GetComponent<DamageHero>().hazardType = 0;
            gameObject4.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup1.Add(gameObject4);
        }
        for (float num4 = 41.4f; num4 <= 43.4f; num4 += 1f)//在36.7f高度从41.4f到43.4f设置地刺，放到spikeGroup2中
        {
            GameObject gameObject5 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num4, 36.7f), Quaternion.identity);
            gameObject5.GetComponent<DamageHero>().damageDealt = 2;
            gameObject5.GetComponent<DamageHero>().hazardType = 0;
            gameObject5.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup2.Add(gameObject5);
        }
        for (float num5 = 46.2f; num5 <= 48.2f; num5 += 1f)
        {
            GameObject gameObject6 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num5, 43.7f), Quaternion.identity);
            gameObject6.GetComponent<DamageHero>().damageDealt = 2;
            gameObject6.GetComponent<DamageHero>().hazardType = 0;
            gameObject6.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup3.Add(gameObject6);
        }
        for (float num6 = 57.6f; num6 <= 61.6f; num6 += 1f)
        {
            GameObject gameObject7 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num6, 45.9f), Quaternion.identity);
            gameObject7.GetComponent<DamageHero>().damageDealt = 2;
            gameObject7.GetComponent<DamageHero>().hazardType = 0;
            gameObject7.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup4.Add(gameObject7);
        }
        for (float num7 = 66.8f; num7 <= 68.8f; num7 += 1f)
        {
            GameObject gameObject8 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num7, 39.1f), Quaternion.identity);
            gameObject8.GetComponent<DamageHero>().damageDealt = 2;
            gameObject8.GetComponent<DamageHero>().hazardType = 0;
            gameObject8.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup5.Add(gameObject8);
        }
        for (float num8 = 71.7f; (double)num8 <= 73.7; num8 += 1f)
        {
            GameObject gameObject9 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num8, 45.1f), Quaternion.identity);
            gameObject9.GetComponent<DamageHero>().damageDealt = 2;
            gameObject9.GetComponent<DamageHero>().hazardType = 0;
            gameObject9.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup6.Add(gameObject9);
        }
        for (float num9 = 57.5f; num9 <= 61.5f; num9 += 1f)
        {
            GameObject gameObject10 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num9, 45.9f), Quaternion.identity);
            gameObject10.GetComponent<DamageHero>().damageDealt = 2;
            gameObject10.GetComponent<DamageHero>().hazardType = 0;
            gameObject10.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup7.Add(gameObject10);
        }
        for (float num10 = 61.6f; num10 <= 65.6f; num10 += 1f)
        {
            GameObject gameObject11 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num10, 51.8f), Quaternion.identity);
            gameObject11.GetComponent<DamageHero>().damageDealt = 2;
            gameObject11.GetComponent<DamageHero>().hazardType = 0;
            gameObject11.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup8.Add(gameObject11);//以上，依次放置了9个地刺
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(63.3f, 49.6f), Quaternion.identity).SetActive(value: true);//该位置平台生成并激活
        for (int j = 57; j <= 59; j++)
        {
            GameObject gameObject12 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(j, 58.2f), Quaternion.identity);
            gameObject12.GetComponent<DamageHero>().damageDealt = 2;
            gameObject12.GetComponent<DamageHero>().hazardType = 0;
            gameObject12.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup9.Add(gameObject12);
        }
        for (float num11 = 63.2f; num11 <= 65.2f; num11 += 1f)
        {
            GameObject gameObject13 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num11, 64.1f), Quaternion.identity);
            gameObject13.GetComponent<DamageHero>().damageDealt = 2;
            gameObject13.GetComponent<DamageHero>().hazardType = 0;
            gameObject13.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup10.Add(gameObject13);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(64.24f, 61.46f), Quaternion.identity).SetActive(value: true);
        for (float num12 = 64.7f; num12 <= 66.7f; num12 += 1f)
        {
            GameObject gameObject14 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num12, 70.8f), Quaternion.identity);
            gameObject14.GetComponent<DamageHero>().damageDealt = 2;
            gameObject14.GetComponent<DamageHero>().hazardType = 0;
            gameObject14.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup11.Add(gameObject14);
        }
        for (float num13 = 57.2f; num13 <= 61.2f; num13 += 1f)
        {
            GameObject gameObject15 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num13, 77.2f), Quaternion.identity);
            gameObject15.GetComponent<DamageHero>().damageDealt = 2;
            gameObject15.GetComponent<DamageHero>().hazardType = 0;
            gameObject15.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup12.Add(gameObject15);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(58.93f, 75.07f), Quaternion.identity).SetActive(value: true);
        for (float num14 = 55.4f; num14 <= 57.4f; num14 += 1f)
        {
            GameObject gameObject16 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num14, 84.7f), Quaternion.identity);
            gameObject16.GetComponent<DamageHero>().damageDealt = 2;
            gameObject16.GetComponent<DamageHero>().hazardType = 0;
            gameObject16.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup13.Add(gameObject16);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(56.32f, 82.04f), Quaternion.identity).SetActive(value: true);
        for (float num15 = 58.9f; num15 <= 60.9f; num15 += 1f)
        {
            GameObject gameObject17 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num15, 89.2f), Quaternion.identity);
            gameObject17.GetComponent<DamageHero>().damageDealt = 2;
            gameObject17.GetComponent<DamageHero>().hazardType = 0;
            gameObject17.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup14.Add(gameObject17);
        }
        _spikeGroup14.Add(GameObject.Find("Ascend Set/Radiant Plat Small (6)"));
        for (float num16 = 61.1f; num16 <= 65.1f; num16 += 1f)
        {
            GameObject gameObject18 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num16, 94.4f), Quaternion.identity);
            gameObject18.GetComponent<DamageHero>().damageDealt = 2;
            gameObject18.GetComponent<DamageHero>().hazardType = 0;
            gameObject18.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup15.Add(gameObject18);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(63.14f, 92.8f), Quaternion.identity).SetActive(value: true);
        for (float num17 = 57.2f; num17 <= 59.2f; num17 += 1f)
        {
            GameObject gameObject19 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num17, 101.1f), Quaternion.identity);
            gameObject19.GetComponent<DamageHero>().damageDealt = 2;
            gameObject19.GetComponent<DamageHero>().hazardType = 0;
            gameObject19.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup16.Add(gameObject19);
        }
        for (float num18 = 63.3f; num18 <= 65.3f; num18 += 1f)
        {
            GameObject gameObject20 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num18, 107.4f), Quaternion.identity);
            gameObject20.GetComponent<DamageHero>().damageDealt = 2;
            gameObject20.GetComponent<DamageHero>().hazardType = 0;
            gameObject20.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup17.Add(gameObject20);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(64.24f, 104.7f), Quaternion.identity).SetActive(value: true);
        for (float num19 = 64.6f; num19 <= 66.6f; num19 += 1f)
        {
            GameObject gameObject21 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num19, 113.7f), Quaternion.identity);
            gameObject21.GetComponent<DamageHero>().damageDealt = 2;
            gameObject21.GetComponent<DamageHero>().hazardType = 0;
            gameObject21.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup18.Add(gameObject21);
        }
        for (float num20 = 57.2f; num20 <= 61.2f; num20 += 1f)
        {
            GameObject gameObject22 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num20, 120.5f), Quaternion.identity);
            gameObject22.GetComponent<DamageHero>().damageDealt = 2;
            gameObject22.GetComponent<DamageHero>().hazardType = 0;
            gameObject22.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup19.Add(gameObject22);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(58.93f, 118.3f), Quaternion.identity).SetActive(value: true);
        for (float num21 = 55.3f; num21 <= 57.3f; num21 += 1f)
        {
            GameObject gameObject23 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num21, 128.4f), Quaternion.identity);
            gameObject23.GetComponent<DamageHero>().damageDealt = 2;
            gameObject23.GetComponent<DamageHero>().hazardType = 0;
            gameObject23.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup20.Add(gameObject23);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(56.32f, 125.74f), Quaternion.identity).SetActive(value: true);
        for (int k = 59; k <= 61; k++)
        {
            GameObject gameObject24 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(k, 133.4f), Quaternion.identity);
            gameObject24.GetComponent<DamageHero>().damageDealt = 2;
            gameObject24.GetComponent<DamageHero>().hazardType = 0;
            gameObject24.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup21.Add(gameObject24);
        }
        for (float num22 = 61.3f; num22 <= 65.3f; num22 += 1f)
        {
            GameObject gameObject25 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num22, 139.2f), Quaternion.identity);
            gameObject25.GetComponent<DamageHero>().damageDealt = 2;
            gameObject25.GetComponent<DamageHero>().hazardType = 0;
            gameObject25.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup22.Add(gameObject25);
        }
        UnityEngine.Object.Instantiate(GameObject.Find("Ascend Set/Radiant Plat Small (4)"), new Vector2(63.14f, 137.03f), Quaternion.identity).SetActive(value: true);
        for (float num23 = 61.8f; num23 <= 63.8f; num23 += 1f)
        {
            GameObject gameObject26 = UnityEngine.Object.Instantiate(_spikeTemplate, new Vector2(num23, 146.5f), Quaternion.identity);
            gameObject26.GetComponent<DamageHero>().damageDealt = 2;
            gameObject26.GetComponent<DamageHero>().hazardType = 0;
            gameObject26.LocateMyFSM("Control").SendEvent("DOWN");
            _spikeGroup23.Add(gameObject26);//以上，放置地刺，生成平台，设置伤害，状态调为down
        }
        Log("Modifying Orbs");//mod化光球
        _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 10;//设定10个
        _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 8;//最少8个
        _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 12;//最多12个
        _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.1f;//等待0.1s
        _attackCommands.GetAction<Wait>("Orb Pause", 0).time = 0.1f;//光球间隔0.1s
        _attackChoices.GetAction<Wait>("Orb Recover", 0).time = 0.5f;//结束之后0.5s
        _attackCommands.GetAction<Wait>("Orb Antic", 0).time.Value = 0.01f;//光球预备0.01s
        Log("Modifying Nail Fan");//mod化脸刺
        _attackCommands.GetAction<SetIntValue>("Nail Fan", 4).intValue.Value = 24;//拿出24根刺
        _attackCommands.GetAction<Wait>("Nail Fan", 2).time.Value = 0.01f;//前摇0.01s
        _attackCommands.GetAction<SetIntValue>("CW Restart", 0).intValue.Value = 24;//拿出24根刺
        _attackCommands.GetAction<SetIntValue>("CCW Restart", 0).intValue.Value = 24; //拿出24根刺
        _attackCommands.GetAction<Wait>("CW Repeat", 0).time = 0.001f;//0.001秒继续生成
        _attackCommands.GetAction<Wait>("CCW Repeat", 0).time = 0.001f;//0.001秒继续生成
        _attackCommands.GetAction<FloatAdd>("CW Restart", 2).add.Value = -7.5f;//转7.5度重新生成
        _attackCommands.GetAction<FloatAdd>("CCW Restart", 2).add.Value = 7.5f;//转7.5度重新生成
        _attackCommands.RemoveAction("CW Restart", 1);//去除了两轮间的等待时间
        _attackCommands.RemoveAction("CCW Restart", 1);
        _attackCommands.RemoveAction("CW Repeat", 0);
        _attackCommands.RemoveAction("CCW Repeat", 0);
        _attackCommands.GetAction<FloatAdd>("CW Spawn", 2).add.Value = -15f;//脸刺夹角为-15度
        _attackCommands.GetAction<FloatAdd>("CCW Spawn", 2).add.Value = 15f;
        Log("Modifying Beam Sweep");//mod化横激光
        _attackChoices.GetAction<Wait>("Beam Sweep L", 0).time.Value = 0.01f;//前摇设为0.01s
        _attackChoices.GetAction<Wait>("Beam Sweep R", 0).time.Value = 0.01f;//前摇设为0.01s
        _attackChoices.ChangeTransition("A1 Choice", "BEAM SWEEP R", "Beam Sweep L");//将一阶段右激光改为左激光
        _attackChoices.ChangeTransition("A2 Choice", "BEAM SWEEP R", "Beam Sweep L 2");//将二阶段右激光改为左激光
        _attackChoices.GetAction<Wait>("Beam Sweep L 2", 0).time.Value = 5.05f;//二阶段激光有5.05s前摇
        _attackChoices.GetAction<Wait>("Beam Sweep R 2", 0).time.Value = 5.05f;
        _attackChoices.GetAction<SendEventByName>("Beam Sweep L 2", 1).sendEvent.Value = "BEAM SWEEP L";//二阶段信号改为一阶段
        _attackChoices.GetAction<SendEventByName>("Beam Sweep R 2", 1).sendEvent.Value = "BEAM SWEEP R";
        Log("Modifying Radial Beam");//mod化脸激光
        _attackCommands.GetAction<SendEventByName>("EB 1", 9).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 1", 10).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 2", 9).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 2", 10).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 3", 9).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 3", 10).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 4", 4).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 4", 5).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 5", 5).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 5", 6).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 6", 5).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 6", 6).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 7", 8).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 7", 9).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 8", 8).delay.Value = 0.3f;
        _attackCommands.GetAction<Wait>("EB 8", 9).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("EB 9", 8).delay.Value = 0.3f;//统一延时0.3s，等待0.5s
        _attackCommands.GetAction<Wait>("EB 9", 9).time.Value = 0.5f;
        _attackCommands.GetAction<SendEventByName>("Aim", 10).delay.Value = 1f;
        _attackCommands.GetAction<Wait>("Aim", 11).time.Value = 0.5f;
        _attackCommands.GetAction<Wait>("Eb Extra Wait", 0).time.Value = 0.01f;//后摇设为0.01s
        _attackCommands.GetAction<Wait>("EB Glow End", 1).time.Value = 0.01f;
        _attackCommands.GetAction<Wait>("EB Glow End 2", 1).time.Value = 0.01f;
        foreach (GameObject radiantBeam in _radiantBeams)
        {////////////////////////////////////////////////////////////////////////
        }
        Log("Modifying Vertical Nail Comb");//mod化剑雨
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 1).delay.Value = 0.25f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 2).delay.Value = 0.5f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep", 3).delay.Value = 0.75f;
        _attackChoices.GetAction<Wait>("Nail Top Sweep", 4).time.Value = 1f;//延时0.25s，0.5s，0.75s，1s后放一层剑雨
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 1).delay.Value = 1.25f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 2).delay.Value = 1.5f;
        _attackChoices.GetAction<SendEventByName>("Nail Top Sweep 2", 3).delay.Value = 1.75f;
        _attackChoices.GetAction<Wait>("Nail Top Sweep 2", 4).time.Value = 1f;
        _control.GetAction<Wait>("Rage Comb", 0).time.Value = 0.25f;
        Log("Modifying Horizontal Nail Comb");//mod化横刺
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 0.25f;
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 1).delay = 1.25f;
        _attackChoices.GetAction<SendEventByName>("Nail L Sweep", 2).delay = 2.25f;
        _attackChoices.GetAction<Wait>("Nail L Sweep", 3).time = 3.5f;//延时0.25s，1.25s，2.25s后放一层横刺
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 0.25f;
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 1).delay = 1.25f;
        _attackChoices.GetAction<SendEventByName>("Nail R Sweep", 2).delay = 2.25f;
        _attackChoices.GetAction<Wait>("Nail R Sweep", 3).time = 3.5f;
        AddNailWall("Nail L Sweep", "COMB R", 1f, 1);
        AddNailWall("Nail R Sweep", "COMB L", 1f, 1);
        AddNailWall("Nail L Sweep", "COMB R", 2f, 1);
        AddNailWall("Nail R Sweep", "COMB L", 2f, 1);
        AddNailWall("Nail L Sweep 2", "COMB R2", 1f, 1);
        AddNailWall("Nail R Sweep 2", "COMB L2", 1f, 1);//添加横刺
        Log("fin.");
    }

    private void Update()
    {
        if (_attackCommands.FsmVariables.GetFsmBool("Repeated").Value)
        {
            switch (CWRepeats)
            {
                case 0:
                    CWRepeats = 1;
                    _attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    break;
                case 1:
                    CWRepeats = 2;
                    _attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    break;
                case 2:
                    _attackCommands.FsmVariables.GetFsmBool("Repeated").Value = false;
                    CWRepeats = 3;
                    break;
                case 3:
                    CWRepeats = 4;
                    break;
            }
        }
        else if (CWRepeats == 4)
        {
            CWRepeats = 0;
        }
        _spikeClone.LocateMyFSM("Control").SendEvent("UP");
        _spikeClone2.LocateMyFSM("Control").SendEvent("UP");
        _spikeClone3.LocateMyFSM("Control").SendEvent("UP");
        _spikeClone4.LocateMyFSM("Control").SendEvent("UP");
        _spikeClone5.LocateMyFSM("Control").SendEvent("UP");//尖刺初始化
        if (_beamsweepercontrol.ActiveStateName == _beamsweeper2control.ActiveStateName)
        {
            switch (_beamsweepercontrol.ActiveStateName)
            {
                case "Beam Sweep L":
                    _beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP R"));
                    break;
                case "Beam Sweep R":
                    _beamsweeper2control.ChangeState(GetFsmEventByName(_beamsweeper2control, "BEAM SWEEP L"));
                    break;
            }//哇嘎奶油
        }
        if (!fullSpikesSet)
        {
            fullSpikesSet = true;//全地刺设置
            _spikeMasterControl.GetAction<SendEventByName>("Spikes Left", 0).sendEvent = "UP";//设置地刺全部up
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
            _spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMin = 0.1f;//没用
            _spikeMasterControl.GetAction<WaitRandom>("Wave L", 7).timeMax = 0.1f;
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 2).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 3).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 4).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 5).sendEvent = "UP";
            _spikeMasterControl.GetAction<SendEventByName>("Wave R", 6).sendEvent = "UP";
            _spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMin = 0.1f;
            _spikeMasterControl.GetAction<WaitRandom>("Wave R", 7).timeMax = 0.1f;//没用
            _spikeMasterControl.SetState("Spike Waves");
            _attackCommands.GetAction<Wait>("Orb Summon", 2).time.Value = 0.25f;//光球等待时间为0.25s
            _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue.Value = 10;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min.Value = 8;
            _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max.Value = 12;
        }
        if (_attackChoices.FsmVariables.GetFsmInt("Arena").Value == 2)//转2阶段
        {
            if (!arena2Set)
            {
                Log("Starting Arena 2");
                arena2Set = true;
                _attackCommands.GetAction<SetIntValue>("Orb Antic", 1).intValue = 10;
                _attackCommands.GetAction<RandomInt>("Orb Antic", 2).min = 8;
                _attackCommands.GetAction<RandomInt>("Orb Antic", 2).max = 12;
                _attackCommands.GetAction<Wait>("Orb Summon", 2).time = 0.25f;//光球设置与一阶段地刺相同
                Log("Modifying Beam Sweeper Area");//mod化横激光区域
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
            foreach (GameObject item in _spikeGroup0)
            {
                item.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item2 in _spikeGroup1)
            {
                item2.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item3 in _spikeGroup2)
            {
                item3.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item4 in _spikeGroup3)
            {
                item4.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item5 in _spikeGroup4)
            {
                item5.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item6 in _spikeGroup5)
            {
                item6.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item7 in _spikeGroup6)
            {
                item7.LocateMyFSM("Control").SendEvent("UP");
            }
            foreach (GameObject item8 in _spikeGroup7)
            {
                item8.LocateMyFSM("Control").SendEvent("UP");
            }
        }
        if (!(base.gameObject.transform.position.y >= 150f))
        {
            return;
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value)//爬楼梯地刺
        {
            foreach (GameObject item9 in _spikeGroup8)
            {
                if (item9.name == "Radiant Spike(Clone)")
                {
                    item9.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item10 in _spikeGroup9)
            {
                if (item10.name == "Radiant Spike(Clone)")
                {
                    item10.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item11 in _spikeGroup10)
            {
                if (item11.name == "Radiant Spike(Clone)")
                {
                    item11.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item12 in _spikeGroup11)
            {
                if (item12.name == "Radiant Spike(Clone)")
                {
                    item12.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item13 in _spikeGroup12)
            {
                if (item13.name == "Radiant Spike(Clone)")
                {
                    item13.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item14 in _spikeGroup13)
            {
                if (item14.name == "Radiant Spike(Clone)")
                {
                    item14.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item15 in _spikeGroup14)
            {
                if (item15.name == "Radiant Spike(Clone)")
                {
                    item15.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item16 in _spikeGroup15)
            {
                if (item16.name == "Radiant Spike(Clone)")
                {
                    item16.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item17 in _spikeGroup16)
            {
                if (item17.name == "Radiant Spike(Clone)")
                {
                    item17.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item18 in _spikeGroup17)
            {
                if (item18.name == "Radiant Spike(Clone)")
                {
                    item18.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item19 in _spikeGroup18)
            {
                if (item19.name == "Radiant Spike(Clone)")
                {
                    item19.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item20 in _spikeGroup19)
            {
                if (item20.name == "Radiant Spike(Clone)")
                {
                    item20.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item21 in _spikeGroup20)
            {
                if (item21.name == "Radiant Spike(Clone)")
                {
                    item21.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item22 in _spikeGroup21)
            {
                if (item22.name == "Radiant Spike(Clone)")
                {
                    item22.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item23 in _spikeGroup22)
            {
                if (item23.name == "Radiant Spike(Clone)")
                {
                    item23.LocateMyFSM("Control").SendEvent("UP");
                }
            }
            foreach (GameObject item24 in _spikeGroup23)
            {
                if (item24.name == "Radiant Spike(Clone)")
                {
                    item24.LocateMyFSM("Control").SendEvent("UP");
                }
            }
        }
        if (_hm.hp < _phaseControl.FsmVariables.GetFsmInt("P5 Acend").Value)//最后一阶段
        {
            GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat").ChangeState(GetFsmEventByName(GameObject.Find("Radiant Plat Small (10)").LocateMyFSM("radiant_plat"), "SLOW VANISH"));
            if (!onePlatSet)
            {
                onePlatSet = true;
                Log("Removing upper right platform");
                _attackCommands.GetAction<Wait>("Orb Antic", 0).time.Value = 0.01f;
                _attackCommands.GetAction<Wait>("FinalOrb Pause", 0).time.Value = 0.25f;
                _attackChoices.GetAction<Wait>("Orb Recover", 0).time.Value = 0.01f;
            }
        }
    }

    private void AddNailWall(string stateName, string eventName, float delay, int index)
    {
        _attackChoices.InsertAction(stateName, new SendEventByName
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
        Modding.Logger.Log("[AnyRadiance2.0]: " + obj);
    }
}
