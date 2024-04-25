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
        public TextureProvider() { player = new List<IImage>();paths = new List<IImage>(); finish = new List<IImage>(); }
        TextureSet textureSet;
        IImage backgroundImage;
        List<IImage> player;
        List<IImage> paths;
        List<IImage> finish;
        public bool isLoaded { get; set; }

        public bool isBackgroundTexture()
        {
            return !textureSet.backgroudIsImage;
        }
        public void loadByName(string name)
        {
           
            textureSet = new TextureSet(name);
            
            loadImages();
            isLoaded = true;
        }
        private void loadImages()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            
            using (Stream stream = assembly.GetManifestResourceStream(textureSet.background))
            {
                #if IOS || ANDROID || MACCATALYST                         
                    backgroundImage = PlatformImage.FromStream(stream);
                #elif WINDOWS
                    backgroundImage = new W2DImageLoadingService().FromStream(stream);
                #endif
            }
            for(int i=0;i<textureSet.paths.Count;i++) {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.paths[i]))
                {
                    #if IOS || ANDROID || MACCATALYST
                        paths.Add(PlatformImage.FromStream(stream));
                    #elif WINDOWS
                        paths.Add(new W2DImageLoadingService().FromStream(stream));
                    #endif
                }

            }

            for (int i = 0; i < textureSet.player.Count; i++)
            {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.player[i]))
                {
                    #if IOS || ANDROID || MACCATALYST
                        player.Add(PlatformImage.FromStream(stream));
                    #elif WINDOWS
                        player.Add(new W2DImageLoadingService().FromStream(stream));
                    #endif
                }
            }
            for (int i = 0; i < textureSet.finish.Count; i++)
            {
                using (Stream stream = assembly.GetManifestResourceStream(textureSet.finish[i]))
                {
                    #if IOS || ANDROID || MACCATALYST
                        finish.Add(PlatformImage.FromStream(stream));
                    #elif WINDOWS
                        finish.Add(new W2DImageLoadingService().FromStream(stream));
                    #endif
                }

            }

        }
        public IImage getPlayerTexture(int i)
        {
            if (i > textureSet.player.Count-1 || i<0)
            {
                i = textureSet.player.Count - 1;
            }
            return player[i];
        }
        public IImage getRouteTexture(int i)
        {
            if (i > textureSet.paths.Count - 1 || i < 0)
            {
                i = textureSet.paths.Count - 1;
            }
            return paths[i];
        }
        public bool isRouteOfItems()
        {
            return textureSet.pathIsIteam;
        }
        public IImage getEndsTexture(int i)
        {
            if (i > textureSet.finish.Count - 1 || i < 0)
            {
                i = textureSet.finish.Count - 1;
            }
            return finish[i];
        }
        public IImage getBackgroundTexture()
        {
            return backgroundImage;
        }
    }
}
