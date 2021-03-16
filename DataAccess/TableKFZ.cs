using CommonTypes;
using System.Data;
using System.Collections.Generic;

namespace DataAccess
{
    public class TableKFZ : Table<IEntity>
    {
        public TableKFZ()
        {
            
        }

        public override void Save(IEntity entity)
        {
            KFZ obj = (KFZ)entity;
            string query = string.Format("INSERT INTO {0} VALUES(0, \"{1}\", \"{2}\", {3}, \"{4}\")", TableName, obj.FahrgestellNR, obj.Kennzeichen, obj.Leistung, obj.Typ);
            _Connection.GetAdapter().Adapter.ExecuteSQL(query);
        }

        public override void Update(IEntity entity)
        {
            KFZ obj = (KFZ)entity;
            string query = string.Format("UPDATE {0} SET FahrgestellNR = \"{1}\", Kennzeichen = \"{2}\", Leistung = {3}, Typ = \"{4}\" WHERE {5} = {6}", 
                                                TableName, obj.FahrgestellNR, obj.Kennzeichen, obj.Leistung, obj.Typ, IDColumnName, obj.ID);
            _Connection.GetAdapter().Adapter.ExecuteSQL(query);
        }

        protected override void Initialize()
        {
            _Connection = MySqlConnection.GetInstance();
            _Connection.Connect();
            TableName = "kfz";
            IDColumnName = "idkfz";
        }

        protected override void LoadObjects(out List<IEntity> result)
        {
            result = new List<IEntity>();

            string query = string.Format("SELECT * FROM {0}", TableName);
            
            DataTable table = _Connection.GetAdapter().Adapter.GetDataTable(query);

            for (int i = 0; i < table.Rows.Count; ++i)
            {
                KFZ kfz = new KFZ();
                kfz.ID = (int)table.Rows[i].ItemArray[0];                
                kfz.FahrgestellNR = table.Rows[i].ItemArray[1].ToString();
                kfz.Kennzeichen = table.Rows[i].ItemArray[2].ToString();
                kfz.Leistung = (int)table.Rows[i].ItemArray[3];
                kfz.Typ = table.Rows[i].ItemArray[4].ToString();

                result.Add(kfz);
            }
        }
    }
}