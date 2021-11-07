using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

public class DbBehaviour : MonoBehaviour
{
    [SerializeField] bool CleanTable;
    // Start is called before the first frame update
    void Start()
    {
        // Plástico 0, Vidrio 1, Órganico 2, Papel 3

        ObjectDB objectDB = new ObjectDB();

        if(CleanTable)
        objectDB.deleteAllData();

        objectDB.addOrReplaceData(new ObjectEntity("2A 8C 82 08", "Trash", "Vidrio quebrado", "Lámina de vidrio que se emplea para cerrar los huecos de ventanas, puertas, vehículos, etc.", 1));
        //objectDB.addOrReplaceData(new ObjectEntity(" 67 5A E6 3B", "Trash", "Botella de plástico", "es un envase muy utilizado en la comercialización de líquidos en productos como lácteos, bebidas o limpia hogares.", 0));
        objectDB.addOrReplaceData(new ObjectEntity("30 B7 D0 A4", "Trash", "Cáscara de huevo", "Desechos de un huevo de gallina.", 2));
        objectDB.addOrReplaceData(new ObjectEntity("8A 74 7D 08", "Trash", "Cáscara de plátano", "Desechos de una fruta.", 2));
        objectDB.addOrReplaceData(new ObjectEntity("77 68 7E 3B", "Trash", "Caja de cartón", "es un objeto, de diferentes tamaños, generalmente con forma de prisma rectangular, con una abertura que se cubre con una tapa.", 2));
        objectDB.addOrReplaceData(new ObjectEntity("67 5A E6 3B", "Trash", "Bolsa de plástico", "Objeto cotidiano utilizado para transportar pequeñas cantidades de mercancía.", 0));
        objectDB.addOrReplaceData(new ObjectEntity("87 92 E6 3B", "Trash", "Hoja blanca", "Hoja delgada que se hace con pasta de fibras vegetales.", 3));

        objectDB.addOrReplaceData(new ObjectEntity("B0 25 4D A8", "Number", "Número 0", "Número 0.", 0));
        objectDB.addOrReplaceData(new ObjectEntity("D7 A6 02 3D", "Number", "Número 1", "Número 1.", 1));
        objectDB.addOrReplaceData(new ObjectEntity("E7 18 8E 3B", "Number", "Número 2", "Número 2.", 2));
        objectDB.addOrReplaceData(new ObjectEntity("D7 E7 A4 3F", "Number", "Número 3", "Número 3.", 3));
        objectDB.addOrReplaceData(new ObjectEntity("BA F7 88 07", "Number", "Número 4", "Número 4.", 4));
        objectDB.addOrReplaceData(new ObjectEntity("A7 B5 E5 3B", "Number", "Número 5", "Número 5.", 5));
        objectDB.addOrReplaceData(new ObjectEntity("B7 93 A4 3B", "Number", "Número 6", "Número 6.", 6));
        objectDB.addOrReplaceData(new ObjectEntity("B7 00 E6 3B", "Number", "Número 7", "Número 7.", 7));
        objectDB.addOrReplaceData(new ObjectEntity("89 36 A9 29", "Number", "Número 8", "Número 8.", 8));
        objectDB.addOrReplaceData(new ObjectEntity("F7 97 9D 3B", "Number", "Número 9", "Número 9.", 9));

        objectDB.close();

        //Fetch All Data
        ObjectDB objectDB2 = new ObjectDB();
        System.Data.IDataReader reader = objectDB2.getAllData();

        int fieldCount = reader.FieldCount;
        List<ObjectEntity> myList = new List<ObjectEntity>();
        while (reader.Read())
        {
            ObjectEntity entity = new ObjectEntity(reader.GetString(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetString(3),
                                    reader.GetInt32(4));

            Debug.Log("id: " + entity._id + ", type: " + entity._type + ", name: " + entity._name + ", description: " + entity._description + ", typeID: " + entity._typeId);
            myList.Add(entity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
