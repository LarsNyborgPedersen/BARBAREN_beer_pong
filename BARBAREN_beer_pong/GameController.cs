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
        private string _period;
        
        private GameController()
        {
            SetupEnvironment();
        }

        private static GameController _instance;

        /**
         *  Use this to access the API
         *  
         */
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
            public string Teamname;
            public int Won;
            public int Lost;
            public int Score;

            public ScoreProbe(string a, int b, int c, int d)
            {
                Teamname = a;
                Won = b;
                Lost = c;
                Score = d;
            }
        }

        /**
         * Returns the list of scores from the current period, sorted by socre
         */
        public ScoreProbe[] GetScores()
        {
            string alpha = GetWorkingTarget();
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
            while (true)
            {
                bool again = false;
                for (int i = 0; i < probes.Length; i++)
                {
                    ScoreProbe a = probes[i];
                    ScoreProbe b = null;
                    if ((i + 1) < probes.Length)
                    {
                        b = probes[i + 1];
                    }

                    if (b == null)
                    {
                        goto Gamma;
                    }

                    if (b.Score > a.Score)
                    {
                        again = true;
                        ScoreProbe c = probes[i];
                        probes[i] = probes[i + 1];
                        probes[i + 1] = c;
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

        /**
         * Increase win-count by 1
         */
        public void IncreseWins(string teamname)
        {
            SetWins(teamname,GetWins(teamname)+1);
        }

        /**
         * Increase lost-count by 1
         */
        public void IncreseLost(string teamname)
        {
            SetLost(teamname,GetLost(teamname)+1);
        }

        /*
         * Decrese win-cout by 1
         */
        public void DecreseWins(string teamname)
        {
            SetWins(teamname,GetWins(teamname)-1);
        }

        /*
         * Decrese lost-count by 1
         */
        public void DecreseLost(string teamname)
        {
            SetLost(teamname,GetLost(teamname)-1);
        }

        private string GetScoreTarget(string teamname)
        {
            if (GetWorkingPeriod() == null)
            {
                throw new Exception("SetWorkingPeriod not used");
            }
            
            string targetfile = GetWorkingTarget();
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

        private string GetWorkingTarget()
        {
            return _projectpath + Path.DirectorySeparatorChar + Period + Path.DirectorySeparatorChar + GetWorkingPeriod() + Path.DirectorySeparatorChar + "scores.txt";
        }

        private void ReplaceFileContext(string teamname, string context)
        {
            if (GetWorkingPeriod() == null)
            {
                throw new Exception("SetWorkingPeriod not used");
            }
            
            string targetfile = GetWorkingTarget();
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

        private string[] GetScoreTagOf(string teamname)
        {
            string target = GetScoreTarget(teamname);
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
        

        /*
         * Set wins by team
         */
        public void SetWins(string teamname, int score)
        {
            string[] tokens = GetScoreTagOf(teamname);
            int rlx = score - int.Parse(tokens[2]);
            string rsl = teamname + "\t" + score + "\t" + tokens[2] + "\t" + rlx;
            ReplaceFileContext(teamname, rsl);
        }

        /*
         * Set losts by team
         */
        public void SetLost(string teamname, int score)
        {
            string[] tokens = GetScoreTagOf(teamname);
            int rlx = int.Parse(tokens[1]) - score ;
            string rsl = teamname + "\t" + tokens[1] + "\t" + score + "\t" + rlx;
            ReplaceFileContext(teamname, rsl);
        }

        /*
         * Get wins by team
         */
        public int GetWins(string teamname)
        {
            return int.Parse(GetScoreTagOf(teamname)[1]);
        }

        /*
         * Get losts by team
         */
        public int GetLost(string teamname)
        {
            return int.Parse(GetScoreTagOf(teamname)[2]);
        }

        /*
         * Get scoe by team
         */
        public int GetScore(string teamname)
        {
            return int.Parse(GetScoreTagOf(teamname)[3]);
        }
        
        //
        // TEAM
        //

        /*
         * Get teamnames in alfabetical order
         */
        public string[] GetTeamNames()
        {
            string[] keys =  Directory.GetDirectories(_projectpath + Path.DirectorySeparatorChar+Team+Path.DirectorySeparatorChar).OrderBy(f=>f).ToArray();
            Stack<string> stack = new Stack<string>();
            foreach (string sleutel in keys)
            {
                stack.Push(sleutel.Replace(_projectpath + Path.DirectorySeparatorChar+Team+Path.DirectorySeparatorChar,""));
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

        /*
         * Define a new team
         */
        public void AddTeam(string newname)
        {
            if (!TeamExists(newname))
            {
                Directory.CreateDirectory(_projectpath + Path.DirectorySeparatorChar+Team+Path.DirectorySeparatorChar + newname);
                Console.WriteLine("Team \"" + newname + "\" has been added to the game");
            }
            else
            {
                Console.WriteLine("Command AddTeam("+newname+") ignored because it already exists");
            }
        }

        /*
         * Get a list of teammatesnames by team
         */
        public string[] GetTeamMembers(string teamname)
        {
            if (TeamExists(teamname))
            {
                string swapfilename = _projectpath + Path.DirectorySeparatorChar + Team + Path.DirectorySeparatorChar + teamname + Path.DirectorySeparatorChar +"members.txt";
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

        /*
         * Removes a teammate from a team
         */
        public void RemoveTeamMember(string teamname, string teammembername)
        {
            if (TeamMemberExists(teamname, teammembername))
            {
                Stack<string> stack = new Stack<string>();
                string swapfilename = _projectpath + Path.DirectorySeparatorChar + Team + Path.DirectorySeparatorChar + teamname + Path.DirectorySeparatorChar+"members.txt";
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

        /*
         * Adds a teammember to the team
         */
        public void AddTeamMember(string teamname, string teammembername)
        {
            if (TeamMemberExists(teamname, teammembername))
            {
                Console.WriteLine(teammembername+" already exists in "+teamname);
            }
            else
            {
                string swapfilename = _projectpath + Path.DirectorySeparatorChar + Team + Path.DirectorySeparatorChar + teamname + Path.DirectorySeparatorChar+"members.txt";
                File.AppendAllText(swapfilename,teammembername);
                Console.WriteLine(teammembername+" is now a part of "+teamname);
            }
        }
        
        //
        // PERIOD
        //


        public string[] GetPeriodsByDate()
        {
            string[] target = GetPeriodNames();
            string pathbase = _projectpath + Path.DirectorySeparatorChar + Period + Path.DirectorySeparatorChar;
            while (true)
            {
                var veranderd = false;
                for (int i = 0; i < target.Length; i++)
                {
                    DateTime a = Directory.GetCreationTime(pathbase + target[i]);
                    DateTime b;
                    if ((i + 1) < target.Length)
                    {
                        b = Directory.GetCreationTime(pathbase + target[i]);
                        if (a.CompareTo(b) < 0)
                        {
                            String c = target[i];
                            String d = target[i + 1];
                            target[i] = d;
                            target[i + 1] = c;
                            veranderd = true;
                        }
                    }
                }

                if (!veranderd)
                {
                    break;
                }
            }

            return target;
        }
        
        //
        // Gets a list of available working periods
        public string[] GetPeriodNames()
        {
            string[] keys =  Directory.GetDirectories(_projectpath + Path.DirectorySeparatorChar+Period+Path.DirectorySeparatorChar).OrderBy(f=>f).ToArray();
            Stack<string> stack = new Stack<string>();
            foreach (string sleutel in keys)
            {
                stack.Push(sleutel.Replace(_projectpath + Path.DirectorySeparatorChar +Period+Path.DirectorySeparatorChar,""));
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
                Directory.CreateDirectory(_projectpath + Path.DirectorySeparatorChar+Period+Path.DirectorySeparatorChar + newname);
                File.Create(_projectpath + Path.DirectorySeparatorChar +Period+Path.DirectorySeparatorChar + newname+Path.DirectorySeparatorChar+"scores.txt").Close();
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
            String pathToDesktop = AppDomain.CurrentDomain.BaseDirectory;//Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (Directory.Exists(pathToDesktop))
            {
                _projectpath = pathToDesktop + Path.DirectorySeparatorChar + "BarbarenBeerPong";
                if (!Directory.Exists(_projectpath))
                {
                    Directory.CreateDirectory(_projectpath);
                    Console.WriteLine("Game directory created: " + Path.DirectorySeparatorChar +_projectpath+Path.DirectorySeparatorChar+"");
                }

                if (!Directory.Exists(_projectpath + Path.DirectorySeparatorChar +Period+ Path.DirectorySeparatorChar))
                {
                    Directory.CreateDirectory(_projectpath + Path.DirectorySeparatorChar +Period + Path.DirectorySeparatorChar);
                    Console.WriteLine("Period directory created inside gaming directory");
                }

                if (!Directory.Exists(_projectpath + Path.DirectorySeparatorChar +Team+ Path.DirectorySeparatorChar))
                {
                    Directory.CreateDirectory(_projectpath + Path.DirectorySeparatorChar +Team+ Path.DirectorySeparatorChar);
                    Console.WriteLine("Teams directory created inside gaming directory");
                }
            }
        }
        
    }
    
}