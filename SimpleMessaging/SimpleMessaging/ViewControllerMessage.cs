using System;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace SimpleMessaging
{
	public partial class ViewControllerMessage : UIViewController
	{
		UITextField SendMessageView;
		internal UITextField SenderIDView;
		internal UILabel ListenMessageView;
		nfloat h = 31.0f;
		UIColor colorLightBlue = UIColor.FromRGB(92, 127, 159);

		public SimpleMessagingUser MessageUser { get; set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Load();
		}

		public async Task Load()
		{
			AppDelegate myAppDel = (UIApplication.SharedApplication.Delegate as AppDelegate);

			await myAppDel.Subscribe();

			RenderView();
		}

		public void ChangeText(string sender, string msg)
		{
			ListenMessageView.Text = $"From {MessageUser.Name}: {msg}";
		}

		private void RenderView()
		{
			Title = "SimpleMessaging - Directed Communication";
			View.BackgroundColor = UIColor.FromRGB(7, 69, 126);
			nfloat w = View.Bounds.Width;

			AppDelegate myAppDel = (UIApplication.SharedApplication.Delegate as AppDelegate);

			var titleLabel = new UILabel
			{
				Text = $"Messaging with {MessageUser.Name}",
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				Frame = new CGRect(10, 80, w - 20, h)
			};

			View.AddSubview(titleLabel);

			ListenMessageView = new UILabel
			{
				Frame = new CGRect(10, 120, w - 20, 3 * h),
				Text = "---",
				TextColor = UIColor.White,
				Font = UIFont.FromName("Helvetica-Bold", 20f),
				TextAlignment = UITextAlignment.Left
			};

			View.AddSubview(ListenMessageView);

			SendMessageView = new UITextField
			{
				Placeholder = "Type Message Here",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new CGRect(10, 222, w - 100, h)
			};
			View.AddSubview(SendMessageView);

			UIButton buttonSendMessage;
			buttonSendMessage = UIButton.FromType(UIButtonType.System);
			buttonSendMessage.Frame = new CGRect(10 + (w - 100) + 10, 222, 70, h);
			buttonSendMessage.SetTitle("Send", UIControlState.Normal);
			buttonSendMessage.SetTitleColor(UIColor.White, UIControlState.Normal);
			buttonSendMessage.BackgroundColor = colorLightBlue;

			buttonSendMessage.TouchUpInside += async (sender, e) =>
			{
				await myAppDel.SendMessage(MessageUser.UserID, SendMessageView.Text);
				SendMessageView.Text = string.Empty;
			};

			View.AddSubview(buttonSendMessage);

			UIButton buttonEndMessaging;
			buttonEndMessaging = UIButton.FromType(UIButtonType.System);
			buttonEndMessaging.Frame = new CGRect(10, 372, w - 20, 44);
			buttonEndMessaging.SetTitle("End Messaging", UIControlState.Normal);
			buttonEndMessaging.SetTitleColor(UIColor.Black, UIControlState.Normal);
			buttonEndMessaging.BackgroundColor = UIColor.Gray;

			buttonEndMessaging.TouchUpInside += async (sender, e) =>
			{
				await myAppDel.ChooseUsers();
			};

			View.AddSubview(buttonEndMessaging);

			UIButton buttonLogout;
			buttonLogout = UIButton.FromType(UIButtonType.System);
			buttonLogout.Frame = new CGRect(10, 432, w - 20, 44);
			buttonLogout.SetTitle("Logout", UIControlState.Normal);
			buttonLogout.SetTitleColor(UIColor.Black, UIControlState.Normal);
			buttonLogout.BackgroundColor = UIColor.Gray;

			buttonLogout.TouchUpInside += async (sender, e) =>
			{
				await myAppDel.Logout();
			};

			View.AddSubview(buttonLogout);
		}
	}
}
