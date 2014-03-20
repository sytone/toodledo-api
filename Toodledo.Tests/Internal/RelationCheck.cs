using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Toodledo.Tests.Internal
{
    /// <summary>basic relation assertion class</summary>
    internal class RelationCheck
    {
        internal static readonly RelationCheck Gt = new RelationCheck("(a>b)", r => r > 0);
        internal static readonly RelationCheck Ge = new RelationCheck("(a>=b)", r => r >= 0);
        internal static readonly RelationCheck Le = new RelationCheck("(a<=b)", r => r <= 0);
        internal static readonly RelationCheck Lt = new RelationCheck("(a<b)", r => r < 0);

        private RelationCheck(string ok, Func<int, bool> checkRes)
        {
            Ok = ok;
            CheckRes = checkRes;
        }

        private string Ok { get; set; }
        private Func<int, bool> CheckRes { get; set; }

        /// <summary>return Ok string or real relation if mismatching relation</summary>
        private string Res(string a, string b, int res)
        {
            return CheckRes(res)
                ? Ok
                : string.Format("({0}{1}{2})",
                    a ?? "null",
                    res < 0 ? "<" : res > 0 ? ">" : "==",
                    b ?? "null");
        }

        internal void Check<T>(T a, T b, string message, params object[] args)
            where T : IComparable<T>
        {
            bool nullable = typeof (T).IsClass;
            bool nullA = nullable && ReferenceEquals(a, null);
            bool nullB = nullable && ReferenceEquals(b, null);
            string strA = nullA ? "null" : a.ToString();
            string strB = nullB ? "null" : b.ToString();
            int res = !nullA ? a.CompareTo(b) : !nullB ? -(b.CompareTo(a)) : 0;
            Assert.AreEqual(Ok, Res(strA, strB, res), message, args);
        }

        internal void Check<T>(T a, T b, params object[] args)
            where T : IComparable<T>
        {
            Check(a, b, "Not " + Ok, args);
        }
    }
}