using Serilog;

namespace OrangeHRM.Automation.Logging;

public static class Log
{
	public static ILogger Logger { get; } =
		new LoggerConfiguration()
			.MinimumLevel.Information()
			.WriteTo.Console()
			.WriteTo.File("TestResults/log.txt", rollingInterval: RollingInterval.Day)
			.CreateLogger();
}
