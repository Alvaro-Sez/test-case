namespace ClayLocks.Configuration;

public static class ClientCertificateConfiguration
{
     public static void AddClientCert(this IWebHostBuilder builder) 
     {
    //     builder.ConfigureKestrel(opt => 
    //         opt.ConfigureEndpointDefaults(listenOpt =>
    //         {
    //             listenOpt.UseHttps(new HttpsConnectionAdapterOptions(){
    //                 SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
    //                 ClientCertificateMode = ClientCertificateMode.AllowCertificate,
    //                 
    //                 ServerCertificate = new X509Certificate2(
    //                     Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path")!, 
    //                     Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password")
    //                 )
    //             });
    //         }));
     }
}
