using Microsoft.Maui.Handlers;

namespace MauiVideoCaptureRepro;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		WebViewHandler.Mapper.AppendToMapping("LocalVideoPlayback", (handler, _) =>
		{
			handler.PlatformView.Settings.AllowFileAccess = true;
			handler.PlatformView.Settings.MediaPlaybackRequiresUserGesture = false;
		});

		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>();
		return builder.Build();
	}
}

public class App : Application
{
	protected override Window CreateWindow(IActivationState? activationState) => new(new MainPage());
}
