
namespace Azure_AI_Demo
{
    internal static class ActivityIndicator
    {
        public static async Task IndicateActivity(Action<string> updateLabel, string message, CancellationToken ct)
        {
            var activityIndicator = "-";
            while (!ct.IsCancellationRequested)
            {
                updateLabel($"{message} {activityIndicator}");
                switch (activityIndicator)
                {
                    case "-":
                        activityIndicator = "\\";
                        break;
                    case "\\":
                        activityIndicator = "|";
                        break;
                    case "|":
                        activityIndicator = "/";
                        break;
                    case "/":
                        activityIndicator = "-";
                        break;
                    default:
                        break;
                }
                Thread.Sleep(100);
            }

            updateLabel(string.Empty);
            await Task.CompletedTask;
        }

        public static void UpdateLabelSafely(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Text = text));
            }
            else
            {
                label.Text = text;
            }
        }
    }
}
