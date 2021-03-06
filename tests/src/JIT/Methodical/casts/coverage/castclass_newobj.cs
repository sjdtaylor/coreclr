// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace JitTest
{
    internal class BaseClass { }

    internal class TestClass : BaseClass
    {
        private static bool Test_NEWOBJ(TestClass _this, int cookie, bool flag)
        {
            TestClass inst;
            switch (cookie)
            {
                case 0:
                    try
                    {
                        inst = (TestClass)(object)(new TestClass());
                        return inst != null;
                    }
                    catch (Exception) { return false; }
                case 1:
                    try
                    {
                        inst = (TestClass)(object)(new DerivedClass());
                        return inst != null;
                    }
                    catch (Exception) { return false; }
                case 2:
                    try
                    {
                        inst = (TestClass)(object)(new BaseClass());
                        return false;
                    }
                    catch (Exception X) { return X is InvalidCastException; }
                case 3:
                    try
                    {
                        inst = (TestClass)(object)(new OtherClass());
                        return false;
                    }
                    catch (Exception X) { return X is InvalidCastException; }
                default:
                    return false;
            }
        }

        private static int Main()
        {
            TestClass _this = new TestClass();
            if (!Test_NEWOBJ(_this, 0, true))
            {
                Console.WriteLine("Failed => 101");
                return 101;
            }
            if (!Test_NEWOBJ(_this, 1, true))
            {
                Console.WriteLine("Failed => 102");
                return 102;
            }
            if (!Test_NEWOBJ(_this, 2, false))
            {
                Console.WriteLine("Failed => 103");
                return 103;
            }
            if (!Test_NEWOBJ(_this, 3, false))
            {
                Console.WriteLine("Failed => 104");
                return 104;
            }
            Console.WriteLine("Passed => 100");
            return 100;
        }
    }

    internal class DerivedClass : TestClass { }
    internal class OtherClass { }
}
