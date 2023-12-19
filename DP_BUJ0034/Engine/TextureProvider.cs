using DP_BUJ0034.Expectations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IImage = Microsoft.Maui.Graphics.IImage;

using System.Reflection;


#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#endif
namespace DP_BUJ0034.Engine
{
    public class TextureProvider
    {
        public TextureProvider() { player = new List<IImage>();routes = new List<IImage>(); ends = new List<IImage>(); }
        TextureSet textureSet;
        IImage backgroundImage;
        List<IImage> player;
        List<IImage> routes;
        List<IImage> ends;
        public bool isLoaded { get; set; }

        public bool isBackgroundTexture()
        {
            return !textureSet.backgroudIsImage;
        }
        public void loadByName(string name)
        {
            if (name == "AtticTextureSet")
            {
                textureSet = new AtticTextureSet();
            }
            else if (name == "SteamSky")
            {
                textureSet = new SteamMountainTextureSet();
            }
            else if (name == "NeonCity")
            {
                textureSet = new NeonCityTextureSet();
            }
            loadImages();
            isLoaded = true;
        }
        private void loadImages()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            //canvas.BlendMode = BlendMode.Difference;
            using (Stream stream = assembly.GetManifestResourceStream(textureSet.background))
            {
                #if IOS || ANDROID || MACCATALYST
                                                // PlatformImage isn't currently supported on Windows.
                                    backgroundImage = PlatformImage.FromStream(stream);
                #elif WINDOWS
                backgroundImage = new W2DImageLoadingService().FromStream(stream);
                #endif
            }
            for(int i=0;i<textureSet.routes.Count;i++) {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.routes[i]))
                {
#if IOS || ANDROID || MACCATALYST
                                                // PlatformImage isn't currently supported on Windows.
                    routes.Add(PlatformImage.FromStream(stream));
#elif WINDOWS
                    routes.Add(new W2DImageLoadingService().FromStream(stream));
#endif
                }

            }

            for (int i = 0; i < textureSet.player.Count; i++)
            {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.player[i]))
                {
#if IOS || ANDROID || MACCATALYST
                                                // PlatformImage isn't currently supported on Windows.
                    player.Add(PlatformImage.FromStream(stream));
#elif WINDOWS
                    player.Add(new W2DImageLoadingService().FromStream(stream));
#endif
                }

            }
            for (int i = 0; i < textureSet.ends.Count; i++)
            {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.ends[i]))
                {
#if IOS || ANDROID || MACCATALYST
                    // PlatformImage isn't currently supported on Windows.
                    ends.Add(PlatformImage.FromStream(stream));
#elif WINDOWS
                    ends.Add(new W2DImageLoadingService().FromStream(stream));
#endif
                }

            }

        }
        public IImage getPlayerTexture(int i)
        {
            if (i > textureSet.player.Count-1 || i<0)
            {
                // throw new SpriteIsNotFoundException("Texture cant be found");
                i = textureSet.player.Count - 1;
            }
            
            return player[i];
        }
        public IImage getRouteTexture(int i)
        {
            if (i > textureSet.routes.Count - 1 || i < 0)
            {
                i = textureSet.routes.Count - 1;
                // throw new SpriteIsNotFoundException("Texture cant be found");
            }
            return routes[i];
        }
        public IImage getEndsTexture(int i)
        {
            if (i > textureSet.ends.Count - 1 || i < 0)
            {
                //throw new SpriteIsNotFoundException("Texture cant be found");
                i = textureSet.ends.Count - 1;
            }
            return ends[i];
        }
        public IImage getBackgroundTexture()
        {
            return backgroundImage;
        }
    }
}
