/*************************************************************************
 *
 * REV SOFTWARE CONFIDENTIAL
 *
 * [2013] - [2015] Rev Software, Inc.
 * All Rights Reserved.
 *
 * NOTICE:  All information contained herein is, and remains
 * the property of Rev Software, Inc. and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Rev Software, Inc.
 * and its suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Rev Software, Inc.
 */
using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace Nuubit.SDK
{
	[Static]
	partial interface Constants
	{
		// extern double RevSDKVersionNumber;
		[Field ("RevSDKVersionNumber", "__Internal")]
		double RevSDKVersionNumber { get; }

		// extern const unsigned char [] RevSDKVersionString;
		[Field ("RevSDKVersionString", "__Internal")]
		IntPtr RevSDKVersionString { get; }
	}

	// @interface RevSDK : NSObject
	[BaseType (typeof (NSObject))]
	interface RevSDK
	{
		// +(void)startWithSDKKey:(NSString *)aSDKKey;
		[Static]
		[Export ("startWithSDKKey:")]
		void StartWithSDKKey (string aSDKKey);
	}
}