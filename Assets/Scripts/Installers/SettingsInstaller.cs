using Diwide.Arkanoid;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    public PlayerInstaller.Settings PlayerSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(PlayerSettings);
    }
}