using Godot;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameJame.System
{
    public class LevelTracker
    {
        private readonly string _savePath = "user://";
        private readonly string _saveFile = "TrackedLevels.cfg";
        private readonly string _levelSectionHeader = "levels_completed";

        /// <summary>
        /// Gets the users data from the config file
        /// </summary>
        /// <param name="trackedLevels"></param>
        /// <returns>True, if the user has any tracked levels, otherwise false</returns>
        public bool TryLevelsCompleted(out TrackedLevels trackedLevels)
        {
            var config = new ConfigFile();
            if (config.Load(_savePath + _saveFile) != Error.Ok)
            {
                GD.Print("The player has not been to any levels");
                trackedLevels = null;
                return false;
            }

            Debug.Assert(config.HasSection(_levelSectionHeader), $"Saved user config is not correctly formated, needs section {_levelSectionHeader}");
            var levelsCompleted = new Dictionary<string, bool>();
            foreach (var scene in config.GetSectionKeys(_levelSectionHeader))
            {
                levelsCompleted[scene] = (bool)config.GetValue(_levelSectionHeader, scene);
            }
            trackedLevels = new TrackedLevels(levelsCompleted);
            return true;
        }

        /// <summary>
        /// Save the level progress of a scene to a config file
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="completed"></param>
        public void SetLevelProgress(string scene, bool completed)
        {
            var config = new ConfigFile();
            var error = config.Load(_savePath + _saveFile);
            if (error != Error.Ok)
            {
                GD.Print( "Creating a new level tracker file for player, as none exists" );
            }
            config.SetValue(_levelSectionHeader, scene, completed);
            config.Save(_savePath + _saveFile);
        }
    }

    public class TrackedLevels
    {
        public Dictionary<string, bool> LevelsCompleted { get; }

        public TrackedLevels(Dictionary<string, bool> levesCompleted)
        {
            LevelsCompleted = levesCompleted;
        }
    }
}
