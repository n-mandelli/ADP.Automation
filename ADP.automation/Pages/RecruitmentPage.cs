using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Automation.Logging;
using OrangeHRM.Automation.Support;

namespace OrangeHRM.Automation.Pages;

public class RecruitmentPage : BasePage
{
	// Left nav
	private readonly By RecruitmentMenu = By.XPath("//span[normalize-space()='Recruitment']");

	// List page
	private readonly By AddBtn = By.XPath("//button[normalize-space()='Add']");

	// Add/Edit form
	private readonly By FirstName = By.Name("firstName");
	private readonly By MiddleName = By.Name("middleName");
	private readonly By LastName = By.Name("lastName");

	private readonly By Email =
		By.XPath("//label[normalize-space()='Email']/../following-sibling::div//input");

	// Vacancy
	private readonly By VacancyDropdown =
		By.XPath("//label[contains(.,'Vacancy')]/../following-sibling::div//div[contains(@class,'oxd-select-text')]");

	private readonly By VacancyFirstOption =
		By.CssSelector(".oxd-select-dropdown .oxd-select-option");

	private readonly By SaveBtn = By.XPath("//button[normalize-space()='Save']");

	// Search
	private readonly By CandidateNameSearch =
		By.XPath("//label[contains(.,'Candidate Name')]/../following-sibling::div//input");

	private readonly By SearchBtn = By.XPath("//button[normalize-space()='Search']");

	// First row action (open/edit)
	private readonly By FirstRowActionBtn =
		By.XPath("(//div[contains(@class,'oxd-table-body')]//button[contains(@class,'oxd-icon-button')])[1]");

	// Success toast
	private readonly By SuccessToast =
		By.CssSelector(".oxd-toast-container .oxd-toast--success");

	public RecruitmentPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

	public void GoTo()
	{
		Waits.Clickable(Wait, RecruitmentMenu).Click();
	}

	public void ClickAdd()
	{
		Waits.Clickable(Wait, AddBtn).Click();
	}

	public void FillRequiredFields(string first, string middle, string last, string email)
	{
		Waits.Visible(Wait, FirstName).SendKeys(first);
		Driver.FindElement(MiddleName).SendKeys(middle);
		Driver.FindElement(LastName).SendKeys(last);

		var emailEl = Waits.Visible(Wait, Email);
		emailEl.Clear();
		emailEl.SendKeys(email);

		TrySelectVacancyIfPresent();
	}

	public void Save()
	{
		Waits.Clickable(Wait, SaveBtn).Click();
	}

	public bool IsSuccessToastVisible()
	{
		try
		{
			return Waits.Visible(Wait, SuccessToast).Displayed;
		}
		catch
		{
			return false;
		}
	}

	public void SearchCandidate(string fullName)
	{
		var input = Waits.Visible(Wait, CandidateNameSearch);
		input.Clear();
		input.SendKeys(fullName);

		Waits.Clickable(Wait, SearchBtn).Click();
	}

	public void OpenFirstResult()
	{
		Waits.Clickable(Wait, FirstRowActionBtn).Click();
	}

	public void EditEmail(string newEmail)
	{
		var emailEl = Waits.Visible(Wait, Email);
		emailEl.Clear();
		emailEl.SendKeys(newEmail);
	}

	private void TrySelectVacancyIfPresent()
	{
		try
		{
			var dropdowns = Driver.FindElements(VacancyDropdown);
			if (dropdowns.Count == 0) return;

			dropdowns[0].Click();

			var option = Waits.Clickable(Wait, VacancyFirstOption);
			option.Click();

			Log.Logger.Information("Vacancy selected (first option).");
		}
		catch (Exception ex)
		{
			// Não falha o teste aqui porque pode não ser obrigatório / DOM pode variar
			Log.Logger.Warning("Could not select Vacancy (might not be required or locator changed). {Msg}", ex.Message);
		}
	}
}
