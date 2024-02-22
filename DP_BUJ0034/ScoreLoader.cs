
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034
{
    public class ScoreLoader
    {
        static ScoreLoader instance;
        LevelInfo[] levelinfo;
        public List<int> stars { get; set; }
        public int complete_score { get; set; }
        private ScoreLoader() { 

        }
        public static ScoreLoader GetInstance()
        {
            if (instance == null)
            {
                instance = new ScoreLoader();
            }
            return instance;
        }
        public  static async Task<(int[],int)> LoadScore()
        {
             List<int> stars = new List<int>();
            int complete_score = 0;
        LevelInfo[]  levelinfo = await LevelLoader.LoadAllLevels();
            for(int i=0;i<levelinfo.Length;i++)
            {
                string data = await SecureStorage.Default.GetAsync(levelinfo[i].name);
                if (data == null)
                {
                    await SecureStorage.Default.SetAsync(levelinfo[i].name, "0");
                    stars.Add(0);
                }
                else
                { 
                    int parsednum=Int32.Parse(data);
                    stars.Add(parsednum);
                    complete_score += parsednum;
                }
            }
            return (stars.ToArray(),complete_score);
        }
        public async Task AddStars(int num, string levelName)
        {
            string data = await SecureStorage.Default.GetAsync(levelName);
            int parsednum = Int32.Parse(data);
            if (parsednum < num)
            {
                await SecureStorage.Default.SetAsync(levelName, "" + num);
            }
        }

        public async Task ResetStars()
        {
            LevelInfo[] levelinfo = await LevelLoader.LoadAllLevels();
            for(int i = 0; i < levelinfo.Length; i++)
            {
                
                await SecureStorage.Default.SetAsync(levelinfo[i].name, "0");
       
                
            }
        }

    }
}
