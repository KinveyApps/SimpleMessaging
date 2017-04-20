using System;
using CoreGraphics;
using UIKit;

namespace SimpleMessaging
{
	public partial class ViewControllerUsers : UIViewController
	{
		internal UITextField SenderIDView;
		internal UILabel MessageView;
		nfloat h = 31.0f;
		UIColor colorDarkBlue = UIColor.FromRGB(7, 69, 126);
		UIColor colorLightBlue = UIColor.FromRGB(92, 127, 159);

		public System.Collections.Generic.List<SimpleMessagingUser> Users { get; set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Load();
		}

		private void Load()
		{
			AppDelegate myAppDel = (UIApplication.SharedApplication.Delegate as AppDelegate);

			myAppDel.SetupStream();

			RenderView();
		}

		private void RenderView()
		{
			Title = "SimpleMessaging - Users";
			View.BackgroundColor = UIColor.FromRGB(7, 69, 126);
			nfloat w = View.Bounds.Width;

			AppDelegate myAppDel = (UIApplication.SharedApplication.Delegate as AppDelegate);

			var titleLabel = new UILabel
			{
				Text = "Users to message:",
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Frame = new CGRect(10, 80, w - 20, h)
			};

			View.AddSubview(titleLabel);

			int startingHeight = 120;

			foreach (var messageUser in Users)
			{
				UIButton buttonMessageUser;
				buttonMessageUser = UIButton.FromType(UIButtonType.System);
				buttonMessageUser.Frame = new CGRect(10, startingHeight, w - 20, 44);
				buttonMessageUser.SetTitle(messageUser.Name, UIControlState.Normal);
				buttonMessageUser.SetTitleColor(UIColor.White, UIControlState.Normal);
				buttonMessageUser.BackgroundColor = colorLightBlue;

				buttonMessageUser.TouchUpInside += (sender, e) =>
				{
					myAppDel.MessageUser(messageUser);
				};

				View.AddSubview(buttonMessageUser);

				startingHeight += 50;
			}

			UIButton buttonLogout;
			buttonLogout = UIButton.FromType(UIButtonType.System);
			buttonLogout.Frame = new CGRect(10, startingHeight + 50, w - 20, 44);
			buttonLogout.SetTitle("Logout", UIControlState.Normal);
			buttonLogout.SetTitleColor(UIColor.Black, UIControlState.Normal);
			buttonLogout.BackgroundColor = UIColor.Gray;

			var user = new UIViewController();
			user.View.BackgroundColor = colorDarkBlue;

			buttonLogout.TouchUpInside += async (sender, e) =>
			{
				await myAppDel.Logout();
			};

			View.AddSubview(buttonLogout);
		}
	}
}
