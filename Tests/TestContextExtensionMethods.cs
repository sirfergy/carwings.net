using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    static class TestContextExtensionMethods
    {
        public static T GetTestRunsetting<T>(this TestContext testContext, string key)
        {
            return (T)testContext.Properties[key];
        }
    }
}
