using System.Linq;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.NUnit;
using NUnit.Framework;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace NuciText.Conversion.ArchitectureTests
{
    [TestFixture]
    public sealed class ProductionArchitectureTests
    {
        private static readonly Architecture architecture =
            new ArchLoader().LoadAssemblies(typeof(NuciTextConverter).Assembly).Build();

        private static readonly Interface publicContract =
            architecture.Interfaces.Single(@interface => @interface.Name == "INuciTextConverter");

        [Test]
        public void GivenProductionAssembly_WhenInspectingTypes_ThenConverterResidesInTheProductionNamespace()
            => Classes().That().HaveName("NuciTextConverter")
                .Should().ResideInNamespace("NuciText.Conversion")
                .Because("the converter must remain within the production namespace")
                .Check(architecture);

        [Test]
        public void GivenProductionAssembly_WhenInspectingTypes_ThenConverterIsSealed()
            => Classes().That().HaveName("NuciTextConverter")
                .Should().BeSealed()
                .Because("the converter implementation must not be extended")
                .Check(architecture);

        [Test]
        public void GivenProductionAssembly_WhenInspectingTypes_ThenConverterImplementsThePublicContract()
            => Classes().That().HaveName("NuciTextConverter")
                .Should().ImplementInterface(publicContract)
                .Because("the implementation must expose the public converter contract")
                .Check(architecture);

        [Test]
        public void GivenProductionAssembly_WhenInspectingTypes_ThenPublicContractResidesInTheProductionNamespace()
            => Interfaces().That().HaveName("INuciTextConverter")
                .Should().ResideInNamespace("NuciText.Conversion")
                .Because("the public contract must remain within the production namespace")
                .Check(architecture);
    }
}
