# OrangeHRM Automation (C# + SpecFlow + NUnit + Selenium)

## How to run
### Prerequisites
- .NET 8 SDK
- Google Chrome installed

### Run in Visual Studio
Open solution -> Test Explorer -> Run

### Run via CLI
dotnet test

## Notes
- Login reads the username/password shown on the login page (fallback Admin/admin123).
- Uses Page Objects and explicit waits.
- Logs: TestResults/log.txt
- Screenshots on failure: TestResults/screenshots/
