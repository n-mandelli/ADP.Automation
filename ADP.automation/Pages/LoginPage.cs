using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Automation.Logging;
using OrangeHRM.Automation.Support;

namespace OrangeHRM.Automation.Pages;

public class LoginPage : BasePage
{
	private const string Url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";

	private readonly By Username = By.Name("username");
	private readonly By Password = By.Name("password");
	private readonly By LoginBtn = By.CssSelector("button[type='submit']");

	// bloco que normalmente contém as credenciais (pode variar)
	private readonly By CredentialsBlock = By.CssSelector(".orangehrm-demo-credentials, .orangehrm-login-footer-sm");

	public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

	public void Open()
	{
		Driver.Navigate().GoToUrl(Url);
	}

	public (string user, string pass) ReadCredentialsFromPage()
	{
		string pageText;
		try
		{
			pageText = Driver.FindElement(CredentialsBlock).Text;
		}
		catch
		{
			pageText = Driver.FindElement(By.TagName("body")).Text;
		}

		string user = Extract(pageText, @"(?i)username\s*:\s*([A-Za-z0-9]+)") ?? "Admin";
		string pass = Extract(pageText, @"(?i)password\s*:\s*([A-Za-z0-9]+)") ?? "admin123";

		Log.Logger.Information("Using credentials from page/fallback. Username={User}", user);
		return (user, pass);
	}

	public void Login(string user, string pass)
	{
		Waits.Visible(Wait, Username).Clear();
		Driver.FindElement(Username).SendKeys(user);

		Waits.Visible(Wait, Password).Clear();
		Driver.FindElement(Password).SendKeys(pass);

		Waits.Clickable(Wait, LoginBtn).Click();
	}

	private static string? Extract(string text, string pattern)
	{
		var m = Regex.Match(text, pattern);
		return m.Success ? m.Groups[1].Value.Trim() : null;
	}
}