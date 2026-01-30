using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Automation.Logging;
using OrangeHRM.Automation.Support;
using TechTalk.SpecFlow;

namespace OrangeHRM.Automation.StepDefinitions;

[Binding]
public sealed class Hooks
{
	public static IWebDriver Driver { get; private set; } = null!;
	public static WebDriverWait Wait { get; private set; } = null!;

	private readonly ScenarioContext _scenarioContext;

	public Hooks(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

	[BeforeScenario]
	public void BeforeScenario()
	{
		Driver = DriverFactory.CreateChrome();
		Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));

		Log.Logger.Information("START Scenario: {Scenario}", _scenarioContext.ScenarioInfo.Title);
	}

	[AfterScenario]
	public void AfterScenario()
	{
		try
		{
			if (_scenarioContext.TestError != null)
			{
				Log.Logger.Error(_scenarioContext.TestError, "Scenario FAILED: {Scenario}", _scenarioContext.ScenarioInfo.Title);

				try
				{
					Directory.CreateDirectory("TestResults/screenshots");
					var path = Path.Combine("TestResults/screenshots",
						$"{Sanitize(_scenarioContext.ScenarioInfo.Title)}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

					var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
					screenshot.SaveAsFile(path);
					Log.Logger.Information("Saved screenshot: {Path}", path);
				}
				catch (Exception ex)
				{
					Log.Logger.Error(ex, "Failed to capture screenshot");
				}
			}
			else
			{
				Log.Logger.Information("Scenario PASSED: {Scenario}", _scenarioContext.ScenarioInfo.Title);
			}
		}
		finally
		{
			try { Driver?.Quit(); } catch { /* ignore */ }
		}
	}

	private static string Sanitize(string s)
		=> string.Concat(s.Select(ch => char.IsLetterOrDigit(ch) ? ch : '_'));
}