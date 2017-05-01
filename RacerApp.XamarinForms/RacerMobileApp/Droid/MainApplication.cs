using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Nuubit.SDK;
using RacerMobileApp.Services;
using Android.Runtime;
using Java.Interop;

namespace RacerMobileAppNew.Droid
{
    [Application(Debuggable = false)]
	class MainApplication : Com.Nuubit.Sdk.NuubitApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
        }

		public override void OnCreate()
		{
			base.OnCreate();
		}
 
    }
//[Register("android/support/multidex/MultiDexApplication", DoNotGenerateAcw = true)]
//public class MultiDexApplication : Com.Nuubit.Sdk.NuubitApplication
//{
//	internal static readonly JniPeerMembers _members =
//		new XAPeerMembers("android/support/multidex/MultiDexApplication", typeof(MultiDexApplication));

//	internal static IntPtr java_class_handle;

//	private static IntPtr id_ctor;

//		public override void OnCreate()
//		{
//			base.OnCreate();
//		}

//	[Register(".ctor", "()V", "", DoNotGenerateAcw = true)]
//	public MultiDexApplication()
//		: base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
//	{
//		if (Handle != IntPtr.Zero)
//			return;

//		try
//		{
//			if (GetType() != typeof(MultiDexApplication))
//			{
//				SetHandle(
//					JNIEnv.StartCreateInstance(GetType(), "()V"),
//					JniHandleOwnership.TransferLocalRef);
//				JNIEnv.FinishCreateInstance(Handle, "()V");
//				return;
//			}

//			if (id_ctor == IntPtr.Zero)
//				id_ctor = JNIEnv.GetMethodID(class_ref, "&lt;init&gt;", "()V");
//			SetHandle(
//				JNIEnv.StartCreateInstance(class_ref, id_ctor),
//				JniHandleOwnership.TransferLocalRef);
//			JNIEnv.FinishCreateInstance(Handle, class_ref, id_ctor);
//		}
//		finally
//		{
//		}
//	}

//	protected MultiDexApplication(IntPtr javaReference, JniHandleOwnership transfer)
//		: base(javaReference, transfer)
//	{
//	}

//	internal static IntPtr class_ref
//	{
//		get { return JNIEnv.FindClass("android/support/multidex/MultiDexApplication", ref java_class_handle); }
//	}

//	protected override IntPtr ThresholdClass
//	{
//		get { return class_ref; }
//	}

//	protected override Type ThresholdType
//	{
//		get { return typeof(MultiDexApplication); }
//	} }
}