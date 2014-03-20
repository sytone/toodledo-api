using System;

namespace Toodledo.Tests.Internal
{
    /// <summary>Assertion extension class</summary>
    public abstract class ExtAssert
    {
        public static void Greater<T>(T a, T b, string message, params object[] args)
            where T : IComparable<T>
        {
            RelationCheck.Gt.Check(a, b, message, args);
        }

        public static void Greater<T>(T a, T b, params object[] args)
            where T : IComparable<T>
        {
            RelationCheck.Gt.Check(a, b, args);
        }

        public static void GreaterOrEqual<T>(T a, T b, string message, params object[] args)
            where T : IComparable<T>
        {
            RelationCheck.Ge.Check(a, b, message, args);
        }

        public static void LessOrEqual<T>(T a, T b, string message, params object[] args)
            where T : IComparable<T>
        {
            RelationCheck.Le.Check(a, b, message, args);
        }

        public static void Less<T>(T a, T b, string message, params object[] args)
            where T : IComparable<T>
        {
            RelationCheck.Lt.Check(a, b, message, args);
        }

        public static void NullOrEmpty(string s, string message, params object[] args)
        {
            StringCheck.NullOrEmpty.Check(s, message, args);
        }
    }
}