﻿using System;
using System.Data.SqlClient;

namespace SQLDependency
{
   class clsNotification:IDisposable
    {
        private string connectionString = @"data source=.\SQLEXPRESS;initial catalog=SQLDependency_Example;integrated security=True;";
        private string command;
        private Action Act;
        public clsNotification(Action obj,string cmd)
        {
            Act = obj;
            command = cmd;
            // Start listening notification
            SqlDependency.Start(connectionString); 
        }

        public void loadData()
        {
            // Connect to DB, create subscriber
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDependency de = new SqlDependency(cmd);

                // This event will run when receive message from DB
                de.OnChange += new OnChangeEventHandler(de_OnChange);

                // Subcriber
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                // Run action what you want to do on changed
                Act?.Invoke();
            }
        }

        private void de_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency de = sender as SqlDependency;
            de.OnChange -= de_OnChange;
            loadData();
        }

        public void Dispose()
        {
            // Unregister the notification subscription for the current instance.
            SqlDependency.Stop(connectionString); 
        }
    }
}
