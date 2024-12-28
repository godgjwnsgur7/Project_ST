using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region TestData
    [Serializable]
    public class JTestData
    {
        public int DataId;
        public string Name;
    }

    [Serializable]
    public class JTestDataLoader : ILoader<int, JTestData>
    {
        public List<JTestData> tests = new List<JTestData>();

        public Dictionary<int, JTestData> MakeDict()
        {
            Dictionary<int, JTestData> dict = new Dictionary<int, JTestData>();
            foreach (JTestData test in tests)
                dict.Add(test.DataId, test);
            return dict;
        }
    }
    #endregion
}