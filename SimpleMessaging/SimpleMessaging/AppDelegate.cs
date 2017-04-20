using System;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using Newtonsoft.Json;
using SQLite.Net.Platform.XamarinIOS;
using Kinvey;

namespace SimpleMessaging
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		Stream<UserMessage> streamMessage;
		ViewControllerMessage messageController;

		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			InitKinvey();
			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}


		public void InitKinvey()
		{
			string appKey = "kid_SJ0SNjQCl";  // LiveService-DirectedCommunication
			string appSecret = "00f2e44bd8e14b4e9762cd0fb3ccbfdc";  // LiveService-DirectedCommunication

			Client.Builder cb = new Client.Builder(appKey, appSecret)
				.setFilePath(NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].ToString())
				.setOfflinePlatform(new SQLitePlatformIOS())
				.setLogger(delegate (string msg)
				{
					Console.WriteLine(msg);
				});

			cb.Build();

			// create a new window instance based on the screen size
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			if (Client.SharedClient.IsUserLoggedIn())
			{
				Client.SharedClient.ActiveUser.Logout();
			}
			var loginController = new LoginViewController();
			var navController = new UINavigationController(loginController);
			Window.RootViewController = navController;

			// make the window visible
			Window.MakeKeyAndVisible();
		}

		public async Task<User> Login(string username, string password)
		{
			try
			{
				await User.LoginAsync(username, password);

				// Register for realtime
				await Client.SharedClient.ActiveUser.RegisterRealtimeAsync();

				await ChooseUsers();
			}
			catch (KinveyException e)
			{
				if (e.ErrorCategory == EnumErrorCategory.ERROR_REALTIME)
				{
					Console.WriteLine("VRG (exception caught) Exception from Realtime operation");
				}
				Console.WriteLine("VRG (exception caught) Exception Error -> " + e.Error);
			}

			return Client.SharedClient.ActiveUser;
		}

		public async Task Logout()
		{
			await Client.SharedClient?.ActiveUser?.UnregisterRealtimeAsync();
			Client.SharedClient?.ActiveUser?.Logout();

			var logInController = new LoginViewController();
			var navController = new UINavigationController(logInController);
			Window.RootViewController = navController;
		}

		public void MessageUser(SimpleMessagingUser messageUser)
		{
			messageController = new ViewControllerMessage();
			messageController.MessageUser = messageUser;

			var navController = new UINavigationController(messageController);
			Window.RootViewController = navController;
		}

		public async Task ChooseUsers()
		{
			var usersStore = DataStore<SimpleMessagingUser>.Collection("DirectedCommUsers", DataStoreType.NETWORK);
			var users = await usersStore.FindAsync();

			var usersController = new ViewControllerUsers();
			usersController.Users = users;

			var navController = new UINavigationController(usersController);
			Window.RootViewController = navController;
		}

		public void SetupStream()
		{
			streamMessage = new Stream<UserMessage>("user-messages");
		}

		public async Task Subscribe()
		{
			var sender = Client.SharedClient.ActiveUser.Id;

			// Set up command subscribe delegate
			var streamDelegate = new KinveyStreamDelegate<UserMessage>
			{
				OnError = (err) =>
				{
					Console.WriteLine("Error: " + err.Message);
				},
				OnNext = (senderID, message) =>
				{
					InvokeOnMainThread(() => messageController.ChangeText(senderID, message.Message));
				},
				OnStatus = (status) =>
				{
					Console.WriteLine("Status: " + status.Status);
				}
			};

			try
			{
				await streamMessage.Listen(streamDelegate);
			}
			catch (KinveyException ke)
			{
				Console.WriteLine("Live Service Exception (listen): " + ke.Message);
			}
		}

		public async Task SendMessage(string toUser, string message)
		{
			var msg = new UserMessage();
			msg.Message = message;
			try
			{
				await streamMessage.Send(toUser, msg);
			}
			catch (KinveyException ke)
			{
				Console.WriteLine("Live Service Exception (send): " + ke.Message);
			}
		}

	}
}
