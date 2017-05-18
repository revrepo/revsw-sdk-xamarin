using System;
using RevSDK.Core;
using RevSDK;

namespace RevSDK.iOS
{
	public class RevDSK : IRevSDK
	{
		public RevDSK()
		{
		}

		public void StartXamarinSDK(string SDKey)
		{
			
            RevSDK.StartWithSDKKey(SDKey);
		}
	}
}
