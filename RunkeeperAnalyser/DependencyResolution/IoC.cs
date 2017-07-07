using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Infrastructure;
using StructureMap;
namespace RunkeeperAnalyser {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<IRunkeeperDataSource>().HttpContextScoped().Use<RunkeeperDb>();
                        });
            return ObjectFactory.Container;
        }
    }
}