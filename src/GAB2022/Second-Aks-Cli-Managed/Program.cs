using System.Reflection;
using k8s;
using k8s.Models;
using Spectre.Console;

AnsiConsole.MarkupLine(
    $"[link=https://github.com//]Demo for working with Kubernetes Api[/]!");

HorizontalRule("Connecting to cluster using default load via .kube/config and list namespaces");

var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
IKubernetes client = new Kubernetes(config);

AnsiConsole.WriteLine($"Listening to master at {config.Host}");

var namespaces = await client.ListNamespaceAsync();

foreach (var ns in namespaces.Items)
{
    AnsiConsole.WriteLine($"{ns.Metadata.Uid} : {ns.Metadata.Name}");
}

Console.Read(); //break for continue

HorizontalRule("List pods in namespace - app");

var listPodInNamespace = await client.ListNamespacedPodAsync("app");

var table = new Table();
table.Border(TableBorder.Rounded);

table.AddColumn("Pod name");
table.AddColumn(new TableColumn("Labels").Centered());

foreach (var currentPod in listPodInNamespace.Items)
{
    var labels = string.Empty;
    foreach (var labelPair in currentPod.Metadata.Labels)
    {
        labels += $" {labelPair.Key}:{labelPair.Value} ";
    }

    table.AddRow(currentPod.Metadata.Name, labels);
}

AnsiConsole.Write(table);

Console.Read(); //break for continue

HorizontalRule("Creating namespace and pod");

var namespaceNameForTest = "test";
var newNamespace = new V1Namespace { Metadata = new V1ObjectMeta { Name = namespaceNameForTest } };

var resultNamespaceCreated = await client.CreateNamespaceAsync(newNamespace);
Console.WriteLine(
    $"Namespace {resultNamespaceCreated.Metadata.Name} has been created and it is in {resultNamespaceCreated.Status.Phase} state");

var pod = new V1Pod
{
    Metadata = new V1ObjectMeta { Name = "nginx-test" },
    Spec = new V1PodSpec
    {
        Containers = new List<V1Container>
        {
            new()
            {
                Image = "nginx:1.7.9",
                Name = "image-nginx-test",
                Ports = new List<V1ContainerPort>
                {
                    new() { ContainerPort = 80 }
                }
            }
        }
    }
};

var createdPodInNamespaceTest = await client.CreateNamespacedPodAsync(pod, namespaceNameForTest);
AnsiConsole.WriteLine(
    $"Pod in namespace {namespaceNameForTest} has been created with state {createdPodInNamespaceTest.Status.Phase}");

Console.Read(); //break for continue

HorizontalRule("Exec into pod");

var webSocket =
    await client.WebSocketNamespacedPodExecAsync(pod.Metadata.Name,
        namespaceNameForTest, "env", pod.Spec.Containers[0].Name);

var demux = new StreamDemuxer(webSocket);
demux.Start();

var buff = new byte[4096];
var stream = demux.GetStream(1, 1);
await stream.ReadAsync(buff, 0, 4096)
    .ConfigureAwait(false);

var str = System.Text.Encoding.Default.GetString(buff);
Console.WriteLine(str); //ouput ls command

Console.Read(); //break for continue

HorizontalRule($"Delete namespace {namespaceNameForTest}");

var status = await client.DeleteNamespaceAsync(namespaceNameForTest, new V1DeleteOptions());
Console.WriteLine(
    $"Namespace {namespaceNameForTest} has been deleted - status {status.Message} - {status.Status}");

Console.Read(); //break for continue

HorizontalRule("Load objects from yaml file");

var typeMap = new Dictionary<string, Type>
{
    { "v1/Pod", typeof(V1Pod) },
    { "v1/Service", typeof(V1Service) },
    { "apps/v1/Deployment", typeof(V1Deployment) }
};

var yamlPath = Path.Join(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location), "sample.yaml");
var objects = await KubernetesYaml.LoadAllFromFileAsync(yamlPath, typeMap);

foreach (var obj in objects) Console.WriteLine(obj);

Console.Read(); //stop and press key to continue

HorizontalRule("Watching pods - watch pods");

var podlistResp = client.ListNamespacedPodWithHttpMessagesAsync("default", watch: true);
using (podlistResp.Watch<V1Pod, V1PodList>((type, item) =>
       {
           Console.WriteLine("==on watch event==");
           Console.WriteLine(type);
           Console.WriteLine(item.Metadata.Name);
           Console.WriteLine("==on watch event==");
       }))
{
    Console.WriteLine("press ctrl + c to stop watching");

    var ctrlc = new ManualResetEventSlim(false);
    Console.CancelKeyPress += (_, _) => ctrlc.Set();
    ctrlc.Wait();
}

Console.Read(); //press any key to continue

void HorizontalRule(string title)
{
    AnsiConsole.WriteLine();
    AnsiConsole.Write(new Rule($"[white bold]{title}[/]").RuleStyle("grey").LeftAligned());
    AnsiConsole.WriteLine();
}