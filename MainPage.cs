using System.Reflection;
using System.Runtime.InteropServices;

namespace MauiVideoCaptureRepro;

public class MainPage : ContentPage
{
	private static readonly string? MauiVersion = typeof(MediaPicker).Assembly
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0];

	private readonly WebView _player = new() { HeightRequest = 600 };

	public MainPage()
	{
		var captureButton = new Button { Text = "Capture video" };
		captureButton.Clicked += OnCaptureClicked;

		Content = new VerticalStackLayout { Padding = 24, Spacing = 16, Children = { captureButton, _player } };
	}

	private async void OnCaptureClicked(object? sender, EventArgs e)
	{
		var video = await MediaPicker.Default.CaptureVideoAsync();
		if (video is null)
		{
			_player.Source = new HtmlWebViewSource { Html = $"<p>{RuntimeInformation.FrameworkDescription}, MAUI {MauiVersion}</p><p><b>FAIL: CaptureVideoAsync returned null</b></p>" };
			return;
		}

		_player.Source = new HtmlWebViewSource
		{
			BaseUrl = "file://" + Path.GetDirectoryName(video.FullPath) + "/",
			Html = $$"""
				<p>{{RuntimeInformation.FrameworkDescription}}, MAUI {{MauiVersion}}, CaptureVideoAsync returned {{video.FileName}}</p>
				<p><b id="status">Playing...</b></p>
				<video src="{{video.FileName}}" autoplay muted playsinline style="width:100%"></video>
				<script>
				  const video = document.querySelector("video");
				  const status = document.getElementById("status");
				  video.onended = () => status.textContent = "PASS: played to the end";
				  video.onerror = () => status.textContent = "FAIL: " + video.error.message;
				</script>
				"""
		};
	}
}
