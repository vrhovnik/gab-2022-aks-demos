using First_Aks_Cli_Rest;
using Newtonsoft.Json;
using Spectre.Console;

var rule = new Rule("[blue]Calling AKS cluster and getting information[/]")
{
    Alignment = Justify.Left,
}.RuleStyle("grey");
AnsiConsole.Write(rule);

//disable https client cert validation
var clientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
};

// set cluster address to connect to the cluster
var serverUrl = Environment.GetEnvironmentVariable("ClusterBaseAddress") ??
                throw new ArgumentNullException("Server address was not provided.");

using var client = new HttpClient(clientHandler);
client.DefaultRequestHeaders.Add("Accept", "application/json");

//provide bearer token for authentication
var bearerToken = Environment.GetEnvironmentVariable("BearerToken");

var namespaceName = AnsiConsole.Ask("Provide namespace name to traverse through pods", string.Empty);

if (string.IsNullOrEmpty(namespaceName))
{
    AnsiConsole.WriteLine("Namespace was not provided, continuing with default namespace");
    namespaceName = "default";
}

var requestData = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri($"{serverUrl}api/v1/namespaces/{namespaceName}/pods", UriKind.RelativeOrAbsolute)
};
requestData.Headers.TryAddWithoutValidation("Authorization", $"Bearer {bearerToken}");

var result = await client.SendAsync(requestData);

var receivedPods = await result.Content.ReadAsStringAsync();

var pods = JsonConvert.DeserializeObject<Pods>(receivedPods);

var table = new Table();
table.Border(TableBorder.Ascii2);

table.AddColumn(new TableColumn("Pod name").Centered());

foreach (var pod in pods.Items)
{
    table.AddRow(pod.Metadata.Name);
}

AnsiConsole.Render(table);