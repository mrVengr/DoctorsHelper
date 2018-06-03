using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;

public class DataBaseController
{
    public void Read()
    {
        OracleConnection conn = new OracleConnection(Constants.ConnString);
        conn.Open();
        OracleCommand cmd = new OracleCommand
        {
            Connection = conn,
            CommandText = "select * from dem",
            CommandType = CommandType.Text
        };

        OracleDataReader dr = cmd.ExecuteReader();
        conn.Dispose();
    }
}