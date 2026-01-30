using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Automation.Support;

namespace OrangeHRM.Automation.Pages;

public class DashboardPage : BasePage
{
	private readonly By DashboardHeader = By.XPath("//h6[normalize-space()='Dashboard']");

	public DashboardPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

	public bool IsAt()
		=> Waits.Visible(Wait, DashboardHeader).Displayed;
}
