using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Location
    {
        public static int CreateNode(int posX,  int posY, string floor, string type)
        {
            using(SigmaContext db = new SigmaContext())
            {
                NODO node = new NODO();
                node.PosX_Nodo = posX;
                node.PosY_Nodo = posY;
                node.Piso_Nodo = floor;
                node.Tipo_Nodo = type;
                db.NODO.Add(node);
                db.SaveChanges();
                return node.Id_Nodo;
            }
        }

        public static bool CreateBeacon(int posX, int posY, string floor, string type, char status)
        {
            int nodeID = CreateNode(posX, posY, floor, type);
            if (nodeID == 0)
            {
                return false;
            }

            using (SigmaContext db = new SigmaContext())
            {
                BEACON beacon = new BEACON();
                beacon.Status_Nodo = char.ToString(status);
                beacon.Id_Nodo_Beacon = nodeID;
                db.BEACON.Add(beacon);
                db.SaveChanges();
                return true;
            }
        }

        public static void AssignValue(int node, int adjacentNode)
        {
            using(SigmaContext db = new SigmaContext())
            {
                PONDERACION value = new PONDERACION();
                value.Id_Nodo_Actual = node;
                value.Id_Nodo_Adyacente = adjacentNode;
                db.PONDERACION.Add(value);
                db.SaveChanges();
            }
        }
    }
}
