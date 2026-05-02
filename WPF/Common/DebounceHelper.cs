namespace HoanMyClinic.Common;
public class DebounceDispatcher
{
	private System.Timers.Timer? _timer;

	public void Debounce(int milliseconds, Action action)
	{
		_timer?.Stop();

		_timer = new System.Timers.Timer(milliseconds)
		{
			AutoReset = false
		};

		_timer.Elapsed += (_, __) => action();
		_timer.Start();
	}
}