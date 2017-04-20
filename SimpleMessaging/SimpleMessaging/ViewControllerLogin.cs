using System;

using CoreGraphics;
using UIKit;

namespace SimpleMessaging
{
	public partial class LoginViewController : UIViewController
	{
		public UIButton buttonLogin;
		public UIButton buttonLoginAuto;
		public UIButton buttonLoginAlice;
		public UIButton buttonLoginBob;
		public UIButton buttonLoginCharlie;
		public UIButton buttonLoginDan;
		UITextField usernameField;
		UITextField passwordField;
		UIColor colorBackgroundButtonLogin = UIColor.FromRGB(92, 127, 159);

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			View.BackgroundColor = UIColor.FromRGB(7, 69, 126);
			Title = "SimpleMessaging - Login";

			nfloat h = 31.0f;
			nfloat w = View.Bounds.Width;

			usernameField = new UITextField
			{
				Placeholder = "Username",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new CGRect(10, 82, w - 20, h)
			};
			View.AddSubview(usernameField);

			passwordField = new UITextField
			{
				SecureTextEntry = true,
				Placeholder = "Password",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new CGRect(10, 122, w - 20, h)
			};
			View.AddSubview(passwordField);


			var buttonWidth = (w / 2) - 20;
			buttonLogin = UIButton.FromType(UIButtonType.System);
			buttonLogin.Frame = new CGRect(10, 162, w - 20, 44);
			buttonLogin.SetTitle("Login", UIControlState.Normal);
			buttonLogin.SetTitleColor(UIColor.White, UIControlState.Normal);
			buttonLogin.BackgroundColor = colorBackgroundButtonLogin;

			buttonLogin.TouchUpInside += async (sender, e) =>
			{
				AppDelegate myAppDel = (UIApplication.SharedApplication.Delegate as AppDelegate);
				await myAppDel.Login(usernameField.Text, passwordField.Text);
			};

			View.AddSubview(buttonLogin);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
