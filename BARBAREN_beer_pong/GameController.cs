using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BARBAREN_beer_pong_lib
{
    public class GameController
    {
        private const string Team = "Team";
        private const string Period = "Period";

        //
        // project path
        private string _projectpath;

        //
        // period path
        private string _period = null;
        
        public GameController()
        {
            SetupEnvironment();
        }

        private static GameController _instance;

        public static GameController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameController();
            }
            return _instance;
        }

        //
        // ADD SCORE
        //

        public class ScoreProbe
        {
            public string teamname;
            public int won;
            public int lost;
            public int score;

            public ScoreProbe(string a, int b, int c, int d)
            {
                teamname = a;
                won = b;
                lost = c;
                score = d;
            }
        }

        public ScoreProbe[] getScores()
        {
            string alpha = getWorkingTarget();
            Stack<ScoreProbe> probe = new Stack<ScoreProbe>();
            foreach (string line in File.ReadLines(alpha))
            {
                if (line.Contains("\t"))
                {
                    string[] selector = line.Split('\t');
                    probe.Push(new ScoreProbe(selector[0],int.Parse(selector[1]),int.Parse(selector[2]),int.Parse(selector[3])));
                }
            }

            ScoreProbe[] probes = probe.ToArray();
            bool again = false;
            while (true)
            {
                again = false;
                for (int i = 0; i < probes.Length; i++)
                {
                    ScoreProbe A = probes[i];
                    ScoreProbe B = null;
                    if ((i + 1) < probes.Length)
                    {
                        B = probes[i + 1];
                    }

                    if (B == null)
                    {
                        goto Gamma;
                    }

                    if (B.score > A.score)
                    {
                        again = true;
                        ScoreProbe C = probes[i];
                        probes[i] = probes[i + 1];
                        probes[i + 1] = C;
                    }
                }
                Gamma:

                if (!again)
                {
                    break;
                }
            }

            return probes;
        }

        public void IncreseWins(string teamname)
        {
            SetWins(teamname,GetWins(teamname)+1);
        }

        public void IncreseLost(string teamname)
        {
            SetLost(teamname,GetLost(teamname)+1);
        }

        public void DecreseWins(string teamname)
        {
            SetWins(teamname,GetWins(teamname)-1);
        }

        public void DecreseLost(string teamname)
        {
            SetLost(teamname,GetLost(teamname)-1);
        }

        private string getScoreTarget(string teamname)
        {
            if (GetWorkingPeriod() == null)
            {
                throw new Exception("SetWorkingPeriod not used");
            }
            
            string targetfile = getWorkingTarget();
            string targetstring = null;
            foreach (string line in File.ReadLines(targetfile))
            {
                if (line.Contains("\t"))
                {
                    if (line.Split('\t')[0].Equals(teamname))
                    {
                        targetstring = line;
                    }
                }
            }

            return targetstring;
        }

        private string getWorkingTarget()
        {
            return _projectpath + "/" + Period + "/" + GetWorkingPeriod() + "/scores.txt";
        }

        private void ReplaceFileContext(string teamname, string context)
        {
            if (GetWorkingPeriod() == null)
            {
                throw new Exception("SetWorkingPeriod not used");
            }
            
            string targetfile = getWorkingTarget();
            Stack<string> stack = new Stack<string>();
            Boolean isadded = true;
            foreach (string line in File.ReadLines(targetfile))
            {
                if (line.StartsWith(teamname + "\t"))
                {
                    stack.Push(context);
                    isadded = false;
                }
                else
                {
                    stack.Push(line);
                }
            }

            if (isadded)
            {
                stack.Push(context);
            }
            StreamWriter writer = File.CreateText(targetfile);
            foreach (string line in stack)
            {
                writer.WriteLine(line);
            }
            writer.Close();
        }

        private string[] getScoreTagOf(string teamname)
        {
            string target = getScoreTarget(teamname);
            if (target == null)
            {
                if (TeamExists(teamname))
                {
                    target = teamname + "\t0\t0\t0";
                }
                else
                {
                    throw new Exception("Invalid teamname");
                }
            }

            string[] tokens = target.Split('\t');
            return tokens;
        }
        

        public void SetWins(string teamname, int score)
        {
            string[] tokens = getScoreTagOf(teamname);
            int rlx = score - int.Parse(tokens[2]);
            string rsl = teamname + "\t" + score + "\t" + tokens[2] + "\t" + rlx;
            ReplaceFileContext(teamname, rsl);
        }

        public void SetLost(string teamname, int score)
        {
            string[] tokens = getScoreTagOf(teamname);
            int rlx = int.Parse(tokens[1]) - score ;
            string rsl = teamname + "\t" + tokens[1] + "\t" + score + "\t" + rlx;
            ReplaceFileContext(teamname, rsl);
        }

        public int GetWins(string teamname)
        {
            return int.Parse(getScoreTagOf(teamname)[1]);
        }

        public int GetLost(string teamname)
        {
            return int.Parse(getScoreTagOf(teamname)[2]);
        }

        public int GetScore(string teamname)
        {
            return int.Parse(getScoreTagOf(teamname)[3]);
        }
        
        //
        // TEAM
        //

        public string[] GetTeamNames()
        {
            string[] keys =  Directory.GetDirectories(_projectpath + "/"+Team+"/").OrderBy(f=>f).ToArray();
            Stack<string> stack = new Stack<string>();
            foreach (string sleutel in keys)
            {
                stack.Push(sleutel.Replace(_projectpath + "/"+Team+"/",""));
            }
            return stack.ToArray();
        }

        private bool TeamExists(string newname)
        {
            foreach (string name in GetTeamNames())
            {
                if (name.Equals(newname))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddTeam(string newname)
        {
            if (!TeamExists(newname))
            {
                Directory.CreateDirectory(_projectpath + "/"+Team+"/" + newname);
                Console.WriteLine("Team \"" + newname + "\" has been added to the game");
            }
            else
            {
                Console.WriteLine("Command AddTeam("+newname+") ignored because it already exists");
            }
        }

        public string[] GetTeamMembers(string teamname)
        {
            if (TeamExists(teamname))
            {
                string swapfilename = _projectpath + "/" + Team + "/" + teamname + "/members.txt";
                if (File.Exists(swapfilename))
                {
                    return File.ReadAllLines(swapfilename);
                }
            }
            return new string[]{};
        }

        private bool TeamMemberExists(string teamname, string teammembername)
        {
            foreach (string member in GetTeamMembers(teamname))
            {
                if (member.Equals(teammembername))
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveTeamMember(string teamname, string teammembername)
        {
            if (TeamMemberExists(teamname, teammembername))
            {
                Stack<string> stack = new Stack<string>();
                string swapfilename = _projectpath + "/" + Team + "/" + teamname + "/members.txt";
                foreach (string context in File.ReadAllLines(swapfilename))
                {
                    if (!context.Equals(teammembername))
                    {
                        stack.Push(context);
                    }
                }

                StreamWriter stream = new StreamWriter(File.Open(swapfilename,FileMode.Open));

                foreach (string item in stack)
                {
                    stream.WriteLine(item);
                }
                stream.Flush();
                stream.Close();
                Console.WriteLine("Removed "+teammembername+" of "+teamname);
            }
            else
            {
                Console.WriteLine("Cannot remove "+teammembername+" because he/she does not exist");
            }
        }

        public void AddTeamMember(string teamname, string teammembername)
        {
            if (TeamMemberExists(teamname, teammembername))
            {
                Console.WriteLine(teammembername+" already exists in "+teamname);
            }
            else
            {
                string swapfilename = _projectpath + "/" + Team + "/" + teamname + "/members.txt";
                File.AppendAllText(swapfilename,teammembername);
                Console.WriteLine(teammembername+" is now a part of "+teamname);
            }
        }
        
        //
        // PERIOD
        //
        
        
        
        
        //
        // Gets a list of available working periods
        public string[] GetPeriodNames()
        {
            string[] keys =  Directory.GetDirectories(_projectpath + "/"+Period+"/").OrderBy(f=>f).ToArray();
            Stack<string> stack = new Stack<string>();
            foreach (string sleutel in keys)
            {
                stack.Push(sleutel.Replace(_projectpath + "/"+Period+"/",""));
            }
            return stack.ToArray();
        }

        //
        // Returns current working period
        public string GetWorkingPeriod()
        {
            return _period;
        }

        //
        // Sets working period
        public void SetWorkingPeriod(string per)
        {
            Console.WriteLine("Working period has been changed from "+_period + " to "+ per);
            _period = per;
        }

        //
        // Checks if period exists in the game
        private bool PeriodExists(string newname)
        {
            foreach (string name in GetPeriodNames())
            {
                if (name.Equals(newname))
                {
                    return true;
                }
            }

            return false;
        }

        //
        // Adds a new period
        public void AddPeriod(string newname)
        {
            if (!PeriodExists(newname))
            {
                Directory.CreateDirectory(_projectpath + "/"+Period+"/" + newname);
                File.Create(_projectpath + "/"+Period+"/" + newname+"/scores.txt").Close();
                Console.WriteLine("Period \"" + newname + "\" has been added to the game");
            }
            else
            {
                Console.WriteLine("Command AddPeriod("+newname+") ignored because it already exists");
            }
        }
        
        //
        // DEFAULT
        //
        
        
        //
        // Checks if all directories and other dependencies are present 
        private void SetupEnvironment()
        {
            string execname = "/" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".exe";
            String pathToDesktop = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(execname,"");//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (Directory.Exists(pathToDesktop))
            {
                _projectpath = pathToDesktop + '/' + "BarbarenBeerPong";
                if (!Directory.Exists(_projectpath))
                {
                    Directory.CreateDirectory(_projectpath);
                    Console.WriteLine("Game directory created: \""+_projectpath+"\"");
                }

                if (!Directory.Exists(_projectpath + "/"+Period+"/"))
                {
                    Directory.CreateDirectory(_projectpath + "/"+Period+"/");
                    Console.WriteLine("Period directory created inside gaming directory");
                }

                if (!Directory.Exists(_projectpath + "/"+Team+"/"))
                {
                    Directory.CreateDirectory(_projectpath + "/"+Team+"/");
                    Console.WriteLine("Teams directory created inside gaming directory");
                }

                return;
            }

            return;
        }
        
    }
    
}