// <copyright file="CloudVariables.cs" company="Jan Ivar Z. Carlsen, Sindri Jóelsson">
// Copyright (c) 2016 Jan Ivar Z. Carlsen, Sindri Jóelsson. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CloudOnce
{
    using CloudPrefs;

    /// <summary>
    /// Provides access to cloud variables registered via the CloudOnce Editor.
    /// This file was automatically generated by CloudOnce. Do not edit.
    /// </summary>
    public static class CloudVariables
    {
        private static readonly CloudInt s_lastLevel = new CloudInt("LastLevel", PersistenceType.Highest, 0);

        public static int LastLevel
        {
            get { return s_lastLevel.Value; }
            set { s_lastLevel.Value = value; }
        }

        private static readonly CloudString s_json = new CloudString("Json", PersistenceType.Latest, "");

        public static string Json
        {
            get { return s_json.Value; }
            set { s_json.Value = value; }
        }

        private static readonly CloudInt s_levelStars = new CloudInt("LevelStars", PersistenceType.Highest, -1);

        public static int LevelStars
        {
            get { return s_levelStars.Value; }
            set { s_levelStars.Value = value; }
        }

        private static readonly CloudInt s_levelID = new CloudInt("LevelID", PersistenceType.Highest, -1);

        public static int LevelID
        {
            get { return s_levelID.Value; }
            set { s_levelID.Value = value; }
        }

        private static readonly CloudString s_newJson = new CloudString("NewJson", PersistenceType.Latest, "");

        public static string NewJson
        {
            get { return s_newJson.Value; }
            set { s_newJson.Value = value; }
        }
    }
}