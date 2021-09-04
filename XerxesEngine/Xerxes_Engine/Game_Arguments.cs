using System;
using System.IO;

namespace Xerxes_Engine
{
    public class Game_Arguments
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

        public static Game_Arguments Get__Default__Game_Arguments() 
            => new Game_Arguments()
            {
                Game_Arguments__ASSET_DIRECTORY = Game_Arguments__DEFAULT_ASSET_DIRECTORY,
                Game_Arguments__SHADER_DIRECTORY = Game_Arguments__DEFAULT_SHADER_DIRECTORY,

                Game_Arguments__WINDOW_TITLE = Game_Arguments__DEFAULT_WINDOW_TITLE,

                Game_Arguments__WINDOW_WIDTH = Game_Arguments__DEFAULT_WINDOW_WIDTH,
                Game_Arguments__WINDOW_HEIGHT = Game_Arguments__DEFAULT_WINDOW_HEIGHT,

                Game_Arguments__LOG_VERBOSITY = Log_Verbosity.Normal,
                Game_Arguments__LOG__THROW_ON_WARNINGS = false,
                Game_Arguments__LOG__THROW_ON_ERRORS = true,
                Game_Arguments__LOG__OUT = Console.Out
            };

        // Game Window Arguments

        public string Game_Arguments__ASSET_DIRECTORY       { get; private set; }
        public string Game_Arguments__SHADER_DIRECTORY     { get; private set; }

        public string Game_Arguments__WINDOW_TITLE          { get; private set; }

        public uint Game_Arguments__WINDOW_WIDTH             { get; private set; }
        public uint Game_Arguments__WINDOW_HEIGHT            { get; private set; }

        // Logger Arguments

        public Log_Verbosity Game_Arguments__LOG_VERBOSITY  { get; private set; }
        public bool Game_Arguments__LOG__THROW_ON_WARNINGS  { get; private set; }
        public bool Game_Arguments__LOG__THROW_ON_ERRORS    { get; private set; }
        public TextWriter Game_Arguments__LOG__OUT          { get; private set; }

        public Game_Arguments Set__Window_Arguments__Game_Arguments
        (
            string assetDirectory = null,
            string shaderDirectory = null,
            string windowTitle = null,
            uint? windowWidth = null,
            uint? windowHeight = null
        )
        {
            Game_Arguments__ASSET_DIRECTORY = 
                assetDirectory 
                ?? Game_Arguments__ASSET_DIRECTORY 
                ?? Game_Arguments__DEFAULT_ASSET_DIRECTORY;
            Game_Arguments__SHADER_DIRECTORY = 
                shaderDirectory
                ?? Game_Arguments__SHADER_DIRECTORY
                ?? Game_Arguments__DEFAULT_SHADER_DIRECTORY;
            Game_Arguments__WINDOW_WIDTH = 
                windowWidth 
                ?? (
                        Game_Arguments__WINDOW_WIDTH > 0 
                        ? Game_Arguments__WINDOW_WIDTH 
                        : Game_Arguments__DEFAULT_WINDOW_WIDTH
                   );
            Game_Arguments__WINDOW_HEIGHT =
                windowHeight
                ?? (
                        Game_Arguments__WINDOW_HEIGHT > 0
                        ? Game_Arguments__WINDOW_HEIGHT
                        : Game_Arguments__DEFAULT_WINDOW_HEIGHT
                   );

            return this;
        }

        public Game_Arguments Set__Logger_Arguments__Game_Arguments
        (
            Log_Verbosity? logVerbosity = null,
            bool? throwOnErrors = null,
            bool? throwOnWarnings = null,
            TextWriter @out = null
        )
        {
            Game_Arguments__LOG_VERBOSITY = 
                logVerbosity
                ?? Game_Arguments__LOG_VERBOSITY;
            Game_Arguments__LOG__THROW_ON_ERRORS = 
                throwOnErrors
                ?? Game_Arguments__LOG__THROW_ON_ERRORS;
            Game_Arguments__LOG__THROW_ON_WARNINGS = 
                throwOnWarnings
                ?? Game_Arguments__LOG__THROW_ON_WARNINGS;
            Game_Arguments__LOG__OUT = 
                @out 
                ?? Game_Arguments__LOG__OUT;

            return this;
        }

        public static Game_Arguments Create()
            => new Game_Arguments();
    }
}
