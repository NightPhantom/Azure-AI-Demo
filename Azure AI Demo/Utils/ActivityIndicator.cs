namespace Azure_AI_Demo.Utils
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

        public static void UpdateTextSafely(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => control.Text = text));
            }
            else
            {
                control.Text = text;
            }
        }
    }
}
