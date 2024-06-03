using Microsoft.AspNetCore.Components;

namespace ChatModel.Ui.Shared;

public class SurveyPromptComponent : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = default!;
}