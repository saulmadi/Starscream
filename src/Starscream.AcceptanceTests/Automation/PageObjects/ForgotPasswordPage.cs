using FluentAutomation;

namespace Starscream.AcceptanceTests.Automation.PageObjects
{
    public class ForgotPasswordPage : PageObject<ForgotPasswordPage>
    {
        const string EmailInput = "input[type=email]";
        const string SubmitButton = "button[type=submit]";
        const string InfoMessage = "div.alert-info";
        
        public ForgotPasswordPage(FluentTest test) : base(test)
        {
            Url = TargetEnvironment.GetApplicationUrl("/#/forgot-password");
            At = () => I.Expect.Exists(EmailInput);            
        }
        
        public ForgotPasswordPage EnterEmailAddress(string emailAddress)
        {
            I.WaitUntil(() => I.Assert.Exists(EmailInput));
            I.Enter(emailAddress).In(EmailInput);
            return this;
        }

        public ForgotPasswordPage ClickSubmitButton()
        {
            I.Click(SubmitButton);
            I.WaitUntil(() => I.Find(InfoMessage).Element.Text != "");            
            return this;
        }

        public string GetInfoMessage()
        {
            return I.Find(InfoMessage).Element.Text;
        }        
    }
}