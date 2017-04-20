# SimpleMessaging
A simple messaging app using Kinvey Live Service

This is a Xamarin.iOS sample app to illustrate how to use Kinvey Live Service for directed communication. 

## Concepts illustrated in this sample
* Logging in a user
* Sending a directed communication message from one user to another

## Prerequisites
* iOS 10 or later
* Xamarin Studio 6.3 or later
* Kinvey-Xamarin-iOS SDK v3.0.0-LiveService02 (bundled in as a developer preview).

## Usage
There are 2 users enabled for this app: Sam and Larry.  By following the steps below, you will be able to send simple text-based messages from one device to another utilizing Kinvey Live Service.
* Launch the app on two devices/simulators.
  * On one device/simulator, log in as Sam (username: "Sam", password: "sam").
  * On the other device, log in as Larry (username: "Larry", password: "larry").
* Once logged in, you will see buttons corresponding to Sam and Larry.
  * On the device logged in as Sam, press the button for Larry to communicate.
  * On the device logged in as Larry, press the button for Sam.
* Once you have a user to message, type in a message and hit Send.  This message should appear on the other device.

__Note:__ Users cannot send messages to themselves.  For example, logging in as Sam and choosing Sam as a user to send messages to does not allow you to send messages to yourself, because that permission has not been granted.

## License

Copyright (c) 2017 Kinvey, Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
