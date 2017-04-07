﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace ScriptsTest
{
    /// <summary>
    /// Provides enumeration of php test files to be used by xUnit.
    /// </summary>
    sealed class ScriptsListDataAttribute : DataAttribute
    {
        static string GetRootDirectory()
        {
            var d = Path.GetDirectoryName(typeof(ScriptsListDataAttribute).GetTypeInfo().Assembly.Location);
            while (!File.Exists(Path.Combine(d, "Peachpie.sln")))
            {
                d = Path.GetDirectoryName(d);
            }

            return d;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var testspath = Path.Combine(GetRootDirectory(), "tests");
            Assert.True(Directory.Exists(testspath), $"Tests directory '{testspath}' cannot be found.");

            var files = Directory.GetFiles(testspath, "*.php", SearchOption.AllDirectories);
            Assert.NotEmpty(files);

            return files.Select(f => new object[] { Path.GetDirectoryName(f), Path.GetFileName(f) });
        }
    }
}
