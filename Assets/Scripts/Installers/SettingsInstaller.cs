using Diwide.Arkanoid;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    public GameManager.Settings GameSettings;
    public PlayerInstaller.Settings PlayerSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings);
        Container.BindInstance(PlayerSettings);
    }
}