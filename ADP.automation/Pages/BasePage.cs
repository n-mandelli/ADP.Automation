using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OrangeHRM.Automation.Pages;

public abstract class BasePage
{
	protected IWebDriver Driver { get; }
	protected WebDriverWait Wait { get; }

	protected BasePage(IWebDriver driver, WebDriverWait wait)
	{
		Driver = driver;
		Wait = wait;
	}
}
