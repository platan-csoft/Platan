using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace sprut
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            if (IsCanExecute("platan"))
            {

                frmMain.FioUser = args[0];
                frmMain.FioPrms = args[1];
                frmMain.TdmsName = args[2];
                frmMain.TdmsSrv = args[3];
                frmMain.UserSqlName = args[4];
                frmMain.UserSqlPassword = args[5];
                frmMain.SqlLink = args[6];


                ////frmMain.FioUser = "USER_6F495E44_846A_4F37_8CFA_CF120FF49FDC";    //"Антоненко Роман Андреевич";//args[0];
                //frmMain.FioUser = "USER_85551D33_2F27_4DE2_A43F_817174A8F610";    //"Астапов Дмитрий Владимирович";//args[0];

                //frmMain.FioPrms = "planner";//args[1];
                //////frmMain.FioPrms = "head";//args[1];
                //frmMain.TdmsName = "TDMSv5_p";//"KGNP_TEST";
                ////frmMain.FioUser = "USER_E513E0B0_C90B_49FC_A411_05FC3097E2DB"; //Рошук
                //////frmMain.FioUser = "USER_29793800_799E_4B18_912B_ABA9725A057F"; //пичуркин
                ////frmMain.FioUser = "USER_85551D33_2F27_4DE2_A43F_817174A8F610";  //АСтапов
                //////frmMain.FioUser = "USER_F2753307_160E_4C7C_B64B_AC3DA8D1AF01"; //Ульянов
                //frmMain.TdmsSrv = "sqlprod2014";// "217.23.133.173";
                //frmMain.UserSqlName = "pdmstotdms";//"platanuser";
                //frmMain.UserSqlPassword = "PdMsToTdMs";//"platanuser";
                //frmMain.SqlLink = "sqlprod2014";//"217.23.133.173";
               

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            else
            {
                MessageBox.Show("На компьютере уже запущена одна копия программы!", "Повторный запуск", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private static bool IsCanExecute(string name)
        {
            int count_process = 0;

            Process[] pr = Process.GetProcesses();
            for (int i = 0; i < pr.Length; i++)
            {
                if (pr[i].ProcessName == name || pr[i].ProcessName == name + ".exe")
                {
                    count_process++;
                }
            }

            return count_process > 1 ? false : true;
        }
    }
}
