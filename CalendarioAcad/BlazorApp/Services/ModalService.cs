namespace BlazorApp.Services
{
    public class ModalService
    {
        public event Func<Task> OnShow;
        public event Func<Task> OnClose;

        public string ActionLabel { get; set; }
        public int? CalendarioId { get; set; }


        public async Task ShowAsync(string actionLabel, int calendarioId)
        {
            ActionLabel = actionLabel;
            CalendarioId = calendarioId;

            if (OnShow != null)
            {
                await OnShow.Invoke();
            }
        }

        public async Task CloseAsync()
        {
            if (OnClose != null)
            {
                await OnClose.Invoke();
            }
        }
    }
}
