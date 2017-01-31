using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDependency
{
    class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Conversation_Detail> LstData { get; set; }
        private clsNotification notification;
        public VM()
        {
            // Initialize instance
            string command = @"select [CD_ID],[Content] from [dbo].[Conversation_Detail]";
            notification = new clsNotification(setData, command);
            // Start listening notification
            notification.loadData();
        }

        // setData() will run when notification.loadData() called, so you do not need to call.
        private void setData() 
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                SQLDependency_ExampleEntities en = new SQLDependency_ExampleEntities();
                LstData = en.Conversation_Detail.ToList();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LstData"));
            });
        }

        private void On_Quit()
        {
            notification.Dispose();
        }
    }
}
