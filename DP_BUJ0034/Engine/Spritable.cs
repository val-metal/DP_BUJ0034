using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using System.Numerics;

using DP_BUJ0034.Expectations;


#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif


namespace DP_BUJ0034.Engine
{
    public class Spritable
    {
        private string spritePath;
        public string SpritePath
        {
            get => spritePath; // Get blok pro čtení hodnoty
            set
            {
                spritePath = value;
                //loadSprite();
            }
        }

        public IImage sprite { get; set; }

        public bool isLoaded { get; set; }

        private void loadSprite()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            //canvas.BlendMode = BlendMode.Difference;
            using (Stream stream = assembly.GetManifestResourceStream(spritePath))
            {
                #if IOS || ANDROID || MACCATALYST
                                                // PlatformImage isn't currently supported on Windows.
                                    sprite = PlatformImage.FromStream(stream);
                #elif WINDOWS
                                sprite = new W2DImageLoadingService().FromStream(stream);
                #endif
            }

            if (sprite is null)
            {
                throw new SpriteIsNotFoundException("Cant find sprite");
            }
            isLoaded = true;

        }
    }
}
