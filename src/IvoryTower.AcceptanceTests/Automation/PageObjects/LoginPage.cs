using FluentAutomation;

namespace IvoryTower.AcceptanceTests.Automation.PageObjects
{
    public class LoginPage : PageObject<LoginPage>
    {
        const string EmailInput = "input[type=email]";
        const string PasswordInput = "input[type=password]";
        const string SubmitButton = "button[type=submit]";
        const string ErrorMessage = "div.alert-danger";
        const string HomePageIdentifier = "#pageTitle";

        public LoginPage(FluentTest test)
            : base(test)
        {
            Url = TargetEnvironment.GetApplicationUrl("/");
            At = () => I.Expect.Exists(EmailInput);
        }

        public LoginPage EnterEmailAddress(string emailAddress)
        {
            I.Enter(emailAddress).In(EmailInput);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            I.Enter(password).In(PasswordInput);
            return this;
        }

        public LoginPage ClickSubmitButton()
        {
            I.Click(SubmitButton);
            return this;
        }

        public string GetErrorMessage()
        {
            return I.Find(ErrorMessage).Element.Text;
        }        

        public string GetTitle()
        {
            I.WaitUntil(() => I.Expect.Exists(HomePageIdentifier));
            ElementProxy elementProxy = I.Find(HomePageIdentifier);
            return elementProxy.Element.Value;
        }
    }
}