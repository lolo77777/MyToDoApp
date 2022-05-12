namespace MyToDo.MVVM.Common.Drivers;

public class AkavacheSuspensionDriver<TAppState> : ISuspensionDriver where TAppState : class
{
    private const string AppStateKey = "appState";

    public AkavacheSuspensionDriver() => BlobCache.ApplicationName = "MyToDo";

    // 默认提供了四个常用持久化的方式：
    // LocalMachine：默认持久化的文件保存在%LOCALAPPDATA%\ApplicationName\BlobCache文件夹下
    // UserAccount：默认持久化的文件保存在%APPDATA%\ApplicationName\BlobCache文件夹下
    // Secure：默认持久化的文件保存在%APPDATA%\ApplicationName\SecretCache文件夹下，数据加密
    // InMemory：默认保存在内存中，数据加密
    public IObservable<Unit> InvalidateState() => BlobCache.UserAccount.InvalidateObject<TAppState>(AppStateKey);

    public IObservable<object> LoadState() => BlobCache.UserAccount.GetObject<TAppState>(AppStateKey);

    public IObservable<Unit> SaveState(object state) => BlobCache.UserAccount.InsertObject(AppStateKey, (TAppState)state);
}