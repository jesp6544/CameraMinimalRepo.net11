# MediaPicker.CaptureVideoAsync regression on Android (.NET MAUI 11)

One page ([MainPage.cs](MainPage.cs)). **Capture video** calls
`MediaPicker.Default.CaptureVideoAsync()` and plays the returned file in a
`WebView` `<video>` element. The page shows **PASS** if the video plays to the
end, or **FAIL** with the reason.

## Steps

1. Start an Android 14 (API 34) emulator.
2. `dotnet build -c Debug -t:Run`
3. Tap **Capture video**, record a few seconds, stop, and accept.

## Results

| Device | MAUI 10.0.130 | MAUI 11.0.10 |
|---|---|---|
| Android 14 emulator (API 34, `google_apis` x86_64) | PASS | Camera app crashes on stop, `CaptureVideoAsync` returns `null` |
| Nokia G22, Android 14 | PASS | Returned video doesn't play (WebView decode error, Google Photos can't play it) |

