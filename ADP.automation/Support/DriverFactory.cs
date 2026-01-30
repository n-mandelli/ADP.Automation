using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace OrangeHRM.Automation.Support;

public static class DriverFactory
{
	public static IWebDriver CreateChrome()
	{
		new DriverManager().SetUpDriver(new ChromeConfig());

		var options = new ChromeOptions();
		options.AddArgument("--start-maximized");
		// options.AddArgument("--headless=new"); // se quiser headless

		return new ChromeDriver(options);
	}
}
