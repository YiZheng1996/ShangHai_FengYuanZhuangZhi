using MainUI.Modules;
using MainUI.Model;
using MainUI.Config;

namespace MainUI
{
    public class Common
    {
        public static OpcStatusGrp opcStatus = new OpcStatusGrp();
        public static AIGrp AIgrp;
        public static AOGrp AOgrp;
        public static DIGrp DIgrp;
        public static DOGrp DOgrp;
        public static NozzleGrp Nozzlegrp;
        public static SubjectsGrp Subjectsgrp;
        public static WSDGrp WSDgrp;
        public static PowerControlGrp powerControlgrp;
        public static PowerReadGrp powerReadgrp;
        public static PLCCalibration plcc;
        public static TestViewModel mTestViewModel = new TestViewModel();
        public static ResultAll mResultAll = new ResultAll();
        public static double liuliangRst;
        public static ParaConfig paraconfig;

        public static void Init()
        {
            AIgrp = new AIGrp();
            AOgrp = new AOGrp();
            DIgrp = new DIGrp();
            DOgrp = new DOGrp();
            Nozzlegrp = new NozzleGrp();
            Subjectsgrp = new SubjectsGrp();
            WSDgrp = new WSDGrp();
            powerControlgrp = new PowerControlGrp();
            powerReadgrp = new PowerReadGrp();
            plcc = new PLCCalibration();
            opcStatus.Init();
            AIgrp.Init();
            AOgrp.Init();
            DIgrp.Init();
            DOgrp.Init();
            Nozzlegrp.Init();
            Subjectsgrp.Init();
            WSDgrp.Init();
            powerControlgrp.Init();
            powerReadgrp.Init();
            plcc.Init();
            paraconfig = new ParaConfig();
        }
    }
}
