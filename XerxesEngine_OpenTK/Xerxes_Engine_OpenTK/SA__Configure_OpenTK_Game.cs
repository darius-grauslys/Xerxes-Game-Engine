using System;
using System.IO;

namespace Xerxes_Engine.Export_OpenTK
{
    public class SA__Configure_OpenTK_Game :
        SA__Configure_Root
    {
        public const string Game_Arguments__DEFAULT_ASSET_DIRECTORY_NAME = "Assets";
        public const string Game_Arguments__DEFAULT_SHADER_DIRECTORY_NAME = "Shaders";
        
        public const string Game_Arguments__DEFAULT_WINDOW_TITLE = "My XerxesEngine Game";

        public const uint Game_Arguments__DEFAULT_WINDOW_WIDTH = 800;
        public const uint Game_Arguments__DEFAULT_WINDOW_HEIGHT = 600;

        public static readonly string Game_Arguments__DEFAULT_ASSET_DIRECTORY =
            Path.Combine
            (
                AppDomain.CurrentDomain.BaseDirectory,
                Game_Arguments__DEFAULT_ASSET_DIRECTORY_NAME
            );
        public static readonly string Game_Arguments__DEFAULT_SHADER_DIRECTORY =
            Path.Combine
            (
             Game_Arguments__DEFAULT_ASSET_DIRECTORY,
                Game_Arguments__DEFAULT_SHADER_DIRECTORY_NAME
            );

        // Game Window Arguments

        public string Game_Arguments__Asset_Directory       { get; private set; }
        public string Game_Arguments__Shader_Directory     { get; private set; }

        public string Game_Arguments__Window_Title          { get; private set; }

        public uint Game_Arguments__Window_Width             { get; private set; }
        public uint Game_Arguments__Window_Height            { get; private set; }

        public SA__Configure_OpenTK_Game
        (
            string assetDirectory = null,
            string shaderDirectory = null,
            string windowTitle = null,
            uint? windowWidth = null,
            uint? windowHeight = null,
            params string[] arguments
        )
        : base(arguments)
        {
            Set__Window_Arguments__Game_Arguments
            (
                assetDirectory,
                shaderDirectory,
                windowTitle,
                windowWidth,
                windowHeight
            );
        }

        public SA__Configure_OpenTK_Game Set__Window_Arguments__Game_Arguments
        (
            string assetDirectory = null,
            string shaderDirectory = null,
            string windowTitle = null,
            uint? windowWidth = null,
            uint? windowHeight = null
        )
        {
            Game_Arguments__Asset_Directory = 
                assetDirectory 
                ?? Game_Arguments__Asset_Directory 
                ?? Game_Arguments__DEFAULT_ASSET_DIRECTORY;
            Game_Arguments__Shader_Directory = 
                shaderDirectory
                ?? Game_Arguments__Shader_Directory
                ?? Game_Arguments__DEFAULT_SHADER_DIRECTORY;
            Game_Arguments__Window_Width = 
                windowWidth 
                ?? (
                        Game_Arguments__Window_Width > 0 
                        ? Game_Arguments__Window_Width 
                        : Game_Arguments__DEFAULT_WINDOW_WIDTH
                   );
            Game_Arguments__Window_Height =
                windowHeight
                ?? (
                        Game_Arguments__Window_Height > 0
                        ? Game_Arguments__Window_Height
                        : Game_Arguments__DEFAULT_WINDOW_HEIGHT
                   );

            return this;
        }

        public static SA__Configure_OpenTK_Game Create()
            => new SA__Configure_OpenTK_Game();
    }
}
