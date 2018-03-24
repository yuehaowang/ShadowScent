using System.Net;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;


public static class AzureServiceClient {

	private const string backendUrl = "http://ug.chinacloudsites.cn";

	private static MobileServiceClient client;

	public static MobileServiceClient Client
	{
		get
		{
			if (client == null)
			{
				#if UNITY_ANDROID

				// Android builds fail at runtime due to missing GZip support, so build a handler that uses Deflate for Android

				HttpClientHandler handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate };
				client = new MobileServiceClient(backendUrl, handler);

				#else

				client = new MobileServiceClient(serviceUrl);

				#endif
			}

			return client;
		}
	}

}
