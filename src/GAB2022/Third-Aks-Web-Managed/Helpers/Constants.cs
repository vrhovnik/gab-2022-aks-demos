namespace Third_Aks_Web_Managed.Helpers;

public static class Constants
{
    public const string SUCCESS = "Success";
    public const string FAILURE = "Failure";
    public const string StoreImageName = "[yourimageregistry].azurecr.io/web:latest";
    public const string DefaultServiceType = ServiceTypeLoadBalancer;
    public const string ServiceTypeLoadBalancer = "LoadBalancer";
    public const string ServiceTypeClusterIp = "ClusterIp";
    public const string ServiceTypeNone = "None";
}