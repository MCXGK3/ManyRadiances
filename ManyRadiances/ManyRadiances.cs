using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace ManyRadiances
{
    public class ManyRadiances : Mod,IGlobalSettings<Settings>,IMenuMod
    {
        internal static ManyRadiances Instance;
        static Settings s_=new();
        public bool ToggleButtonInsideMenu => true;
        public static FsmStateAction[] origQactions;
        public string[] ars = {"Any",
            "Any",
            "",
            "Ultimatum",
            "愚妄",
            "Supernova",
            "Any",
            "Atomic",
            "铁头"};
        public string[] gg = {"God of meme.",
            "God of meme.",
            "",
            "God of light, sworn to crush any rebellion",
            "盲目与无知的光芒神",
            "God of 10^46 joules",
            "God of meme.",
            "",
            ""};
        public string[] grs = {"Ok.",
            "Ok.",
            "",
            "Incredible! For a mere Speck to take up arms and defy the brilliant deity's ultimatum is to be consigned to oblivion, and yet thou survive!\n\nBut couldst thou ever hope to overcome that mighty God tuned at the core of dream and mind, when met in perfect state, at peak of all others? We think not!\n\nSeriously, thy time is probably better spent elsewhere.",
            "难以置信，小鬼魂。你的力量竟足以对抗无上的光芒\n\n但你又能否在那高高的万神殿之巅，征服在光明中联合的意志？\n\n我竟有些害怕你了，小鬼魂。",
            "Okay.",
            "k",
            "",
            ""};


        public ManyRadiances() : base("ManyRadiances")
        {
            Instance = this;
        }

        public override string GetVersion()
        {
            return "0.0.0.1";
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Instance = this;
            ars[2]= s_.super;
            gg[2] = s_.gg;
            grs[2] = s_.grs;
            On.PlayMakerFSM.OnEnable += mod_rad;
            ModHooks.LanguageGetHook += langet;
        }

        private string langet(string key, string sheetTitle, string orig)
        {
            switch (key)
            {
                case "ABSOLUTE_RADIANCE_SUPER": if (Pri(s_)==-1) return orig; return ars[Pri(s_)];
                case "ABSOLUTE_RADIANCE_MAIN": if (Pri(s_) == -1) return orig; if (Pri(s_) == 2) return s_.main; else return orig;
                case  "GG_S_RADIANCE": if (Pri(s_) == -1) return orig; return gg[Pri(s_)];
                case "GODSEEKER_RADIANCE_STATUE": if (Pri(s_) == -1) return orig; return grs[Pri(s_)];
                default: return orig;
            }
        }

        private void mod_rad(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            if (self.gameObject.name == "Absolute Radiance" && self.FsmName == "Control")
            {
                if (s_.any1) { self.gameObject.AddComponent<any1>(); }
                if (s_.any2) { self.gameObject.AddComponent<any2>(); }
                if (s_.any3) { }
                if (s_.Ultimatum) { self.gameObject.AddComponent<ultimatum>(); }
                if (s_.Dumb) { origQactions = HeroController.instance.gameObject.LocateMyFSM("Spell Control").GetState("Q2 Land").Actions;self.gameObject.AddComponent<dumb>(); }
                if (s_.anyPrime) { self.gameObject.AddComponent<anyprime>(); }
                if (s_.Supernova) { self.gameObject.AddComponent<supernova>(); }
                if (s_.Atomic) { self.gameObject.AddComponent<atomic>(); }
                if(s_.IronHead) { self.gameObject.AddComponent<ironhead>(); }
            }
            orig(self);
        }

        void IGlobalSettings<Settings>.OnLoadGlobal(Settings s)
        {
            s_ = s;
        }

        Settings IGlobalSettings<Settings>.OnSaveGlobal()
        {
            return s_;
        }
        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            List<IMenuMod.MenuEntry> menu = new();
            menu.Add(
                new()
                {
                    Name = "AnyRadiance1.0",
                    Description = "any辐光1.0",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.any1? 0 : 1,
                    Saver = i =>s_.any1 = i == 0
                });
            menu.Add(
                new()
                {
                    Name = "AnyRadiance2.0",
                    Description = "any辐光2.0",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.any2? 0 : 1,
                    Saver = i => s_.any2= i == 0
                });
            menu.Add(
                new()
                {
                    Name = "AnyRadiance3.0",
                    Description = "any辐光3.0(懒得做了）",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.any3? 0 : 1,
                    Saver = i => s_.any3= i == 0
                });
            menu.Add(
                new()
                {
                    Name = "UltimatumRadiance",
                    Description = "终焉辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.Ultimatum? 0 : 1,
                    Saver = i => s_.Ultimatum= i == 0
                });
            menu.Add(
                new()
                {
                    Name = "DumbRadiance",
                    Description = "愚妄辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.Dumb? 0 : 1,
                    Saver = i =>s_.Dumb = i == 0
                });
            menu.Add(
                new()
                {
                    Name = "SupernovaRadiance",
                    Description = "超新星辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.Supernova? 0 : 1,
                    Saver = i => s_.Supernova= i == 0
                });
            menu.Add(
                new()
                {
                    Name = "AnyRadiancePrime",
                    Description = "全盛any辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.anyPrime? 0 : 1,
                    Saver = i => s_.anyPrime= i == 0
                });
            menu.Add(
                new()
                {
                    Name = "AtomicRadiance",
                    Description = "原子辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () =>s_.Atomic? 0 : 1,
                    Saver = i =>s_.Atomic = i == 0
                });
            menu.Add(
                new()
                {
                    Name = "IronHeadRadiance",
                    Description = "铁头辐光",
                    Values = new string[]
                 {
                     Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                 },
                    Loader = () => s_.IronHead ? 0 : 1,
                    Saver = i => s_.IronHead = i == 0
                });


            return menu;
        }
        private int Pri(Settings s)
        {
            if (s.any3) return 2;
            if (s.any1) return 0;
            if (s.any2) return 1;
            if (s.Ultimatum) return 3;
            if (s.Dumb) return 4;
            if (s.Supernova) return 5;
            if(s.anyPrime) return 6;
            if (s.Atomic) return 7;
            if (s.IronHead) return 8;
            else return -1;
        }
    }
}