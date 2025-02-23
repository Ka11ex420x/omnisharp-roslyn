﻿using OmniSharp.Services;
using TestUtility;
using Xunit;
using Xunit.Abstractions;

namespace OmniSharp.Tests
{
    public class DotNetCliServiceFacts : AbstractTestFixture
    {
        private const string DotNetVersion = "7.0.100-rc.1.22431.12";
        private int Major { get; }
        private int Minor { get; }
        private int Patch { get; }
        private string Release { get; }

        public DotNetCliServiceFacts(ITestOutputHelper output)
            : base(output)
        {
            var version = SemanticVersion.Parse(DotNetVersion);
            Major = version.Major;
            Minor = version.Minor;
            Patch = version.Patch;
            Release = version.PreReleaseLabel;
        }

        [Fact]
        public void GetVersion()
        {
            using (var host = CreateOmniSharpHost(dotNetCliVersion: DotNetCliVersion.Current))
            {
                var dotNetCli = host.GetExport<IDotNetCliService>();

                var cliVersion = dotNetCli.GetVersion();

                Assert.False(cliVersion.HasError);

                var version = cliVersion.Version;

                Assert.Equal(Major, version.Major);
                Assert.Equal(Minor, version.Minor);
                Assert.Equal(Patch, version.Patch);
                Assert.Equal(Release, version.PreReleaseLabel);
            }
        }

        [Fact]
        public void GetInfo()
        {
            using (var host = CreateOmniSharpHost(dotNetCliVersion: DotNetCliVersion.Current))
            {
                var dotNetCli = host.GetExport<IDotNetCliService>();

                var info = dotNetCli.GetInfo();

                Assert.Equal(Major, info.Version.Major);
                Assert.Equal(Minor, info.Version.Minor);
                Assert.Equal(Patch, info.Version.Patch);
                Assert.Equal(Release, info.Version.PreReleaseLabel);
            }
        }
    }
}
