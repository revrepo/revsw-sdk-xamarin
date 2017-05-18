using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nuubit.SDK
{
    public class CaptiveNetworkException : WebException
    {
        const string DefaultCaptiveNetworkErrorMessage = "Hostnames don't match, you are probably on a captive network";

        /// <summary>
        /// Gets the source URI.
        /// </summary>
        /// <value>The source URI.</value>
        public Uri SourceUri { get; private set; }

        /// <summary>
        /// Gets the destination URI.
        /// </summary>
        /// <value>The destination URI.</value>
        public Uri DestinationUri { get; private set; }

        public CaptiveNetworkException(Uri sourceUri, Uri destinationUri) : base(DefaultCaptiveNetworkErrorMessage)
        {
            SourceUri = sourceUri;
            DestinationUri = destinationUri;
        }
    }
}
