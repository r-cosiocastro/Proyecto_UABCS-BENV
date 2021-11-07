using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public struct ObjectEntity
    {
        public string _id { get; set; }
        public String _type { get; set; }
        public string _name { get; set; }
        public string _description { get; set; }
        public int _typeId { get; set; }

        public ObjectEntity(string id, String type, string name, string description)
        {
            _id = id;
            _type = type;
            _name = name;
            _description = description;
            _typeId = 0;
        }

        public ObjectEntity(string id, string name, string description)
        {
            _id = id;
            _type = "Default";
            _name = name;
            _description = description;
            _typeId = 0;
        }

        public ObjectEntity(string id, String type, string name, string description, int typeId)
        {
            _id = id;
            _type = type;
            _name = name;
            _description = description;
            _typeId = typeId;
        }



        public ObjectEntity getRandomObject()
        {
            return new ObjectEntity("0", "Trash", "Basura", "Basura");
        }


    }
}
