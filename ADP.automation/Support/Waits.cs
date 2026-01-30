using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OrangeHRM.Automation.Support;

public static class Waits
{
	public static IWebElement Visible(WebDriverWait wait, By locator)
		=> wait.Until(ExpectedConditions.ElementIsVisible(locator));

	public static IWebElement Clickable(WebDriverWait wait, By locator)
		=> wait.Until(ExpectedConditions.ElementToBeClickable(locator));

	public static bool TextPresent(WebDriverWait wait, By locator, string containsText)
		=> wait.Until(d =>
		{
			try
			{
				var el = d.FindElement(locator);
				return el.Text.Contains(containsText, StringComparison.OrdinalIgnoreCase);
			}
			catch { return false; }
		});
}
