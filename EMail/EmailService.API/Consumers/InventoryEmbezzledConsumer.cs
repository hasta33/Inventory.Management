using InventoryManagement.Shared.RabbitMQ;
using InventoryManagement.Shared.RabbitMQ.Commands;
using MassTransit;

namespace Email.API.Consumers
{
    public class InventoryEmbezzledConsumer : IConsumer<InventoryEmbezzledMessageCommand>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public InventoryEmbezzledConsumer(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }




        public async Task Consume(ConsumeContext<InventoryEmbezzledMessageCommand> context)
        {
            Console.WriteLine($"Consume çalıştı {context.Message}");
            var sendEndpointSuccess = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.InventoryEmbezzled}"));
            //var sendEndpointFail = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.ExecutedDocumentFail}"));
            /*try
            {
                var startableWorkflow = await _workflowLaunchpad.FindStartableWorkflowAsync(context.Message.WorkFlowDefinationId);
                var result = await _workflowLaunchpad.ExecuteStartableWorkflowAsync(startableWorkflow);
                //Console.WriteLine(result.WorkflowInstance.DefinitionId);
                //RabbitMQ üzerinde eğer created-message 
                var documentInstanceSuccess = new DocumentInstanceMessageCommand
                {
                    DefinitionId = result.WorkflowInstance.DefinitionId,
                    TenantId = result.WorkflowInstance.TenantId,
                    Version = result.WorkflowInstance.Version,
                    WorkflowStatus = result.WorkflowInstance.WorkflowStatus.ToString(),
                    CorrelationId = result.WorkflowInstance.CorrelationId,
                    ContextType = result.WorkflowInstance.ContextType,
                    ContextId = result.WorkflowInstance.ContextId,
                    Name = result.WorkflowInstance.Name,
                    CreatedAt = Convert.ToDateTime(result.WorkflowInstance.CreatedAt),
                    LastExecutedAt = Convert.ToDateTime(result.WorkflowInstance.LastExecutedAt),
                    FinishedAt = Convert.ToDateTime(result.WorkflowInstance.FinishedAt),
                    CancelledAt = Convert.ToDateTime(result.WorkflowInstance.CancelledAt),
                    FaultedAt = Convert.ToDateTime(result.WorkflowInstance.FaultedAt),
                    DefinitionVersionId = result.WorkflowInstance.DefinitionVersionId
                };
                await sendEndpointSuccess.Send<DocumentInstanceMessageCommand>(documentInstanceSuccess);
            }
            catch (Exception e)
            {
                //var documentInstanceFail = new DocumentInstanceMessageCommand
                //{
                //    DefinitionId = result.WorkflowInstance.DefinitionId,
                //    TenantId = result.WorkflowInstance.TenantId,
                //    Version = result.WorkflowInstance.Version,
                //    WorkflowStatus = result.WorkflowInstance.WorkflowStatus.ToString(),
                //    CorrelationId = result.WorkflowInstance.CorrelationId,
                //    ContextType = result.WorkflowInstance.ContextType,
                //    ContextId = result.WorkflowInstance.ContextId,
                //    Name = result.WorkflowInstance.Name,
                //    CreatedAt = Convert.ToDateTime(result.WorkflowInstance.CreatedAt),
                //    LastExecutedAt = Convert.ToDateTime(result.WorkflowInstance.LastExecutedAt),
                //    FinishedAt = Convert.ToDateTime(result.WorkflowInstance.FinishedAt),
                //    CancelledAt = Convert.ToDateTime(result.WorkflowInstance.CancelledAt),
                //    FaultedAt = Convert.ToDateTime(result.WorkflowInstance.FaultedAt),
                //    DefinitionVersionId = result.WorkflowInstance.DefinitionVersionId
                //};
            }*/
        }
    }
}
