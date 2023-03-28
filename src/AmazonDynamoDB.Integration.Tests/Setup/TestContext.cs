using System.Runtime.InteropServices;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace AmazonDynamoDB.Integration.Tests.Setup;

public class TestContext : IAsyncLifetime
{
    private readonly DockerClient _dockerClient;
    private readonly DockerClientConfiguration _dockerClientConfig;
    private const string _ContainerImageUri = "amazon/dynamodb-local";
    private const string _PortDefault = "8000";
    private string _containerId;

    private string DockerApiUri
    {
        get
        {
            bool isWindowsOS = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWindowsOS)
                return "npipe://./pipe/docker_engine";

            bool isLinuxOS = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            bool isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            if (isLinuxOS || isMacOS)
                return "unix:/var/run/docker.sock";

            throw new Exception("Unable to determine what OS this is running on");
        }
    }

    public TestContext()
    {
        _dockerClient = new DockerClientConfiguration(new Uri(DockerApiUri), null, TimeSpan.FromMinutes(5)).CreateClient();
    }


    public async Task InitializeAsync()
    {
        await PullImageAsync();
        await StartContainerAsync();
        await InitializeTestDataSetupAsync();
    }
    public async Task DisposeAsync()
    {
        if (_containerId != null)
        {
            var images = await _dockerClient.Images.ListImagesAsync(new ImagesListParameters { All = true });
            var image = images.Where(c => c.RepoTags.Any(x => x == $"{_ContainerImageUri}:latest")).FirstOrDefault();
            await _dockerClient.Containers.KillContainerAsync(_containerId, new ContainerKillParameters());
            await _dockerClient.Images.DeleteImageAsync($"{_ContainerImageUri}:latest", new ImageDeleteParameters { Force = true });
        }
    }    



    private async Task PullImageAsync()
    {
        await _dockerClient.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = _ContainerImageUri,
                Tag = "latest"
            },
            new AuthConfig(),
            new Progress<JSONMessage>()
        );
    }
    private async Task StartContainerAsync()
    {
        var response = await _dockerClient.Containers.CreateContainerAsync(
            new CreateContainerParameters
            {
                Image = _ContainerImageUri,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { _PortDefault, default(EmptyStruct) }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { _PortDefault, new List<PortBinding> { new PortBinding { HostPort = _PortDefault} } }
                    },
                    PublishAllPorts = true
                }
            }
        );

        _containerId = response.ID;
        await _dockerClient.Containers.StartContainerAsync(_containerId, null);
    }
    private async Task InitializeTestDataSetupAsync()
    {
        await new TestDataSetup().CreateTableAsync();
    }
}
