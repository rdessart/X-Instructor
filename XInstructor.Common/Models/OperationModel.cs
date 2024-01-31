using CommunityToolkit.Mvvm.ComponentModel;
using XInstructor.Common.Messages;

namespace XInstructor.Common.Models
{
    public partial class OperationModel(uint id, ClientModel client, DateTime sendedTimestamp) : ObservableObject
    {
        public enum OperationStatus
        {
            Unknown,
            Sent,
            Received,
            Sucess,
            Failed,
            Expired
        }

        [ObservableProperty]
        private uint _id = id;

        [ObservableProperty]
        private ClientModel _client = client;

        [ObservableProperty]
        private DateTime _sendedTimestamp = sendedTimestamp;

        [ObservableProperty]
        private DateTime? _closedTimestamp = null;

        [ObservableProperty]
        private OperationStatus _status = OperationStatus.Unknown;

        [ObservableProperty]
        private string _statusText = "Unknown";

        partial void OnStatusChanged(OperationStatus value)
        {
            switch (value)
            {
                case OperationStatus.Unknown:
                    StatusText = "Unknown";
                    break;
                case OperationStatus.Sent:
                    StatusText = "Send";
                    break;
                case OperationStatus.Received:
                    StatusText = "Response received";
                    break;
                case OperationStatus.Sucess:
                    StatusText = "Sucess";
                    break;
                case OperationStatus.Failed:
                    StatusText = "Failed";
                    break;
                case OperationStatus.Expired:
                    StatusText = "Timed-Out";
                    break;
                default:
                    break;
            }
            new NetworkOperationStatusChanged(this);
        }
    }
}
