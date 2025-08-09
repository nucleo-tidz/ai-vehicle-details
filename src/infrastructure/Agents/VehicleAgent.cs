namespace infrastructure.Agents
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.ChatCompletion;

    [Experimental("AI")]
    internal class VehicleAgent(Kernel _kernel, IConfiguration configuration) : AgentBase(_kernel, configuration), IVehicleAgent
    {

        public async Task<string> Execute(byte[] regitraionPlate)
        {
            string agentReply = string.Empty;
            var agent = base.GetAzureAgent(configuration["VehicleAgentId"]);
            AgentThread thread = new AzureAIAgentThread(agent.Item2);
            ChatMessageContentItemCollection messages = new ChatMessageContentItemCollection();
           // messages.Add(new TextContent("Get registration number from the image"));
            messages.Add(new ImageContent(regitraionPlate, "image/jpeg"));
            ChatMessageContent chatMessageContent = new(AuthorRole.User, messages);
            await foreach (ChatMessageContent response in agent.Item1.InvokeAsync(chatMessageContent, thread))
            {
                agentReply = agentReply + response.Content;
            }
            return agentReply;

        }
    }
}
