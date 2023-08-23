#if UNITY_STANDALONE
    #define ENABLE_FILE_LOG
#endif
using Zenject;

namespace Diwide.Arkanoid
{
    public class WriteLogToFileInstaller : MonoInstaller<WriteLogToFileInstaller>
    {
        public override void InstallBindings()
        {
            #if ENABLE_FILE_LOG
                Container.Bind<WriteLogToFile>().FromNewComponentOnRoot().AsSingle().NonLazy();
            #endif
        }
    }
}