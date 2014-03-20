using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toodledo.Tests.Internal
{
    /// <summary>basic string assertion class</summary>
    internal class StringCheck
    {
        private string Ok { get; set; }
        private StringCheck()
        {
            Ok = string.Empty;
        }
        /// <summary>return Ok string or actual string which is neiter null nor empty</summary>
        private string Res(string s)
        {
            return string.IsNullOrEmpty(s) ? Ok : s.Substring(0, Math.Min(s.Length, 20));
        }

        internal void Check(string s, string message, params object[] args)
        {
            Assert.AreEqual(Ok, Res(s), message, args);
        }
        internal static readonly StringCheck NullOrEmpty = new StringCheck();
    }
}
