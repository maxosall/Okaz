using Microsoft.AspNetCore.Components;
namespace Okaz.Client.Shared;
public partial class ConfirmationModel: ComponentBase
{
  public bool ShowConfirmation {get; set;} =false;
  public void Show () => ShowConfirmation = true;

  [Parameter]
  public EventCallback<bool> ConfirmationChanged {get;set;}
  [Parameter]
  public string Message {get;set;} ="Are you sure you want to delete this product?";
  [Parameter]
  public string Heading  {get; set;} ="Delete confirmation";
  protected async Task OnConfirmationChanged (bool value)
  {
    ShowConfirmation = false;
    await ConfirmationChanged.InvokeAsync(value);
  }
}