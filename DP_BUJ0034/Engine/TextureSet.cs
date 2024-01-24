using DP_BUJ0034.Expectations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DP_BUJ0034.Engine
{
    public class TextureSet
    {
        public List<string> player;
        public List<string> paths;
        public List<string> finish;
        string nameOfLevel;
        public string background { get; set; }
        public bool backgroudIsImage;
        public bool pathIsTexture;
        public bool pathIsColor;
        public bool pathIsIteam;


        public TextureSet(string nameOfLevel)
        {
            player = new List<string>(); paths = new List<string>(); finish = new List<string>();

            player.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Player_1.png");
            
            player.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Player_2.png");
            player.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Player_3.png");
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            Stream streamt = assembly.GetManifestResourceStream("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Texture_1.png");
            Stream streamc = assembly.GetManifestResourceStream("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Color_1.png");
            Stream streami = assembly.GetManifestResourceStream("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Item_1.png");

            if (streamt!=null)
            {
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Texture_1.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Texture_2.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Texture_3.png");
                pathIsTexture = true;
                streamt.Close();
            }
            else if (streamc is not null)
            {
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Color_1.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Color_2.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Color_3.png");
                pathIsColor = true;
                streamc.Close();
            }
            else if (streami is not null)
            {
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Item_1.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Item_2.png");
                paths.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Path_Item_3.png");
                pathIsIteam = true;
                streami.Close();
            }
            else
            {
                throw new SpriteIsNotFoundException();
            }


            finish.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Finish_1.png");
            finish.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Finish_2.png");
            finish.Add("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Finish_3.png");

            Stream streambi = assembly.GetManifestResourceStream("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Background_Img.png");
            Stream streambt = assembly.GetManifestResourceStream("DP_BUJ0034.Resources.Images." + nameOfLevel + ".Background_Texture.png");

            if (streambi is not null)
            {
                background = "DP_BUJ0034.Resources.Images." + nameOfLevel + ".Background_Img.png";
                streambi.Close();
                backgroudIsImage = true;
            }
            else if (streambt is not null)
            {
                background = "DP_BUJ0034.Resources.Images." + nameOfLevel + ".Background_Texture.png";
                streambt.Close();
            }
            else
            {
                throw new SpriteIsNotFoundException();
            }


            this.nameOfLevel = nameOfLevel;
        }


    }
}
